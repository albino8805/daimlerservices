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
    public class FolderController : BaseController<FolderViewModel, Folder, FolderFilter>
    {
        public IFolderManager _folderManager;

        public FolderController(IFolderManager manager) : base(manager)
        {
            _folderManager = manager;
        }

        [HttpGet]
        public override IActionResult GetAll([FromQuery] QueryParameter pagingParameter, [FromQuery] FolderFilter entityFilter)
        {
            return base.GetAll(pagingParameter, entityFilter);
        }

        [HttpGet("{id}")]
        public override IActionResult GetById(int id) => base.GetById(id);

        [HttpGet("completePath/{id}")]
        public IActionResult GetCompletePath(int id)
        {
            var result = new JsonMessageResult();

            try
            {
                result.Success = 1;
                result.Detail = new { Folders = _folderManager.GetCompletePath(id) };
            }
            catch (Exception ex)
            {
                result.Success = 0;
                result.Detail = new { Error = ex.Message };
            }

            return Json(result);
        }

        [HttpPost]
        public override IActionResult Add([FromBody] FolderViewModel folder)
        {
            var result = new JsonMessageResult();

            try
            {
                folder.CreatedBy = TokenHelper.GetUserId(HttpContext.Request);
                _folderManager.Add(folder);

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

        [HttpPatch("{id}")]
        public override IActionResult Update(int id, [FromBody] FolderViewModel folder) => base.Update(id, folder);
    }
}
