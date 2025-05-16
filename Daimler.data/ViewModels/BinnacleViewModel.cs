using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.data.ViewModels
{
    public class BinnacleViewModel : BaseViewModel
    {
        public int UserFK { get; set; }

        public int ModuleFK { get; set; }

        public string? Description { get; set; }
    }
}
