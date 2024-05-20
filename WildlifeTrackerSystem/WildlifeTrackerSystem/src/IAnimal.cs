namespace WildlifeTrackerSystem.src
{
    /// <summary>
    ///     An interface which helps us to make rules for the subclasses: Animal, Bird, Fish , mammal and reptile
    /// </summary>
    public interface IAnimal
    {
        /// <summary>
        ///   The animal name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        ///   The animal ID
        /// </summary>
        string Id { get; set; }

        /// <summary>
        ///   The animal age
        /// </summary>
        int Age { get; set; }


        /// <summary>
        ///   The animal category type
        /// </summary>
        CategoryType Category { get; set; }

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
        ///   Copies the animal object, used to prevent unintended modifications to the original objects stored in the list.
        /// </summary>
        /// <returns> Animal object </returns>
        object CopyAnimal();

        /// <summary>
        ///   Gets the data of the animal as a dictionary. This method can be overridden in a derived class.
        /// </summary>
        /// <returns> A dictionary containing the animal's data  </returns>
        Dictionary<string, string> GetAnimalData();
    }
}
