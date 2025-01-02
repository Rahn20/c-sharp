using HotelBookingApp.Core;
using System;
using System.Text.RegularExpressions;

namespace HotelBookingApp.ViewModels.GuestViewModels
{
    public class GuestViewModel : ViewModelBase
    {
        private string _firstName = string.Empty;
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged(nameof(FirstName));

                    ErrorMessage = string.IsNullOrEmpty(value) ? "First Name cannot be empty" : string.Empty;
                }
            }
        }

        private string _lastName = string.Empty;
        public string LastName
        {
            get => _lastName;
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged(nameof(LastName));

                    ErrorMessage = string.IsNullOrEmpty(value) ? "Last Name cannot be empty" : string.Empty;
                }
            }
        }

        private string _email = string.Empty;
        public string Email
        {
            get => _email;
            set
            {
                if (_email != value && Regex.IsMatch(value, @"^[a-zA-Z0-9@._]+$") || string.IsNullOrEmpty(value))
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email));

                    ErrorMessage = string.IsNullOrEmpty(value) ? "Email cannot be empty" : string.Empty;
                }
            }
        }

        private string _phoneNumber = string.Empty;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (_phoneNumber != value && value.All(char.IsDigit))
                {
                    _phoneNumber = value;
                    OnPropertyChanged(nameof(PhoneNumber));

                    ErrorMessage = string.IsNullOrEmpty(value) ? "Phone Number cannot be empty" : string.Empty;
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
