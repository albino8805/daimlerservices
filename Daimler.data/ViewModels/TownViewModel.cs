using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.data.ViewModels
{
    public class TownViewModel : BaseViewModel
    {
        public string? Name { get; set; }

        public int StateFK { get; set; }

        public StateViewModel? State { get; set; }
    }
}
