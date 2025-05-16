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
    public class BinnacleController : BaseController<BinnacleViewModel, Binnacle, BinnacleFilter>
    {
        public BinnacleController(IBinnacleManager manager) : base(manager) { }

        [HttpPost]
        public override IActionResult Add([FromBody] BinnacleViewModel customer) => base.Add(customer);

        [HttpDelete("{id}")]
        public override IActionResult Delete(int id) => base.Delete(id);

        [HttpGet]
        public override IActionResult GetAll([FromQuery] QueryParameter pagingParameter, [FromQuery] BinnacleFilter entityFilter)
        {
            return base.GetAll(pagingParameter, entityFilter);
        }

        [HttpGet("{id}")]
        public override IActionResult GetById(int id) => base.GetById(id);

        [HttpPatch("{id}")]
        public override IActionResult Update(int id, [FromBody] BinnacleViewModel binnacle) => base.Update(id, binnacle);
    }
}
