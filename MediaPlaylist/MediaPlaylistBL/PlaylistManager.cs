using MediaPlaylistStore;
using UtilitiesLib;
using MediaPlaylistDAL;

namespace MediaPlaylistBL
{
    /// <summary>
    ///   Provides business logic to manage operations related to playlists, such as creating, updating, removing, and retrieving.
    /// </summary>
    public class PlaylistManager : DictionaryManager<Playlist>
    {
        private readonly MediaBL mediaObj;

        // Tracks the ID of the last created playlist.
        private int lastCreatedID;

        public PlaylistManager()
        {
            lastCreatedID = 0;
            mediaObj = new MediaBL();
        }


        /// <summary>
        ///   Adds a new media item to a specified playlist.
        /// </summary>
        /// <param name="playlistID"> The unique identifier of the playlist to add media to. </param>
        /// <param name="data"> The media item to be added to the playlist. </param>
        /// <exception cref="ArgumentException"> Thrown if the playlist with the given ID is not found.</exception>
        public void AddMediaToPlaylist(int playlistID, Media data)
        {
            Playlist? getPlaylist = GetById(playlistID);

            if (getPlaylist == null) throw new ArgumentException("Playlist ID not found", nameof(getPlaylist));

            mediaObj.AddMedia(getPlaylist, data);
        }


        /// <summary>
        ///    Removes a media item from a specified playlist.
        /// </summary>
        /// <param name="playlistID"> The unique identifier of the playlist from which to remove media. </param>
        /// <param name="mediaId"> The unique identifier of the media item to remove </param>
        /// <exception cref="ArgumentException"> Thrown if the playlist with the given ID is not found </exception>
        public void RemoveMediaFromPlaylist(int playlistID, int mediaId)
        {
            Playlist? getPlaylist = GetById(playlistID);

            if (getPlaylist == null) throw new ArgumentException("Playlist ID not found", nameof(getPlaylist));

            mediaObj.RemoveMedia(getPlaylist, mediaId);
        }


        /// <summary>
        ///   Creates a new playlist with the specified title and description.
        /// </summary>
        /// <param name="title"> The title of the playlist </param>
        /// <param name="description"> The description of the playlist. </param>
        public void CreatePlaylist(string title, string description)
        {
            // Increase the ID by 1
            lastCreatedID++;
            Playlist createPlaylist = new Playlist()
            {
                Id = lastCreatedID,
                Title = title,
                Description = description,
                CreationDate = DateTime.Now,
                Medias = new List<Media>()
            };

            AddToDictionary(createPlaylist);
        }

        /// <summary>
        ///   Gets the most recently created playlist.
        /// </summary>
        /// <returns> The last created Playlist. </returns>
        public Playlist GetLastPlaylist() => GetLastValue();


        /// <summary>
        ///   Deletes a playlist by its ID.
        /// </summary>
        /// <param name="playlistID"> The unique identifier of the playlist to delete. </param>
        public void DeletePlaylist(int playlistID) => DeleteById(playlistID);


        /// <summary>
        ///   Updates an existing playlist with new title, description, and media items.
        /// </summary>
        /// <param name="playlistId"> The unique identifier of the playlist to update </param>
        /// <param name="title"> The new title of the playlist </param>
        /// <param name="description"> The new description of the playlist </param>
        /// <param name="mediaItems">  The updated list of media items for the playlist </param>
        /// <exception cref="ArgumentException"> Thrown if the playlist with the given ID is not found </exception>
        public void UpdatePlaylist(int playlistId, string title, string description, List<Media> mediaItems)
        {
            Playlist? getPlaylist = GetById(playlistId);

            if (getPlaylist == null) throw new ArgumentException("Playlist ID not found", nameof(getPlaylist));

            getPlaylist.Title = title;
            getPlaylist.Description = description;
            getPlaylist.LastModifiedDate = DateTime.Now;
            getPlaylist.Medias = mediaItems;

            ChangeById(playlistId, getPlaylist);
        }


        /// <summary>
        ///   Searches for media in a playlist based on media type and a search keyword.
        /// </summary>
        /// <param name="playlistId"> The unique identifier of the playlist to search in </param>
        /// <param name="mediaType"> The type of media to search for (ex. Song, Audiobook, Podcast) </param>
        /// <param name="searchWord"> The search term to use for filtering media items. </param>
        /// <returns> An IEnumerable of items that match the search criteria. </returns>
        /// <exception cref="ArgumentException"> Thrown if the playlist with the given ID is not found </exception>
        public IEnumerable<Media> SearchForMedia(int playlistId, AudioType mediaType, string searchWord)
        {
            Playlist? getPlaylist = GetById(playlistId);

            if (getPlaylist == null) throw new ArgumentException("Playlist ID not found", nameof(getPlaylist));

            return mediaObj.SearchMediaByType(getPlaylist.Medias, mediaType, searchWord);
        }
    }
}
