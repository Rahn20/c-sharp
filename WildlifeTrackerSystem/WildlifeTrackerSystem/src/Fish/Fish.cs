
namespace WildlifeTrackerSystem.src.Fish
{
    public abstract class Fish : Animal
    {
        #region Fields
        private FishType fishType;
        private float waterTemperature;         // The water temperature for the fish in Celsius
        private string habitat = "";            // The environment where the fish is commonly found (saltwater, freshwater , ..)
        #endregion


        public Fish() : base()
        {
            Category = CategoryType.Fish;
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
            set { habitat = !string.IsNullOrEmpty(value) ? value : "-1"; }
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
        /// <returns> A new object of the specified fish type </returns>
        /// <exception cref="ArgumentException"></exception>
        public static Fish CreateFish(FishType type)
        {
            switch (type)
            {
                case FishType.Goldfish: return new Goldfish();
                case FishType.Shark: return new Shark();
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
            Dictionary<string, string> keyValuePairs = base.GetAnimalData();

            keyValuePairs.Add("Fish type", fishType.ToString());
            keyValuePairs.Add("Habitat", habitat == "-1" ? "-" : habitat);
            keyValuePairs.Add("Water temperature", waterTemperature.ToString());

            return keyValuePairs;
        }


        /// <summary>
        ///   Gets the animal information as a string with values.
        /// </summary>
        /// <returns> A string containing the animal's data </returns>
        public override string GetExtraInfo()
        {
            string fishInfo = base.GetExtraInfo();
            fishInfo += string.Format("{0, -15} {1, 10}\n", "Fish type:", fishType);
            fishInfo += string.Format("{0, -15} {1, 10}\n", "Habitat:", habitat);
            fishInfo += string.Format("{0, -15} {1, 10}\n", "Water temperature:", waterTemperature);
            return fishInfo;
        }

        public override abstract FoodSchedule GetFoodSchedule();

        public override abstract object CopyAnimal();
        #endregion
    }
}
