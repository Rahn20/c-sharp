using System;
using System.IO;
using System.Reflection;


namespace MediaLibrary
{
    internal class Library
    {
        // the path to the csv file
        private string path;



        /**
         * <summary>
         *      Sets the path variable to the path of the csv file.
         * </summary>
         */
        public Library()
        {
            // returns the url to the main directory containing "/bin/Debug"
            var currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            // replace bin\debug with empty string and add the csv filename to the path.
            path = currentDirectory.Replace("\\bin\\Debug", "") + "\\mediaLibrary.csv";
        }


        /**
         * <summary>
         *      Registers a new book, puts the book data in the file.
         * </summary>
         * 
         * <param name="title"> the title of the book to be registered </param>
         * <param name="pages"> number of pages the book consists of </param>
         */
        public void RegisterBook(string title, int pages)
        {
            if (!File.Exists(path))
            {
                // create the file if it does not exist.
                File.Create(path);
            }

            // open a stream from the file and add book data.
            FileStream stream = new FileStream(path, FileMode.Append, FileAccess.Write);

            // create a printer that converts the text into raw bytes.
            StreamWriter sw = new StreamWriter(stream);

            // Add the text
            sw.WriteLine($"{title}: {pages} sidor");

            // close file, release resources
            sw.Dispose();
        }


        /**
         * <summary>
         *      Reads the contents of the file.
         * </summary>
         * 
         * <returns> The file contents </returns>
         */
        public string ReadFromFile()
        {
            string text = "";

            if (File.Exists(path))
            {
                // open a stream from the file
                FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);

                // create a reader that reads bytes in the stream as char-values.
                StreamReader read = new StreamReader(stream);

                // create a string that gets the file contents from the reader.
                text = read.ReadToEnd();

                read.Dispose();
            }

            return text;
        }


        /**
         * <summary>
         *       Registers a new Soundtrack, puts the soundtrack data in the file.
         * </summary>
         * 
         * 
         * <param name="title"> the soundtracks title</param>
         * <param name="playTime"> the playing time of the soundtrack </param>
         */
        public void RegisterSoundTrack(string title, double playTime)
        {
            if (!File.Exists(path))
            {
                File.Create(path);
            }

            FileStream stream = new FileStream(path, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(stream);

            sw.WriteLine($"{title}: {playTime} minuter");
            sw.Dispose();
        }


        /**
         * <summary>
         *      Register a new movie, puts the movie data in the file.
         * </summary>
         * 
         * <param name="title"> the title of the film</param>
         * <param name="resolution"> the movie resolution </param>
         * <param name="playTime"> the movie playtime </param>
         */
        public void RegisterMovie(string title, string resolution, double playTime)
        {
            if (!File.Exists(path))
            {
                File.Create(path);
            }


            FileStream stream = new FileStream(path, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(stream);

            sw.WriteLine($"{title}: {playTime} minuter, {resolution}");
            sw.Dispose();
        }
    }
}
