using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using InvoiceMaker.Core;
using InvoiceMaker.Models;
using InvoiceMaker.Services;

namespace InvoiceMaker.ViewModels
{
    public class InvoiceContentViewModel : ViewModelBase, IDisposable
    {
        private readonly InvoiceManager _invoiceManager;
        private readonly Validations _validations;


        private bool _isContentVisible = false;
        public bool IsContentVisible
        {
            get => _isContentVisible;
            private set
            {
                _isContentVisible = value;
                OnPropertyChanged(nameof(IsContentVisible));
            }
        }

        public ObservableCollection<Product> Products { get; private set; }
        //public bool HasErrors => _validations.HasErrors;

        #region Invoice Properties

        private string _companySenderName = string.Empty;
        public string CompanySenderName 
        {
            get => _companySenderName;
            private set
            {
                _companySenderName = value;
                OnPropertyChanged(nameof(CompanySenderName));
            }
        }

        private string _invoiceNumber = string.Empty;
        public string InvoiceNumber
        {
            get => _invoiceNumber;
            private set
            {
                _invoiceNumber = value;
                OnPropertyChanged(nameof(InvoiceNumber));
            }
        }

        private DateTime _invoiceDate = new DateTime(2020,1,1);
        public DateTime InvoiceDate
        {
            get => _invoiceDate;
            set
            {
                if (value <= _invoiceDueDate)
                {
                    _invoiceDate = value;
                    OnPropertyChanged(nameof(InvoiceDate));
                    //_invoiceManager.Invoice.InvoiceDate = new DateOnly(_invoiceDate.Year, _invoiceDate.Month, _invoiceDate.Day);
                }
                else
                {
                    _validations.ClearErrors(nameof(InvoiceDate));
                    _validations.AddError("The start date cannot be after the end date.", nameof(InvoiceDate));
                }
            }
        }

        private DateTime _invoiceDueDate = new DateTime(2029,1,1);
        public DateTime InvoiceDueDate
        {
            get => _invoiceDueDate;
            set
            {
                if (value >= _invoiceDate)
                {
                    _invoiceDueDate = value;
                    OnPropertyChanged(nameof(InvoiceDueDate));
                }
                else
                {
                    _validations.ClearErrors(nameof(InvoiceDueDate));
                    _validations.AddError("The end date cannot be before the start date.", nameof(InvoiceDueDate));
                }
            }
        }

        #endregion

        #region Company receiver properties

        private string _companyReceiverName = string.Empty;
        public string CompanyReceiverName
        {
            get => _companyReceiverName;
            private set
            {
                _companyReceiverName = value;
                OnPropertyChanged(nameof(CompanyReceiverName));
            }
        }

        private string _companyReceiverPerson = string.Empty;
        public string CompanyReceiverPerson
        {
            get => _companyReceiverPerson;
            private set
            {
                _companyReceiverPerson = value;
                OnPropertyChanged(nameof(CompanyReceiverPerson));
            }
        }

        private string _companyReceiverStreet = string.Empty;
        public string CompanyReceiverStreet
        {
            get => _companyReceiverStreet;
            private set
            {
                _companyReceiverStreet = value;
                OnPropertyChanged(nameof(CompanyReceiverStreet));
            }
        }

        private string _companyReceiverCode_City = string.Empty;
        public string CompanyReceiverCode_City
        {
            get => _companyReceiverCode_City;
            private set
            {
                _companyReceiverCode_City = value;
                OnPropertyChanged(nameof(CompanyReceiverCode_City));
            }
        }

        private string _companyReceiverCountry = string.Empty;
        public string CompanyReceiverCountry
        {
            get => _companyReceiverCountry;
            private set
            {
                _companyReceiverCountry = value;
                OnPropertyChanged(nameof(CompanyReceiverCountry));
            }
        }
        #endregion

        #region Company senders properties

        private string _companySenderStreet = string.Empty;
        public string CompanySenderStreet
        {
            get => _companySenderStreet;
            private set
            {
                _companySenderStreet = value;
                OnPropertyChanged(nameof(CompanySenderStreet));
            }
        }

        private string _companySenderCode_City = string.Empty;
        public string CompanySenderCode_City
        {
            get => _companySenderCode_City;
            private set
            {
                _companySenderCode_City = value;
                OnPropertyChanged(nameof(CompanySenderCode_City));
            }
        }

        private string _companySenderCountry = string.Empty;
        public string CompanySenderCountry
        {
            get => _companySenderCountry;
            private set
            {
                _companySenderCountry = value;
                OnPropertyChanged(nameof(CompanySenderCountry));
            }
        }

