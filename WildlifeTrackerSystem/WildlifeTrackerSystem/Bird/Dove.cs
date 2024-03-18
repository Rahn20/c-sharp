
namespace WildlifeTrackerSystem.Bird
{
    public class Dove : Bird
    {
        private string noiseLevel = "";     // 3 levels, high, medium and low.


        /// <summary>
        ///   Default constructor, sets the birdtype to Dove.
        /// </summary>
        public Dove() : base() 
        { 
            BirdType = BirdType.Dove;
        }

        /// <summary>
        ///   Copy constructor
        /// </summary>
        /// <param name="dove"> The Dove instance (object) to copy </param>
        public Dove(Dove dove)
        {
            // Set common properties (Animal attributes)
            Category = CategoryType.Bird;
            Name = dove.Name;
            Age = dove.Age;
            Gender = dove.Gender;
            ImagePath = dove.ImagePath;
            Id = dove.Id;

            // Set Bird properties
            Wingspan = dove.Wingspan;
            Color = dove.Color;
            BirdType = BirdType.Dove;

            // Set Dove propertie
            noiseLevel = dove.noiseLevel;
        }

        /// <summary>
        ///    Copy the common data from a Bird object
        /// </summary>
        /// <param name="other"> The Bird instance to copy </param>
        public Dove(Bird other)
        {
            // Animal properties
            Category = CategoryType.Bird;
            Name = other.Name;
            Age = other.Age;
            Gender = other.Gender;
            ImagePath = other.ImagePath;
            Id = other.Id;

            // Bird properties
            Wingspan = other.Wingspan;
            Color = other.Color;
            BirdType = BirdType.Dove;
        }

        public string NoiseLevel
        {
            get { return noiseLevel; }
            set { noiseLevel = value; }
        }

        public override string ToString()
        {
            string output = string.Format("Id: {0}, name: {1}, birdType: {2}, bird color: {3}, Noise level: {4}",
                    Id, Name, BirdType, Color, noiseLevel);

            return output;
        }

        /// <summary>
        ///   Gets the data of the Dove as a dictionary. (Contains Animal + Bird + Dove data)
        /// </summary>
        /// <returns> A dictionary containing the Dove's data </returns>
        public override Dictionary<string, string> GetAnimalData()
        {
            return new()
            {
                { "ID", Id},
                { "Name", Name },
                { "Age", Age.ToString() },
                { "Gender", Gender.ToString() },
                { "Category", Category.ToString() },
                { "Bird type", BirdType.ToString() },
                { "Wingspan", Wingspan.ToString() },
                { "Color", Color },
                { "Noise level", noiseLevel },
            };
        }
    }
}
