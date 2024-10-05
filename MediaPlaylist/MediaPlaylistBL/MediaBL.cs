using System;
using System.Collections.Generic;
using MediaPlaylistDAL;
using MediaPlaylistStore;

namespace MediaPlaylistBL
{
    /// <summary>
    ///   Provides business logic related to media items, including operations to add, remove and search for media within playlists.
    /// </summary>
    public class MediaBL
    {
        private readonly MediaDAL _media;

        public MediaBL() 
        {
            _media = new MediaDAL();
        }


        public async Task CreateMedia(int playlistId, Media data)
        {
            if (data is Song song)
            {
                await _media.Add(song, playlistId);
            }
            else if (data is Audiobook audiobook)
            {
                await _media.Add(audiobook, playlistId);
            }
            else    // For Podcast
            {
                await _media.Add((Podcast)data, playlistId);
            }
        }

        public async Task DeleteMedia(Media media) => await _media.Delete(media);

        public async Task<List<Media>> SearchMediaByType(int playlistId, AudioType type, string searchWord) 
        {
            return await _media.SearchMediaByAudioType(playlistId, type, searchWord);
        }

        public async Task UpdateMedia(Media media) => await _media.Update(media);
    }
}
