using HotelBookingApp.Core;
using HotelBookingApp.Models;
using HotelBookingApp.Services;
using HotelBookingBL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HotelBookingApp.ViewModels.PaymentViewModels
{
    public class PaymentsListViewModel : ViewModelBase
    {
        private readonly IDataInitializationService _dataService;
        private readonly AppManagerBL _appManagerBL;

        public ObservableCollection<PaymentDTOConverter> PaymentList => _dataService.Payments;
        public int RegisteredPayments => _dataService.Payments.Count;

        public string _searchBox = string.Empty;
        public string SearchBox
        {
            get => _searchBox;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _ = _dataService.GetAllPaymentsAsync();
                }

                _searchBox = value;
                OnPropertyChanged(nameof(SearchBox));
            }
        }

        public ICommand SearchByGuestOrRoomCommand { get; }


        public PaymentsListViewModel(AppManagerBL appManagerBL, IDataInitializationService data)
        {
            _dataService = data;
            _appManagerBL = appManagerBL;

            SearchByGuestOrRoomCommand = new CommandBaseAsync(_ => SearchPayments());
        }

        private async Task SearchPayments()
        {
            try
            {
                if (!string.IsNullOrEmpty(SearchBox))
                {
                    var payments = await _appManagerBL.PaymentBL.FilterByGuestOrRoom(SearchBox);
                    PaymentList.Clear();

                    foreach (var payment in payments)
                    {
                        PaymentList.Add(new PaymentDTOConverter(payment));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Something went wrong filtering payments by guest name: {ex.Message}", 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
