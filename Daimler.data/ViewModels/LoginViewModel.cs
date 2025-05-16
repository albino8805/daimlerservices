using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.data.ViewModels
{
    public class LoginViewModel
    {
        public UserViewModel? User { get; set; }

        public List<ModuleActionViewModel>? Permissions { get; set; }

        public string? Token { get; set; }

        public DateTime Expires { get; set; }
    }
}
