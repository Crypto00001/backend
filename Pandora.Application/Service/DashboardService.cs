using Pandora.Domain.Domain;
using Pandora.Domain.Repository;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Collections.Generic;
using Pandora.Application.Contract;
using System;
using System.Linq;
using Pandora.Application.ViewModel;

namespace Pandora.Application.Service
{
    public class DashboardService : IDashboardService
    {
        private readonly DashboardRepository _dashboardRepository;
        public DashboardService(DashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public List<DashboardViewModel> GetAll()
        {
            return _dashboardRepository.Get().Select(q => new DashboardViewModel
            {
                ItemName = q.ItemName,
                ItemValue = q.ItemValue
            }).ToList();
        }

    }
}
