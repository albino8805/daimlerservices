using Daimler.data.ViewModels;
using Daimler.domain.Filters;
using Daimler.domain.Helpers;
using Daimler.domain.IManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ActionEntity = Daimler.data.Models.Action;

namespace Daimler.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionController : BaseController<ActionViewModel, ActionEntity, ActionFilter>
    {
        public ActionController(IActionManager manager) : base(manager) { }

        [HttpPost]
        public override IActionResult Add(ActionViewModel action) => base.Add(action);

        [HttpDelete("{id}")]
        public override IActionResult Delete(int id) => base.Delete(id);

        [HttpGet]
        public override IActionResult GetAll([FromQuery] QueryParameter pagingParameter, [FromQuery] ActionFilter entityFilter)
        {
            return base.GetAll(pagingParameter, entityFilter);
        }

        [HttpGet("{id}")]
        public override IActionResult GetById(int id) => base.GetById(id);

        [HttpPatch("{id}")]
        public override IActionResult Update(int id, ActionViewModel action) => base.Update(id, action);
    }
}
