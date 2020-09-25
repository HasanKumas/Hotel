using AutoMapper;
using Hotel.Data;
using Hotel.Models;
using Hotel.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuestViewModel = Hotel.Models.GuestViewModel;

namespace Hotel.Services
{
    public class GuestService : IGuestService
    {
        private readonly IGuestsRepository _guestsRepository;
        private readonly IMapper _mapper;
        public GuestService(IGuestsRepository guestsRepository, IMapper mapper)
        {
            _guestsRepository = guestsRepository;
            _mapper = mapper;
        }
        //Add a new guest
        public bool AddGuest(GuestViewModel guest)
        {
            return _guestsRepository.AddGuest(_mapper.Map<Guest>(guest));
        }
        //GET All Guests list
        public async Task<IList<GuestViewModel>> AllGuests()
        {
            var result = await _guestsRepository.AllGuests();
            var mappedResult = _mapper.Map<IList<GuestViewModel>>(result);
            return mappedResult.ToList();
        }
        //GET Guest Details
        public async Task<GuestViewModel> GetGuest(int id)
        {
            var guest = await _guestsRepository.GetGuest(id);
            return _mapper.Map<GuestViewModel>(guest);
        }
        //POST Update Guest
        public bool EditGuest(GuestViewModel guest)
        {
            return _guestsRepository.EditGuest(_mapper.Map<Guest>(guest));
        }

        public bool DeleteGuest(int id)
        {
            return _guestsRepository.DeleteGuest(id);
        }
    }
}
