namespace UtilitiesLib
{
    /// <summary>
    ///   Provides methods to retrieve various metadata information for a media file.
    /// </summary>
    public interface IShellHelper
    {
        /// <summary>
        ///   Gets the artist of the media file.
        /// </summary>
        /// <returns> The artist name as a string </returns>
        string GetArtist();

        /// <summary>
        ///  Gets the duration of the media file.
        /// </summary>
        /// <returns> The duration of the file as a Timespan </returns>
        TimeSpan GetFileDuration();

        /// <summary>
        ///   Gets the file name of the media file.
        /// </summary>
        /// <returns>The name of the file as a string </returns>
        string GetFileName();

        /// <summary>
        ///   Gets the title of the media file.
        /// </summary>
        /// <returns> The title of the file as a string. </returns>
        string GetTitle();

        /// <summary>
        ///  Gets the album name associated with the media file.
        /// </summary>
        /// <returns> The album name as a string. </returns>
        string GetAlbum();

        /// <summary>
        ///  Gets the genre of the media file.
        /// </summary>
        /// <returns> The genre as a string </returns>
        string GetGenre();

        /// <summary>
        ///   Gets the bit rate of the media file.
        /// </summary>
        /// <returns> The file's bit rate in kilobits per second (kbps) as a string. </returns>
        string GetFileBitRate();

        /// <summary>
        ///  Gets the author of the media file.
        /// </summary>
        /// <returns> The author name as a string </returns>
        string GetAuthor();
    }
}
