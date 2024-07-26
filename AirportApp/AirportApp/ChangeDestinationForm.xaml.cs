using System.Windows;

using AirportApp.src;

namespace AirportApp
{
    /// <summary>
    /// Interaction logic for ChangeDestinationForm.xaml
    /// </summary>
    public partial class ChangeDestinationForm : Window
    {
        private Airplane flightObj;

        public ChangeDestinationForm(Airplane flightObj)
        {

            this.flightObj = flightObj;
            InitializeComponent();

            // Fill the inputs fields
            BoxFlightID.Text = flightObj.FlightID;
            BoxName.Text = flightObj.Name;
        }


        // Event handler for taking off igen button.
        private void BtnTakeOff_Click(object sender, RoutedEventArgs e)
        {
            // True if the input is not empty. The Name field can be empty
            bool validateDestination = !string.IsNullOrEmpty(BoxDestination.Text);
            bool validateTime = double.TryParse(BoxFlightTime.Text, out double timeValue);

            if (validateDestination && validateTime)
            {
                flightObj.Destination = BoxDestination.Text;
                flightObj.FlightTime = timeValue;

                this.DialogResult = true; // Set DialogResult to true
                this.Close(); // Close the dialog
            }
            else
            {
                MessageBox.Show("Destination and flightTime cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
