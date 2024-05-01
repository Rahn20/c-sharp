
namespace WildlifeTrackerSystem.src
{
    /// <summary>
    ///     An interface which helps us to make rules for the subclasses: Animal, Bird, Fish , mammal and reptile
    /// </summary>
    public interface IAnimal
    {

        // Property signatures

        /// <summary>
        ///   The animal name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        ///   The animal ID
        /// </summary>
        string Id { get; set; }

        /// <summary>
        ///   the animal gender
        /// </summary>
        GenderType Gender { get; set; }

        /// <summary>
        ///   Gets the food schedule for a specific animal species
        /// </summary>
        /// <returns> An object of the FoodSchedule assigned to the particular object </returns>
        FoodSchedule GetFoodSchedule();


        /// <summary>
        ///   Gets the animal information as a string with values
        /// </summary>
        /// <returns> A string with animal data </returns>
        string GetExtraInfo();


        /// <summary>
        ///   Gets the data of the animal as a dictionary. This method can be overridden in a derived class.
        /// </summary>
        /// <returns> A dictionary containing the animal's data  </returns>
        Dictionary<string, string> GetAnimalData();
    }
}
