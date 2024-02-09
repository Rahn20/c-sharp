using System;
using System.IO;
using System.Reflection;


namespace MediaLibrary
{
    internal class Library
    {
        // Specify the path to the CSV file.
        private readonly string path;


        /// <summary>
        ///  Sets the variable 'path' to the CSV file path.
        /// </summary>
        public Library()
        {
            // Return the URL to the main directory containing "/bin/Debug."
            var currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            // Replace "bin\Debug" with an empty string and append the CSV filename to the path.
            path = currentDirectory.Replace("\\bin\\Debug", "") + "\\mediaLibrary.csv";
        }


        /// <summary>
        ///  Registers a new book and adds its data to the file
        /// </summary>
        /// <param name="pages"> The number of pages the book consists of. </param>
        /// <param name="title"> the title of the book to be registered </param>
        public void RegisterBook(string title, int pages)
        {
            if (!File.Exists(path)) { File.Create(path); }

            // Open a stream from the file and add book data.
            FileStream stream = new FileStream(path, FileMode.Append, FileAccess.Write);

            // Create a printer that converts the text into raw bytes.
            StreamWriter sw = new StreamWriter(stream);

            sw.WriteLine($"{title}: {pages} sidor");

            // close file, release resources
            sw.Dispose();
        }


        /// <summary>
        ///  Reads the contents of the file
        /// </summary>
        /// 
        /// <returns> The file contents</returns>
        public string ReadFromFile()
        {
            string text = "";

            if (File.Exists(path))
            {
                // Open a stream from the file.
                FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);

                // Create a reader that reads bytes in the stream as char-values.
                StreamReader read = new StreamReader(stream);

                text = read.ReadToEnd();
                read.Dispose();
            }
            return text;
        }


        /// <summary>
        ///  Registers a new soundtrack and adds its data to the file
        /// </summary>
        /// 
        /// <param name="title"> The title of the soundtrack </param>
        /// <param name="playTime"> The playing time of the soundtrack </param>
        public void RegisterSoundTrack(string title, double playTime)
        {
            if (!File.Exists(path)) { File.Create(path); }

            FileStream stream = new FileStream(path, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(stream);

            sw.WriteLine($"{title}: {playTime} minuter");
            sw.Dispose();
        }


        /// <summary>
        ///  Registers a new movie and adds its data to the file.
        /// </summary>
        /// 
        /// <param name="playTime"> The movie playtime </param>
        /// <param name="title"> The title of the film </param>
        /// <param name="resolution"> The movie resolution </param>
        public void RegisterMovie(string title, string resolution, double playTime)
        {
            if (!File.Exists(path)) { File.Create(path); }

            FileStream stream = new FileStream(path, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(stream);

            sw.WriteLine($"{title}: {playTime} minuter, {resolution}");
            sw.Dispose();
        }
    }
}
