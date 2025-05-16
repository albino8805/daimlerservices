using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.domain.Filters
{
    public class FolderFilter : IFilter
    {
        public Nullable<int> ParentId { get; set; }
        public bool Active { get; set; } = true;
    }
}
