﻿namespace WildlifeTrackerSystem.src.Mammal
{
    /// <summary>
    ///   A concrete class inheriting from Mammal
    /// </summary>
    public class Dog : Mammal
    {

        #region Fields
        private DogBreed dogBreed;
        private int energyLevel;        // The dogs activity level, between 0-5.

        private FoodSchedule foodSchedule = new FoodSchedule();
        #endregion


        public Dog() : base()
        {
            MammalType = MammalType.Dog;
            SetFoodSchedule();
        }

        /// <summary>
        ///   Copy constructor
        /// </summary>
        /// <param name="dog"> The Dog instance (object) to copy </param>
        public Dog(Dog dog)
        {
            // Set common properties (Animal attributes)
            Category = CategoryType.Mammal;
            Name = dog.Name;
            Age = dog.Age;
            Gender = dog.Gender;
            ImagePath = dog.ImagePath;
            Id = dog.Id;

            // Set mammal properties
            MammalType = MammalType.Dog;
            Color = dog.Color;
            Lifespan = dog.Lifespan;
            NumOfTeeth = dog.NumOfTeeth;
            Weight = dog.Weight;
            Speed = dog.Speed;
            Height = dog.Height;

            // Set Dog properties
            dogBreed = dog.dogBreed;
            energyLevel = dog.energyLevel;
        }


        #region Properties
        public DogBreed DogBreed
        {
            get { return dogBreed; }
            set { dogBreed = value; }
        }

        public int EnergyLevel
        {
            get { return energyLevel; }
            set { energyLevel = value; }
        }
        #endregion

        public override string ToString()
        {
            string output = string.Format("Id: {0}, name: {1}, number of teeth: {2}, height: {3}, weight: {4}, dog energylevel {5}",
                Id, Name, NumOfTeeth, Height, Weight, energyLevel);

            return output;
        }


        /// <summary>
        ///   Gets the data of the Dog as a dictionary. (Contains Animal + mammal + Dog data)
        /// </summary>
        /// <returns> A dictionary containing the Dog's data </returns>
        public override Dictionary<string, string> GetAnimalData()
        {
            Dictionary<string, string> keyValuePairs = base.GetAnimalData();

            keyValuePairs.Add("Dog breed", dogBreed.ToString());
            keyValuePairs.Add("Energy level", energyLevel.ToString());

            return keyValuePairs;
        }

        /// <summary>
        ///   Sets the food schedule for the Dog.
        /// </summary>
        private void SetFoodSchedule()
        {
            foodSchedule.EaterType = EaterType.Omnivorous;
            foodSchedule.Food.AddToList("Morning: chicken or turkey");
            foodSchedule.Food.AddToList("Lunch: carrots, green beans, sweet potatoes and brown rice");
            foodSchedule.Food.AddToList("Evening: eggs and fish like salmon and sardines");
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
        ///   Copies the animal object, used to prevent unintended modifications to the original objects.
        /// </summary>
        /// <returns> Dog object with all data </returns>
        public override object CopyAnimal()
        {
            return new Dog
            {
                Category = Category,
                Name = Name,
                Age = Age,
                Gender = Gender,
                ImagePath = ImagePath,
                Id = Id,
                MammalType = MammalType,
                Color = Color,
                Lifespan = Lifespan,
                NumOfTeeth = NumOfTeeth,
                Weight = Weight,
                Speed = Speed,
                Height = Height,
                DogBreed = dogBreed,
                EnergyLevel = energyLevel,
            };
        }
    }
}