        private string _companySenderPhoneNumber = string.Empty;
        public string CompanySenderPhoneNumber
        {
            get => _companySenderPhoneNumber;
            private set
            {
                _companySenderPhoneNumber = value;
                OnPropertyChanged(nameof(CompanySenderPhoneNumber));
            }
        }

        private string _companySenderURL = string.Empty;
        public string CompanySenderURL
        {
            get => _companySenderURL;
            private set
            {
                _companySenderURL = value;
                OnPropertyChanged(nameof(CompanySenderURL));
            }
        }
        #endregion

        #region Price/discount properties

        private string _discount = string.Empty;
        public string Discount
        {
            get => _discount;
            set
            {
                _discount = value;
                OnPropertyChanged(nameof(Discount));

                if (string.IsNullOrEmpty(_discount))
                {
                    AmountToPay = _invoiceManager.GetTotalPrice();
                }
                else if (float.TryParse(_discount, out float discountValue) && discountValue > 0 && discountValue < 100)
                {
                    _invoiceManager.Invoice.Discount = discountValue;
                    AmountToPay = _invoiceManager.Invoice.CalculateDiscount(_invoiceManager.GetTotalPrice());
                }
                else
                {
                    _discount = string.Empty;
                    AmountToPay = _invoiceManager.GetTotalPrice();
                    _validations.ClearErrors(nameof(Discount));
                    _validations.AddError("The Discount is not a valid number. ", nameof(Discount));
                }
            }
        }

        private double _amountToPay;
        public double AmountToPay
        {
            get => _amountToPay;
            private set
            {
                _amountToPay = value;
                OnPropertyChanged(nameof(AmountToPay));
            }
        }

        #endregion


        public InvoiceContentViewModel(INavigationService navigationService, InvoiceManager manager)
        {
            _invoiceManager = manager;
            _validations = new Validations();
            Products = new ObservableCollection<Product>();

            _invoiceManager.InvoiceUpdated += OnInvoiceUpdated;
            _validations.ErrorsChanged += OnErrorChanged;
           
        }


        // Handles the "InvoiceUpdated" event triggered when an invoice is updated.
        private void OnInvoiceUpdated(object sender, Invoice e)
        {
            if (_invoiceManager.Invoice != null)
            {
                // Set the dates to their default values
                InvoiceDate = new DateTime(2020, 1, 1);
                InvoiceDueDate = new DateTime(2029, 1, 1);
                IsContentVisible = true;
                UpdateGUI();
            }
        }


        // Handles the "ErrorsChanged" event triggered when validation errors change.
        private void OnErrorChanged(object sender, DataErrorsChangedEventArgs e)
        {
            if (_validations.HasErrors)
            {
                MessageBox.Show(e.PropertyName, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        // Adds text/data to TextBlocks, DatePicker, and ListView of products.
        private void UpdateGUI()
        {
            Invoice invoice = _invoiceManager.Invoice;
            Company companySender = invoice.CompanySender;
            Company companyReceiver = invoice.CompanyReceiver;

            // View products data
            AddProductsToList();

            // Invoice information
            InvoiceNumber = invoice.InvoiceNumber;
            AmountToPay = _invoiceManager.GetTotalPrice();
            InvoiceDate = invoice.InvoiceDate.ToDateTime(new TimeOnly());
            InvoiceDueDate = invoice.DueDate.ToDateTime(new TimeOnly());

            // company Receiver information
            CompanyReceiverName = companyReceiver.Name;
            CompanyReceiverPerson = invoice.Person;
            CompanyReceiverStreet = companyReceiver.Address.Street;
            CompanyReceiverCode_City = $"{companyReceiver.Address.ZipCode} {companyReceiver.Address.City}";
            CompanyReceiverCountry = companyReceiver.Address.Country;

            // Sender company information
            CompanySenderName = companySender.Name;
            CompanySenderStreet = companySender.Address.Street;
            CompanySenderCountry = companySender.Address.Country;
            CompanySenderCode_City = $"{companySender.Address.ZipCode} {companySender.Address.City}";

            CompanySenderPhoneNumber = companySender.PhoneNumber.ToString() ?? string.Empty;
            CompanySenderURL = companySender.URL ?? string.Empty;
        }


        // Clears the current ListView and repopulates it with products.
        private void AddProductsToList()
        {
            Products.Clear();

            foreach (Product item in _invoiceManager.Invoice.Products)
            {
                Products.Add(item);
            }
        }


        /// <summary>
        ///   Performs cleanup operations before the ViewModel is reclaimed by garbage collection. 
        ///   Unsubscribe to event to prevent memory leaks.
        /// </summary>
        public void Dispose()
        {
            _invoiceManager.InvoiceUpdated -= OnInvoiceUpdated;
            _validations.ErrorsChanged -= OnErrorChanged;

            GC.SuppressFinalize(this);
        }
    }
}
