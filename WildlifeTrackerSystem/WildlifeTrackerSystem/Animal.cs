
namespace WildlifeTrackerSystem
{
    public class Animal
    {
        #region Fields
        private GenderType gender;
        private CategoryType category;
        private int age;
        private string id = "";     // ex: M001, B001
        private string name = "";
        private string imagePath = "";
        #endregion

        /// <summary>
        ///   Default constructor
        /// </summary>
        public Animal() { }

        /// <summary>
        ///  Initializing. Id is generated automatically.
        /// </summary>
        /// <param name="name"> The name of the animal </param>
        /// <param name="age"> The age of the animal. </param>
        /// <param name="imagePath"> The file path to the image of the animal </param>
        /// <param name="gender"> The gender of the animal. </param>
        /// <param name="category"> The category of the animal </param>
        public Animal(string id, string name, int age, string imagePath, GenderType gender, CategoryType category)
        {
            this.name = name;
            this.age = age;
            this.imagePath = imagePath;
            this.gender = gender;
            this.category = category;
            this.id = id;
        }

        #region Properties
        public string Name {
            get { return name; }
            set { name = value; }
        }

        public int Age {
            get { return age; }
            set {  age = value; }
        }

        public string Id {
            get { return id; }
            set { id = value; }
        }

        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }

        public CategoryType Category {
            get { return category; }
            set { category = value; }
        
        }

        public GenderType Gender {
            get { return gender; }
            set { gender = value; }
        }
        #endregion


        #region Methods
        public override string ToString()
        {
            string output = string.Format("Id: {0}, name: {1}, age: {2}, gender: {3}, category: {4}",
                id, name, age, gender, category);

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
        #endregion
    }
}
