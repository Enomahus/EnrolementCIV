using Application.Api;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("demo")]
    [OpenApiTag("demo")]
    public class DemoController : ApiControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Welcome to the Enrolment CIV Docker World !";
        }
    }
}
