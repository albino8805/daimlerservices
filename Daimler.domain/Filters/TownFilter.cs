using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.domain.Filters
{
    public class TownFilter : IFilter
    {
        public int StateFK { get; set; }

        public bool? Active { get; set; } = true;
    }
}
