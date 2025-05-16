using Daimler.data.IRepository;
using Daimler.domain.Filters;
using Daimler.domain.Helpers;
using Daimler.domain.IManager;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.domain.Manager
{
    public class BaseManager<VM, E> : IBaseManager<VM, E> where VM : class where E : class
    {
        public readonly IFolderHelper _folderHelper;
        public readonly IBaseRepository<E> _repository;

        public BaseManager(IFolderHelper folderHelper, IBaseRepository<E> repository)
        {
            _folderHelper = folderHelper;
            _repository = repository;
        }

        public virtual VM Add(VM viewModel)
        {
            E entity = this.AddConverter(viewModel);

            _repository.Add(entity);

            return viewModel;
        }

        public virtual void Delete(int id, int userId)
        {
            _repository.Delete(id, userId);
        }

        public virtual Tuple<List<VM>, PagedResult<E>> GetAll(QueryParameter pagingParameter, IFilter filter)
        {
            var source = _repository.GetAll().GetPaged(pagingParameter, filter);
            var resources = CollectionConverter(source.Results.ToList());

            source.Results = null;
            return new Tuple<List<VM>, PagedResult<E>>(resources, source);
        }

        public virtual VM GetById(int id)
        {
            E entity = _repository.GetById(id);

            return this.SingleConverter(entity);
        }

        public virtual void Update(int id, VM viewModel)
        {
            E entity = _repository.GetById(id);

            if (entity == null)
            {
                throw new Exception("No se encontró el registro.");
            }

            entity = this.UpdatedConverter(viewModel, entity);

            _repository.Update(entity);
        }

        public byte[] DownloadFile(string fileName, int folderFK)
        {
            var filePath = _folderHelper.GetPathOfFile(folderFK);

            return System.IO.File.ReadAllBytes(@"./upload" + filePath + "/" + fileName);
        }

        public bool CheckExtensionFile(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            return (extension.ToUpper() == ".PDF" || extension.ToUpper() == ".PNG" || extension.ToUpper() == ".JPEG" ||
                  extension.ToUpper() == ".JPG" || extension.ToUpper() == ".XML" || extension.ToUpper() == ".DOC" || extension.ToUpper() == ".DOCX" ||
                  extension.ToUpper() == ".XLS" || extension.ToUpper() == ".XLSX");
        }

        public async Task<string> WriteFile(int folderFK, IFormFile file)
        {
            string filePath = _folderHelper.GetCompletePath(folderFK);

            string fileName;

            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            fileName = Guid.NewGuid().ToString() + extension;

            var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), $"Upload{filePath}");

            if (!Directory.Exists(pathBuilt))
            {
                Directory.CreateDirectory(pathBuilt);
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), $"Upload{filePath}",
               fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }

        public async Task<string> UploadFile(int folderFK, IFormFile file)
        {
            if (CheckExtensionFile(file))
            {
                return await WriteFile(folderFK, file);
            }
            else
            {
                throw new Exception("El archivo no se cargó");
            }
        }

        #region Converter
        public virtual E AddConverter(VM viewModel)
        {
            throw new NotImplementedException();
        }

        public virtual E UpdatedConverter(VM viewModel, E entity)
        {
            throw new NotImplementedException();
        }

        public virtual VM SingleConverter(E entity)
        {
            throw new NotImplementedException();
        }

        public virtual List<VM> CollectionConverter(List<E> entities)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
