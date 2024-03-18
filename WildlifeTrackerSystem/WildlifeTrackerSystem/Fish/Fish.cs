
namespace WildlifeTrackerSystem.Fish
{
    public class Fish : Animal
    {
        #region Fields
        private FishType fishType;
        private float waterTemperature;         // The water temperature for the fish in Celsius
        private string habitat = "";            // The environment where the fish is commonly found (saltwater, freshwater , ..)
        #endregion


        /// <summary>
        ///   Default constructor, sets the categorytype to Fish.
        /// </summary>
        public Fish() : base() 
        {
            Category = CategoryType.Fish;
        }

        /// <summary>
        ///   Copy constructor
        /// </summary>
        /// <param name="fish"> The fish instance (object) to copy </param>
        public Fish(Fish fish)
        {
            // Set common properties (Animal attributes)
            Category = CategoryType.Fish;
            Name = fish.Name;
            Age = fish.Age;
            Gender = fish.Gender;
            ImagePath = fish.ImagePath;
            Id = fish.Id;

            // Set Fish properties
            habitat = fish.habitat; 
            waterTemperature = fish.waterTemperature;
            fishType = fish.fishType;
        }

        /// <summary>
        ///  Copy the common data from an Animal object
        /// </summary>
        /// <param name="animal"> The Animal instance to copy </param>
        public Fish(Animal animal)
        {
            Category = CategoryType.Fish;
            Name = animal.Name;
            Age = animal.Age;
            Gender = animal.Gender;
            ImagePath = animal.ImagePath;
            Id = animal.Id;
        }

        #region Properties
        public FishType FishType
        {
            get { return fishType; }
            set { fishType = value; }
        }

        /// <summary>
        ///   Gets or sets the habitat of the fish. It is not necessary for the attribute to be filled with data; it can be empty.
        ///   If the habitat is empty, set its value to "-1".
        /// </summary>
        public string Habitat
        {
            get { return habitat; }
            set { habitat = (!string.IsNullOrEmpty(value) ? value : "-1"); }
        }

        public float WaterTemperature
        {
            get { return waterTemperature; }
            set { waterTemperature = value; }
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            string output = string.Format("Id: {0}, name: {1}, Fishtype: {2}, watertemperature {3}",
                Id, Name, fishType, waterTemperature);

            return output;
        }

        /// <summary>
        ///    Creates a new instance based on the specified fish type.
        /// </summary>
        /// <param name="type"> The type of Fish to create </param>
        /// <param name="fish"> The fish object  </param>
        /// <returns> A new object of the specified fish type </returns>
        /// <exception cref="ArgumentException"></exception>
        public static Fish CreateFish(FishType type, Fish fish)
        {
            switch (type)
            {
                case FishType.Goldfish: return new Goldfish(fish);
                case FishType.Shark: return new Shark(fish);
                default:
                    throw new ArgumentException("Invalid fish type", nameof(type));
            }
        }


        /// <summary>
        ///   Gets the data of the Fish as a dictionary. (Contains Animal + fish data)
        /// </summary>
        /// <returns> A dictionary containing the fish's data </returns>
        public override Dictionary<string, string> GetAnimalData()
        {
            return new()
            {
                { "ID", Id },
                { "Name", Name },
                { "Age", Age.ToString() },
                { "Gender", Gender.ToString() },
                { "Category", Category.ToString() },
                { "Fish type", FishType.ToString() },
                { "Habitat", habitat == "-1" ? "-" : habitat },
                { "Water temperature", waterTemperature.ToString() },
            };
        }
        #endregion
    }
}
