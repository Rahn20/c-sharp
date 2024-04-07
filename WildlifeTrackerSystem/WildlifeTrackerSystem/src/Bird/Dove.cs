﻿namespace WildlifeTrackerSystem.src.Bird
{
    /// <summary>
    ///   A concrete class inheriting from Bird
    /// </summary>
    public class Dove : Bird
    {
        private string noiseLevel = "";         // 3 levels, high, medium and low.
        private FoodSchedule foodSchedule = new FoodSchedule();

        /// <summary>
        ///   Default constructor, sets the birdtype to Dove.
        /// </summary>
        public Dove() : base()
        {
            BirdType = BirdType.Dove;
            SetFoodSchedule();
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
            // Call the base implementation to get the dictionary
            Dictionary<string, string> keyValuePairs = base.GetAnimalData();
            keyValuePairs.Add("Noise level", noiseLevel);
            return keyValuePairs;
        }


        /// <summary>
        ///   Gets the animal information as a string with values.
        /// </summary>
        /// <returns> A string containing the animal's data </returns>
        public override string GetExtraInfo()
        {
            string doveInfo = string.Format("{0, -15} {1, 10}\n", "NoiseLevel:", noiseLevel);
            return base.GetExtraInfo() + doveInfo;
        }


        /// <summary>
        ///   Sets the food schedule for the animal.
        /// </summary>
        private void SetFoodSchedule()
        {
            //foodSchedule = new FoodSchedule();
            foodSchedule.EaterType = EaterType.Herbivore;
            foodSchedule.AddToFoodList("Morning: grasses and weed seeds");
            foodSchedule.AddToFoodList("Lunch: grains");
            foodSchedule.AddToFoodList("Evening: berries and greens (parts of grasses)");
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
