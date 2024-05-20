namespace WildlifeTrackerSystem.src.Reptile
{
    /// <summary>
    ///   An abstract class inheriting from Animal
    /// </summary>
    public abstract class Reptile : Animal
    {
        #region Fields
        private ReptileType reptileType;
        private int numOfLegs;              // The number of legs the reptile has.
        private string habitat;            // The natural environment where the reptile is commonly found, (deserts, forests .. )
        #endregion


        public Reptile() : base()
        {
            Category = CategoryType.Reptile;
            habitat = string.Empty;
        }

        #region Properties
        public ReptileType ReptileType
        {
            get { return reptileType; }
            set { reptileType = value; }
        }

        /// <summary>
        ///   Gets or sets the hbitat of the reptile. It is not necessary for the attribute to be filled with data; it can be empty.
        ///   If the habitat is empty, set its value to "-1".
        /// </summary>
        public string Habitat
        {
            get { return habitat; }
            set { habitat = !string.IsNullOrEmpty(value) ? value : "-1"; }
        }

        public int NumOfLegs
        {
            get { return numOfLegs; }
            set { numOfLegs = value; }
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            string output = string.Format("Id: {0}, name: {1}, Number of legs: {2}",
                Id, Name, numOfLegs);

            return output;
        }

        /// <summary>
        ///    Creates a new instance based on the specified reptile type.
        /// </summary>
        /// <param name="type"> The type of reptile to create </param>
        /// <returns> A new object of the specified reptile type </returns>
        /// <exception cref="ArgumentException"></exception>
        public static Reptile CreateReptile(ReptileType type)
        {
            switch (type)
            {
                case ReptileType.Frog: return new Frog();
                case ReptileType.Snake: return new Snake();
                default:
                    throw new ArgumentException("Invalid reptile type", nameof(type));
            }
        }

        /// <summary>
        ///   Gets the data of the reptile as a dictionary. (Contains Animal + reptile data)
        /// </summary>
        /// <returns> A dictionary containing the reptile's data </returns>
        public override Dictionary<string, string> GetAnimalData()
        {
            Dictionary<string, string> keyValuePairs = base.GetAnimalData();

            keyValuePairs.Add("Reptile type", reptileType.ToString());
            keyValuePairs.Add("Habitat", habitat == "-1" ? "-" : habitat);
            keyValuePairs.Add("Number of legs", numOfLegs.ToString());

            return keyValuePairs;
        }

        public override abstract FoodSchedule GetFoodSchedule();

        public override abstract object CopyAnimal();
        #endregion
    }
}
