
namespace AirportApp.src
{
    /// <summary>
    ///   Represents the event arguments for an airplane event.
    /// </summary>
    public class AirplaneEventArgs : EventArgs
    {
        public string Message { get; private set; }
        public string Name { get; private set; }


        /// <summary>
        ///   Initializes a new instance of the AirplaneEventArgs with the specified name and message.
        /// </summary>
        /// <param name="name"> The name of the airplane. </param>
        /// <param name="message"> The message associated with the event </param>
        public AirplaneEventArgs(string name, string message)
        {
            Name = name;
            Message = message;
        }
    }
}
