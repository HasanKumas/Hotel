using AutoMapper;
using Hotel.Business;
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
    public class ReservationService : IReservationService
    {
        private readonly IReservationsRepository _reservationsRepository;
        private readonly IRoomsRepository _roomsRepository;
        private readonly IMapper _mapper;
        public ReservationService(IReservationsRepository reservationsRepository, IRoomsRepository roomsRepository, IMapper mapper)
        {
            _reservationsRepository = reservationsRepository;
            _roomsRepository = roomsRepository;
            _mapper = mapper;
        }
        //Get available Rooms
        public async Task<IList<RoomDetailViewModel>> GetAvailableRooms(ReservationDetailViewModel currentReservation)
        {
            var rooms = _mapper.Map<IList<RoomDetailViewModel>>(await _roomsRepository.AllRooms()).ToList();
            
            //adjust the size to include rooms with range like 3-4 persons and 5-6 persons
            var size = 0;
            switch(currentReservation.NumberOfGuests)
            {
                case 1:
                    {
                        size = 1;
                        break;
                    }
                case 2:
                    {
                        size = 2;
                        break;
                    }
                case 3:
                    {
                        size = 4;
                        break;
                    }
                case 4:
                    {
                        size = 4;
                        break;
                    }
                case 5:
                    {
                        size = 6;
                        break;
                    }
                default:
                    {
                        size = currentReservation.NumberOfGuests;
                        break;
                    }

            }

            List<RoomDetailViewModel> availableRooms = rooms.Where(room => ((int)room.RoomSize).Equals(size))
                .Where(room => room.EntranceAvailableDate <= currentReservation.CheckInDate)
                .Where(room => room.Maintenances == null ||
                                                    room.Maintenances.All(maintenance => maintenance.StartDate >= currentReservation.CheckOutDate ||
                                                    maintenance.EndDate <= currentReservation.CheckInDate))
                .Where(room => room.Reservations == null ||
                                                    room.Reservations.All(reservation => reservation.CheckInDate >= currentReservation.CheckOutDate ||
                                                    reservation.CheckOutDate <= currentReservation.CheckInDate))
                .ToList();
            
            return availableRooms;
        }
        //Get alternate Rooms
        public async Task<IList<RoomDetailViewModel>> GetAlternateRooms(ReservationDetailViewModel currentReservation)
        {

            var rooms = _mapper.Map<IList<RoomDetailViewModel>>(await _roomsRepository.AllRooms()).ToList();

            List<RoomDetailViewModel> alternateRooms = rooms
                .Where(room => (int)room.RoomSize >= (currentReservation.NumberOfGuests / 2))
                .Where(room => room.EntranceAvailableDate <= currentReservation.CheckInDate)
                .Where(room => room.Maintenances == null ||
                                                    room.Maintenances.All(maintenance => maintenance.StartDate >= currentReservation.CheckOutDate ||
                                                    maintenance.EndDate <= currentReservation.CheckInDate))
                .Where(room => room.Reservations == null ||
                    room.Reservations.All(reservation => reservation.CheckInDate >= currentReservation.CheckOutDate ||
                                            reservation.CheckOutDate <= currentReservation.CheckInDate))
                .ToList();

            var totalBeds = Calculation.CalculateTotalBeds(alternateRooms);
            if(totalBeds >= currentReservation.NumberOfGuests)
            {
                return alternateRooms;
            }
            return alternateRooms = new List<RoomDetailViewModel> { };
        }
        //Add a new reservation
        public bool AddReservation(ReservationDetailViewModel reservation)
        {
            var r = _mapper.Map<Reservation>(reservation);
            return _reservationsRepository.AddReservation(r);
        }
        //GET All Reservations list
        public async Task<IList<ReservationDetailViewModel>> AllReservations()
        {
            var result = await _reservationsRepository.AllReservations();
            var mappedResult = _mapper.Map<IList<ReservationDetailViewModel>>(result);
            return mappedResult.ToList();
        }
        //GET Reservation Details
        public async Task<ReservationDetailViewModel> GetReservation(int id)
        {
            var reservation = await _reservationsRepository.GetReservation(id);

            return _mapper.Map<ReservationDetailViewModel>(reservation);
        }
        //POST Update Reservation
        public bool EditReservation(ReservationDetailViewModel reservation)
        {
            return _reservationsRepository.EditReservation(_mapper.Map<Reservation>(reservation));
        }

        public bool DeleteReservation(int id)
        {
            return _reservationsRepository.DeleteReservation(id);
        }
    }
}
