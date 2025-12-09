using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Exceptions.Auth;
using Application.Interfaces.Services;
using Application.Models.Auth;
using Infrastructure.Configurations;
using Infrastructure.Persistence.Entities;
using Infrastructure.Persistence.SQLServer.Contexts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Tools.Exceptions;
using Tools.Helpers;

namespace WebApi.Services
{
    public class TokenService(
        IOptions<TokenConfiguration> tokenConf,
        WritableDbContext context,
        TimeProvider timeProvider //,
    //ITokenRoleClaimBuilder<long> tokenRoleClaimBuilder
    ) : ITokenService
    {
        public async Task<string> CreateRefreshTokenAsync(
            Guid userId,
            CancellationToken cancellationToken = default
        )
        {
            var newToken = new RefreshTokenDao()
            {
                UserId = userId != Guid.Empty ? userId : null,
                Expiry = timeProvider
                    .GetUtcNow()
                    .AddMinutes(tokenConf.Value.RefreshTokenExpirationMinutes),
            };
            await context.RefreshTokens.AddAsync(newToken, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return Base64Helper.GuidToBase64(newToken.Id);
        }

        public async Task<Tokens> CreateTokensAsync(
            UserDao user,
            IEnumerable<string> roles,
            CancellationToken cancellationToken = default
        )
        {
            try
            {
                string accessToken = await CreateTokenAsync(user, roles, cancellationToken);
                string refreshToken = await CreateRefreshTokenAsync(user.Id, cancellationToken);

                return new Tokens(accessToken, refreshToken);
            }
            catch (Exception ex)
            {
                throw new UserAuthenticationException(user.UserName!, ex);
            }
        }

        public void TrackRefreshTokensToClean()
        {
            var now = timeProvider.GetUtcNow();
            var expiredTokens = context.RefreshTokens.Where(t => t.Expiry < now);
            context.RefreshTokens.RemoveRange(expiredTokens);
        }

        private async Task<string> CreateTokenAsync(
            UserDao user,
            IEnumerable<string> roles,
            CancellationToken cancellationToken
        )
        {
            var signingCredentials = GetSigningCredentials();
            //var claims = await GetClaimsAsync(user, roles, cancellationToken);
            var claims = GetClaims(user, roles);

            var tokenOptions = GenerateTokenOptions(signingCredentials, [.. claims]);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return await Task.FromResult(token);
        }

        private JwtSecurityToken GenerateTokenOptions(
            SigningCredentials signingCredentials,
            List<Claim> claims
        )
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: tokenConf.Value.ValidIssuer,
                claims: claims,
                expires: timeProvider
                    .GetUtcNow()
                    .DateTime.AddMinutes(tokenConf.Value.AccessTokenExpirationMinutes),
                signingCredentials: signingCredentials
            );
            return tokenOptions;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var secretStr =
                tokenConf.Value.Secret
                ?? throw new ConfigurationMissingException(
                    "Missing configuration : JwtConfig.Secret"
                );
            var key = Encoding.UTF8.GetBytes(secretStr);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        //private async Task<List<Claim>> GetClaimsAsync(
        //    UserDao user,
        //    IEnumerable<string> roles,
        //    CancellationToken cancellationToken = default
        //)
        //{
        //    var rolesClaims = await tokenRoleClaimBuilder.BuildRolesClaimsAsync(
        //        roles,
        //        cancellationToken
        //    );

        //    List<Claim> claims =
        //    [
        //        new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        //        new(JwtRegisteredClaimNames.Name, user.UserName!),
        //        new("lastName", user.LastName),
        //        new("firstName", user.FirstName),
        //        .. rolesClaims,
        //    ];

        //    return claims;
        //}

        private static IEnumerable<Claim> GetClaims(UserDao user, IEnumerable<string> roles)
        {
            var userId = user.Id.ToString();
            var userName = user.UserName ?? string.Empty;
            var lastName = user.LastName ?? string.Empty;
            var firstName = user.FirstName ?? string.Empty;

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, userId),
                new(JwtRegisteredClaimNames.Name, userName),
                new(ClaimTypes.NameIdentifier, userId),
                new("lastName", lastName),
                new("firstName", firstName),
            };

            // Rôles(distincts, non vides)
            foreach (var role in roles.Where(r => !string.IsNullOrWhiteSpace(r)).Distinct())
            {
                claims.Add(new(ClaimTypes.Role, role));
            }

            return claims;
        }
    }
}
