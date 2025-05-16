using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.domain.Filters
{
    public class StateFilter : IFilter
    {
        public int? CountryFK { get; set; }

        public bool? Active { get; set; } = true;
    }
}
