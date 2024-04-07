
namespace WildlifeTrackerSystem.src
{
    /// <summary>
    ///     An interface which helps us to make rules for the subclasses: Animal, Bird, Fish , mammal and reptile
    /// </summary>
    internal interface IAnimal
    {

        // Property signatures
        public string Name { get; set; }
        public string Id { get; set; }
        public GenderType Gender { get; set; }

        public FoodSchedule GetFoodSchedule();

        public string GetExtraInfo();
    }
}
