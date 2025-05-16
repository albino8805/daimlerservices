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
    public class StateController : BaseController<StateViewModel, State, StateFilter>
    {
        public StateController(IStateManager manager) : base(manager) { }

        [HttpPost]
        public override IActionResult Add([FromBody] StateViewModel state) => base.Add(state);

        [HttpDelete("{id}")]
        public override IActionResult Delete(int id) => base.Delete(id);

        [HttpGet]
        public override IActionResult GetAll([FromQuery] QueryParameter pagingParameter, [FromQuery] StateFilter entityFilter)
        {
            return base.GetAll(pagingParameter, entityFilter);
        }

        [HttpGet("{id}")]
        public override IActionResult GetById(int id) => base.GetById(id);

        [HttpPatch("{id}")]
        public override IActionResult Update(int id, [FromBody] StateViewModel state) => base.Update(id, state);
    }
}
