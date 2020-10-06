using Hotel.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Data
{
    public class MaintenancesRepository : IMaintenancesRepository
    {
        private readonly HotelContext _context;

        public MaintenancesRepository (HotelContext context)
        {
            _context = context;
        }

        public int AddMaintenance(Maintenance maintenance)
        {
            //Get Room
            var room = _context.Rooms.Find(maintenance.Room.RoomId);
            maintenance.Room = room;

            _context.Maintenances.Add(maintenance);
            _context.SaveChanges();
            return maintenance.MaintenanceId;
        }
        //GET All Maintenances list
        public async Task<IList<Maintenance>> AllMaintenances()
        {
            return await _context.Maintenances.Include(maintenance => maintenance.Room ).ToListAsync();
        }
        //GET Maintenance Details
        public async Task<Maintenance> GetMaintenance(int id)
        {
            return await _context.Maintenances.Include(maintenance => maintenance.Room).FirstOrDefaultAsync(m => m.MaintenanceId == id);
        }

        //POST Update Maintenance
        public bool EditMaintenance(Maintenance maintenance)
        {
            try
            {
                _context.Update(maintenance);
                return _context.SaveChanges() > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaintenanceExists(maintenance.MaintenanceId))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }
        private bool MaintenanceExists(int id)
        {
            return _context.Maintenances.Any(e => e.MaintenanceId == id);
        }

        public bool DeleteMaintenance(int id)
        {
            var maintenance = _context.Maintenances.Find(id);
            _context.Maintenances.Remove(maintenance);
            return _context.SaveChanges() > 0;
        }
    }
}
