using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Exceptions.Auth;
using Application.Interfaces.Services;
using Application.Models.Auth;
using Infrastructure.Configurations;
using Infrastructure.Persistence.Entities;
using Infrastructure.Persistence.SQLServer.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pcea.Core.Net.Authorization.Web.Interfaces.Services;
using Tools.Exceptions;
using Tools.Helpers;

namespace WebApi.Services
{
    public class TokenService(
        IOptions<TokenConfiguration> tokenConf,
        WritableDbContext context,
        TimeProvider timeProvider,
        ITokenRoleClaimBuilder<long> tokenRoleClaimBuilder
    ) : ITokenService
    {
        public async Task<string> CreateRefreshTokenAsync(
            Guid userId,
            CancellationToken cancellationToken = default
        )
        {
            try
            {
                if (userId == Guid.Empty)
                    throw new ArgumentException("userId must be a non-empty GUID.", nameof(userId));

                //var userExists = await context
                //    .Users.AsNoTracking()
                //    .AnyAsync(u => u.Id == userId, cancellationToken);
                //if (!userExists)
                //{
                //    throw new InvalidOperationException(
                //        $"User {userId} not found. Cannot create refresh token."
                //    );
                //}
                var expires = timeProvider
                    .GetUtcNow()
                    .AddMinutes(tokenConf.Value.RefreshTokenExpirationMinutes);

                var newToken = new RefreshTokenDao() { UserId = userId, Expiry = expires };

                await context.RefreshTokens.AddAsync(newToken, CancellationToken.None);
                await context.SaveChangesAsync(CancellationToken.None);

                return Base64Helper.GuidToBase64(newToken.Id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Tokens> CreateTokensAsync(
            UserDao user,
            IEnumerable<string> roles,
            CancellationToken cancellationToken = default
        )
        {
            try
            {
                ArgumentNullException.ThrowIfNull(user);

                string accessToken = await CreateTokenAsync(user, roles, cancellationToken);
                string refreshToken = await CreateRefreshTokenAsync(user.Id, cancellationToken);

                return new Tokens(accessToken, refreshToken);
            }
            catch (Exception ex)
            {
                var label = string.IsNullOrWhiteSpace(user?.UserName)
                    ? user?.Id.ToString() ?? "unknown-user"
                    : user.UserName;
                //throw new UserAuthenticationException(user.UserName!, ex);
                throw new UserAuthenticationException(label, ex);
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
            ArgumentNullException.ThrowIfNull(user);

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
            var expires = timeProvider
                .GetUtcNow()
                .AddMinutes(tokenConf.Value.AccessTokenExpirationMinutes);

            var tokenOptions = new JwtSecurityToken(
                issuer: tokenConf.Value.ValidIssuer,
                claims: claims,
                //expires: timeProvider
                //    .GetUtcNow()
                //    .DateTime.AddMinutes(tokenConf.Value.AccessTokenExpirationMinutes),
                expires: expires.UtcDateTime,
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
        //        [],
        //        cancellationToken
        //    );

        //    List<Claim> claims =
        //    [
        //        new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        //        new(JwtRegisteredClaimNames.Name, user.UserName!),
        //        new(JwtRegisteredClaimNames.Email, user.Email!),
        //        new("lastName", user.LastName),
        //        new("firstName", user.FirstName),
        //        .. rolesClaims,
        //    ];

        //    return claims;
        //}

        private static List<Claim> GetClaims(UserDao user, IEnumerable<string> roles)
        {
            //var userId = user.Id.ToString();
            //var userName = user.UserName!;
            //var email = user.Email!;
            //var lastName = user.LastName;
            //var firstName = user.FirstName;

            //var claims = new List<Claim>
            //{
            //    new(JwtRegisteredClaimNames.Sub, userId),
            //    new(JwtRegisteredClaimNames.Name, userName),
            //    new(JwtRegisteredClaimNames.Email, email),
            //    new(ClaimTypes.NameIdentifier, userId),
            //    new("lastName", lastName),
            //    new("firstName", firstName),
            //};

            //// Rôles(distincts, non vides)
            //foreach (var role in roles.Where(r => !string.IsNullOrWhiteSpace(r)).Distinct())
            //{
            //    claims.Add(new(ClaimTypes.Role, role));
            //}

            //return claims;

            var safeRoles = roles?.Where(r => !string.IsNullOrWhiteSpace(r)).Distinct() ?? [];

            var userId = user.Id.ToString();
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, userId),
                new(
                    JwtRegisteredClaimNames.Name,
                    string.IsNullOrWhiteSpace(user.UserName) ? userId : user.UserName
                ),
                new(ClaimTypes.NameIdentifier, userId),
            };

            if (!string.IsNullOrWhiteSpace(user.Email))
                claims.Add(new(JwtRegisteredClaimNames.Email, user.Email));

            if (!string.IsNullOrWhiteSpace(user.LastName))
                claims.Add(new("lastName", user.LastName));

            if (!string.IsNullOrWhiteSpace(user.FirstName))
                claims.Add(new("firstName", user.FirstName));

            foreach (var role in safeRoles)
            {
                claims.Add(new(ClaimTypes.Role, role));
            }

            return claims;
        }
    }
}
