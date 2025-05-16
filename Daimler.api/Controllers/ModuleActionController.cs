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
    public class ModuleActionController : BaseController<ModuleActionViewModel, ModuleAction, ModuleActionFilter>
    {
        private readonly IModuleActionManager _moduleActionManager;

        public ModuleActionController(IModuleActionManager manager) : base(manager)
        {
            _moduleActionManager = manager;
        }

        [HttpPost]
        public override IActionResult Add([FromBody] ModuleActionViewModel moduleAction) => base.Add(moduleAction);

        [HttpDelete("{id}")]
        public override IActionResult Delete(int id) => base.Delete(id);

        [HttpGet]
        public override IActionResult GetAll([FromQuery] QueryParameter pagingParameter, [FromQuery] ModuleActionFilter entityFilter)
        {
            return base.GetAll(pagingParameter, entityFilter);
        }

        [HttpGet("{id}")]
        public override IActionResult GetById(int id) => base.GetById(id);

        [HttpPatch("{id}")]
        public override IActionResult Update(int id, [FromBody] ModuleActionViewModel moduleAction) => base.Update(id, moduleAction);

        [HttpGet("catalog")]
        public IActionResult GetCatalog()
        {
            var result = new JsonMessageResult();

            try
            {
                result.Success = 1;
                result.Detail = new { Models = _moduleActionManager.GetCatalog() };
            }
            catch (Exception ex)
            {
                result.Success = 0;
                result.Detail = new { Error = ex.Message.ToString() };
            }

            return Json(result);
        }
    }
}
