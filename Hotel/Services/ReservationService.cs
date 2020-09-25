using AutoMapper;
using Hotel.Data;
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
        private readonly IMapper _mapper;
        public ReservationService(IReservationsRepository reservationsRepository, IMapper mapper)
        {
            _reservationsRepository = reservationsRepository;
            _mapper = mapper;
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
