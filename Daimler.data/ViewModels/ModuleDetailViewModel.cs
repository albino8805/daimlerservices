using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.data.ViewModels
{
    public class ModuleDetailViewModel
    {
        public ModuleViewModel? Module { get; set; }

        public List<ActionViewModel>? Actions { get; set; }
    }
}
