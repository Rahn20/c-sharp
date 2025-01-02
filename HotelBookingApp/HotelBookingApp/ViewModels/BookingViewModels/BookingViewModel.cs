using HotelBookingApp.Core;
using HotelBookingBL;
using HotelBookingDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelBookingApp.ViewModels.BookingViewModels
{
    public class BookingViewModel : ViewModelBase
    {

        private DateTime _checkInDate = DateTime.Now;
        public DateTime CheckInDate
        {
            get => _checkInDate;
            set
            {
                if (value < CheckOutDate)
                {
                    _checkInDate = value;
                    OnPropertyChanged(nameof(CheckInDate));

                    TotalAmount = (CheckOutDate - CheckInDate).Days * Room.Price;

                    if (Payment != null)
                    {
                        var (Receive, Pay) = BookingBL.CalculateBookingNewPrice(TotalAmount, Payment);

                        PayAmount = Pay;
                        ReceiveAmount = Receive;
                    }
                }

                ErrorMessage = value > CheckOutDate ? "Check-in date must be before check-out date" : string.Empty;
            }
        }


        private DateTime _checkOutDate = DateTime.Now.AddDays(7);
        public DateTime CheckOutDate
        {
            get => _checkOutDate;
            set
            {
                if (value > CheckInDate)
                {
                    _checkOutDate = value;
                    OnPropertyChanged(nameof(CheckOutDate));

                    TotalAmount = (CheckOutDate - CheckInDate).Days * Room.Price;

                    if (Payment != null)
                    {
                        var (Receive, Pay) = BookingBL.CalculateBookingNewPrice(TotalAmount, Payment);
                        
                        PayAmount = Pay;
                        ReceiveAmount = Receive;
                    }
                }

                ErrorMessage = value < CheckInDate ? "Check-in date must be before check-out date!" : string.Empty;
            }
        }

        private Room _room;
        public Room Room
        {
            get => _room;
            set
            {
                _room = value;
                OnPropertyChanged(nameof(Room));
            }
        }

        private Payment? _payment;
        public Payment? Payment
        {
            get => _payment;
            set
            {
                _payment = value;
                OnPropertyChanged(nameof(Payment));
            }
        }

        private decimal _payAmount;
        public decimal PayAmount
        {
            get => _payAmount;
            set
            {
                _payAmount = value;
                OnPropertyChanged(nameof(PayAmount));
            }
        }

        private decimal _receiveAmount;
        public decimal ReceiveAmount
        {
            get => _receiveAmount;
            set
            {
                _receiveAmount = value;
                OnPropertyChanged(nameof(ReceiveAmount));
            }
        }

        private decimal _totalAmount;
        public decimal TotalAmount
        {
            get => _totalAmount;
            set
            {
                _totalAmount = value;
                OnPropertyChanged(nameof(TotalAmount));
            }
        }


        private PaymentMethod _selectedPaymentMethod;
        public PaymentMethod SelectedPaymentMethod
        {
            get => _selectedPaymentMethod;
            set
            {
                _selectedPaymentMethod = value;
                OnPropertyChanged(nameof(SelectedPaymentMethod));
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
