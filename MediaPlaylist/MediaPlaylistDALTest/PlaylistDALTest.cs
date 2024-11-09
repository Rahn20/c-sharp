using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

using MediaPlaylistDAL.DbContexts;
using MediaPlaylistDAL;
using MediaPlaylistStore;

namespace MediaPlaylistDALTest
{
    [TestFixture]
    public class PlaylistDALTest
    {
        private MediaPlaylistContext _context;
        private PlaylistDAL _playlistDal;


        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            // Set environment variable for the entire test run
            Environment.SetEnvironmentVariable("MEDIAPLAYLIST_DB_PROVIDER", "InMemory");
        }

        [SetUp]
        public void Setup()
        {
            _context = new MediaPlaylistContext();

            _context.Playlists.Add(new Playlist
            {
                Title = "title 1",
                CreationDate = DateTime.Now,
                Medias = new List<Media>(),
                Description = "Description 1"
            });

            _context.SaveChanges();
            _playlistDal = new PlaylistDAL(_context);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Environment.SetEnvironmentVariable("MEDIAPLAYLIST_DB_PROVIDER", string.Empty);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }


        [Test]
        public async Task GetAll_Valid_ReturnsAllPlaylists()
        {
            // Act
            var actual = await _playlistDal.GetAll();

            // Assert
            Assert.That(actual.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task GetById_Valid_ReturnsPlaylist()
        {
            var actual = await _playlistDal.GetById(1);

            Assert.That(actual.Title, Is.EqualTo("title 1"));
            Assert.That(actual.PlaylistId, Is.EqualTo(1));
        }

        [Test]
        [TestCase(0, "Error while retrieving the specific playlist with specific ID 0 from the database")]
        [TestCase(2, "Error while retrieving the specific playlist with specific ID 2 from the database")]
        public void GetById_InvalidID_ShouldThrowException(int playlistId, string message)
        {
            var excep = Assert.ThrowsAsync<Exception>(async () => await _playlistDal.GetById(playlistId));
            Assert.That(excep.Message, Is.EqualTo(message));
        }


        [Test]
        public async Task Add_Valid_PlaylistAdded()
        {
            await _playlistDal.Add(new Playlist
            {
                Title = "Playlist title",
                CreationDate = DateTime.Now,
                Medias = new List<Media>(),
                Description = "Description 2"
            });

            Assert.That(_context.Playlists.Count, Is.EqualTo(2));
        }

        [Test]
        public void Add_AddedPlaylistId_ShouldThrowException()
        {
            var excep = Assert.ThrowsAsync<Exception>(async () => await _playlistDal.Add(new Playlist
            {
                PlaylistId = 1,
                Title = "Playlist title",
                CreationDate = DateTime.Now,
                Medias = new List<Media>(),
                Description = "Description 2"
            }));

            Assert.That(excep.Message, Is.EqualTo("Error while adding a new playlist to the database"));
        }


        [Test]
        public async Task Update_Valid_PlaylistUpdated()
        {
            await _playlistDal.Update(new Playlist
            {
                PlaylistId = 1,
                Title = "Updated title",
                LastModifiedDate = DateTime.Now,
                Description = "Updated Description"
            });

            Assert.That(_context.Playlists.Last().Title, Is.EqualTo("Updated title"));
            Assert.That(_context.Playlists.Last().Description, Is.EqualTo("Updated Description"));
            Assert.IsNotNull(_context.Playlists.Last().LastModifiedDate);
        }

        [Test]
        public void Update_InvalidPlaylistId_ShouldThrowException()
        {
            var excep = Assert.ThrowsAsync<Exception>(async () => await _playlistDal.Update(new Playlist
            {
                PlaylistId = 4,
                Title = "Updated title",
                LastModifiedDate = DateTime.Now,
                Description = "Updated Description"
            }));
        }

        [Test]
        public void Delete_PlaylistNotFound_ShouldThrowException()
        {
            var excep = Assert.ThrowsAsync<Exception>(async () => await _playlistDal.Delete(new Playlist
            {
                PlaylistId = 4,
                Title = "title",
                LastModifiedDate = DateTime.Now,
                Description = "Description"
            }));
        }

        [Test]
        public async Task GetLastCreatedPlaylist_Valid_ReturnsPlaylist()
        {
            var actual = await _playlistDal.GetLastCreatedPlaylist();

            Assert.That(actual.Title, Is.EqualTo("title 1"));
            Assert.That(actual.PlaylistId, Is.EqualTo(1));
        }
    }
}
