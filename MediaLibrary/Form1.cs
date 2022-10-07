using System;
using System.Drawing;
using System.Windows.Forms;

namespace MediaLibrary
{
    public partial class Form1 : Form
    {
        private Library library = new Library();

        public Form1()
        {
            InitializeComponent();
            TextResult.Text = library.ReadFromFile();
        }


        // by clicking on the register book button, a new book should be registered
        private void BtnRegisterBook_Click(object sender, EventArgs e)
        {
            string title = BoxBookTitle.Text;

            try
            {
                int pages = int.Parse(BoxBookPages.Text);  // throws FormatException if conversion fails

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

        // by clicking on the register soundtrack button, a new soundtrack should be registered
        private void BtnRegisterSound_Click(object sender, EventArgs e)
        {
            string title = BoxSoundTitle.Text;

            try
            {
                double playTime = double.Parse(BoxSoundTime.Text);  // throws FormatException if conversion fails

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

        // by clicking on the register movie button, a new movie should be registered
        private void BtnRegisterMovie_Click(object sender, EventArgs e)
        {
            string title = BoxMovieTitle.Text;
            string resolution = BoxMovieResolution.Text;

            try
            {
                double playTime = double.Parse(BoxMovieTime.Text);  // throws FormatException if conversion fails

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


         // change the color of the text/clear the text when a key is pressed
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
