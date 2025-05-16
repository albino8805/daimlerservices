using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.domain.Helpers
{
    public class JwtSetting
    {
        public string Secret { get; set; }
        public int TimeLifeInDays { get; set; }
    }
}
