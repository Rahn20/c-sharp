using System;
using System.Windows;

using AirportApp.src;

namespace AirportApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ControlTower tower;

        public MainWindow()
        {
            InitializeComponent();

            tower = new ControlTower();

            // Subscribe to the OnAirplaneUpdated event.
            tower.AirplaneUpdated += OnAirplaneUpdated;
        }


        /// <summary>
        ///   Event handler for updating airplane information.
        /// </summary>
        /// <param name="sender"> The source of the event. </param>
        /// <param name="e"> The updated airplane message </param>
        private void OnAirplaneUpdated(object sender, string e)
        {
            LstDisplayMsg.Items.Add(e);
        }


        // Event handler for Add flight button.
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            // True if the inputs is not empty.
            bool validateDestination = !string.IsNullOrEmpty(BoxDestination.Text);
            bool validateName = !string.IsNullOrEmpty(BoxName.Text);
            bool validateID = !string.IsNullOrEmpty(BoxFlightID.Text);
            bool validateTime = double.TryParse(BoxFlightTime.Text, out double timeValue);

            if (validateDestination && validateID && validateName && validateTime)
            {
                Airplane flight = new Airplane
                {
                    Name = BoxName.Text,
                    FlightID = BoxFlightID.Text,
                    FlightTime = timeValue,
                    Destination = BoxDestination.Text,
                    LocalTime = DateTime.Now,
                };

                tower.AddToList(flight);
                LstAirplane.Items.Add(flight.ToString());
                tower.SendingToRunway(flight);

                UpdateGUI();
            }
            else
            {
                MessageBox.Show("Name, flightID, destination and flightTime cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }



        /// <summary>
        ///   Clears Destination, Name,FlightID and FlightTime boxes.
        /// </summary>
        private void UpdateGUI()
        {
            BoxDestination.Clear();
            BoxName.Clear();
            BoxFlightID.Clear();
            BoxFlightTime.Clear();
        }


        // Event handler for taking off button.
        private void BtnTakeOff_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = LstAirplane.SelectedIndex;

            if (selectedIndex != -1)
            {

                try
                {
                    // Gets an Airplane copied object.
                    Airplane? getFlightObj = tower.GetAt(selectedIndex);

                    if (getFlightObj == null) return;

                    // If the airplane has reached the target, display a new form for the user to change the destination and flight time.
                    if (getFlightObj.Destination == "Home")
                    {
                        ChangeDestinationForm destinationFrm = new ChangeDestinationForm(getFlightObj);

                        if (destinationFrm.ShowDialog() == true)
                        {
                            // Get the selected item
                            string selectedAirplane = (string)LstAirplane.Items[selectedIndex];

                            // Remove the old item
                            LstAirplane.Items.RemoveAt(selectedIndex);

                            // Insert the updated item at the same index
                            LstAirplane.Items.Insert(selectedIndex, getFlightObj.ToString());
                            tower.OrderTakeOff(getFlightObj);
                        }
                    }
                    else
                    {
                        tower.OrderTakeOff(getFlightObj);  
                    }

                    // Replaces the copied object with the actual object
                    tower.ChangeAt(selectedIndex, getFlightObj);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Select a flight to take off.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}