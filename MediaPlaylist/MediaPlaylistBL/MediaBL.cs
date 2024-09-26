using MediaPlaylistStore;

namespace MediaPlaylistBL
{
    /// <summary>
    ///   Provides business logic related to media items, including operations to add, remove and search for media within playlists.
    /// </summary>
    public class MediaBL
    {
        // Tracks the ID of the last created media item.
        private int lastCreatedMediaID;

        public MediaBL() 
        {
            lastCreatedMediaID = 0;
        }


        /// <summary>
        ///   Adds a media item to a specified playlist.
        /// </summary>
        /// <param name="playlistData"> The playlist object to which the media item will be added. </param>
        /// <param name="data"> The media item to add (can be a Song, Audiobook, or Podcast) </param>
        public void AddMedia(Playlist playlistData, Media data)
        {
            // Increase the ID by 1 each time a new media is added/created
            lastCreatedMediaID++;
            data.Id = lastCreatedMediaID;

            if (data is Song song)
            {
                playlistData.Medias.Add(song);
            }
            else if (data is Audiobook audiobook)
            {
                playlistData.Medias.Add(audiobook);
            }
            else    // For Podcast
            {
                playlistData.Medias.Add((Podcast)data);
            }
        }

        /// <summary>
        ///   Removes a media item from a specified playlist.
        /// </summary>
        /// <param name="playlist"> The playlist from which the media item will be removed. </param>
        /// <param name="mediaId"> The unique identifier of the media item to remove </param>
        public void RemoveMedia(Playlist playlist, int mediaId)
        {
            Media? getMedia = playlist.Medias.Find(item => item.Id == mediaId);
            
            if (getMedia != null) 
            {
                playlist.Medias.Remove(getMedia);
            } 
            else
            {
                throw new ArgumentException("The media item could not be removed.", nameof(getMedia));
            }
        }


        /// <summary>
        ///   Searches for media items within a list based on media type and a search keyword.
        /// </summary>
        /// <param name="medias"> The list of media items to search through </param>
        /// <param name="type"> The type of media to filter (e.g., Song, Audiobook, Podcast) </param>
        /// <param name="searchWord">The keyword to search for in the media item names and titles </param>
        /// <returns>An IEnumerable of media items that match the specified type and contain the search word. </returns>
        public IEnumerable<Media> SearchMediaByType(List<Media> medias, AudioType type, string searchWord) 
        {
            return medias.Where<Media>(item => item.AudioType == type && 
                 (item.Name.Contains(searchWord, StringComparison.OrdinalIgnoreCase) ||
                    item.Title.Contains(searchWord, StringComparison.OrdinalIgnoreCase)));
        }
    }
}
