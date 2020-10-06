using AutoMapper;
using Hotel.Data.Models;
using Hotel.Data;
using Hotel.Mappers;
using Hotel.Models;
using Hotel.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomsRepository _roomsRepository;
        private readonly IMaintenanceService _maintenanceService;
        private readonly IMapper _mapper;
        public RoomService(IRoomsRepository roomsRepository, IMaintenanceService maintenanceService, IMapper mapper)
        {
            _roomsRepository = roomsRepository;
            _maintenanceService = maintenanceService;
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

        public async Task<IList<RoomDetailViewModel>> CanBlockedRoomsAsync(string roomNumber, DateTime startDate, DateTime endDate)
        {
            var room = _mapper.Map<RoomDetailViewModel>( await _roomsRepository.GetRoomByNumber(roomNumber));

            bool isAvailableToBlock =  room.Reservations == null ||
                    room.Reservations.All(reservation => reservation.CheckInDate >= endDate ||
                    reservation.CheckOutDate <= startDate);

            List<RoomDetailViewModel> roomList = new List<RoomDetailViewModel>();

            if (isAvailableToBlock)
            {
                roomList.Add(room);
                return roomList;
            }
            else
            {
                var result = await _roomsRepository.AllRooms();
                var rooms = _mapper.Map<IList<RoomDetailViewModel>>(result).ToList();
                roomList = rooms.Where(alternateRoom => ((int)alternateRoom.RoomSize).Equals((int)room.RoomSize))
                                                                .Where(alternateRoom => alternateRoom.RoomType.Equals(room.RoomType))
                                                                .Where(alternateRoom => alternateRoom.Maintenances == null || 
                                                                    alternateRoom.Maintenances.All(maintenance => maintenance.StartDate >= endDate ||
                                                                    maintenance.EndDate <= startDate))
                                                                .Where(alternateRoom => alternateRoom.EntranceAvailableDate <= startDate)
                                                                .Where(alternateRoom => alternateRoom.Reservations == null ||
                                                                    alternateRoom.Reservations.All(reservation => reservation.CheckInDate >= endDate ||
                                                                    reservation.CheckOutDate <= startDate)).ToList();

                return roomList;
            }
        }
    }
}
