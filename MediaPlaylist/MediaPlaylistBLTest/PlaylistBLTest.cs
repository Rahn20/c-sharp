using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;
using MediaPlaylistBL;
using MediaPlaylistDAL;
using MediaPlaylistStore;

namespace MediaPlaylistBLTest
{
    [TestClass]
    public class PlaylistBLTest
    {
        private Mock<IPlaylistOperations> _playlistDalMock;
        private PlaylistBL _playlist;
        private List<Playlist> _testData = TestDataStore.CreatePlaylistTestData();


        [TestInitialize]
        public void Setup()
        {
            // Create a mock for the IPlaylistOperations
            _playlistDalMock = new Mock<IPlaylistOperations>();

            // Inject the mock into the PlaylistBL
            _playlist = new PlaylistBL(_playlistDalMock.Object);
        }


        [TestMethod]
        public async Task GetPlaylistById_ValidId_RetrievesPlaylist()
        {
            // Arrange
            _playlistDalMock.Setup(dal => dal.GetById(1)).ReturnsAsync(_testData[0]);

            // Act
            var actualResult = await _playlist.GetPlaylistById(1);

            // Assert
            Assert.AreEqual(_testData[0], actualResult);
        }


        // This is the parameterized test method
        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        public async Task GetPlaylistById_InvalidPlaylistId_ExceptionThrown(int invalidId)
        {
            // Arrange
            _playlistDalMock.Setup(dal => dal.GetById(invalidId)).ReturnsAsync(_testData[0]);

            // Act and Assert
            var exception = await Assert.ThrowsExceptionAsync<Exception>(() => _playlist.GetPlaylistById(invalidId));

            // Assert the exception message
            Assert.AreEqual($"ID must be greater than zero, playlistid: {invalidId}", exception.Message);
        }


        [TestMethod]
        public async Task CreatePlaylist_Valid_PlaylistCreated()
        {
            await _playlist.CreatePlaylist(_testData[0].Title, _testData[0].Description);

            _playlistDalMock.Verify(dal => dal.Add(It.Is<Playlist>(p =>
                p.Title == _testData[0].Title &&
                p.Description == _testData[0].Description &&
                p.CreationDate.Date == _testData[0].CreationDate.Date &&
                p.LastModifiedDate == null &&
                p.Medias.Count == 0)), Times.Once);
        }


        [TestMethod]
        [DataRow("")]
        [DataRow(null)]
        public async Task CreatePlaylist_InvalidTitle_ShouldThrowException(dynamic invalid)
        {
            await Assert.ThrowsExceptionAsync<Exception>(() => _playlist.CreatePlaylist(invalid, "Some description"));
        }


        [TestMethod]
        public async Task UpdatePlaylist_Valid_PlaylistUpdated()
        {
            _playlistDalMock.Setup(dal => dal.GetById(_testData[0].PlaylistId)).ReturnsAsync(_testData[0]);
            await _playlist.UpdatePlaylist(_testData[0].PlaylistId, "Updated title", string.Empty);

            _playlistDalMock.Verify(dal => dal.Update(It.Is<Playlist>(p =>
                p.PlaylistId == _testData[0].PlaylistId &&
                p.Title == "Updated title" &&
                p.Description == string.Empty &&
                p.LastModifiedDate != null)), Times.Once);
        }


        [TestMethod]
        [DataRow(0, "")]
        [DataRow(1, "")]
        [DataRow(-1, "title")]
        [DataRow(2, null)]
        public async Task UpdatePlaylist_Invalid_ShouldThrowException(int invalidId, dynamic invalidTitle)
        {
            await Assert.ThrowsExceptionAsync<Exception>(() => _playlist.UpdatePlaylist(invalidId, invalidTitle, string.Empty));
        }
    }
}
