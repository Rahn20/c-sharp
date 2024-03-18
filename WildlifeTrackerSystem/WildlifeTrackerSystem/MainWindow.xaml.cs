using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using WildlifeTrackerSystem.Bird;
using WildlifeTrackerSystem.Fish;
using WildlifeTrackerSystem.Mammal;
using WildlifeTrackerSystem.Reptile;

namespace WildlifeTrackerSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AnimalManager animalManager;
        private string animalImagePath = "";        // Temporarily store the image; it will later be added to the animal list
        private bool allDataAdded = false;          // Indicates whether all animal data has been added and validated. 

        public MainWindow()
        {
            // Visual Studio initializations
            InitializeComponent();

            animalManager = new AnimalManager();

            // My initializations
            InitializeGUI();
        }

        /// <summary>
        ///   Prepares the form before display. Initiate box controls with default values.
        /// </summary>
        private void InitializeGUI()
        {
            // Fill list box with animal category
            BoxCategory.ItemsSource = Enum.GetValues(typeof(CategoryType)).Cast<CategoryType>();

            // Fill the comboboxes with values from enums
            BoxGoldfishBreed.ItemsSource = Enum.GetValues(typeof(GoldfishBreed)).Cast<GoldfishBreed>().OrderBy(fishBreed => fishBreed.ToString());
            BoxSharkSpecies.ItemsSource = Enum.GetValues(typeof(SharkSpecie)).Cast<SharkSpecie>().OrderBy(sharkSpecie => sharkSpecie.ToString());
            BoxDogBreed.ItemsSource = Enum.GetValues(typeof(DogBreed)).Cast<DogBreed>().OrderBy(dogBreed => dogBreed.ToString());
            BoxDogEnergilevel.ItemsSource = new int[] { 1, 2, 3, 4, 5 };
            BoxWolfSpecies.ItemsSource = Enum.GetValues(typeof(WolfSpecie)).Cast<WolfSpecie>().OrderBy(wolfSpecie => wolfSpecie.ToString());
        }


        /// <summary>
        ///    Recursively clears all TextBoxes within the specified container (parent). 
        ///    BASE CASE: The recursion stops when there are no more children to process.
        /// </summary>
        /// <param name="parent"> The parent container in the visual tree to start searching from </param>
        private static void ClearAllTextBoxes(DependencyObject parent)
        {
            // Iterates through each child element of the parent in the visual tree.
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                // Clears the content of the TextBoxes.
                if (child is System.Windows.Controls.TextBox textBox)
                {
                    textBox.Clear();
                }

                // Recursively calls itself to clear TextBoxes in child elements
                ClearAllTextBoxes(child);
            }
        }


        // Event handler for the selectionChange in the animal category selection listbox.
        private void BoxCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Hide the StackPanel that shows the Animal specification text with inputs.
            PanelContainer.Visibility = Visibility.Collapsed;

            if (BoxCategory.SelectedItem != null)
            {
                // Clear existing ItemsSource in Species-BoxList and add the selected value
                BoxSpecies.ItemsSource = null;
                BoxSpecies.ItemsSource = AnimalManager.GetAnimalTypeValues((CategoryType)BoxCategory.SelectedIndex);
            }
        }


        // Event handler for the selectionChange in the species selection listbox.
        private void BoxSpecies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BoxCategory.SelectedItem != null && BoxSpecies.SelectedItem != null)
            {
                PanelContainer.Visibility = Visibility.Visible;
                SetVisibilityToCollapsed();

                var animalType = BoxSpecies.SelectedItem.ToString();

                // Show the appropriate Stackpanel based on the selected animal type and category
                switch ((CategoryType)BoxCategory.SelectedIndex)
                {
                    case CategoryType.Bird:
                        PanelBird.Visibility = Visibility.Visible;

                        if (animalType == "Dove") PanelDove.Visibility = Visibility.Visible;
                        else PanelEagle.Visibility = Visibility.Visible;    // Eagle
                        break;
                    case CategoryType.Fish:
                        PanelFish.Visibility = Visibility.Visible;

                        if (animalType == "Goldfish") PanelGoldfish.Visibility = Visibility.Visible;
                        else PanelShark.Visibility = Visibility.Visible;    // Shark  
                        break;
                    case CategoryType.Mammal:
                        PanelMammal.Visibility = Visibility.Visible;

                        if (animalType == "Dog") PanelDog.Visibility = Visibility.Visible;
                        else PanelWolf.Visibility = Visibility.Visible;    // wolf  
                        break;
                    case CategoryType.Reptile:
                        PanelReptile.Visibility = Visibility.Visible;

                        if (animalType == "Snake") PanelSnake.Visibility = Visibility.Visible;
                        else PanelFrog.Visibility = Visibility.Visible;    // frog  
                        break;
                }
            }
        }


        // Event handler for the click event of the "Add Image" button.
        private void BtnAddImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create OpenFileDialog
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Select a picture";
                openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg;*.bmp)|*.png;*.jpeg;*.jpg;*.bmp|All files (*.*)|*.*";

                // Show OpenFileDialog and get result
                bool? result = openFileDialog.ShowDialog();

                if (result == true)
                {
                    animalImagePath = openFileDialog.FileName;

                    // Split the file path to get the image name and print it out on GUI
                    string[] parts = openFileDialog.FileName.Split("\\");
                    TxtImagePath.Text = parts[parts.Length - 1];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding image: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        /// <summary>
        ///   Loads and displays an image from the specified image path.
        /// </summary>
        /// <param name="imagePath"> The path to the image file </param>
        private void LoadAndDisplayImage(string imagePath)
        {
            try
            {
                // Create a BitmapImage element
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imagePath, UriKind.RelativeOrAbsolute); // set image source to the selected image path
                bitmap.EndInit();

                // Set the Image control's source to the loaded BitmapImage
                BoxImageViewAnimal.Source = bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading image: {ex.Message}");
            }
        }


        /// <summary>
        ///   Reads and validates animal input from GUI elements. 
        /// </summary>
        /// <param name="animal"> An object of Animal to store the input data </param>
        /// <returns> True if the input is valid and successfully read, otherwise false </returns>
        private bool ReadAnimalInput(out Animal animal)
        {
            animal = new Animal();

            if (BoxGender.SelectedItem != null && BoxCategory.SelectedItem != null && BoxSpecies.SelectedItem != null)
            {
                animal.ImagePath = animalImagePath;
                animal.Gender = (GenderType)BoxGender.SelectedIndex;
                animal.Category = (CategoryType)BoxCategory.SelectedIndex;

                // Try to convert age from Age TextBox to a valid integer and validate Name TextBox
                if (int.TryParse(BoxAge.Text, out int ageValue) && !string.IsNullOrEmpty(BoxName.Text))
                {
                    animal.Name = BoxName.Text;
                    animal.Age = ageValue;
                    return true;
                } 
                else
                {
                    MessageBox.Show("Name and age cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            } else
            {
                MessageBox.Show("The Gender, Animal category, and animal species need to be selected", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return false;
        }


        /// <summary>
        ///    Reads and validates Bird input from GUI elements. 
        /// </summary>
        /// <param name="birdObj"> An object of Bird class to store the bird input data </param>
        /// <returns> True if the input is valid, otherwise false </returns>
        private bool ReadBirdInput(Bird.Bird birdObj)
        {
            if (float.TryParse(BoxBirdWingspan.Text, out float wingspanValue) && !string.IsNullOrEmpty(BoxBirdColor.Text))
            {
                birdObj.Wingspan = wingspanValue;
                birdObj.Color = BoxBirdColor.Text;
                return true;
            }
            else
            {
                MessageBox.Show("Color and wingspan cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
        }

        /// <summary>
        ///    Reads and validates bird types input from GUI elements.
        /// </summary>
        /// <param name="animalObj"> The animal object to read input for </param>
        /// <returns> True if the input is valid, otherwise false </returns>
        private bool ReadBirdTypeInput(Animal animalObj)
        {
            // Check if the animal is a Dove and Dove noise level is selected
            if (animalObj is Dove dove && BoxDoveNoiseLevel.SelectedItem != null)
            {
                dove.NoiseLevel = BoxDoveNoiseLevel.Text;
                return true;
            }
            else if (animalObj is Eagle eagle && float.TryParse(BoxEagleSpeed.Text, out float speedValue))
            {
                eagle.Speed = speedValue;
                return true;
            }
            else 
            {
                MessageBox.Show("Eagle speed cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
        }


        /// <summary>
        ///   Reads and validates fish-specific input from GUI elements.
        /// </summary>
        /// <param name="fishObj"> An object of Fish to store the input data </param>
        /// <returns> True if the fish input is valid, otherwise false  </returns>
        private bool ReadFishInput(Fish.Fish fishObj)
        {
            fishObj.Habitat = BoxFishHabitat.Text;  // Can be empty, no need to validate

            if (float.TryParse(BoxFishWaterTemp.Text, out float waterTempValue))
            {
                fishObj.WaterTemperature = waterTempValue;
                return true;
            } else
            {
                MessageBox.Show("Fish water temperature cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
        }

        /// <summary>
        ///    Reads and validates fish type input from GUI elements.
        /// </summary>
        /// <param name="animalObj"> The animal object to read input for </param>
        /// <returns> True if the input is valid, otherwise false </returns>
        private bool ReadFishTypeInput(Animal animalObj)
        {
            if (animalObj is Goldfish goldfish && BoxGoldfishTailType.SelectedItem != null && BoxGoldfishBreed.SelectedItem != null)
            {
                goldfish.Breed = (GoldfishBreed)BoxGoldfishBreed.SelectedItem;
                goldfish.TailType = BoxGoldfishTailType.Text;
                return true;
            }
            else if (animalObj is Shark shark)
            {
                bool readSpeed = float.TryParse(BoxSharkSwimmingSpeed.Text, out float speedValue);
                bool readWeight = float.TryParse(BoxSharkWeight.Text, out float weightValue);
                bool readLength = Int16.TryParse(BoxSharkLength.Text, out Int16 lengthValue);

                if (readLength && readSpeed && readWeight && BoxSharkSpecies.SelectedItem != null)
                {
                    shark.Specie = (SharkSpecie)BoxSharkSpecies.SelectedItem;
                    shark.Weight = weightValue;
                    shark.Length = lengthValue;
                    shark.SwimmingSpeed = speedValue;
                    return true;
                }
                else
                {
                    MessageBox.Show("Shark weight, length and swimming speed cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Goldfish Tail type cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return false;
        }

        /// <summary>
        ///    Reads and validates Mammal input from GUI elements.
        /// </summary>
        /// <param name="mammalObj"> An object of Mammal to store the input data </param>
        /// <returns> True if the input is valid, otherwise false </returns>
        private bool ReadMammalInput(Mammal.Mammal mammalObj)
        {
            mammalObj.Color = BoxMammalColor.Text;  // can be empty, no need to validate

            bool readLifespan= Int16.TryParse(BoxMammalLifespan.Text, out Int16 lifespanValue);
            bool readnumOfTeeth = Int16.TryParse(BoxMammalNumOfTeeth.Text, out Int16 teethValue);
            bool readWeight = float.TryParse(BoxMammalWeight.Text, out float weightValue);
            bool readSpeed = float.TryParse(BoxMammalSpeed.Text, out float speedValue);
            bool readHeight = float.TryParse(BoxMammalHeight.Text, out float heightValue);

            if (readLifespan && readnumOfTeeth && readWeight && readSpeed && readHeight)
            {
                mammalObj.Lifespan = lifespanValue;
                mammalObj.NumOfTeeth = teethValue;
                mammalObj.Weight = weightValue;
                mammalObj.Height = heightValue;
                mammalObj.Speed = speedValue;
                return true;
            }
            else
            {
                MessageBox.Show("Mammal lifespan, teeth, weight, speed and height cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            return false;
        }

        /// <summary>
        ///    Reads and validates Mammal type input from GUI elements.
        /// </summary>
        /// <param name="animalObj"> The animal object to read input for </param>
        /// <returns> True if the input is valid, otherwise false  </returns>
        private bool ReadMammalTypeInput(Animal animalObj)
        {
            if (animalObj is Wolf wolf && BoxWolfSpecies.SelectedItem != null && !string.IsNullOrEmpty(BoxWolfEyeColor.Text))
            {
                wolf.WolfSpecie = (WolfSpecie)BoxWolfSpecies.SelectedItem;
                wolf.EyeColor = BoxWolfEyeColor.Text;
                return true;
            }
            else if (animalObj is Dog dog && BoxDogBreed.SelectedItem != null && BoxDogEnergilevel.SelectedItem != null)
            {
                dog.DogBreed = (DogBreed)BoxDogBreed.SelectedItem;
                dog.EnergyLevel = Int16.Parse(BoxDogEnergilevel.Text);
                return true;
            }
            else
            {
                MessageBox.Show("Wolf OR Dog fields cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return false;
        }

        /// <summary>
        ///    Reads and validates Reptile input from GUI elements.
        /// </summary>
        /// <param name="reptileObj"> An object of Reptile to store the input data </param>
        /// <returns> True if the input is valid, otherwise false </returns>
        private bool ReadReptileInput(Reptile.Reptile reptileObj)
        {
            reptileObj.Habitat = BoxReptileHabitat.Text;  // can be empty, no need to validate

            if (Int16.TryParse(BoxReptileNumOfLegs.Text, out Int16 legsValue))
            {
                reptileObj.NumOfLegs = legsValue;
                return true;
            }
            else
            {
                MessageBox.Show("Reptile number of legs cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            return false;
        }

        /// <summary>
        ///   Reads and validates Reptile types input from GUI elements.
        /// </summary>
        /// <param name="animalObj"> The animal object to read input for </param>
        /// <returns> True if the input is valid, otherwise false </returns>
        private bool ReadReptileTypeInput(Animal animalObj)
        {
            bool snakeLength = Int16.TryParse(BoxSnakeLength.Text, out Int16 lengthValue);

            if (animalObj is Snake snake && snakeLength)
            {
                snake.Length = lengthValue;
                bool isChecked = CheckboxIsVenomous.IsChecked ?? false; // If IsChecked is null, default to false

                if (isChecked) snake.IsVenomous = true;
                else snake.IsVenomous = false;

                return true;
            }
            else if (animalObj is Frog frog && !string.IsNullOrEmpty(BoxFrogDiet.Text))
            {
                frog.Diet = BoxFrogDiet.Text;
                return true;
            }
            else
            {
                MessageBox.Show("Snake or Frog fields cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return false;
        }

        // Event handler for the click event of the "Add Animal" button.
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Animal animalObj;
            bool animalInputOk = ReadAnimalInput(out animalObj);

            // check animal validation
            if (animalInputOk)
            {
                switch ((CategoryType)BoxCategory.SelectedItem)
                {
                    case CategoryType.Bird:
                        Bird.Bird birdObj = new Bird.Bird(animalObj);
                        bool readBirdOk = ReadBirdInput(birdObj);

                        // check Bird validation
                        if (readBirdOk)
                        {
                            animalObj = Bird.Bird.CreateBird((BirdType)BoxSpecies.SelectedIndex, birdObj);

                            // True if bird passes validation
                            if (ReadBirdTypeInput(animalObj)) allDataAdded = true;
                        }
                        break;
                    case CategoryType.Fish:
                        Fish.Fish fishObj = new Fish.Fish(animalObj);
                        bool readFishOk = ReadFishInput(fishObj);

                        if (readFishOk)
                        {
                            animalObj = Fish.Fish.CreateFish((FishType)BoxSpecies.SelectedIndex, fishObj);
                            
                            if (ReadFishTypeInput(animalObj)) allDataAdded = true;
                        }
                        break;

                    case CategoryType.Mammal:
                        Mammal.Mammal mammalObj = new Mammal.Mammal(animalObj);
                        bool readMammalOk = ReadMammalInput(mammalObj);

                        if (readMammalOk)
                        {
                            animalObj = Mammal.Mammal.CreateMammal((MammalType)BoxSpecies.SelectedIndex, mammalObj);

                            if (ReadMammalTypeInput(animalObj)) allDataAdded = true;
                        }
                        break;
                    case CategoryType.Reptile:
                        Reptile.Reptile reptileObj = new Reptile.Reptile(animalObj);
                        bool readReptileOk = ReadReptileInput(reptileObj);

                        if (readReptileOk)
                        {
                            animalObj = Reptile.Reptile.CreateReptile((ReptileType)BoxSpecies.SelectedIndex, reptileObj);

                            if (ReadReptileTypeInput(animalObj)) allDataAdded = true;
                        }
                        break;
                }

                CreateAnimal(animalObj);
                UpdateAnimalResult(animalObj);
            }
          
        }

        /// <summary>
        ///   Creates an animal ID and adds the animal object to the animal list if all data is added and has passed validations.
        /// </summary>
        /// <param name="animalObj"> The animal object to be created/added to the list </param>
        private void CreateAnimal(Animal animalObj)
        {
            if (allDataAdded)
            {
                animalObj.Id = animalManager.GetNewID(animalObj.Category);
                animalManager.AddToList(animalObj);
                allDataAdded = false;
            }
        }

        /// <summary>
        ///   Updates the animal result view with the data of the last added animal.
        /// </summary>
        /// <param name="animalObj"> The animal object to update the result view with </param>
        private void UpdateAnimalResult(Animal animalObj)
        {
            Animal? getAnimalData = animalManager.GetLastAnimal;

            // Checks if the animalList is not empty and the animal object has an ID
            if (getAnimalData != null &&  !string.IsNullOrEmpty(animalObj.Id))
            {
                ItemsControlAnimalView.ItemsSource = getAnimalData.GetAnimalData();
                LoadAndDisplayImage(getAnimalData.ImagePath);

                TxtImagePath.Text = "";
                animalImagePath = "";
                ClearAllTextBoxes(this); // passing MainWindow instance  
            }
        }


        /// <summary>
        ///   Sets visibility to collapsed for all child StackPanels within PanelContainer (Animal specification text with inputs).
        /// </summary>
        private void SetVisibilityToCollapsed()
        {
            // Iterate through each child StackPanel in PanelContainer
            foreach (UIElement child in PanelContainer.Children)
            {
                // Check if the child is a StackPanel
                if (child is StackPanel stackPanel)
                {
                    stackPanel.Visibility = Visibility.Collapsed;
                }
            }
        }


        /// <summary>
        ///   Allows only numeric input in a TextBox and limits the input length to fit Int16 range.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumericOnly(object sender, TextCompositionEventArgs e)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9]");
            bool isTextNumeric = reg.IsMatch(e.Text);

            // Check if the sender is a TextBox and its length is equal or greater than 5
            if (sender is System.Windows.Controls.TextBox textBox && textBox.Text.Length >= 5)    // Adjust for int16's max range (5 digits)
            {
                e.Handled = true; // Prevent further input if the length is already at maximum.
                return;
            }

            e.Handled = isTextNumeric;
        }

        /// <summary>
        ///   Allows only float and integer (numeric or comma) input in a TextBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FloatAndIntOnly(object sender, TextCompositionEventArgs e)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9,]");
            bool isTextOk = reg.IsMatch(e.Text);

            if (sender is System.Windows.Controls.TextBox textBox && textBox.Text.Length >= 5)
            {
                e.Handled = true; // Prevent further input if the length is already at maximum.
                return;
            }

            e.Handled = isTextOk;
        }
    }
}