using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("demo")]
    public class DemoController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Welcome to the Enrolment CIV Docker World !";
        }
    }
}
