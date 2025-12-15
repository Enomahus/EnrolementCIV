using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Common.Enums;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authentication;
using Pcea.Core.Net.Authorization.Application.Interfaces.Services;
using Pcea.Core.Net.Authorization.Persistence.Interfaces.Services;
using Pcea.Core.Net.Authorization.Web.Interfaces.Services;
using Pcea.Core.Net.Authorization.Web.Services;

namespace WebApi.Services
{
    public class CurrentUserService(
        IHttpContextAccessor httpContextAccessor,
        ITokenRoleClaimBuilder<long> tokenRoleClaimBuilder,
        IPermissionsService<Guid> permissionsService
    )
        : CurrentUserTokenService<Guid, long>(
            httpContextAccessor,
            tokenRoleClaimBuilder,
            permissionsService
        ),
            ICurrentUserService,
            ICurrentUserPermissionsProvider,
            ICurrentUserEntityPermissionsProvider<long>
    {
        public Guid? UserId =>
            Guid.TryParse(
                _httpContextAccessor
                    .HttpContext?.User.Claims.FirstOrDefault(c =>
                        c.Type == ClaimTypes.NameIdentifier
                    )
                    ?.Value ?? "",
                out var parsedId
            )
                ? parsedId
                : null;

        public string? UserEmail =>
            _httpContextAccessor
                .HttpContext?.User.Claims.FirstOrDefault(c =>
                    c.Type == JwtRegisteredClaimNames.Name
                )
                ?.Value;

        public string? ClientIp =>
            _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "";

        public string? LanguageCode =>
            _httpContextAccessor
                .HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Locality)
                ?.Value;

        public Task<string?> GetTokenAsync(CancellationToken token = default)
        {
            return _httpContextAccessor.HttpContext?.GetTokenAsync("access_token")
                ?? Task.FromResult<string?>(null);
        }

        public override Task<bool> IsCurrentUserAuthenticatedAsync(
            CancellationToken cancellationToken = default
        )
        {
            return Task.FromResult(UserId.HasValue);
        }

        protected override Guid ParseRoleId(string roleIdStr)
        {
            return Guid.Parse(roleIdStr);
        }
    }
}
