using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.domain.Helpers
{
    public class JsonMessageResult
    {
        public int Success { get; set; }

        public object Detail { get; set; }
    }
}
