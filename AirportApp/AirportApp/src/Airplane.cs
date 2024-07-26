using System.Windows.Threading;

namespace AirportApp.src
{
    public class Airplane
    {
        //  Timer to handle timed events
        private DispatcherTimer dispatcherTimer;

        // Boolean to indicate if the airplane can land
        private bool canLand;

        public Airplane() 
        { 
            canLand = false;
            Status = 0;
        }


        #region Properties

        public string FlightID { get; set; }            // The flight identification number
        public string Name { get; set; }                // The name of the airplane
        public string Destination { get; set; }       // The destination of the flight
        public double FlightTime { get; set; }       // The duration of the flight in hours
        public DateTime LocalTime { get; set; }     // The local time of the flight (departure time)
        public int Status { get; set; }           // 0 means arrived/on-land, 1 means in-flight

        #endregion


        #region Methods

        /// <summary>
        ///   Handles the landing procedure for the airplane. If the airplane is allowed to land (status is 1 and "can land" is true).
        ///   Updates the status, sets "can land" to false, logs the landing information and and raises the Landed event.
        /// </summary>
        public void OnLanding()
        {
            if (!canLand || Status == 0) return;

            Status = 0;
            canLand = false;
            string infoMessage = $"{FlightID}, {Name} has landed in {Destination}, {DateTime.Now:HH:mm:ss}";
            Destination = "Home";

            // Create an AirplaneEventArgs (corresponding to e) object with the sender being "this".
            AirplaneEventArgs airplane = new AirplaneEventArgs(Name, infoMessage);

            Landed?.Invoke(this, airplane);  // Raise the event
        }


        /// <summary>
        ///    Handles the taking off procedure for the airplane.
        ///    Logs the taking off information and and raises the TakeOff event.
        /// </summary>
        public void OnTakeOff()
        {
            Status = 1;
            string infoMessage = $"The flight {FlightID}, {Name} is taking off, destination {Destination}, time {DateTime.Now:HH:mm:ss}";

            AirplaneEventArgs airplane = new AirplaneEventArgs(Name, infoMessage);
            TakeOff?.Invoke(this, airplane);
        }


        /// <summary>
        ///    Handles the sending to runway procedure for the airplane.
        ///    Logs the sent to runway information and and raises the SentToRunway event.
        /// </summary>
        public void OnSentToRunway()
        {
            string infoMessage = $"{FlightID}, {Name} heading for {Destination} sent to runway!, {LocalTime:HH:mm:ss}";

            AirplaneEventArgs args = new AirplaneEventArgs(Name, infoMessage);
            SentToRunway?.Invoke(this, args);
        }


        /// <summary>
        ///   Event handler for the timer tick event.
        ///   Sets CanLand to true, stops the timer, and triggers the OnLanding event.
        /// </summary>
        /// <param name="sender"> The source of the event </param>
        /// <param name="e"> The event data </param>
        private void DispatcherTimer_tick(object sender, EventArgs e)
        {
            canLand = true;     // Allow landing
            StopTimer();        // Stop the timer
            OnLanding();        // Handle the landing event
        }


        /// <summary>
        ///   Sets up and starts a dispatcher timer with a specified interval of seconds.
        /// </summary>
        public void SetupTimer()
        {
            int timeInSec = (int)FlightTime;
            dispatcherTimer = new DispatcherTimer();

            // When the specified interval passes, the DispatcherTimer_tick method will be called.
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, timeInSec);
            dispatcherTimer.Start();
        }


        /// <summary>
        ///  Stops the dispatcher timer.
        /// </summary>
        private void StopTimer() => dispatcherTimer.Stop();


        public override string ToString()
        {
            return $"{FlightID}, {Name} heading for {Destination}, flight time {FlightTime} h";
        }


        /// <summary>
        ///  Copies the Airplane object, used to prevent unintended modifications to the original objects.
        /// </summary>
        /// <returns>  Airplane object with all data </returns>
        public Airplane CopyAirplane()
        {
            return new Airplane
            {
                Name = Name,
                FlightID = FlightID,
                Destination = Destination,
                FlightTime = FlightTime,
                LocalTime = LocalTime,
                Status = Status,
                canLand = canLand,
            };
        }

        #endregion


        #region Events

        // Declare the Landed event
        public event EventHandler<AirplaneEventArgs> Landed;

        // Declare TakeOff event
        public event EventHandler<AirplaneEventArgs> TakeOff;

        // Declare HeadingFor event
        public event EventHandler<AirplaneEventArgs> SentToRunway;

        #endregion
    }
}
