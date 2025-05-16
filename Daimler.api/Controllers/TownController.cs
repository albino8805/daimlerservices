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
    public class TownController : BaseController<TownViewModel, Town, TownFilter>
    {
        public TownController(ITownManager manager) : base(manager) { }

        [HttpPost]
        public override IActionResult Add([FromBody] TownViewModel town) => base.Add(town);

        [HttpDelete("{id}")]
        public override IActionResult Delete(int id) => base.Delete(id);

        [HttpGet]
        public override IActionResult GetAll([FromQuery] QueryParameter pagingParameter, [FromQuery] TownFilter entityFilter)
        {
            return base.GetAll(pagingParameter, entityFilter);
        }

        [HttpGet("{id}")]
        public override IActionResult GetById(int id) => base.GetById(id);

        [HttpPatch("{id}")]
        public override IActionResult Update(int id, [FromBody] TownViewModel town) => base.Update(id, town);
    }
}
