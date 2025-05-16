using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.data.ViewModels
{
    public class ActionViewModel : BaseViewModel
    {
        public string? Name { get; set; }

        public bool Checked { get; set; } = false;
    }
}
