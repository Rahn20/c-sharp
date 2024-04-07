namespace WildlifeTrackerSystem.src.Reptile
{
    public class Snake : Reptile
    {
        #region Fields
        private int length;         // The length of the snake
        private bool isVenomous;    // Whether the snake is venomous or non-venomous.

        private FoodSchedule foodSchedule = new FoodSchedule();
        #endregion

        /// <summary>
        ///   Default constructor, sets the reptiletype to snake.
        /// </summary>
        public Snake() : base()
        {
            ReptileType = ReptileType.Snake;
            SetFoodSchedule();
        }

        /// <summary>
        ///   Copy constructor
        /// </summary>
        /// <param name="snake"> The snake instance (object) to copy </param>
        public Snake(Snake snake)
        {
            // Set common properties (Animal attributes)
            Category = CategoryType.Reptile;
            Name = snake.Name;
            Age = snake.Age;
            Gender = snake.Gender;
            ImagePath = snake.ImagePath;
            Id = snake.Id;

            // Set reptile properties
            ReptileType = ReptileType.Snake;
            Habitat = snake.Habitat;
            NumOfLegs = snake.NumOfLegs;

            // Set snake properties
            length = snake.length;
            isVenomous = snake.isVenomous;
        }


        #region Properties
        public int Length
        {
            get { return length; }
            set { length = value; }
        }
        public bool IsVenomous
        {
            get { return isVenomous; }
            set { isVenomous = value; }
        }

        #endregion

        public override string ToString()
        {
            string output = string.Format("Id: {0}, name: {1}, Number of legs: {2}, snake length: {3}",
                Id, Name, NumOfLegs, length);

            return output;
        }


        /// <summary>
        ///   Gets the data of the snake as a dictionary. (Contains Animal + reptile + snake data)
        /// </summary>
        /// <returns> A dictionary containing the snake's data </returns>
        public override Dictionary<string, string> GetAnimalData()
        {
            Dictionary<string, string> keyValuePairs = base.GetAnimalData();

            keyValuePairs.Add("Snake length", length.ToString());
            keyValuePairs.Add("Is venomous", isVenomous ? "yes" : "No");

            return keyValuePairs;
        }

        /// <summary>
        ///   Gets the animal information as a string with values.
        /// </summary>
        /// <returns> A string containing the animal's data </returns>
        public override string GetExtraInfo()
        {
            string info = base.GetExtraInfo();

            info += string.Format("{0, -15} {1, 10}\n", "Snake length:", length);
            info += string.Format("{0, -15} {1, 10}\n", "Is venomous:", isVenomous ? "yes" : "No");
            return info;
        }


        /// <summary>
        ///   Sets the food schedule for the animal.
        /// </summary>
        private void SetFoodSchedule()
        {
            foodSchedule.EaterType = EaterType.Carnivore;
            foodSchedule.AddToFoodList("Morning: rodents and birds");
            foodSchedule.AddToFoodList("Lunch: frogs, toads and lizards");
            foodSchedule.AddToFoodList("Evening: small snakes");
        }


        /// <summary>
        ///   Retrieves the foodschedule for the animal.
        /// </summary>
        /// <returns> The food schedule for the animal. </returns>
        public override FoodSchedule GetFoodSchedule()
        {
            return foodSchedule;
        }
    }
}
