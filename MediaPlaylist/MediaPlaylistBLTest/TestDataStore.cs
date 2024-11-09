using MediaPlaylistStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlaylistBLTest
{
    internal static class TestDataStore
    {

        public static List<Playlist> CreatePlaylistTestData()
        {
            List<Playlist> playlists = new List<Playlist>();

            playlists.Add(new Playlist
            {
                PlaylistId = 1,
                Title = "title 1",
                CreationDate = DateTime.Now,
                Medias = new List<Media>(),
                Description = "Description 1"
            });

            playlists.Add(new Playlist
            {
                PlaylistId = 2,
                Title = "title 2",
                CreationDate = DateTime.Now,
                Medias = new List<Media>(),
                Description = "Description 2"
            });

            return playlists;
        }

        public static List<Media> CreateMediaTestData()
        {
            List<Media> mediaList = new List<Media>();

            mediaList.Add(new Song
            {
                PlaylistId = 1,
                MediaId = 1,
                Album = "Album 1",
                AudioType = AudioType.Song,
                FullPath = "",
                Duration = new TimeSpan(0, 3, 45),
                Size = 134f,
                Name = "Song name",
                Playlist = new Playlist(),
                Title = "Title 1",
            });

            mediaList.Add(new Audiobook
            {
                PlaylistId = 3,
                MediaId = 2,
                AudioType = AudioType.Audiobook,
                FullPath = "",
                Duration = new TimeSpan(0, 7, 45),
                Size = 176f,
                Name = "Audiobook name",
                Playlist = new Playlist(),
                Title = "Title 2",
                Author = "Audiobook Author"
            });

            mediaList.Add(new Podcast
            {
                PlaylistId = 3,
                MediaId = 3,
                AudioType = AudioType.Audiobook,
                FullPath = "",
                Duration = new TimeSpan(0, 3, 67),
                Size = 176f,
                Name = "Podcast name",
                Playlist = new Playlist(),
                Title = "Podcast Title",
                EpisodeNumber = 12,
            });

            return mediaList;
        }
    }
}
