
namespace WildlifeTrackerSystem.Reptile
{
    public class Snake : Reptile
    {
        #region Fields
        private int length;         // The length of the snake
        private bool isVenomous;    // Whether the snake is venomous or non-venomous.
        #endregion

        /// <summary>
        ///   Default constructor, sets the reptiletype to snake.
        /// </summary>
        public Snake() : base() 
        {
            ReptileType = ReptileType.Snake;
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

        /// <summary>
        ///    Copy the common data from an reptile object
        /// </summary>
        /// <param name="other"> The reptile instance to copy </param>
        public Snake(Reptile other)
        {
            // Animal properties
            Category = CategoryType.Reptile;
            Name = other.Name;
            Age = other.Age;
            Gender = other.Gender;
            ImagePath = other.ImagePath;
            Id = other.Id;

            // Reptile properties
            ReptileType = ReptileType.Snake;
            Habitat = other.Habitat;
            NumOfLegs = other.NumOfLegs;
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
            return new()
            {
                { "ID", Id },
                { "Name", Name },
                { "Age", Age.ToString() },
                { "Gender", Gender.ToString() },
                { "Category", Category.ToString() },
                { "Reptile type", ReptileType.ToString() },
                { "Habitat", Habitat == "-1" ? "-" : Habitat },
                { "Number of legs", NumOfLegs.ToString() },
                { "Snake length", length.ToString() },
                { "Is venomous", isVenomous ? "yes" : "No" },
            };
        }
    }
}
