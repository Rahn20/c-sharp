using System;
using InvoiceMaker.Core;
using InvoiceMaker.Services;

namespace InvoiceMaker.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ViewModelBase HeaderViewModel { get; }
        public ViewModelBase InvoiceContentViewModel { get; }

        public MainViewModel(INavigationService navigationService) 
        {
            HeaderViewModel = navigationService.Navigate<HeaderViewModel>();
            InvoiceContentViewModel = navigationService.Navigate<InvoiceContentViewModel>();
        }
    }
}
