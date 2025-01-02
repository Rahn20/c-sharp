using HotelBookingApp.Core;
using HotelBookingApp.Models;
using HotelBookingApp.Services;
using HotelBookingBL;
using HotelBookingDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace HotelBookingApp.ViewModels.GuestViewModels
{
    public class AddGuestViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly AppManagerBL _appManagerBL;
        private readonly GuestsListViewModel _guestListVM;

        public GuestViewModel GuestViewModel { get; }
        public ICommand AddGuestCommand { get; }


        public AddGuestViewModel(INavigationService navigationService, AppManagerBL appManagerBL, 
            GuestsListViewModel guestsListViewModel)
        {
            _appManagerBL = appManagerBL;
            _navigationService = navigationService;
            _guestListVM = guestsListViewModel;

            GuestViewModel = new GuestViewModel();
            AddGuestCommand = new CommandBaseAsync(_ => AddNewGuest());
        }

        private async Task AddNewGuest()
        {
            try
            {
                if (await ValidateGuestInput())
                {
                    Guest guest = new Guest
                    {
                        FirstName = GuestViewModel.FirstName,
                        LastName = GuestViewModel.LastName,
                        Email = GuestViewModel.Email,
                        PhoneNumber = GuestViewModel.PhoneNumber,
                    };

                    await _appManagerBL.GuestBL.AddGuest(guest);
                    _guestListVM.GuestList.Add(new GuestDTOConverter(guest));
                    _navigationService.NavigateTo<GuestsListViewModel>();

                    // clear inputs 
                    GuestViewModel.FirstName = string.Empty;
                    GuestViewModel.LastName = string.Empty;
                    GuestViewModel.Email = string.Empty;
                    GuestViewModel.PhoneNumber = string.Empty;
                    GuestViewModel.ErrorMessage = string.Empty;
                }
            }
            catch (Exception ex)
            {
                GuestViewModel.ErrorMessage = ex.Message;
            }
        }

        private async Task<bool> ValidateGuestInput()
        {
            // Validate name
            if (string.IsNullOrWhiteSpace(GuestViewModel.LastName) ||
                string.IsNullOrWhiteSpace(GuestViewModel.FirstName))
            {
                GuestViewModel.ErrorMessage = "Guest first name and last name cannot be empty!";
                return false;
            }

            // Validate contact information
            if (string.IsNullOrWhiteSpace(GuestViewModel.PhoneNumber) ||
                string.IsNullOrWhiteSpace(GuestViewModel.Email))
            {
                GuestViewModel.ErrorMessage = "Guest Phone Number and Email cannot be empty!";
                return false;
            }

            // Validate phone number
            if (!GuestBL.IsPhoneNumberValid(GuestViewModel.PhoneNumber))
            {
                GuestViewModel.ErrorMessage = "The guest phone number is not valid. It should start with 0 and contains 10 numbers, ex: 0123456789.";
                return false;
            }

            // Validate email asynchronously
            if (!await _appManagerBL.GuestBL.IsEmailValid(GuestViewModel.Email))
            {
                GuestViewModel.ErrorMessage = "Email address is invalid or already exists.";
                return false;
            }

            // All validations passed
            return true;
        }

        //private bool CanAddNewGuest()
        //{
        //    bool validateName = !string.IsNullOrEmpty(GuestViewModel.LastName) || !string.IsNullOrEmpty(GuestViewModel.FirstName);
        //    bool validatePhone = !string.IsNullOrEmpty(GuestViewModel.PhoneNumber);
        //    bool validateEmail = !string.IsNullOrEmpty(GuestViewModel.Email);

        //    return (validateName && validatePhone && validateEmail);
        //}
    }
}
