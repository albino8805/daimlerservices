﻿using Daimler.data.Models;
using Daimler.data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.domain.IManager
{
    public interface IUserManager : IBaseManager<UserViewModel, User>
    {
        void UpdatePassword(PasswordViewModel viewModel);
    }
}
