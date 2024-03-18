
namespace WildlifeTrackerSystem.Reptile
{
    public class Frog : Reptile
    {
        private string diet = "";   // The diet of the frog, example: Most frogs eat insects and spiders.


        /// <summary>
        ///   Default constructor, sets the reptiletype to frog.
        /// </summary>
        public Frog() : base()
        {
            ReptileType = ReptileType.Frog;
        }

        /// <summary>
        ///   Copy constructor
        /// </summary>
        /// <param name="frog"> The frog instance (object) to copy </param>
        public Frog(Frog frog)
        {
            // Set common properties (Animal attributes)
            Category = CategoryType.Reptile;
            Name = frog.Name;
            Age = frog.Age;
            Gender = frog.Gender;
            ImagePath = frog.ImagePath;
            Id = frog.Id;

            // Set Reptile properties
            ReptileType = ReptileType.Frog;
            Habitat = frog.Habitat;
            NumOfLegs = frog.NumOfLegs;

            // Set frog properties
            diet = frog.diet;  
        }

        /// <summary>
        ///    Copy the common data from an reptile object
        /// </summary>
        /// <param name="other"> The reptile instance to copy </param>
        public Frog(Reptile other)
        {
            // Animal properties
            Category = CategoryType.Reptile;
            Name = other.Name;
            Age = other.Age;
            Gender = other.Gender;
            ImagePath = other.ImagePath;
            Id = other.Id;

            // reptile properties
            ReptileType = ReptileType.Frog;
            Habitat = other.Habitat;
            NumOfLegs = other.NumOfLegs;
        }

        public string Diet
        {
            get { return diet; }
            set { diet = value; }
        }

        public override string ToString()
        {
            string output = string.Format("Id: {0}, name: {1}, Number of legs: {2}, frog diet: {3}",
                Id, Name, NumOfLegs, diet);

            return output;
        }


        /// <summary>
        ///   Gets the data of the frog as a dictionary. (Contains Animal + reptile + frog data)
        /// </summary>
        /// <returns> A dictionary containing the frog's data </returns>
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
                { "Frog diet", diet },
            };
        }
    }
}
