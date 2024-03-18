
namespace WildlifeTrackerSystem.Reptile
{
    public class Reptile : Animal
    {
        #region Fields
        private ReptileType reptileType;
        private int numOfLegs;                  // The number of legs the reptile has.
        private string habitat = "";            // The natural environment where the reptile is commonly found, (deserts, forests .. )
        #endregion


        /// <summary>
        ///   Default constructor, sets the categorytype to Reptile.
        /// </summary>
        public Reptile() : base() 
        { 
            Category = CategoryType.Reptile;
        }


        /// <summary>
        ///    Copy constructor
        /// </summary>
        /// <param name="reptile"> The reptile instance (object) to copy </param>
        public Reptile(Reptile reptile)
        {
            // Set common properties (Animal attributes)
            Category = CategoryType.Reptile;
            Name = reptile.Name;
            Age = reptile.Age;
            Gender = reptile.Gender;
            ImagePath = reptile.ImagePath;
            Id = reptile.Id;

            // Set Reptile properties
            reptileType = reptile.reptileType;
            habitat = reptile.habitat;
            numOfLegs = reptile.numOfLegs;
        }


        /// <summary>
        ///  Copy the common data from an Animal object
        /// </summary>
        /// <param name="animal"> The Animal instance to copy </param>
        public Reptile(Animal animal)
        {
            Category = CategoryType.Reptile;
            Name = animal.Name;
            Age = animal.Age;
            Gender = animal.Gender;
            ImagePath = animal.ImagePath;
            Id = animal.Id;
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
            set { habitat = (!string.IsNullOrEmpty(value) ? value : "-1"); }
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
        /// <param name="reptile"> The reptile object </param>
        /// <returns>  A new object of the specified reptile type </returns>
        /// <exception cref="ArgumentException"></exception>
        public static Reptile CreateReptile(ReptileType type, Reptile reptile)
        {
            switch (type)
            {
                case ReptileType.Frog: return new Frog(reptile);
                case ReptileType.Snake: return new Snake(reptile);
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
            return new()
            {
                { "ID", Id },
                { "Name", Name },
                { "Age", Age.ToString() },
                { "Gender", Gender.ToString() },
                { "Category", Category.ToString() },
                { "Reptile type", reptileType.ToString() },
                { "Habitat", habitat == "-1" ? "-" : habitat },
                { "Number of legs", numOfLegs.ToString() },
            };
        }
        #endregion
    }
}
