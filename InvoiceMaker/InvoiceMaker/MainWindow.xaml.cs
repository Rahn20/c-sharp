using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using InvoiceMaker.src;

namespace InvoiceMaker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private InvoiceManager manager;

        // True if the user selected a text file to view in the window; otherwise, false
        private bool fileSelected; 

        public MainWindow()
        {
            InitializeComponent();

            manager = new InvoiceManager();
            fileSelected = false;
        }


        // "Open" submenu opens a text file according to the user’s option.
        private void MnuFileOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Title = "Select a text file",
                DefaultExt = ".txt",
                Filter = "Text Files (.txt)|*.txt;",
            };


            if (ofd.ShowDialog() is true)
            {
                try
                {
                    fileSelected = true;
                    manager.MakeInvoice(ofd.FileName);
                    UpdateGUI();
                    
                }
                catch (Exception err)
                {
                    MessageBox.Show($"Error opening file: {err.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        /// <summary>
        ///   Adds text/data to TextBlocks, DatePicker, and ListView of products
        /// </summary>
        private void UpdateGUI()
        {
            Invoice invoice = manager.Invoice;
            Company companySender = invoice.CompanySender;
            Company companyReceiver = invoice.CompanyReceiver;

            // View products data
            LstViewProducts.ItemsSource = invoice.Products;

            // Invoice information
            TxtInvoiceNumber.Text = invoice.InvoiceNumber;
            BoxDiscount.Text = "0.0";
            BoxTotal.Text = manager.GetTotalPrice().ToString();
            InvoiceDate.SelectedDate = invoice.InvoiceDate.ToDateTime(TimeOnly.Parse("00:00"));
            InvoiceDueDate.SelectedDate = invoice.DueDate.ToDateTime(TimeOnly.Parse("00:00"));
    
            // company Receiver information
            TxtCompanyReceiverName.Text = companyReceiver.Name;
            TxtCompanyReceiverPerson.Text = invoice.Person;
            TxtCompanyReceiverStreet.Text = companyReceiver.Address.Street;
            TxtCompanyReceiverCodeCity.Text = $"{companyReceiver.Address.ZipCode} {companyReceiver.Address.City}";
            TxtCompanyReceiverCountry.Text = companyReceiver.Address.Country;

            // Sender company information
            TxtCompanySenderName.Text = companySender.Name;
            TxtCompanySenderStreet.Text = companySender.Address.Street;
            TxtCompanySenderCountry.Text = companySender.Address.Country;
            TxtCompanySenderCodeCity.Text = $"{companySender.Address.ZipCode} {companySender.Address.City}";

            TxtPhoneNumber.Text = companySender.PhoneNumber.ToString();
            TxtURL.Text = companySender.URL;
        }


        // "Save As Image" submenu saves the invoice (Window) to PNG.
        private void MnuFileSaveAsImage_Click(object sender, RoutedEventArgs e)
        {
            if (fileSelected is false) return;

            // Define the size of the RenderTargetBitmap
            int width = (int)this.ActualWidth;  // This window
            int height = (int)this.ActualHeight;

            // Create the RenderTargetBitmap
            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);

            // Render this window
            renderTargetBitmap.Render(this);

            // Create a PNG encoder
            PngBitmapEncoder pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

            // Let the user choose the output file path
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PNG Files (*.png)|*.png",
                DefaultExt = "png",
                FileName = "Invoice.png"
            };


            if (saveFileDialog.ShowDialog() == true)
            {
                // Save the rendered bitmap to the selected file
                using (FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    pngEncoder.Save(fileStream);
                }
            }
        }

        // "Exit" submenu, closes the application.
        private void MnuFileExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        // Event handler for discount calculation. Displays the discounted price in the textbox.
        private void BoxDiscount_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool discount = float.TryParse(BoxDiscount.Text, out float discountValue);

            if (fileSelected && discount)
            {
                manager.Invoice.Discount = discountValue;

                double calculation = manager.Invoice.CalculateDiscount(manager.GetTotalPrice());
                BoxTotal.Text = calculation.ToString();
            }
        }


        // Event handler for the invoice start date. Changes the date based on the user's option
        private void InvoiceDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (fileSelected is false) return;

            DateTime selectedDate = InvoiceDate.SelectedDate ?? DateTime.Now;
            manager.Invoice.InvoiceDate = new DateOnly(selectedDate.Year, selectedDate.Month, selectedDate.Day);  
        }


        // Event handler for the invoice due date. Changes the date based on the user's option
        private void InvoiceDueDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (fileSelected is false) return;

            DateTime selectedDate = InvoiceDueDate.SelectedDate ?? DateTime.Now; // Default to today's date
            manager.Invoice.DueDate = new DateOnly(selectedDate.Year, selectedDate.Month, selectedDate.Day);
        }
    }
}