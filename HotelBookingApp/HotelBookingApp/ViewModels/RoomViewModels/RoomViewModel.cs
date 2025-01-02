using HotelBookingApp.Core;
using HotelBookingDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace HotelBookingApp.ViewModels.RoomViewModels
{
    public class RoomViewModel : ViewModelBase
    {
        private string _roomNumber = string.Empty;
        public string RoomNumber
        {
            get => _roomNumber;
            set
            {
                if (_roomNumber != value && value.All(char.IsDigit))
                {
                    _roomNumber = value;
                    OnPropertyChanged(nameof(RoomNumber));
                }

                ErrorMessage = string.IsNullOrEmpty(value) ? "Room Number cannot be empty" : string.Empty;
            }
        }

        private RoomType _roomType;
        public RoomType RoomType
        {
            get => _roomType;
            set
            {
                if (_roomType != value)
                {
                    _roomType = value;
                    OnPropertyChanged(nameof(RoomType));
                }
            }
        }


        private string _price = string.Empty;
        public string Price
        {
            get => _price;
            set
            {
                if (_price != value && Regex.IsMatch(value, @"[0-9,]+$") || string.IsNullOrEmpty(value))
                {
                    _price = value;
                    OnPropertyChanged(nameof(Price));
                }

                ErrorMessage = string.IsNullOrEmpty(value) ? "Room Price cannot be empty" : string.Empty;
            }
        }


        private string _description = string.Empty;
        public string Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }


        private bool _isRoomAvailable = true;
        public bool IsRoomAvailable
        {
            get => _isRoomAvailable;
            set
            {
                if (_isRoomAvailable != value)
                {
                    _isRoomAvailable = value;
                    OnPropertyChanged(nameof(IsRoomAvailable));
                }
            }
        }


        private string _errorMessage = string.Empty;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                if (_errorMessage != value)
                {
                    _errorMessage = value;
                    OnPropertyChanged(nameof(ErrorMessage));
                }
            }
        }
    }
}
