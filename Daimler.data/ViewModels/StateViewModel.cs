using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.data.ViewModels
{
    public class StateViewModel : BaseViewModel
    {
        public string? Name { get; set; }

        public int CountryFK { get; set; }

        public CountryViewModel? Country { get; set; }
    }
}
