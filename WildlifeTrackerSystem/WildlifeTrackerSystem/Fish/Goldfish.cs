
namespace WildlifeTrackerSystem.Fish
{
    public class Goldfish : Fish
    {
        private GoldfishBreed breed;
        private string tailType = "";        // The goldfish tail type, which can have various tail types (single-tail, double-tail)


        /// <summary>
        ///   Default constructor, sets the fishtype to Goldfish.
        /// </summary>
        public Goldfish() : base() 
        {
            FishType = FishType.Goldfish;
        }


        /// <summary>
        ///   Copy constructor
        /// </summary>
        /// <param name="goldfish"> The Goldfish instance (object) to copy </param>
        public Goldfish(Goldfish goldfish)
        {
            // Set common properties (Animal attributes)
            Category = CategoryType.Fish;
            Name = goldfish.Name;
            Age = goldfish.Age;
            Gender = goldfish.Gender;
            Id = goldfish.Id;

            // Set Fish properties
            Habitat = goldfish.Habitat;
            WaterTemperature = goldfish.WaterTemperature;
            FishType = FishType.Goldfish;

            // Set Goldfish properties
            tailType = goldfish.tailType;
            breed = goldfish.breed;
        }

        /// <summary>
        ///   Copy the common data from a Fish object
        /// </summary>
        /// <param name="other"> The Fish instance to copy </param>
        public Goldfish(Fish other)
        {
            // Animal properties
            Category = CategoryType.Fish;
            Name = other.Name;
            Age = other.Age;
            Gender = other.Gender;
            ImagePath = other.ImagePath;
            Id = other.Id;

            // Fish properties
            Habitat = other.Habitat;
            WaterTemperature = other.WaterTemperature;
            FishType = FishType.Goldfish;
        }

        #region Properties
        public GoldfishBreed Breed
        {
            get { return breed; }
            set { breed = value; }
        }

        public string TailType
        {
            get { return tailType; }
            set {  tailType = value; }
        }
        #endregion


        public override string ToString()
        {
            string output = string.Format("Id: {0}, name: {1}, fishtype: {2}, watertemperature {3}, tailtype: {4}",
                Id, Name, FishType, WaterTemperature, tailType);

            return output;
        }

        /// <summary>
        ///   Gets the data of the Goldfish as a dictionary. (Contains Animal + Fish + Goldfish data)
        /// </summary>
        /// <returns> A dictionary containing the goldfish's data </returns>
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
                { "Habitat", Habitat == "-1" ? "-" : Habitat },
                { "Water temperature", WaterTemperature.ToString() },
                { "Goldfish breed", breed.ToString() },
                { "Tail type", tailType },
            };
        }
    }
}
