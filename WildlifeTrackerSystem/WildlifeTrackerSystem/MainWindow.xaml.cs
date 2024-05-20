using Microsoft.Win32;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using WildlifeTrackerSystem.src;
using WildlifeTrackerSystem.src.Bird;
using WildlifeTrackerSystem.src.Fish;
using WildlifeTrackerSystem.src.Mammal;
using WildlifeTrackerSystem.src.Reptile;

namespace WildlifeTrackerSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AnimalManager animalManager;
        private string animalImagePath = string.Empty;      // Temporarily store the image; it will later be added to the animal list
        private string fileNamePath = string.Empty;         // Temporarily store the file path; it will later be read and written
        private int savedToFile = 0;                        // The number of objects that have been saved to the file.
        private bool animalChanged = false;                 // False if the animal object hasn't changed, otherwise true.
        private bool animalRemoved = false;                 // False if the animal object hasn't deleted, otherwise true.

        public MainWindow()
        {
            // Visual Studio initializations
            InitializeComponent();

            Closing += MainWindow_Closing;  // Closing event

            animalManager = new AnimalManager();
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

            BoxTotalAnimalsNr.Text = animalManager.Count.ToString();
        }


        // Closing event, if data hasn't been saved to the file and the user clicks "No", the window will not be closed.
        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            bool dataSaved = CheckDataSavedToFile();

            if (dataSaved is false)
            {
                e.Cancel = true;    // Cancel closing the window when the user clicks "No"
            }
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
                BoxSpecies.ItemsSource = animalManager.GetAnimalTypeValues((CategoryType)BoxCategory.SelectedIndex);
            }
        }

        // Event handler for the selectionChange in the species selection listbox.
        private void BoxSpecies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BoxCategory.SelectedItem != null && BoxSpecies.SelectedItem != null)
            {
                PanelContainer.Visibility = Visibility.Visible;
                SetVisibilityToCollapsed();
                
                var animalType = BoxSpecies.SelectedValue.ToString();

                // View animals based on the animal species
                ListViewAnimals.ItemsSource = animalManager.SearchSpecificObject(animalType);
                BoxEaterType.Text = string.Empty;
                BoxViewFoodSchedule.ItemsSource = null;

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
        private void ReadAnimalInput(out Animal? animal)
        {
            animal = null;

            if (BoxGender.SelectedItem != null && BoxCategory.SelectedItem != null && BoxSpecies.SelectedItem != null)
            {
                // Try to convert age from Age TextBox to a valid integer and validate Name TextBox
                if (int.TryParse(BoxAge.Text, out int ageValue) && !string.IsNullOrEmpty(BoxName.Text))
                {
                    animal = animalManager.CreateAnimal((CategoryType)BoxCategory.SelectedIndex, BoxSpecies.SelectedItem);

                    animal.ImagePath = animalImagePath;
                    animal.Gender = (GenderType)BoxGender.SelectedIndex;
                    animal.Category = (CategoryType)BoxCategory.SelectedIndex;
                    animal.Name = BoxName.Text;
                    animal.Age = ageValue;
                } 
                else
                {
                    MessageBox.Show("Name and age cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            } else
            {
                MessageBox.Show("The Gender, Animal category, and animal species need to be selected", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        /// <summary>
        ///    Reads and validates Bird input from GUI elements. 
        /// </summary>
        /// <param name="animal"> An object of Bird class to store the bird input data </param>
        /// <returns> True if the input is valid, otherwise false </returns>
        private bool ReadBirdInput(Bird animal)
        {
            if (float.TryParse(BoxBirdWingspan.Text, out float wingspanValue) && !string.IsNullOrEmpty(BoxBirdColor.Text))
            {
                animal.Wingspan = wingspanValue;
                animal.Color = BoxBirdColor.Text;
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
        private bool ReadFishInput(Fish fishObj)
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
        private bool ReadMammalInput(Mammal mammalObj)
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
        private bool ReadReptileInput(Reptile reptileObj)
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


        /// <summary>
        ///   Validates all animal data, including category and species
        /// </summary>
        /// <param name="category"> The animal category </param>
        /// <param name="animalObj"> The animal object to validate </param>
        /// <returns> True if all data passes through validations; otherwise, false </returns>
        private bool ValidateAnimal(CategoryType category, Animal animalObj)
        {
            switch (category)
            {
                case CategoryType.Bird:
                    bool readBirdOk = ReadBirdInput((Bird)animalObj);

                    // True if bird passes validation
                    if (readBirdOk && ReadBirdTypeInput(animalObj))
                    {
                        return true;
                    }
                    break;
                case CategoryType.Fish:
                    bool readFishOk = ReadFishInput((Fish)animalObj);

                    if (readFishOk && ReadFishTypeInput(animalObj))
                    {
                        return true;
                    }
                    break;
                case CategoryType.Mammal:
                    bool readMammalOk = ReadMammalInput((Mammal)animalObj);

                    if (readMammalOk && ReadMammalTypeInput(animalObj))
                    {
                        return true;
                    }
                    break;
                case CategoryType.Reptile:
                    bool readReptileOk = ReadReptileInput((Reptile)animalObj);

                    if (readReptileOk && ReadReptileTypeInput(animalObj))
                    {
                        return true;
                    }
                    break;
            }

            return false;
        }


        // Event handler for the click event of the "Add Animal" button.
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Animal? animalObj;
            ReadAnimalInput(out animalObj);

            // check animal validation
            if (animalObj != null)
            {
                bool validation = ValidateAnimal((CategoryType)BoxCategory.SelectedItem, animalObj);

                if (validation)
                {
                    animalObj.Id = animalManager.GetNewID(animalObj.Category);
                    bool addedAnimal = animalManager.AddToList(animalObj);
                    Animal? lastAnimal = animalManager.GetLastElement; // get last added animal

                    if (addedAnimal == true && lastAnimal != null)
                    {
                        // Update the animal result view with the data of the last added animal.
                        ItemsControlAnimalView.ItemsSource = lastAnimal.GetAnimalData();
                        LoadAndDisplayImage(lastAnimal.ImagePath);
                        ListViewAnimals.ItemsSource = animalManager.SearchSpecificObject(BoxSpecies.SelectedValue.ToString());

                        BoxTotalAnimalsNr.Text = animalManager.Count.ToString();

                        TxtImagePath.Text = "";
                        animalImagePath = "";
                        ClearAllTextBoxes(this);
                    }
                }
            }
        }

        // Sets visibility to collapsed for all child StackPanels within PanelContainer (Animal specification text with inputs).
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

        // Allows only numeric input in a TextBox and limits the input length to fit Int16 range.
        private void NumericOnly(object sender, TextCompositionEventArgs e)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9]");
            bool isTextNumeric = reg.IsMatch(e.Text);

            // Adjust for int16's max range (5 digits)
            if (sender is System.Windows.Controls.TextBox textBox && textBox.Text.Length >= 5)
            {
                e.Handled = true; // Prevent further input if the length is already at maximum.
                return;
            }

            e.Handled = isTextNumeric;
        }

        // Allows only float and integer (numeric or comma) input in a TextBox.
        private void FloatAndIntOnly(object sender, TextCompositionEventArgs e)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9,]");
            bool isTextOk = reg.IsMatch(e.Text);

            if (sender is System.Windows.Controls.TextBox textBox && textBox.Text.Length >= 5)
            {
                e.Handled = true;
                return;
            }

            e.Handled = isTextOk;
        }

        // Event handler for the selected animal, view animal info and foodschedule.
        private void ListViewAnimals_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListViewAnimals.SelectedIndex == -1) return;

            Animal animal = (Animal)ListViewAnimals.SelectedItem;

            if (animal == null) return;

            ItemsControlAnimalView.ItemsSource = animal.GetAnimalData();
            LoadAndDisplayImage(animal.ImagePath);

            FoodSchedule foodSchedule = animal.GetFoodSchedule();

            BoxEaterType.Text = foodSchedule.ToString();
            BoxViewFoodSchedule.ItemsSource = null;
            BoxViewFoodSchedule.ItemsSource = foodSchedule.Food.ToStringArray();
        }

        // Event handler for the Food item button, and view food items in the list box.
        private void BtnFoodItems_Click(object sender, RoutedEventArgs e)
        {
            FoodItemForm foodItemForm = new FoodItemForm();

            // The user clicked "OK" in the FoodItemForm Window
            if (foodItemForm.ShowDialog() == true && foodItemForm.items.Ingredients.Count != 0)
            {
                BoxViewIngredients.Items.Add(foodItemForm.items.ToString());
            }
        }

        // Event handler for Change animal button. Update selected animal, set the animalChanged to true.
        private void BtnChangeAnimal_Click(object sender, RoutedEventArgs e)
        {
            if (ListViewAnimals.SelectedIndex != -1)
            {
                Animal animalObj = (Animal)ListViewAnimals.SelectedItem;
                int index = animalManager.GetIndexById(animalObj.Id);

                if (index == -1) return;

                // Try to convert age from Age TextBox to a valid integer and validate Name TextBox
                if (int.TryParse(BoxAge.Text, out int ageValue) && !string.IsNullOrEmpty(BoxName.Text))
                {
                    animalObj.ImagePath = string.IsNullOrEmpty(animalImagePath) ? animalObj.ImagePath : animalImagePath;
                    animalObj.Gender = BoxGender.SelectedIndex != -1 ? (GenderType)BoxGender.SelectedIndex : animalObj.Gender;
                    animalObj.Name = BoxName.Text;
                    animalObj.Age = ageValue;

                    // validate all animal data
                    bool validation = ValidateAnimal(animalObj.Category, animalObj);

                    if (validation)
                    {
                        animalManager.ChangeAt(index, animalObj);
                        animalChanged = true;       // True, animal object has been changed

                        ListViewAnimals.ItemsSource = null;
                        ListViewAnimals.ItemsSource = animalManager.SearchSpecificObject(BoxSpecies.SelectedValue.ToString());
                        TxtImagePath.Text = "";
                        animalImagePath = "";
                        ClearAllTextBoxes(this);
                    }
                }
                else
                {
                    MessageBox.Show("Name and age cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Select an animal from the list above to change", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Event handler for delete animal button. detele the selected animal. set the animalRemoved to true.
        private void BtnDeleteAnimal_Click(object sender, RoutedEventArgs e)
        {
            if (ListViewAnimals.SelectedIndex != -1)
            {
                int index = animalManager.GetIndexById(((Animal)ListViewAnimals.SelectedItem).Id);

                if (index == -1) return;

                animalManager.DeleteAt(index);

                animalRemoved = true;       // True, animal object has been removed
                BoxTotalAnimalsNr.Text = animalManager.Count.ToString();

                ListViewAnimals.ItemsSource = null;
                ListViewAnimals.ItemsSource = animalManager.SearchSpecificObject(BoxSpecies.SelectedValue.ToString());
            }
            else
            {
                MessageBox.Show("Select an animal from the list above to delete", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        /// <summary>
        ///   Updates the FoodSchedule-, Ingredients- and Animals-views, empty the image path and clear all boxes.
        /// </summary>
        private void UpdateGUI()
        {
            TxtImagePath.Text = "";
            animalImagePath = "";

            BoxEaterType.Text = "";
            BoxViewFoodSchedule.ItemsSource = null;         // empty food schedule box
            BoxViewIngredients.ItemsSource = null;          // empty Ingredients box
            ItemsControlAnimalView.ItemsSource = null;      // empty the animal specification view
            ListViewAnimals.ItemsSource = null;             // empty the list of animals
            ClearAllTextBoxes(this);
        }

        /// <summary>
        ///   Checks if all data in the list has been saved to the file. If not, display a message box to 
        ///   prompt the user to either save the data or ignore it.
        /// </summary>
        /// <returns> True if the data has been ignored. otherwise, false. </returns>
        private bool CheckDataSavedToFile()
        {
            MessageBoxResult result = MessageBoxResult.Yes;     // "Yes" means ignoring the saved data

            if (animalManager.Count != savedToFile || animalChanged == true || animalRemoved == true)
            {
                result = MessageBox.Show("There is unsaved data. Do you want to ignore it?", "Unsaved Data", MessageBoxButton.YesNo, MessageBoxImage.Question);
            }

            if (result == MessageBoxResult.Yes) return true;
            else return false;
        }

        // "New" submenu initialize the program exactly as at start-up. If data has not been saved a Messagebox will be
        // display to confirm proceeding without saving current data or go back to the current session.
        private void MnuFileNew_Click(object sender, RoutedEventArgs e)
        {
            bool dataSaved = CheckDataSavedToFile();

            if (dataSaved is true)
            {
                animalManager.DeleteAll();
                animalManager.ResetAnimalsIDs();
                fileNamePath = "";
                savedToFile = animalManager.Count;

                CurrenFilePath.Text = "--";
                BoxTotalAnimalsNr.Text = animalManager.Count.ToString();
                UpdateGUI();
            }
        }


        // "Save As" submenu lets the user to save data to either a text file or a Json file with a name and location selected by the user.
        // Set the animalRemoved and animalChanged to the original boolean (False) and save the current amount of objects to savedToFile variable.
        private void MnuFileSaveAs_Click(object sender, RoutedEventArgs e)
        {
            MenuItem? clickedItem = sender as MenuItem;

            if (clickedItem == null) return;

            string defaultExtension;
            string filter;

            if (clickedItem.Name == "MnuFileSaveAsText")
            {
                defaultExtension = ".txt";
                filter = "File (.txt)|*.txt;";
            }
            else // Save as json file
            {
                defaultExtension = ".json";
                filter = "File (.json)|*.json;";
            }

            // Configure save file dialog box
            SaveFileDialog saveFile = new SaveFileDialog
            {
                Title = "Save file",
                DefaultExt = defaultExtension,
                Filter = filter
            };


            // Show save file dialog box
            if (saveFile.ShowDialog() is true)
            {
                fileNamePath = saveFile.FileName;   // Store the current file path
                CurrenFilePath.Text = saveFile.FileName;

                try
                {
                    animalManager.JsonSerialize(fileNamePath);

                    animalRemoved = false;
                    animalChanged = false;
                    savedToFile = animalManager.Count;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving the file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }


        // "Save" submenu saves the data using the currently saved file. If the saved file is empty, display a SaveFileDialog for the user.
        // Set the animalRemoved and animalChanged to the original boolean (False) and save the current amount of objects to savedToFile variable.
        private void MnuFileSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(fileNamePath) && !File.Exists(fileNamePath))
                {
                    SaveFileDialog saveFile = new SaveFileDialog
                    {
                        Title = "Save file",
                        DefaultExt = ".json",
                        Filter = "JSON Files (*.json)|*.json|Text Files (*.txt)|*.txt" // Filter options
                    };

                    if (saveFile.ShowDialog() is true)
                    {
                        fileNamePath = saveFile.FileName;
                        CurrenFilePath.Text = saveFile.FileName;

                        animalManager.JsonSerialize(fileNamePath);
                    }
                }
                else
                {
                    animalManager.JsonSerialize(fileNamePath);
                }

                animalRemoved = false;
                animalChanged = false;
                savedToFile = animalManager.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving the file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        // "Exit" submenu, closes the application
        private void MnuFileExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        // "Open" submenu opens a text or Json file according to the user’s option.
        // Set the animalRemoved and animalChanged to the original boolean (False) and save the current amount of objects to savedToFile variable.
        private void MnuFileOpen_click(object sender, RoutedEventArgs e)
        {
            MenuItem? clickedItem = sender as MenuItem;

            if (clickedItem == null) return;
            if (CheckDataSavedToFile() is false) return;

            string defaultExtension;
            string filter;

            if (clickedItem.Name == "MnuFileOpenText")
            {
                defaultExtension = ".txt";
                filter = "File (.txt)|*.txt;";
            }
            else // Save as json file
            {
                defaultExtension = ".json";
                filter = "File (.json)|*.json;";
            }

            // Configure open file dialog box
            OpenFileDialog openFile = new OpenFileDialog
            {
                Title = "Open file",
                DefaultExt = defaultExtension,
                Filter = filter
            };

            // Show open file dialog box
            if (openFile.ShowDialog() is true)
            {
                fileNamePath = openFile.FileName;   // Store the current file path
                CurrenFilePath.Text = openFile.FileName;
                UpdateGUI();

                try
                {
                    animalManager.DeleteAll();
                    animalManager.JsonDeserialize(fileNamePath);
                    animalManager.UpdateCurrentIDs();

                    animalRemoved = false;
                    animalChanged = false;
                    savedToFile = animalManager.Count;

                    BoxTotalAnimalsNr.Text = animalManager.Count.ToString();
                    ListViewAnimals.ItemsSource = animalManager.GetAllItems();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error reading/opening the file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
    }
}