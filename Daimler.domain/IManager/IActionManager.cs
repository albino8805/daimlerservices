using Daimler.data.ViewModels;
using ActionEntity = Daimler.data.Models.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.domain.IManager
{
    public interface IActionManager : IBaseManager<ActionViewModel, ActionEntity>
    {

    }
}
