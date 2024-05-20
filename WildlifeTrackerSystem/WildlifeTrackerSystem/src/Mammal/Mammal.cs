namespace WildlifeTrackerSystem.src.Mammal
{
    /// <summary>
    ///   An abstract class inheriting from Animal
    /// </summary>
    public abstract class Mammal : Animal
    {
        #region Fields
        private MammalType mammalType;
        private int lifespan;                   // Represents the average lifespan of the mammal.
        private int numOfTeeth;                 // The number of teeth of the mammal.
        private float weight;                   // The weight of the mammal in kg
        private float speed;                    // The speed of the mammal in km/h
        private float height;                   // The height of the mammal in cm
        private string color = "";              // Represents the color of fur or hair on the mammal's body.
        #endregion


        public Mammal() : base()
        {
            Category = CategoryType.Mammal;
        }


        #region Properties
        public MammalType MammalType
        {
            get { return mammalType; }
            set { mammalType = value; }
        }

        /// <summary>
        ///   Gets or sets the color of the mammal. It is not necessary for the attribute to be filled with data; it can be empty.
        ///   If the color is empty, set its value to "-1".
        /// </summary>
        public string Color
        {
            get { return color; }
            set { color = !string.IsNullOrEmpty(value) ? value : "-1"; }
        }

        public int Lifespan
        {
            get { return lifespan; }
            set { lifespan = value; }
        }

        public int NumOfTeeth
        {
            get { return numOfTeeth; }
            set { numOfTeeth = value; }
        }

        public float Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        public float Height
        {
            get { return height; }
            set { height = value; }
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            string output = string.Format("Id: {0}, name: {1}, number of teeth: {2}, height: {3}, weight: {4}",
                Id, Name, numOfTeeth, height, weight);

            return output;
        }

        /// <summary>
        ///   Creates a new instance based on the specified mammal type.
        /// </summary>
        /// <param name="type"> The type of mammal to create </param>
        /// <returns> A new object of the specified mammal type </returns>
        /// <exception cref="ArgumentException"></exception>
        public static Mammal CreateMammal(MammalType type)
        {
            switch (type)
            {
                case MammalType.Dog: return new Dog();
                case MammalType.Wolf: return new Wolf();
                default:
                    throw new ArgumentException("Invalid mammal type", nameof(type));
            }
        }

        /// <summary>
        ///   Gets the data of the Mammal as a dictionary. (Contains Animal + mammal data)
        /// </summary>
        /// <returns> A dictionary containing the mammal's data </returns>
        public override Dictionary<string, string> GetAnimalData()
        {
            Dictionary<string, string> keyValuePairs = base.GetAnimalData();

            keyValuePairs.Add("Mammal type", mammalType.ToString());
            keyValuePairs.Add("Color", color == "-1" ? "-" : color);
            keyValuePairs.Add("Lifespan", lifespan.ToString());
            keyValuePairs.Add("Number of teeth", numOfTeeth.ToString());
            keyValuePairs.Add("Weight", weight.ToString());
            keyValuePairs.Add("Height", height.ToString());
            keyValuePairs.Add("Speed", speed.ToString());

            return keyValuePairs;
        }

        public override abstract FoodSchedule GetFoodSchedule();

        public override abstract object CopyAnimal();

        #endregion
    }
}
