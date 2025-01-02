using HotelBookingApp.Core;
using HotelBookingApp.Services;
using HotelBookingBL;
using HotelBookingDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HotelBookingApp.ViewModels.GuestViewModels
{
    public class UpdateGuestViewModel : ViewModelBase, IDisposable
    {
        private readonly AppManagerBL _appManagerBL;
        private readonly INavigationService _navigationService;
        private readonly GuestsListViewModel _guestsListViewModel;
        private Guest _currentGuest;

        public GuestViewModel GuestViewModel { get; }
        public ICommand UpdateGuestCommand { get; }

        public UpdateGuestViewModel(INavigationService navigationService, AppManagerBL appManagerBL,
            GuestsListViewModel guestsListViewModel)
        {
            _appManagerBL = appManagerBL;
            _navigationService = navigationService;
            _guestsListViewModel = guestsListViewModel;
            _guestsListViewModel.GuestUpdate += OnGuestUpdated;

            GuestViewModel = new GuestViewModel();
            UpdateGuestCommand = new CommandBaseAsync(_ => UpdateGuest());
        }

        private void OnGuestUpdated(Guest guest)
        {
            _currentGuest = guest;
            GuestViewModel.FirstName = guest.FirstName;
            GuestViewModel.LastName = guest.LastName;
            GuestViewModel.Email = guest.Email;
            GuestViewModel.PhoneNumber = guest.PhoneNumber;
        }

        private async Task UpdateGuest()
        {
            try
            {
                if (string.IsNullOrEmpty(GuestViewModel.FirstName) || string.IsNullOrEmpty(GuestViewModel.LastName))
                {
                    GuestViewModel.ErrorMessage = "Guest first name and last name cannot be empty!";
                    return;
                }
                else if (string.IsNullOrEmpty(GuestViewModel.PhoneNumber) || !GuestBL.IsPhoneNumberValid(GuestViewModel.PhoneNumber))
                {
                    GuestViewModel.ErrorMessage = "The guest phone number is not valid. It should start with 0 and contains 10 numbers, ex: 0123456789.";
                    return;
                }

                _currentGuest.FirstName = GuestViewModel.FirstName;
                _currentGuest.LastName = GuestViewModel.LastName;
                _currentGuest.PhoneNumber = GuestViewModel.PhoneNumber;

                await _appManagerBL.GuestBL.UpdateGuest(_currentGuest);
                _navigationService.NavigateTo<GuestsListViewModel>();
                GuestViewModel.ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                GuestViewModel.ErrorMessage = ex.Message;
            }
        }

        public void Dispose()
        {
            _guestsListViewModel.GuestUpdate -= OnGuestUpdated;
            GC.SuppressFinalize(this);
        }
    }
}
