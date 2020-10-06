using Hotel.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Data
{
    public interface IMaintenancesRepository
    {
        Task<IList<Maintenance>> AllMaintenances();

        int AddMaintenance(Maintenance maintenance);

        Task<Maintenance> GetMaintenance(int id);
        bool EditMaintenance(Maintenance maintenance);
        bool DeleteMaintenance(int id);
    }
}
