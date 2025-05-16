using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.domain.Filters
{
    public class UserFilter : IFilter
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public bool? Active { get; set; } = true;

        public IQueryable FilterByName(IQueryable query)
        {
            return query.Where("Name.ToUpper().Trim().Contains(@0)", this.Name?.ToUpper().Trim());
        }

        public IQueryable FilterByEmail(IQueryable query)
        {
            return query.Where("Email.ToUpper().Trim().Contains(@0)", this.Email?.ToUpper().Trim());
        }
    }
}
