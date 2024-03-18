
namespace WildlifeTrackerSystem.Bird
{
    public class Eagle : Bird
    {
        private float speed;        // The flight speed of the eagle in km/h.


        /// <summary>
        ///   Default constructor, sets the birdtype to Eagle.
        /// </summary>
        public Eagle() : base() 
        {
            BirdType = BirdType.Eagle;
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

        /// <summary>
        ///   Copy the common data from a Bird object
        /// </summary>
        /// <param name="other">  The Bird object to copy </param>
        public Eagle(Bird other)
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
            BirdType = BirdType.Eagle;
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
            return new()
            {
                { "ID", Id },
                { "Name", Name },
                { "Age", Age.ToString() },
                { "Gender", Gender.ToString() },
                { "Category", Category.ToString() },
                { "Bird type", BirdType.ToString() },
                { "Wingspan", Wingspan.ToString() },
                { "Color", Color },
                { "Speed", speed.ToString() },
            };
        }
    }
}
