﻿namespace WildlifeTrackerSystem.src.Fish
{
    /// <summary>
    ///   A concrete class inheriting from Fish
    /// </summary>
    public class Goldfish : Fish
    {
        private GoldfishBreed breed;
        private string tailType;        // The goldfish tail type, which can have various tail types (single-tail, double-tail)
        private FoodSchedule foodSchedule = new FoodSchedule();

        public Goldfish() : base()
        {
            FishType = FishType.Goldfish;
            tailType = string.Empty;

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


        #region Methods

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
        ///   Sets the food schedule for the goldfish.
        /// </summary>
        private void SetFoodSchedule()
        {
            foodSchedule.EaterType = EaterType.Omnivorous;
            foodSchedule.Food.AddToList("Morning: flakes or pellets");
            foodSchedule.Food.AddToList("Lunch: peas and chopped lettuce");
            foodSchedule.Food.AddToList("Evening: insects and insect larvae");
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
        ///    Copies the animal object, used to prevent unintended modifications to the original objects.
        /// </summary>
        /// <returns> Goldfish object with all data </returns>
        public override object CopyAnimal()
        {
            return new Goldfish
            {
                Category = CategoryType.Fish,
                Name = Name,
                Age = Age,
                Gender = Gender,
                ImagePath = ImagePath,
                Id = Id,
                Habitat = Habitat,
                WaterTemperature = WaterTemperature,
                FishType = FishType.Goldfish,
                TailType = tailType,
                Breed = breed,
            };
        }
    }

    #endregion
}
