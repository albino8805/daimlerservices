using Daimler.domain.Filters;
using Daimler.domain.Helpers;
using Daimler.domain.IManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Daimler.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<VM, E, F> : Controller where VM : class where E : class where F : class
    {
        private readonly IBaseManager<VM, E> _manager;

        public BaseController(IBaseManager<VM, E> manager)
        {
            _manager = manager;
        }

        [HttpPost]
        public virtual IActionResult Add(VM viewModel)
        {
            var result = new JsonMessageResult();

            try
            {
                _manager.Add(viewModel);

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

        [HttpDelete]
        public virtual IActionResult Delete(int id)
        {
            var result = new JsonMessageResult();

            try
            {
                _manager.Delete(id, TokenHelper.GetUserId(HttpContext.Request));

                result.Success = 1;
                result.Detail = new { Message = "El registro fue eliminado!" };
            }
            catch (Exception ex)
            {
                result.Success = 0;
                result.Detail = new { Error = ex.Message };
            }

            return Json(result);
        }

        [HttpGet]
        public virtual IActionResult GetAll([FromQuery] QueryParameter pagingParameter, [FromQuery] F entityFilter)
        {
            var result = new JsonMessageResult();

            try
            {
                result.Success = 1;
                result.Detail = new { Models = _manager.GetAll(pagingParameter, (IFilter)entityFilter) };
            }
            catch (Exception ex)
            {
                result.Success = 0;
                result.Detail = new { Error = ex.Message.ToString() };
            }

            return Json(result);
        }

        [HttpGet]
        public virtual IActionResult GetById(int id)
        {
            var result = new JsonMessageResult();

            try
            {
                result.Success = 1;
                result.Detail = new { Model = _manager.GetById(id) };
            }
            catch (Exception ex)
            {
                result.Success = 0;
                result.Detail = new { Error = ex.Message };
            }

            return Json(result);
        }

        [HttpPatch]
        public virtual IActionResult Update(int id, VM viewModel)
        {
            var result = new JsonMessageResult();

            try
            {
                _manager.Update(id, viewModel);

                result.Success = 1;
                result.Detail = new { Message = "El registro fue actualizado!" };
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
