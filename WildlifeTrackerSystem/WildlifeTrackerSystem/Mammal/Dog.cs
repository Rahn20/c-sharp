
namespace WildlifeTrackerSystem.Mammal
{
    public class Dog : Mammal
    {

        #region Fields
        private DogBreed dogBreed;
        private int energyLevel;        // The dogs activity level, between 0-5.
        #endregion


        /// <summary>
        ///   Default constructor, sets the mammaltype to Dog.
        /// </summary>
        public Dog() : base() 
        {
            MammalType = MammalType.Dog;
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

        /// <summary>
        ///    Copy the common data from a mammal object
        /// </summary>
        /// <param name="other"> The mammal instance to copy </param>
        public Dog(Mammal other)
        {
            // Animal properties
            Category = CategoryType.Mammal;
            Name = other.Name;
            Age = other.Age;
            Gender = other.Gender;
            ImagePath = other.ImagePath;
            Id = other.Id;

            // Mammal properties
            MammalType = MammalType.Dog;
            Color = other.Color;
            Lifespan = other.Lifespan;
            NumOfTeeth = other.NumOfTeeth;
            Weight = other.Weight;
            Speed = other.Speed;
            Height = other.Height;
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
            return new()
            {
                { "ID", Id },
                { "Name", Name },
                { "Age", Age.ToString() },
                { "Gender", Gender.ToString() },
                { "Category", Category.ToString() },
                { "Mammal type", MammalType.ToString() },
                { "Color", Color == "-1" ? "-" : Color },
                { "Lifespan", Lifespan.ToString() },
                { "Number of teeth", NumOfTeeth.ToString() },
                { "Weight",  Weight.ToString()},
                { "Height", Height.ToString() },
                { "Speed", Speed.ToString() },
                { "Dog breed", dogBreed.ToString() },
                { "Energy level", energyLevel.ToString() },
            };
        }
    }
}
