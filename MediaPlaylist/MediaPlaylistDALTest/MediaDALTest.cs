using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

using MediaPlaylistDAL.DbContexts;
using MediaPlaylistDAL;
using MediaPlaylistStore;
using Microsoft.EntityFrameworkCore;

namespace MediaPlaylistDALTest
{
    [TestFixture]
    public class MediaDALTest
    {
        private MediaPlaylistContext _context;
        private MediaDAL _mediaDal;

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

            var playlist = new Playlist
            {
                Title = "title 1",
                CreationDate = DateTime.Now,
                Medias = new List<Media>(),
                Description = "Description 1"
            };

            _context.Playlists.Add(playlist);
            _context.SaveChanges();

            _context.Medias.Add(new Song
            {
                PlaylistId = playlist.PlaylistId,
                Album = "Album 1",
                AudioType = AudioType.Song,
                FullPath = "/path/to/new/song",
                Duration = new TimeSpan(0, 3, 45),
                Size = 134f,
                Name = "Song name",
                Title = "Title 1",
            });

            _context.SaveChanges();
            _mediaDal = new MediaDAL(_context);
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
        public async Task GetAll_Valid_ReturnsAllMedia()
        {
            // Act
            var actual = await _mediaDal.GetAll();

            // Assert
            Assert.That(actual.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task GetById_Valid_ReturnsMedia()
        {
            var actual = await _mediaDal.GetById(1);

            Assert.That(actual.Name, Is.EqualTo("Song name"));
            Assert.That(actual.AudioType, Is.EqualTo(AudioType.Song));
        }

        [Test]
        [TestCase(0, "Error while retrieving the specific media with specific ID 0 from the database")]
        [TestCase(-2, "Error while retrieving the specific media with specific ID -2 from the database")]
        public void GetById_InvalidID_ShouldThrowException(int mediaId, string message)
        {
            var excep = Assert.ThrowsAsync<Exception>(async () => await _mediaDal.GetById(mediaId));
            Assert.That(excep.Message, Is.EqualTo(message));
        }


        [Test]
        public async Task Add_Valid_MediaAdded()
        {
            await _mediaDal.Add(new Podcast
            {
                AudioType = AudioType.Podcast,
                FullPath = "",
                Duration = new TimeSpan(0, 3, 45),
                Size = 134f,
                Name = "Podcast name",
                Title = "Podcast Title",
                Host = "Host name"
            }, playlistId: 1);

            Assert.That(_context.Medias.Count, Is.EqualTo(2));
        }

        [Test]
        public void Add_MissingRequiredProperty_ShouldThrowException()
        {
            // podcast required property "Host"
            Assert.ThrowsAsync<Exception>(async () => await _mediaDal.Add(new Podcast
            {
                AudioType = AudioType.Podcast,
                FullPath = "",
                Duration = new TimeSpan(days: 0, hours: 0, minutes: 30, seconds: 0),
                Size = 134f,
                Name = "Podcast name",
                Title = "Podcast Title",
            }, playlistId: 1));
        }


        [Test]
        public async Task Update_Valid_MediaUpdated()
        {
            await _mediaDal.Update(new Song
            {
                MediaId = 1,
                Album = "Updated Album",
                AudioType = AudioType.Song,
                FullPath = "/path/to/new/song",
                Duration = new TimeSpan(0, 3, 45),
                Size = 134f,
                Name = "Updated Song name",
                Title = "Updated Title",
            });

            Assert.That(_context.Medias.Last().Title, Is.EqualTo("Updated Title"));
            Assert.That(_context.Medias.Last().Name, Is.EqualTo("Updated Song name"));
            Assert.That(_context.Medias.Last().AudioType, Is.EqualTo(AudioType.Song));
        }

        [Test]
        public void Update_InvalidMediaId_ShouldThrowException()
        {
            var excep = Assert.ThrowsAsync<Exception>(async () => await _mediaDal.Update(new Song
            {
                MediaId = 100,
                Album = "Updated Album",
                AudioType = AudioType.Song,
                FullPath = "/path/to/new/song",
                Duration = new TimeSpan(0, 3, 45),
                Size = 134f,
                Name = "Updated Song name",
                Title = "Updated Title",
            }));
        }

        [Test]
        public void Delete_MediaNotFound_ShouldThrowException()
        {
            var excep = Assert.ThrowsAsync<Exception>(async () => await _mediaDal.Delete(new Song
            {
                MediaId = 100,
                Album = "Album",
                AudioType = AudioType.Song,
                FullPath = "/path/to/new/song",
                Duration = new TimeSpan(0, 3, 45),
                Size = 134f,
                Name = "Song name",
                Title = "Title",
            }));
        }
    }
}
