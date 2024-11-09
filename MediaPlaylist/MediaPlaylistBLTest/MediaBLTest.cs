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
    public class MediaBLTest
    {
        private Mock<IMediaOperations> _mediaDalMock;
        private MediaBL _media;
        private List<Media> _testData = TestDataStore.CreateMediaTestData();


        [TestInitialize]
        public void Setup()
        {
            // Create a mock for the IMediaOperations
            _mediaDalMock = new Mock<IMediaOperations>();

            // Inject the mock into the MediaBL
            _media = new MediaBL(_mediaDalMock.Object);
        }


        [TestMethod]
        public async Task CreateMedia_SongTypeValid_SongCreated()
        {
            // Act
            await _media.CreateMedia(_testData[0].PlaylistId, _testData[0]);

            // Assert
            _mediaDalMock.Verify(dal => dal.Add(_testData[0], _testData[0].PlaylistId), Times.Once);
        }

        [TestMethod]
        public async Task CreateMedia_PodcastTypeValid_PodcastCreated()
        {
            await _media.CreateMedia(_testData[2].PlaylistId, _testData[2]);

            _mediaDalMock.Verify(dal => dal.Add(_testData[2], _testData[2].PlaylistId), Times.Once);
        }

        [TestMethod]
        public async Task CreateMedia_AudiobookTypeValid_AudiobookCreated()
        {
            await _media.CreateMedia(_testData[2].PlaylistId, _testData[1]);

            _mediaDalMock.Verify(dal => dal.Add(_testData[1], _testData[2].PlaylistId), Times.Once);
        }


        [TestMethod]
        [DataRow(0)]
        [DataRow(-2)]
        public async Task SearchMediaByType_InvalidPlaylistId_ShouldThrowException(int playlistId)
        {
            await Assert.ThrowsExceptionAsync<Exception>(() => _media.SearchMediaByType(playlistId, AudioType.Song, "search"));
        }
    }
}
