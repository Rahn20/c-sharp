
namespace WildlifeTrackerSystem.Mammal
{
    public class Wolf : Mammal
    {
        #region Fields
        private WolfSpecie wolfSpecie;
        private string eyeColor = "";       // The eye color of the wolf
        #endregion


        /// <summary>
        ///   Default constructor, sets the mammaltype to Wolf.
        /// </summary>
        public Wolf() : base() 
        {
            MammalType = MammalType.Wolf;
        }


        /// <summary>
        ///   Copy constructor
        /// </summary>
        /// <param name="wolf"> The Wolf instance (object) to copy </param>
        public Wolf(Wolf wolf)
        {
            // Set common properties (Animal attributes)
            Category = CategoryType.Mammal;
            Name = wolf.Name;
            Age = wolf.Age;
            Gender = wolf.Gender;
            ImagePath = wolf.ImagePath;
            Id = wolf.Id;

            // Set mammal properties
            MammalType = MammalType.Wolf;
            Color = wolf.Color;
            Lifespan = wolf.Lifespan;
            NumOfTeeth = wolf.NumOfTeeth;
            Weight = wolf.Weight;
            Speed = wolf.Speed;
            Height = wolf.Height;

            // Set wolf properties
            wolfSpecie = wolf.wolfSpecie;
            eyeColor = wolf.eyeColor;
        }

        /// <summary>
        ///    Copy the common data from a mammal object
        /// </summary>
        /// <param name="other"> The mammal instance to copy </param>
        public Wolf(Mammal other)
        {
            // Animal properties
            Category = CategoryType.Mammal;
            Name = other.Name;
            Age = other.Age;
            Gender = other.Gender;
            ImagePath = other.ImagePath;
            Id = other.Id;

            // mammal properties
            MammalType = MammalType.Wolf;
            Color = other.Color;
            Lifespan = other.Lifespan;
            NumOfTeeth = other.NumOfTeeth;
            Weight = other.Weight;
            Speed = other.Speed;
            Height = other.Height;
        }

        #region Properties
        public WolfSpecie WolfSpecie
        {
            get { return wolfSpecie; }
            set { wolfSpecie = value; }
        }

        public string EyeColor
        {
            get { return eyeColor; }
            set { eyeColor = value; }
        }
        #endregion

        public override string ToString()
        {
            string output = string.Format("Id: {0}, name: {1}, number of teeth: {2}, height: {3}, weight: {4}, wolf eyecolor. {5}",
                Id, Name, NumOfTeeth, Height, Weight, eyeColor);

            return output;
        }

        /// <summary>
        ///   Gets the data of the wolf as a dictionary. (Contains Animal + mammal + wolf data)
        /// </summary>
        /// <returns> A dictionary containing the wolf's data </returns>
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
                { "Wolf specie", wolfSpecie.ToString() },
                { "Eye color", eyeColor },
            };
        }
    }
}
