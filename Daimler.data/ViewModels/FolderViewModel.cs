using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.data.ViewModels
{
    public class FolderViewModel : BaseViewModel
    {
        public string? Name { get; set; }

        public int? ParentId { get; set; }

        public DateTime CreatedAt { get; set; }

        public int? CreatedBy { get; set; }
    }
}
