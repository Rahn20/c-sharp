using HotelBookingApp.Core;
using HotelBookingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingApp.ViewModels.RoomViewModels
{
    public class RoomDetailsViewModel : ViewModelBase, IDisposable
    {
        private RoomsListViewModel _roomsListViewModel;

        public RoomDTOConverter CurrentRoom { get; private set; }
        public bool IsBooked => CurrentRoom.TotalBookings > 0;


        public RoomDetailsViewModel(RoomsListViewModel roomsListViewModel)
        {
            _roomsListViewModel = roomsListViewModel;
            _roomsListViewModel.RoomView += OnRoomView;
        }

        private void OnRoomView(RoomDTOConverter currentRoom)
        {
            CurrentRoom = currentRoom;
        }

        public void Dispose()
        {
            _roomsListViewModel.RoomView -= OnRoomView;
            GC.SuppressFinalize(this);  
        }
    }
}
