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
    public class ProfileController : BaseController<ProfileViewModel, Profile, ProfileFilter>
    {
        private readonly IProfileManager _profileManager;
        public ProfileController(IProfileManager manager) : base(manager)
        {
            _profileManager = manager;
        }

        [HttpPost]
        public override IActionResult Add([FromBody] ProfileViewModel profile)
        {
            var result = new JsonMessageResult();

            try
            {
                _profileManager.Add(profile);

                result.Success = 1;
                result.Detail = new { Message = "El registro se creó exitosamente!" };
            }
            catch (Exception ex)
            {
                result.Success = 0;
                result.Detail = new { Error = ex.Message };
            }

            return Json(result);
        }

        [HttpDelete("{id}")]
        public override IActionResult Delete(int id) => base.Delete(id);

        [HttpGet]
        public override IActionResult GetAll([FromQuery] QueryParameter pagingParameter, [FromQuery] ProfileFilter entityFilter)
        {
            return base.GetAll(pagingParameter, entityFilter);
        }

        [HttpGet("{id}")]
        public override IActionResult GetById(int id) => base.GetById(id);

        [HttpPatch("{id}")]
        public override IActionResult Update(int id, [FromBody] ProfileViewModel profile) => base.Update(id, profile);
    }
}
