using System.Diagnostics.CodeAnalysis;
using Application.Api;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace Application.Features.Countries.GetAllCountries
{
    [ExcludeFromCodeCoverage]
    [ApiController]
    [Route("country")]
    [OpenApiTag("country")]
    public class GetCountriesController : ApiControllerBase
    {
        /// <summary>
        /// Get the countries
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet()]
        [OpenApiOperation("GetCountries", "Récupère les pays.", "")]
        [AllowAnonymous]
        [ProducesResponseType(
            StatusCodes.Status200OK,
            Type = typeof(Result<List<GetCountriesResponse>>)
        )]
        public async Task<Result<List<GetCountriesResponse>>> GetCountriesAsync(
            CancellationToken cancellationToken
        )
        {
            var query = new GetCountriesQuery();
            return await Mediator.Send(query, cancellationToken);
        }
    }
}
