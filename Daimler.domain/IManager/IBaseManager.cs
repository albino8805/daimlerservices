using Daimler.domain.Filters;
using Daimler.domain.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.domain.IManager
{
    public interface IBaseManager<VM, E> where VM : class where E : class
    {
        VM Add(VM state);

        void Delete(int id, int userId);

        Tuple<List<VM>, PagedResult<E>> GetAll(QueryParameter pagingParameter, IFilter filter);

        VM GetById(int id);

        void Update(int id, VM state);

        byte[] DownloadFile(string fileName, int folderFK);

        bool CheckExtensionFile(IFormFile file);

        Task<string> WriteFile(int folderFK, IFormFile file);

        Task<string> UploadFile(int folderFK, IFormFile file);
    }
}
