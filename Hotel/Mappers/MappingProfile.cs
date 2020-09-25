using AutoMapper;
using Hotel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Mappers
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Reservation, Models.ReservationViewModel>();
            CreateMap<Reservation, Models.ReservationDetailViewModel>()
                .ForMember(m => m.Rooms, o => o.MapFrom(s => s.RoomReservations.
                                                        Select(ma => ma.Room)));
            CreateMap<Models.ReservationDetailViewModel, Reservation>()
                .ForMember(m => m.RoomReservations, o => o.MapFrom(s => s.Rooms.
                                                        Select(room => new RoomReservation { RoomId = room.RoomId, ReservationId = s.ReservationId})));
            CreateMap<Room, Models.RoomViewModel>().ReverseMap();
            CreateMap<Room, Models.RoomDetailViewModel>()
                .ForMember(m => m.Reservations, o => o.MapFrom(s => s.RoomReservations.
                                                        Select(ma => ma.Reservation)));

            CreateMap<Guest, Models.GuestViewModel>().ReverseMap();
            CreateMap<Maintenance, Models.MaintenanceViewModel>().ReverseMap();
            CreateMap<RoomSize, Models.RoomSizeViewModel>().ReverseMap();
            CreateMap<RoomType, Models.RoomTypeViewModel>().ReverseMap();
            
        }
        
    }
}
