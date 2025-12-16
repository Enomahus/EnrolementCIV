using System.Diagnostics.CodeAnalysis;
using Application.Api;
using Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Tools.Exceptions.Errors;

namespace Application.Features.Users.GetAllRoles
{
    [ExcludeFromCodeCoverage]
    [ApiController]
    [Route("user")]
    [OpenApiTag("user")]
    public class GetAllRolesController : ApiControllerBase
    {
        /// <summary>
        /// Get the roles
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("all-roles", Name = "GetAllRoles")]
        [OpenApiOperation(
            "GetAllRoles",
            "Récupère tous les rôles possibles pour un utilisateur.",
            ""
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<List<RoleModel>>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result<Error>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result<Error>))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(Result<Error>))]
        public async Task<Result<List<RoleModel>>> GetAllRolesAsync(
            CancellationToken cancellationToken
        )
        {
            var query = new GetAllRolesQuery() { };
            return await Mediator.Send(query, cancellationToken);
        }
    }
}
