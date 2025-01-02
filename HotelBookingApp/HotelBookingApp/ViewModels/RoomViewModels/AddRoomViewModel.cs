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

namespace HotelBookingApp.ViewModels.RoomViewModels
{
   public class AddRoomViewModel : ViewModelBase
   {
        private readonly AppManagerBL _appManagerBL;
        private readonly INavigationService _navigationService;
        private readonly RoomsListViewModel _roomsListViewModel;

        public RoomViewModel RoomViewModel { get; }
        public RoomType[] RoomTypes { get; }

        public ICommand AddRoomCommand { get; }


        public AddRoomViewModel(INavigationService navigationService, AppManagerBL appManagerBL, 
            RoomsListViewModel roomsListViewModel)
        {
            _appManagerBL = appManagerBL;
            _navigationService = navigationService;
            _roomsListViewModel = roomsListViewModel;

            RoomTypes = Enum.GetValues(typeof(RoomType)).Cast<RoomType>().ToArray();

            RoomViewModel = new RoomViewModel();
            AddRoomCommand = new CommandBaseAsync(_ => AddNewRoom());
        }

        private async Task AddNewRoom()
        {
            try
            {
                if (string.IsNullOrEmpty(RoomViewModel.RoomNumber) || string.IsNullOrEmpty(RoomViewModel.Price))
                {
                    RoomViewModel.ErrorMessage = "Room Number and Price cannot be empty!";
                    return;
                }

                int roomNumber = int.Parse(RoomViewModel.RoomNumber);
                decimal roomPrice = decimal.Parse(RoomViewModel.Price);

                if (!await _appManagerBL.RoomBL.IsRoomNumberValid(roomNumber))
                {
                    RoomViewModel.ErrorMessage = $"The room thas has the number {roomNumber} already esists.";
                    return;
                }

                Room room = new Room
                {
                    RoomNumber = roomNumber,
                    Price = roomPrice,
                    Description = RoomViewModel.Description,
                    IsAvailable = RoomViewModel.IsRoomAvailable,
                    RoomType = RoomViewModel.RoomType,
                };

                await _appManagerBL.RoomBL.AddRoom(room);
                _roomsListViewModel.RoomsList.Add(new RoomDTOConverter(room));
                _navigationService.NavigateTo<RoomsListViewModel>();

                ClearInputs();
            }
            catch (Exception ex)
            {
                RoomViewModel.ErrorMessage = ex.Message;
            }
        }

        private void ClearInputs()
        {
            RoomViewModel.RoomNumber = string.Empty;
            RoomViewModel.Description = string.Empty;
            RoomViewModel.Price = string.Empty;
            RoomViewModel.IsRoomAvailable = true;
            RoomViewModel.ErrorMessage = string.Empty;
        }
   }
}
