using Hotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Services.Interfaces
{
    public interface IGuestService
    {
        Task<IList<GuestViewModel>> AllGuests();

        int AddGuest(GuestViewModel guest);

        Task<GuestViewModel> GetGuest(int id);
        Task<GuestViewModel> GetGuestByName(string lastName);
        bool EditGuest(GuestViewModel guest);
        bool DeleteGuest(int id);
    }
}
