using Shell32;      // Add reference to Shell32.dll

namespace UtilitiesLib
{
    /// <summary>
    ///   Provides implementation for retrieving metadata information for a media file
    ///   using the Windows Shell "Shell32" and implements the <see cref="IShellHelper"/> interface.
    /// </summary>
    public class ShellHelper : IShellHelper
    {
        // Represents the folder containing the media file.
        private readonly Folder _folder;

        // Represents the media file within the folder.
        private readonly FolderItem _folderItem;


        public ShellHelper(string filePath)
        {
            Shell shell = new Shell();
            _folder = shell.NameSpace(Path.GetDirectoryName(filePath));
            _folderItem = _folder.ParseName(Path.GetFileName(filePath));
        }


        public string GetArtist()
        {
            // Shell32 Property, Column 13 indicates to Artist
            return _folder.GetDetailsOf(_folderItem, 13);
        }

        public TimeSpan GetFileDuration()
        {
            // Index 27 is used for media duration in Windows. Duration in HH:mm:ss
            string durationString = _folder.GetDetailsOf(_folderItem, 27);

            if (string.IsNullOrWhiteSpace(durationString)) return TimeSpan.Zero;

            if (TimeSpan.TryParse(durationString, out TimeSpan duration))
            {
                return duration;
            }
            else
            {
                throw new FormatException($"The provided string '{durationString}' is not a valid TimeSpan format.");
            }
        }

        public string GetFileName()
        {
            return _folder.GetDetailsOf(_folderItem, 0);
        }

        public string GetTitle()
        {
            return _folder.GetDetailsOf(_folderItem, 21);
        }

        public string GetAlbum()
        {
            return _folder.GetDetailsOf(_folderItem, 14);
        }

        public string GetGenre()
        {
            return _folder.GetDetailsOf(_folderItem, 16);
        }

        public string GetFileBitRate()
        {
            // Bit rate in kbps
            return _folder.GetDetailsOf(_folderItem, 28);
        }

        public string GetAuthor()
        {
            return _folder.GetDetailsOf(_folderItem, 20);
        }
    }
}
