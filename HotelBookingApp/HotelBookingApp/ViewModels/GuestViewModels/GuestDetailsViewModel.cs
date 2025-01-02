using HotelBookingApp.Core;
using HotelBookingApp.Models;
using HotelBookingBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelBookingApp.ViewModels.GuestViewModels
{
    public class GuestDetailsViewModel : ViewModelBase, IDisposable
    {
        private readonly AppManagerBL _appManagerBL;
        private readonly GuestsListViewModel _guestsListVM;

        public GuestDTOConverter CurrentGuest { get; private set; }
        public bool HasBookings => CurrentGuest.NumberOfBookings > 0;

        public GuestDetailsViewModel(GuestsListViewModel guestsListViewModel)
        {
            _guestsListVM = guestsListViewModel;
            _guestsListVM.GuestView += OnGuestViewDetails;
        }

        private void OnGuestViewDetails(GuestDTOConverter guest)
        {
            CurrentGuest = guest;
        }


        public void Dispose()
        {
            _guestsListVM.GuestView -= OnGuestViewDetails;
            GC.SuppressFinalize(this);
        }
    }
}
