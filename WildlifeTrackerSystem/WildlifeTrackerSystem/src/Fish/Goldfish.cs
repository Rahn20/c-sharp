namespace WildlifeTrackerSystem.src.Fish
{
    public class Goldfish : Fish
    {
        private GoldfishBreed breed;
        private string tailType = "";        // The goldfish tail type, which can have various tail types (single-tail, double-tail)
        
        private FoodSchedule foodSchedule = new FoodSchedule();

        /// <summary>
        ///   Default constructor, sets the fishtype to Goldfish.
        /// </summary>
        public Goldfish() : base()
        {
            FishType = FishType.Goldfish;
            SetFoodSchedule();
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


        #region Properties
        public GoldfishBreed Breed
        {
            get { return breed; }
            set { breed = value; }
        }

        public string TailType
        {
            get { return tailType; }
            set { tailType = value; }
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
            Dictionary<string, string> keyValuePairs = base.GetAnimalData();

            keyValuePairs.Add("Goldfish breed", breed.ToString());
            keyValuePairs.Add("Tail type", tailType);
  
            return keyValuePairs;
        }

        /// <summary>
        ///   Gets the animal information as a string with values.
        /// </summary>
        /// <returns> A string containing the animal's data </returns>
        public override string GetExtraInfo()
        {
            string info = base.GetExtraInfo();

            info += string.Format("{0, -15} {1, 10}\n", "Goldfish breed:", breed);
            info += string.Format("{0, -15} {1, 10}\n", "Tail type:", tailType);
            return info;
        }

        /// <summary>
        ///   Sets the food schedule for the animal.
        /// </summary>
        private void SetFoodSchedule()
        {
            foodSchedule.EaterType = EaterType.Omnivorous;
            foodSchedule.AddToFoodList("Morning: flakes or pellets");
            foodSchedule.AddToFoodList("Lunch: peas and chopped lettuce");
            foodSchedule.AddToFoodList("Evening: insects and insect larvae");
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
