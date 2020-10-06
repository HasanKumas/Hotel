using AutoMapper;
using Hotel.Data.Models;
using Hotel.Models;
using System.Linq;

namespace Hotel.Mappers
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Reservation, ReservationViewModel>();
            CreateMap<Reservation, ReservationDetailViewModel>()
                .ForMember(m => m.Rooms, o => o.MapFrom(s => s.RoomReservations.
                                                        Select(ma => ma.Room)));
            CreateMap<ReservationDetailViewModel, Reservation>()
                .ForMember(m => m.RoomReservations, o => o.MapFrom(s => s.Rooms.
                                                        Select(room => new RoomReservation { RoomId = room.RoomId, ReservationId = s.ReservationId})));
            CreateMap<Room, RoomViewModel>();
            CreateMap<Room, RoomDetailViewModel>()
                .ForMember(m => m.Reservations, o => o.MapFrom(s => s.RoomReservations.
                                                        Select(ma => ma.Reservation)));
            CreateMap<RoomDetailViewModel, Room>()
                .ForMember(m => m.RoomReservations, o => o.MapFrom(s => s.Reservations.
                                                        Select(reservation => new RoomReservation { ReservationId = reservation.ReservationId, RoomId = s.RoomId})));

            CreateMap<Guest, GuestViewModel>().ReverseMap();
            CreateMap<Maintenance, MaintenanceViewModel>().ReverseMap();
            CreateMap<RoomSize, RoomSizeViewModel>().ReverseMap();
            CreateMap<RoomType, RoomTypeViewModel>().ReverseMap();
            CreateMap<RoomStatus, RoomTypeViewModel>().ReverseMap();
            CreateMap<ReservationStatus, ReservationDetailViewModel>().ReverseMap();
            
        }
        
    }
}
