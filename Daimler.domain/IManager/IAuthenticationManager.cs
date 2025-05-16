using Daimler.data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.domain.IManager
{
    public interface IAuthenticationManager
    {
        LoginViewModel ValidateUser(AuthenticationViewModel authentication);
    }
}
