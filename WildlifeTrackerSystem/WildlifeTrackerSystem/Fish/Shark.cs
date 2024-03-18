
namespace WildlifeTrackerSystem.Fish
{
    public class Shark : Fish
    {
        #region Fields
        private SharkSpecie specie;
        private float swimmingSpeed;    // The shark swimming capabilities in km/h
        private int length;             // the shark length in meter
        private float weight;           // the shark weight in kg
        #endregion


        /// <summary>
        ///   Default constructor, sets the fishtype to Shark.
        /// </summary>
        public Shark() : base() 
        {
            FishType = FishType.Shark;
        }


        /// <summary>
        ///    Copy constructor
        /// </summary>
        /// <param name="shark"> The shark instance (object) to copy </param>
        public Shark(Shark shark)
        {
            // Set common properties (Animal attributes)
            Category = CategoryType.Fish;
            Name = shark.Name;
            Age = shark.Age;
            Gender = shark.Gender;
            ImagePath = shark.ImagePath;
            Id = shark.Id;

            // Set Fish properties
            Habitat = shark.Habitat;
            WaterTemperature = shark.WaterTemperature;
            FishType = FishType.Shark;

            // Set Shark properties
            specie = shark.specie;
            swimmingSpeed = shark.swimmingSpeed;
            length = shark.length;
            weight = shark.weight;
        }

        /// <summary>
        ///   Copy the common data from a Fish object
        /// </summary>
        /// <param name="other"> The Fish instance to copy </param>
        public Shark(Fish other)
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
            FishType = FishType.Shark;
        }


        #region Properties
        public SharkSpecie Specie
        {
            get { return specie; }
            set { specie = value; }
        }

        public float SwimmingSpeed
        {
            get { return swimmingSpeed; }
            set { swimmingSpeed = value; }
        }

        public float Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        public int Length
        {
            get { return length; }
            set { length = value; }
        }

        #endregion
        public override string ToString()
        {
            string output = string.Format("Id: {0}, name: {1}, Fishtype: {2}, watertemperature {3}, Swimmingspeed: {4}, shark lenth {5}",
                Id, Name, FishType, WaterTemperature, swimmingSpeed, length);

            return output;
        }


        /// <summary>
        ///   Gets the data of the Shark as a dictionary. (Contains Animal + Fish + Shark data)
        /// </summary>
        /// <returns> A dictionary containing the Shark's data </returns>
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
                { "Shark specie", specie.ToString() },
                { "Swimming speed", swimmingSpeed.ToString() },
                { "Weight", weight.ToString() },
                { "Length", length.ToString() },
            };
        }
    }
}
