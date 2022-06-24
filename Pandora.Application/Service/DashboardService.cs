using Pandora.Domain.Repository;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Collections.Generic;
using Pandora.Application.Contract;
using System.Linq;
using Pandora.Application.ViewModel;
using System.Threading.Tasks;

namespace Pandora.Application.Service
{
    public class DashboardService : IDashboardService
    {
        private readonly DashboardRepository _dashboardRepository;
        public DashboardService(DashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<List<DashboardViewModel>> GetAll()
        {
            return (await _dashboardRepository.GetAll()).Select(q => new DashboardViewModel
            {
                ItemName = q.ItemName,
                ItemValue = q.ItemValue
            }).ToList();
        }

    }
}
