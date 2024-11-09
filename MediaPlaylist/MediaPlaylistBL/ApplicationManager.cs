using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaPlaylistDAL;
using MediaPlaylistDAL.DbContexts;
using MediaPlaylistStore;

namespace MediaPlaylistBL
{
    /// <summary>
    ///   Provides business logic to manage operations related to playlist and media items.
    /// </summary>
    public class ApplicationManager
    {
        private readonly MediaBL _mediaObj;
        private readonly PlaylistBL _playlistObj;

        public ApplicationManager()
        {
            MediaDAL mediaDAL = new MediaDAL(new MediaPlaylistContext());
            PlaylistDAL playlistDAL = new PlaylistDAL(new MediaPlaylistContext());

            _mediaObj = new MediaBL(mediaDAL);
            _playlistObj = new PlaylistBL(playlistDAL);
        }

        /// <summary> Creates a new playlist with the specified title and description. </summary>
        /// <param name="title"> The title of the playlist </param>
        /// <param name="description"> The description of the playlist. </param>
        public async Task AddPlaylist(string title, string description) => await _playlistObj.CreatePlaylist(title, description);


        /// <summary> Gets all the playlist items from the data source. </summary>
        /// <returns> An enumerable collection of all Playlist items</returns>
        public async Task<IEnumerable<Playlist>> GetPlaylists() => await _playlistObj.GetAllPlaylists();


        /// <summary> Gets the most recently created playlist. </summary>
        /// <returns> The last created Playlist. </returns>
        public async Task<Playlist> GetLastPlaylist() => await _playlistObj.GetLastPlaylist();


        /// <summary> Retrieves a playlist based on the playlist unique identifier </summary>
        /// <param name="playlistId"> The unique identifier of the playlist. </param>
        /// <returns> The playlist object from the data source. </returns>
        public async Task<Playlist> GetPlaylistById(int playlistId) => await _playlistObj.GetPlaylistById(playlistId);


        /// <summary> Adds a new media to a specific playlist </summary>
        /// <param name="playlistID"> The unique identifier of the playlist to add media to. </param>
        /// <param name="data"> The media item to be added to the playlist. </param>
        public async Task AddMedia(int playlistID, Media media) => await _mediaObj.CreateMedia(playlistID, media);


        /// <summary> Updates an existing playlist with a new title and description. </summary>
        /// <param name="playlistId"> The unique identifier of the playlist to update </param>
        /// <param name="title"> The new title of the playlist </param>
        /// <param name="description"> The new description of the playlist </param>
        public async Task UpdatePlaylist(int playlistId, string title, string description)
        {
            await _playlistObj.UpdatePlaylist(playlistId, title, description);
        }

        /// <summary> Removes a media item from a specific playlist  </summary>
        /// <param name="media"> The unique identifier of the media item to remove </param>
        public async Task RemoveMedia(Media media) => await _mediaObj.DeleteMedia(media);


        /// <summary> Deletes a playlist by its ID. </summary>
        /// <param name="playlist"> The unique identifier of the playlist to delete. </param>
        public async Task RemovePlaylist(Playlist playlist) => await _playlistObj.DeletePlaylist(playlist);


        /// <summary>
        ///   Searches for media in a playlist based on media type and a search keyword.
        /// </summary>
        /// <param name="playlistId"> The unique identifier of the playlist to search in </param>
        /// <param name="mediaType"> The type of media to search for (ex. Song, Audiobook, Podcast) </param>
        /// <param name="searchWord"> The search term to use for filtering media items. </param>
        /// <returns> A list of items that match the search criteria. </returns>
        public async Task<List<Media>> SearchForMedia(int playlistId, AudioType mediaType, string searchWord)
        {
            return await _mediaObj.SearchMediaByType(playlistId, mediaType, searchWord);
        }

        /// <summary> Updates an existing media item </summary>
        /// <param name="media"> The media item to update. </param>
        public async Task UpdateMediaItem(Media media) => await _mediaObj.UpdateMedia(media);
    }
}

