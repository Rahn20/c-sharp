using MediaPlaylistDAL;
using MediaPlaylistStore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaPlaylistBL
{
    /// <summary>
    ///  Provides business logic related to playlist items, including operations to add, remove and update playlists.
    /// </summary>
    public class PlaylistBL
    {
        private readonly IPlaylistOperations _playlist;

        /// <summary>
        ///   Constructor injection of the DAL dependency.
        /// </summary>
        public PlaylistBL(IPlaylistOperations playlist)
        {
            _playlist = playlist;
        }


        public async Task<IEnumerable<Playlist>> GetAllPlaylists() => await _playlist.GetAll();


        public async Task<Playlist> GetLastPlaylist() => await _playlist.GetLastCreatedPlaylist();


        public async Task<Playlist> GetPlaylistById(int playlistId)
        {
            if (playlistId <= 0) 
                throw new Exception($"ID must be greater than zero, playlistid: {playlistId}");

            return await _playlist.GetById(playlistId);
        }


        public async Task CreatePlaylist(string title, string description)
        {
            if (string.IsNullOrEmpty(title)) throw new Exception("Playlist title cannot be empty!"); 

            Playlist playlist = new Playlist
            {
                Title = title,
                Description = description,
                CreationDate = DateTime.Now,
                Medias = new List<Media>()
            };

            await _playlist.Add(playlist);
        }

        public async Task UpdatePlaylist(int playlistId, string title, string description)
        {
            if (string.IsNullOrEmpty(title) || playlistId <= 0)
                throw new Exception("Invalid playlist id or empty title!");

            Playlist playlist = new Playlist
            {
                PlaylistId = playlistId,
                Title = title,
                Description = description,
                LastModifiedDate = DateTime.Now,
            };

            await _playlist.Update(playlist);
        }

        public async Task DeletePlaylist(Playlist playlist) => await _playlist.Delete(playlist);
    }
}
