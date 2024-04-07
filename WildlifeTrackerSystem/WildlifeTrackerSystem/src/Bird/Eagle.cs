namespace WildlifeTrackerSystem.src.Bird
{
    /// <summary>
    ///   A concrete class inheriting from Bird
    /// </summary>
    public class Eagle : Bird
    {
        private float speed;            // The flight speed of the eagle in km/h.
        private FoodSchedule foodSchedule = new FoodSchedule();

        /// <summary>
        ///   Default constructor, sets the birdtype to Eagle.
        /// </summary>
        public Eagle() : base()
        {
            BirdType = BirdType.Eagle;
            SetFoodSchedule();
        }


        /// <summary>
        ///   Copy constructor
        /// </summary>
        /// <param name="eagle"> The Eagle instance (object) to copy </param>
        public Eagle(Eagle eagle)
        {
            // Set common properties (Animal attributes)
            Category = CategoryType.Bird;
            Name = eagle.Name;
            Age = eagle.Age;
            Gender = eagle.Gender;
            ImagePath = eagle.ImagePath;
            Id = eagle.Id;

            // Set Bird properties
            Wingspan = eagle.Wingspan;
            Color = eagle.Color;
            BirdType = BirdType.Eagle;

            // Set Eagle propertie
            speed = eagle.speed;
        }

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public override string ToString()
        {
            string output = string.Format("Id: {0}, name: {1}, birdType: {2}, bird color: {3}, Eagle speed: {4}",
                    Id, Name, BirdType, Color, speed);

            return output;
        }



        /// <summary>
        ///   Gets the data of the Eagle as a dictionary. (Contains Animal + Bird + Eagle data)
        /// </summary>
        /// <returns> A dictionary containing the Eagle's data </returns>
        public override Dictionary<string, string> GetAnimalData()
        {
            Dictionary<string, string> keyValuePairs = base.GetAnimalData();
            keyValuePairs.Add("Speed", speed.ToString());
            return keyValuePairs;
        }

        /// <summary>
        ///   Gets the animal information as a string with values.
        /// </summary>
        /// <returns> A string containing the animal's data </returns>
        public override string GetExtraInfo()
        {
            string eagleInfo = string.Format("{0, -15} {1, 10}\n", "Speed:", speed);
            return base.GetExtraInfo() + eagleInfo;
        }


        /// <summary>
        ///   Sets the food schedule for the animal.
        /// </summary>
        private void SetFoodSchedule()
        {
            foodSchedule.EaterType = EaterType.Carnivore;
            foodSchedule.AddToFoodList("Morning: fish and small birds");
            foodSchedule.AddToFoodList("Lunch: snakes, small mammals and rodents");
            foodSchedule.AddToFoodList("Evening: reptiles");
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
