using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.domain.Filters
{
    public class ProfileFilter : IFilter
    {
        public string? Name { get; set; }

        public bool? Active { get; set; } = true;

        public IQueryable FilterByName(IQueryable query)
        {
            return query.Where("Name.ToUpper().Trim().Contains(@0)", this.Name?.ToUpper().Trim());
        }
    }
}
