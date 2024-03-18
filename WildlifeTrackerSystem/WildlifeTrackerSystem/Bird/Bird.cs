
namespace WildlifeTrackerSystem.Bird
{
    public class Bird : Animal
    {
        #region Fields
        private BirdType birdType;
        private float wingspan;         // The length of the bird's wings when fully extended, in cm.
        private string color = "";      // The color of the bird
        #endregion


        /// <summary>
        ///   Default constructor, sets the categorytype to Bird.
        /// </summary>
        public Bird() : base()
        {
            Category = CategoryType.Bird;
        }

        /// <summary>
        ///   Copy constructor
        /// </summary>
        /// <param name="bird"> The bird instance (object) to copy </param>
        public Bird(Bird bird)
        {
            // Set common properties (Animal attributes)
            Category = CategoryType.Bird;
            Name = bird.Name;
            Age = bird.Age;
            Gender = bird.Gender;
            ImagePath = bird.ImagePath;
            Id = bird.Id;

            // Set Bird properties
            birdType = bird.birdType;
            wingspan = bird.wingspan;
            color = bird.color;
        }

        /// <summary>
        ///   Copy the common data from an Animal object
        /// </summary>
        /// <param name="animal"> The Animal instance to copy </param>
        public Bird(Animal animal)
        {
            Category = CategoryType.Bird;
            Name = animal.Name;
            Age = animal.Age;
            Gender = animal.Gender;
            ImagePath = animal.ImagePath;
            Id = animal.Id;
        }


        #region Properties
        public BirdType BirdType 
        {
            get { return birdType; }
            set { birdType = value; }
        }

        public float Wingspan
        {
            get { return wingspan; }
            set {  wingspan = value; }
        }

        public string Color
        {
            get { return color; }
            set {  color = value; }
        }
        #endregion


        #region Methods
        public override string ToString()
        {
            string output = string.Format("Id: {0}, name: {1}, birdType: {2}, wingspan: {3}, bird color: {4}",
                Id, Name, birdType, wingspan, color);

            return output;
        }

        /// <summary>
        ///   Creates a new instance based on the specified bird type.
        /// </summary>
        /// <param name="type"> The type of bird to create </param>
        /// <param name="bird"> The bird object </param>
        /// <returns> A new object of the specified bird type </returns>
        /// <exception cref="ArgumentException"></exception>
        public static Bird CreateBird(BirdType type, Bird bird)
        {
            switch (type)
            {
                case BirdType.Dove: return new Dove(bird);
                case BirdType.Eagle: return new Eagle(bird);
                default:
                    throw new ArgumentException("Invalid birdtype", nameof(type));
            }
        }


        /// <summary>
        ///   Gets the data of the Bird as a dictionary. (Contains Animal + Bird data)
        /// </summary>
        /// <returns> A dictionary containing the bird's data </returns>
        public override Dictionary<string, string> GetAnimalData()
        {
            return new()
            {
                { "ID", Id },
                { "Name", Name },
                { "Age", Age.ToString() },
                { "Gender", Gender.ToString() },
                { "Category", Category.ToString() },
                { "Bird type", birdType.ToString() },
                { "Wingspan", wingspan.ToString() },
                { "Color", color },
            };
        }
        #endregion
    }
}