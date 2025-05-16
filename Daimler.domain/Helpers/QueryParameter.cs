using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Daimler.domain.Helpers
{
    public class QueryParameter
    {
        const int maxPageSize = 20;

        public int pageNumber { get; set; } = 1;

        [JsonIgnore]
        private int _pageSize { get; set; } = 10;

        public string Sort { get; set; } = "";
        public string Fields { get; set; } = "";
        public bool AllowPaging { get; set; } = true;
        /// <summary>
        /// Enables the sending of item1 and item2 at same response. 
        /// False as default value
        /// </summary>
        public bool FullResponse { get; set; }
        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        public virtual int pageSize
        {

            get { return _pageSize; }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
