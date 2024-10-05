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
        private readonly PlaylistDAL _playlist;


        public PlaylistBL()
        {
            _playlist = new PlaylistDAL();
        }


        public async Task<IEnumerable<Playlist>> GetAllPlaylists() => await _playlist.GetAll();

        public async Task<Playlist> GetLastPlaylist() => await _playlist.GetLastCreatedPlaylist();

        public async Task<Playlist> GetPlaylistById(int playlistId) => await _playlist.GetById(playlistId);


        public async Task CreatePlaylist(string title, string description)
        {
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
