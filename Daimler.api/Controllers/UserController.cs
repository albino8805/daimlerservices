using Daimler.data.Models;
using Daimler.data.ViewModels;
using Daimler.domain.Filters;
using Daimler.domain.Helpers;
using Daimler.domain.IManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Daimler.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController<UserViewModel, User, UserFilter>
    {
        private readonly IUserManager _userManager;

        public UserController(IUserManager manager) : base(manager)
        {
            _userManager = manager;
        }

        [HttpPost]
        public override IActionResult Add([FromBody] UserViewModel user) => base.Add(user);

        [HttpDelete("{id}")]
        public override IActionResult Delete(int id) => base.Delete(id);

        [HttpGet]
        public override IActionResult GetAll([FromQuery] QueryParameter pagingParameter, [FromQuery] UserFilter entityFilter)
        {
            return base.GetAll(pagingParameter, entityFilter);
        }

        [HttpGet("{id}")]
        public override IActionResult GetById(int id) => base.GetById(id);

        [HttpPatch("{id}")]
        public override IActionResult Update(int id, [FromBody] UserViewModel user) => base.Update(id, user);

        [HttpPatch("password")]
        public virtual IActionResult UpdatePassword([FromBody] PasswordViewModel viewModel)
        {
            var result = new JsonMessageResult();

            try
            {
                viewModel.UserId = TokenHelper.GetUserId(HttpContext.Request);

                _userManager.UpdatePassword(viewModel);

                result.Success = 1;
                result.Detail = new { Message = "La contraseña fue actualizada!" };
            }
            catch (Exception ex)
            {
                result.Success = 0;
                result.Detail = new { Error = ex.Message };
            }

            return Json(result);
        }
    }
}
