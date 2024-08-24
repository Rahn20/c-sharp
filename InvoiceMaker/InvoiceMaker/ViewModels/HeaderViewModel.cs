using System;
using System.Linq;
using System.Windows.Input;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Media;
using System.Windows.Controls;

using InvoiceMaker.Services;
using InvoiceMaker.Views;
using InvoiceMaker.Core;

namespace InvoiceMaker.ViewModels
{
    public class HeaderViewModel : ViewModelBase
    {
        private readonly InvoiceManager _invoiceManager;
        private readonly InvoiceContentViewModel _contentViewModel;

        // True if the user selected a text file to view in the window; otherwise, false.
        private bool _fileSelected;


        public ICommand OpenInvoiceCommand { get; }
        public ICommand SaveAsImageCommand { get; }
        public ICommand ExitCommand { get; }


        public HeaderViewModel(INavigationService navigationService, InvoiceManager manager, InvoiceContentViewModel contentViewModel) 
        {
            _invoiceManager = manager;
            _fileSelected = false;
            _contentViewModel = contentViewModel;

            OpenInvoiceCommand = new RelayCommand(execute => OpenInvoice());
            SaveAsImageCommand = new RelayCommand(execute => SaveAsImage());
            ExitCommand = new RelayCommand(execute => Exit());
        }


        // "Open" submenu opens a text file according to the user’s option.
        private void OpenInvoice() 
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
                    _invoiceManager.MakeInvoice(ofd.FileName);
                    _fileSelected = true;
                }
                catch (Exception err)
                {
                    MessageBox.Show($"Error opening file: {err.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        // "Save As Image" submenu saves the invoice (UserControl) to PNG.
        private void SaveAsImage()
        {
            if (_fileSelected is false) return;

            UIElement view = new InvoiceContentView { DataContext = _contentViewModel };

            // Measure and arrange the InvoiceContentView
            view.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            view.Arrange(new Rect(new Size(view.DesiredSize.Width, view.DesiredSize.Height)));
            view.UpdateLayout();

            // Use a container with size slightly larger than the content to ensure no clipping
            var container = new Grid
            {
                Width = view.DesiredSize.Width,
                Height = view.DesiredSize.Height
            };
            container.Children.Add(view);

            // Measure and arrange the container
            container.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            container.Arrange(new Rect(new Size(container.DesiredSize.Width, container.DesiredSize.Height)));
            container.UpdateLayout();

            // Get the actual width and height
            int width = (int)container.ActualWidth;
            int height = (int)container.ActualHeight;

            // Create the RenderTargetBitmap
            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
            renderTargetBitmap.Render(view);

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


        // Closes the application.
        private void Exit()
        {
            Application.Current.MainWindow.Close();
        }
    }
}
