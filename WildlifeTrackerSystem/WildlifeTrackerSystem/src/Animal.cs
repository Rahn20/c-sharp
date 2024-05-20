
namespace WildlifeTrackerSystem.src
{
    // An abstract Class: cannot be used to create objects (to access it, it must be inherited from another class).

    /// <summary>
    ///   An abstract class implements the IAnimal interface.
    /// </summary>
    public abstract class Animal : IAnimal
    {
        #region Fields
        private GenderType gender;          // The gender of the animal
        private CategoryType category;      // The category of the animal
        private int age;                    // The age of the animal
        private string id = "";             // ex: M001, B001
        private string name = "";           // The name of the animal
        private string imagePath = "";      // The file path to the image of the animal
        #endregion

        /// <summary>
        ///   Default constructor
        /// </summary>
        public Animal() { }

        #region Properties
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }

        public CategoryType Category
        {
            get { return category; }
            set { category = value; }

        }
   
        public GenderType Gender
        {
            get { return gender; }
            set { gender = value; }
        }
        #endregion


        #region Methods
        public override string ToString()
        {
            string output = string.Format("Id: {0}, name: {1}, age: {2}, gender: {3}, category: {4}",
                id, name, age, Gender, category);

            return output;
        }

        /// <summary>
        ///   Gets the data of the animal as a dictionary. This method can be overridden in a derived class.
        /// </summary>
        /// <returns> A dictionary containing the animal's data </returns>
        public virtual Dictionary<string, string> GetAnimalData()
        {
            return new()
            {
                { "ID", id },
                { "Name", name },
                { "Age", age.ToString() },
                { "Gender", gender.ToString() },
                { "Category", category.ToString() },
            };
        }


        /// <summary>
        ///   Copies the animal object, used to prevent unintended modifications to the original objects stored in the (collection) ListManager class.
        /// </summary>
        /// <returns> Animal object </returns>
        public abstract object CopyAnimal();


        /// <summary>
        ///   Gets the food schedule for a specific animal species.
        ///   The implementation of this method is to be provided by the subclasses.
        /// </summary>
        /// <returns> An object of the FoodSchedule assigned to the particular object. </returns>
        public abstract FoodSchedule GetFoodSchedule();
        #endregion
    }
}
