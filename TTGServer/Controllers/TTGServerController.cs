using Microsoft.AspNetCore.Mvc;

namespace TTGServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TTGServerController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "OK";
        }
    }
}