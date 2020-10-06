using Hotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Services.Interfaces
{
    public interface IMaintenanceService
    {
        Task<IList<MaintenanceViewModel>> AllMaintenances();

        int AddMaintenance(MaintenanceViewModel maintenance);

        Task<MaintenanceViewModel> GetMaintenance(int id);
        bool EditMaintenance(MaintenanceViewModel maintenance);
        bool DeleteMaintenance(int id);
    }
}
