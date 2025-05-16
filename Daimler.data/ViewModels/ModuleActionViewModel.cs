using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.data.ViewModels
{
    public class ModuleActionViewModel : BaseViewModel
    {
        public int ModuleFK { get; set; }

        public int ActionFK { get; set; }

        public int ProfileFK { get; set; }
    }
}
