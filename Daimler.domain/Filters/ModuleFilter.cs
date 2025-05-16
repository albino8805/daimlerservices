using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.domain.Filters
{
    public class ModuleFilter : IFilter
    {
        public bool? Active { get; set; } = true;
    }
}
