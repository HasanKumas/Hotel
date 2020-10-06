using AutoMapper;
using Hotel.Data;
using Hotel.Data.Models;
using Hotel.Models;
using Hotel.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Services
{
    public class MaintenanceService : IMaintenanceService
    {
        private readonly IMaintenancesRepository _maintenancesRepository;
        private readonly IMapper _mapper;
        public MaintenanceService(IMaintenancesRepository maintenancesRepository, IMapper mapper)
        {
            _maintenancesRepository = maintenancesRepository;
            _mapper = mapper;
        }
        //Add a new maintenance
        public int AddMaintenance(MaintenanceViewModel maintenance)
        {
            return _maintenancesRepository.AddMaintenance(_mapper.Map<Maintenance>(maintenance));
        }
        //GET All Maintenances list
        public async Task<IList<MaintenanceViewModel>> AllMaintenances()
        {
            var result = await _maintenancesRepository.AllMaintenances();
            var mappedResult = _mapper.Map<IList<MaintenanceViewModel>>(result);
            return mappedResult.ToList();
        }
        //GET Maintenance Details
        public async Task<MaintenanceViewModel> GetMaintenance(int id)
        {
            var maintenance = await _maintenancesRepository.GetMaintenance(id);
            return _mapper.Map<MaintenanceViewModel>(maintenance);
        }
        //POST Update Maintenance
        public bool EditMaintenance(MaintenanceViewModel maintenance)
        {
            return _maintenancesRepository.EditMaintenance(_mapper.Map<Maintenance>(maintenance));
        }

        public bool DeleteMaintenance(int id)
        {
            return _maintenancesRepository.DeleteMaintenance(id);
        }
    }
}
