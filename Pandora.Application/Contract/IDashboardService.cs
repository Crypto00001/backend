using Pandora.Application.Command.Users;
using Pandora.Application.ViewModel;
using Pandora.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Application.Contract
{
    public interface IDashboardService
    {
        List<DashboardViewModel> GetAll();
    }
}
