
namespace WildlifeTrackerSystem.src.Bird
{
    /// <summary>
    ///   An abstract class inheriting from Animal
    /// </summary>
    public abstract class Bird : Animal
    {
        #region Fields
        private BirdType birdType;
        private float wingspan;         // The length of the bird's wings when fully extended, in cm.
        private string color = "";      // The color of the bird
        #endregion


        /// <summary>
        ///   Default constructor, sets the categorytype to Bird.
        /// </summary>
        public Bird() : base()
        {
            Category = CategoryType.Bird;
        }


        #region Properties
        public BirdType BirdType
        {
            get { return birdType; }
            set { birdType = value; }
        }

        public float Wingspan
        {
            get { return wingspan; }
            set { wingspan = value; }
        }

        public string Color
        {
            get { return color; }
            set { color = value; }
        }
        #endregion


        #region Methods
        public override string ToString()
        {
            string output = string.Format("Id: {0}, name: {1}, birdType: {2}, wingspan: {3}, bird color: {4}",
                Id, Name, birdType, wingspan, color);

            return output;
        }

        /// <summary>
        ///   Creates a new instance based on the specified bird type.
        /// </summary>
        /// <param name="type"> The type of bird to create </param>
        /// <returns> A new object of the specified bird type </returns>
        /// <exception cref="ArgumentException"></exception>
        public static Bird CreateBird(BirdType type)
        {
            switch (type)
            {
                case BirdType.Dove: return new Dove();
                case BirdType.Eagle: return new Eagle();
                default:
                    throw new ArgumentException("Invalid birdtype", nameof(type));
            }
        }

        /// <summary>
        ///   Gets the data of the Bird as a dictionary. (Contains Animal + Bird data)
        /// </summary>
        /// <returns> A dictionary containing the bird's data </returns>
        public override Dictionary<string, string> GetAnimalData()
        {
            // Call the base implementation to get the dictionary
            Dictionary<string, string> keyValuePairs = base.GetAnimalData();

            keyValuePairs.Add("Bird type", birdType.ToString());
            keyValuePairs.Add("Wingspan", wingspan.ToString());
            keyValuePairs.Add("Color", color);

            return keyValuePairs;
        }


        /// <summary>
        ///   Gets the animal information as a string with values.
        /// </summary>
        /// <returns> A string containing the animal's data </returns>
        public override string GetExtraInfo()
        {
            string birdInfo = base.GetExtraInfo();

            birdInfo += string.Format("{0, -15} {1, 10}\n", "Bird type:", birdType);
            birdInfo += string.Format("{0, -15} {1, 10}\n", "Wingspan:", wingspan);
            birdInfo += string.Format("{0, -15} {1, 10}\n", "Color:", color);
            return birdInfo;
        }

        public override abstract FoodSchedule GetFoodSchedule();

        #endregion
    }
}