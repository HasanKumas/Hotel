using AutoMapper;
using Hotel.Data;
using Hotel.Mappers;
using Hotel.Models;
using Hotel.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoomViewModel = Hotel.Models.RoomViewModel;

namespace Hotel.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomsRepository _roomsRepository;
        private readonly IMapper _mapper;
        public RoomService(IRoomsRepository roomsRepository, IMapper mapper)
        {
            _roomsRepository = roomsRepository;
            _mapper = mapper;
        }
        //Add a new room
        public bool AddRoom(RoomDetailViewModel room)
        {
            return _roomsRepository.AddRoom(_mapper.Map<Room>(room));
        }
        //GET All Rooms list
        public async Task<IList<RoomDetailViewModel>> AllRooms()
        {
            var result = await _roomsRepository.AllRooms();
            var mappedResult = _mapper.Map<IList<RoomDetailViewModel>>(result);
            return mappedResult.ToList();
        }
        //GET Room Details
        public async Task<RoomDetailViewModel> GetRoom(int id)
        {
            var room = await _roomsRepository.GetRoom(id);
            return _mapper.Map<RoomDetailViewModel>(room);
        }
        //POST Update Room
        public bool EditRoom(RoomDetailViewModel room)
        {
            return _roomsRepository.EditRoom(_mapper.Map<Room>(room));
        }

        public bool DeleteRoom(int id)
        {
            return _roomsRepository.DeleteRoom(id);
        }
    }
}
