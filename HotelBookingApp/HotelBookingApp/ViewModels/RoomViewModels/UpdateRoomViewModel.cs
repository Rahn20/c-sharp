using HotelBookingApp.Core;
using HotelBookingApp.Services;
using HotelBookingBL;
using HotelBookingDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelBookingApp.ViewModels.RoomViewModels
{
    public class UpdateRoomViewModel : ViewModelBase, IDisposable
    {
        private readonly AppManagerBL _appManagerBL;
        private readonly INavigationService _navigationService;
        private readonly RoomsListViewModel _roomsListViewModel;
        private Room _currentRoom;

        public RoomViewModel RoomViewModel { get; }
        public RoomType[] RoomTypes { get; }

        public ICommand UpdateRoomCommand { get; }

        public UpdateRoomViewModel(INavigationService navigationService, AppManagerBL appManagerBL,
            RoomsListViewModel roomsListViewModel)
        {
            _appManagerBL = appManagerBL;
            _navigationService = navigationService;
            _roomsListViewModel = roomsListViewModel;

            _roomsListViewModel.RoomUpdate += OnRoomUpdate;
            RoomTypes = Enum.GetValues(typeof(RoomType)).Cast<RoomType>().ToArray();
            RoomViewModel = new RoomViewModel();
            UpdateRoomCommand = new CommandBaseAsync(_ => UpdateRoom());
        }

        private void OnRoomUpdate(Room room)
        {
            _currentRoom = room;

            RoomViewModel.RoomNumber = _currentRoom.RoomNumber.ToString();
            RoomViewModel.RoomType = _currentRoom.RoomType;
            RoomViewModel.IsRoomAvailable = _currentRoom.IsAvailable;
            RoomViewModel.Price = _currentRoom.Price.ToString("F2");
            RoomViewModel.Description = _currentRoom.Description;
        }

        private async Task UpdateRoom()
        {
            try
            {
                if (string.IsNullOrEmpty(RoomViewModel.Price))
                {
                    RoomViewModel.ErrorMessage = "Room Price cannot be empty!";
                    return;
                }

                decimal roomPrice = decimal.Parse(RoomViewModel.Price);

                _currentRoom.Price = roomPrice;
                _currentRoom.Description = RoomViewModel.Description;
                _currentRoom.RoomType = RoomViewModel.RoomType;
                _currentRoom.IsAvailable = RoomViewModel.IsRoomAvailable;

                await _appManagerBL.RoomBL.UpdateRoom(_currentRoom);
                _navigationService.NavigateTo<RoomsListViewModel>();
            }
            catch (Exception ex)
            {
                RoomViewModel.ErrorMessage = ex.Message;
            }
        }

        public void Dispose()
        {
            _roomsListViewModel.RoomUpdate -= OnRoomUpdate;
            GC.SuppressFinalize(this);
        }
    }
}
