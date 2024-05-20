namespace WildlifeTrackerSystem.src.Reptile
{
    /// <summary>
    ///   A concrete class inheriting from Reptile
    /// </summary>
    public class Snake : Reptile
    {
        #region Fields
        private int length;         // The length of the snake
        private bool isVenomous;    // Whether the snake is venomous or non-venomous.

        private FoodSchedule foodSchedule = new FoodSchedule();
        #endregion

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
        ///   Sets the food schedule for the Snake.
        /// </summary>
        private void SetFoodSchedule()
        {
            foodSchedule.EaterType = EaterType.Carnivore;
            foodSchedule.Food.AddToList("Morning: rodents and birds");
            foodSchedule.Food.AddToList("Lunch: frogs, toads and lizards");
            foodSchedule.Food.AddToList("Evening: small snakes");
        }


        /// <summary>
        ///   Retrieves the foodschedule for the animal.
        /// </summary>
        /// <returns> The food schedule for the animal. </returns>
        public override FoodSchedule GetFoodSchedule()
        {
            return foodSchedule;
        }


        /// <summary>
        ///   Copies the animal object, used to prevent unintended modifications to the original objects.
        /// </summary>
        /// <returns> Snake object with all data </returns>
        public override object CopyAnimal()
        {
            return new Snake
            {
                Category = Category,
                Name = Name,
                Age = Age,
                Gender = Gender,
                ImagePath = ImagePath,
                Id = Id,
                ReptileType = ReptileType,
                Habitat = Habitat,
                NumOfLegs = NumOfLegs,
                Length = length,
                IsVenomous = isVenomous,
            };
        }
    }
}
