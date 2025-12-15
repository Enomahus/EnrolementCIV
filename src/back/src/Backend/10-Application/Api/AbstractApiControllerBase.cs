using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Api
{
    [ExcludeFromCodeCoverage]
    [ApiController]
    [Route("api/")]
    public abstract class AbstractApiControllerBase : ControllerBase
    {
        private ISender? _mediator;

        //protected ISender Mediator =>
        //    _mediator ??= HttpContext.RequestServices.GetService<ISender>()!;

        protected ISender Mediator =>
            _mediator ??=
                HttpContext?.RequestServices.GetService<ISender>()
                ?? throw new InvalidOperationException(
                    "ISender is not registered in the current request's service provider."
                );
    }
}
