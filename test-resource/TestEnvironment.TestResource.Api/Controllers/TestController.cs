using Microsoft.AspNetCore.Mvc;

namespace TestEnvironment.TestResource.Api.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        #region Public Methods

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(true);
        }

        #endregion Public Methods
    }
}
