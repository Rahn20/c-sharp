using HotelBookingApp.Core;
using HotelBookingApp.Models;
using HotelBookingApp.Services;
using HotelBookingBL;
using HotelBookingDTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HotelBookingApp.ViewModels.GuestViewModels
{
    public class GuestsListViewModel : ViewModelBase
    {
        private readonly AppManagerBL _appManagerBL;
        private readonly IDataInitializationService _dataService;

        public ObservableCollection<GuestDTOConverter> GuestList => _dataService.Guests;
        public int RegisteredGuests => _dataService.Guests.Count;


        public string _searchBox = string.Empty;
        public string SearchBox
        {
            get => _searchBox;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _ = _dataService.GetAllGuestsAsync();
                }

                _searchBox = value;
                OnPropertyChanged(nameof(SearchBox));
            }
        }


        #region Commands
        public ICommand AddGuestCommand { get; }
        public ICommand ViewGuestCommand { get; }
        public ICommand UpdateGuestCommand { get; }
        public ICommand RemoveGuestCommand { get; }

        public ICommand SearchByNameCommand { get; }
        #endregion

        public GuestsListViewModel(INavigationService navigationService, IDataInitializationService dataService,
            AppManagerBL appManagerBL)
        {
            _appManagerBL = appManagerBL;
            _dataService = dataService;
 
            AddGuestCommand = new CommandBase(_ => navigationService.NavigateTo<AddGuestViewModel>());
            RemoveGuestCommand = new CommandBaseAsync(RemoveGuest);
            SearchByNameCommand = new CommandBaseAsync(_ => SearchByName());

            ViewGuestCommand = new CommandBase((object parameter) =>
            {
                navigationService.NavigateTo<GuestDetailsViewModel>();
                GuestView?.Invoke(parameter as GuestDTOConverter);
            });
            UpdateGuestCommand = new CommandBase((object parameter) =>
            {
                navigationService.NavigateTo<UpdateGuestViewModel>();
                GuestUpdate?.Invoke(parameter as Guest);
            });

            // Syntax indicates a fire-and-forget call where I don't await the task, but run it asynchronously.
            //_ = GetAllGuests();
        }

        public event Action<Guest> GuestUpdate;
        public event Action<GuestDTOConverter> GuestView;

        private async Task RemoveGuest(object parameter)
        {
            try
            {
                if (parameter is GuestDTOConverter guest)
                {
                    MessageBoxResult result = MessageBox.Show(
                        $"Are you sure you want to remove the guest {guest.GuestFullName}?\n Removing the guest will also remove their bookings and payments.", "Warning",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        await _appManagerBL.GuestBL.DeleteGuest(guest.Guest);
                        await _dataService.GetAllBookingsAsync();
                        await _dataService.GetAllPaymentsAsync();

                        GuestList.Remove(guest);
                        OnPropertyChanged(nameof(RegisteredGuests));
                    }
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Error removing guest: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task SearchByName()
        {
            try
            {
                if (!string.IsNullOrEmpty(SearchBox))
                {
                    var guests = await _appManagerBL.GuestBL.FilterByGuestName(SearchBox);
                    GuestList.Clear();

                    foreach (var guest in guests)
                    {
                        GuestList.Add(new GuestDTOConverter(guest));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Something went wrong filtering guests: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
