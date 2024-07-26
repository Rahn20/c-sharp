
namespace AirportApp.src
{
    /// <summary>
    ///   Represents a control tower managing airplanes.
    /// </summary>
    public class ControlTower : ListManager<Airplane>
    {
        public ListManager<string> DisplayMessage { get; private set; }


        public ControlTower()
        {
            DisplayMessage = new ListManager<string>();
        }


        // Event raised when an airplane's information is updated.
        public event EventHandler<string> AirplaneUpdated;


        #region Methods

        /// <summary>
        ///   Event handler for the AirplaneUpdated event. Handles the display of airplane information.
        /// </summary>
        /// <param name="sender"> The sender of the event. </param>
        /// <param name="e"> The event arguments containing airplane information. </param>
        public void OnDisplayInfo(object sender, AirplaneEventArgs e)
        {
            DisplayMessage.AddToList(e.Message);
            AirplaneUpdated?.Invoke(this, e.Message);

            if (((Airplane)sender).Destination == "Home")
            {
                // Unsubscribe to the landed event when the flight reaches their home (target).
                ((Airplane)sender).Landed -= OnDisplayInfo;
            }
        }


        /// <summary>
        ///   Sends an airplane to the runway.
        /// </summary>
        /// <param name="airPlaneObj"> The airplane object to send to the runway. </param>
        public void SendingToRunway(Airplane airPlaneObj)
        {
            // Subscribe to the publisher (SentToRunway event).
            airPlaneObj.SentToRunway += OnDisplayInfo;

            // Call the method to raise the event.
            airPlaneObj.OnSentToRunway();
        }



        /// <summary>
        ///    Orders takeoff and landing for an airplane. Unsubscribe to the SentToRunway event. 
        ///    And Subscribe to the TakeOff and Landed events. Call the setup timer.
        /// </summary>
        /// <param name="flight"> The airplane to take off. </param>
        /// <exception cref="Exception"></exception>
        public void OrderTakeOff(Airplane flight)
        {
            if (flight.Status == 1) throw new Exception("The selected Airplane is under flight");

            flight.SentToRunway -= OnDisplayInfo;

            // Subscribe to the publisher
            flight.TakeOff += OnDisplayInfo;
            flight.OnTakeOff();
            flight.SetupTimer();

            // Order landing
            flight.TakeOff -= OnDisplayInfo;
            flight.Landed += OnDisplayInfo;
        }


        #endregion
    }
}
