using Daimler.data.ViewModels;
using Daimler.domain.Helpers;
using Daimler.domain.IManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Daimler.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationManager _manager;

        public AuthenticationController(IAuthenticationManager authenticationManager)
        {
            _manager = authenticationManager;
        }

        [HttpPost]
        public IActionResult Post([FromBody] AuthenticationViewModel authentication)
        {
            var result = new JsonMessageResult();

            try
            {
                result.Success = 1;
                result.Detail = new { Login = _manager.ValidateUser(authentication) };
            }
            catch (Exception ex)
            {
                result.Success = 0;
                result.Detail = new { Error = ex.Message };
            }

            return Ok(result);
        }
    }
}
