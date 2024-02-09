using System;
using System.Drawing;
using System.Windows.Forms;

namespace MediaLibrary
{
    public partial class Form1 : Form
    {
        private readonly Library library = new Library();

        public Form1()
        {
            InitializeComponent();
            TextResult.Text = library.ReadFromFile();
        }


        // Register a new book when the "Register Book" button is clicked.
        private void BtnRegisterBook_Click(object sender, EventArgs e)
        {
            string title = BoxBookTitle.Text;

            try
            {
                int pages = int.Parse(BoxBookPages.Text);  // Throws FormatException if conversion fails.

                library.RegisterBook(title, pages);
                TextResult.Refresh();
                TextResult.Text = library.ReadFromFile();

                BoxBookTitle.Clear();
                BoxBookPages.Clear();
            }
            catch
            {
                MessageBox.Show("Antal sidor ska vara ett tal.", "Något gick fel!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Register a new soundtrack when the "Register Soundtrack" button is clicked.
        private void BtnRegisterSound_Click(object sender, EventArgs e)
        {
            string title = BoxSoundTitle.Text;

            try
            {
                double playTime = double.Parse(BoxSoundTime.Text);  // Throws FormatException if conversion fails.

                library.RegisterSoundTrack(title, playTime);
                TextResult.Refresh();
                TextResult.Text = library.ReadFromFile();

                BoxSoundTime.Clear();
                BoxSoundTitle.Clear();
            }
            catch
            {
                MessageBox.Show("Speltiden måste vara ett tal", "Något gick fel!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        // Register a new movie when the "Register Movie" button is clicked.
        private void BtnRegisterMovie_Click(object sender, EventArgs e)
        {
            string title = BoxMovieTitle.Text;
            string resolution = BoxMovieResolution.Text;

            try
            {
                double playTime = double.Parse(BoxMovieTime.Text);  // Throws FormatException if conversion fails.

                library.RegisterMovie(title, resolution, playTime);
                TextResult.Refresh();
                TextResult.Text = library.ReadFromFile();

                BoxMovieResolution.Clear();
                BoxMovieTime.Clear();
                BoxMovieTitle.Clear();
            }
            catch
            {
                MessageBox.Show("Speltiden måste vara ett tal.", "Något gick fel!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        // Change the color of the text and clear the text when a key is pressed.
        private void BoxMovieResolution_KeyDown(object sender, KeyEventArgs e)
        {
            if (BoxMovieResolution.Text == "ex: HD")
            {
                BoxMovieResolution.Text = "";
                BoxMovieResolution.ForeColor = Color.Black;
            }
        }
    }
}
