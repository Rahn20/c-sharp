using HotelBookingApp.Core;
using HotelBookingApp.Models;
using HotelBookingApp.Services;
using HotelBookingApp.ViewModels.BookingViewModels;
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

namespace HotelBookingApp.ViewModels.RoomViewModels
{
    public class RoomsListViewModel : ViewModelBase
    {
        private readonly AppManagerBL _appManagerBL;
        private readonly IDataInitializationService _dataService;

        public ObservableCollection<RoomDTOConverter> RoomsList => _dataService.Rooms;
        public int RegisteredRooms => _dataService.Rooms.Count;


        private bool _availableRooms = false;
        public bool AvailableRooms
        {
            get => _availableRooms;
            set
            {
                _availableRooms = value;
                OnPropertyChanged(nameof(AvailableRooms));

                _ = FilterRoomsAsync();   // Call the method asynchronously
            }
        }

        public ICommand AddRoomCommand { get; }
        public ICommand RemoveRoomCommand { get; }
        public ICommand UpdateRoomCommand { get; }
        public ICommand ViewRoomCommand { get; }
        public ICommand AddBookingCommand { get; }

        public RoomsListViewModel(INavigationService navigationService, IDataInitializationService dataInitializationService,
            AppManagerBL appManagerBL) 
        {
            _appManagerBL = appManagerBL;
            _dataService = dataInitializationService;

            AddRoomCommand = new CommandBase(_ => navigationService.NavigateTo<AddRoomViewModel>());
            RemoveRoomCommand = new CommandBaseAsync(RemoveRoom);

            AddBookingCommand = new CommandBase((object parameter) => 
            {
                navigationService.NavigateTo<AddBookingViewModel>();
                AddBooking?.Invoke(parameter as Room);
            });
            UpdateRoomCommand = new CommandBase((object parameter) => 
            {
                navigationService.NavigateTo<UpdateRoomViewModel>();
                RoomUpdate?.Invoke(parameter as Room); 
            });
            ViewRoomCommand = new CommandBase((object parameter) =>
            {
                navigationService.NavigateTo<RoomDetailsViewModel>();
                RoomView?.Invoke(parameter as RoomDTOConverter);
            });
        }

        public event Action<Room> RoomUpdate;
        public event Action<RoomDTOConverter> RoomView;
        public event Action<Room> AddBooking;


        private async Task FilterRoomsAsync()
        {
            if (AvailableRooms)
            {
                var filteredRooms = RoomsList.Where(room => room.IsAvailable).ToList();
                RoomsList.Clear();
                foreach (var room in filteredRooms)
                {
                    RoomsList.Add(room);
                }
            }
            else
            {
                await _dataService.GetAllRoomsAsync();
            }
        }

        private async Task RemoveRoom(object arg)
        {
            try
            {
                if (arg is RoomDTOConverter currentRoom)
                {
                    MessageBoxResult result = MessageBox.Show(
                        $"Are you sure you want to remove the room with room number {currentRoom.RoomNumber}?\n Removing the room will also remove its associated bookings and payments.",
                        "Warning",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        await _appManagerBL.RoomBL.DeleteRoom(currentRoom.Room);
                        await _dataService.GetAllBookingsAsync();
                        await _dataService.GetAllPaymentsAsync();

                        RoomsList.Remove(currentRoom);
                        OnPropertyChanged(nameof(RegisteredRooms));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error removing room: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
