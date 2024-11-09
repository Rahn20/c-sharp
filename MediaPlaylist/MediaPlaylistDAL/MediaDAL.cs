using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using MediaPlaylistStore;
using MediaPlaylistDAL.DbContexts;

namespace MediaPlaylistDAL
{
    /// <summary>
    ///   Provides data access layer (DAL) operations for the Media entity.
    ///   Implements the IDatabaseOperations interface to perform CRUD operations on media records in the database.
    /// </summary>
    public class MediaDAL : IMediaOperations
    {
        private MediaPlaylistContext _dbcontext;

        public MediaDAL(MediaPlaylistContext context)
        {
            _dbcontext = context;
        }

        public async Task<List<Media>> GetAll()
        {
            try
            {
                return await _dbcontext.Medias.Include(m => m.Playlist).ToListAsync<Media>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while retrieving the media from the database.", ex);
            }
        }

        public async Task<Media> GetById(int mediaId)
        {
            try
            {
                Media? media = await _dbcontext.Medias
                    .Include(m => m.Playlist)
                    .FirstOrDefaultAsync<Media>(m => m.MediaId == mediaId);

                if (media == null) throw new Exception("Media not found");
                return media;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while retrieving the specific media with specific ID {mediaId} from the database", ex);
            }

        }

        public async Task Add(Media media, int playlistId)
        {
            try
            {
                Playlist? playlist = await _dbcontext.Playlists
                    .Include(p => p.Medias)
                    .FirstOrDefaultAsync(m => m.PlaylistId == playlistId);

                if (playlist == null) throw new Exception("Playlist not found");

                playlist.Medias.Add(media);
                await _dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while adding a new media to the playlist, with playlist Id {playlistId}", ex);
            }

        }

        public async Task Update(Media media)
        {
            try
            {
                Media? existingMedia = await _dbcontext.Medias.FindAsync(media.MediaId);

                if (existingMedia == null) throw new Exception("Media not found!");

                existingMedia.Title = media.Title;
                existingMedia.Name = media.Name;

                if (media is Song updatedSong && existingMedia is Song existingSong)
                {
                    existingSong.Artist = updatedSong.Artist;
                    existingSong.Genre = updatedSong.Genre;
                    existingSong.Album = updatedSong.Album;
                }
                else if (media is Audiobook updatedAudiobook && existingMedia is Audiobook existingAudiobook)
                {
                    existingAudiobook.Publisher = updatedAudiobook.Publisher;
                    existingAudiobook.Author = updatedAudiobook.Author;
                    existingAudiobook.Genre = updatedAudiobook.Genre;
                }
                else if (media is Podcast updatedPodcast && existingMedia is Podcast existingPodcast)
                {
                    existingPodcast.Host = updatedPodcast.Host;
                    existingPodcast.EpisodeNumber = updatedPodcast.EpisodeNumber;
                    existingPodcast.Guests = updatedPodcast.Guests;
                }

                _dbcontext.Medias.Update(existingMedia);
                await _dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while updating the media with media ID {media.MediaId} in the database.", ex);
            }
        }

        public async Task Delete(Media media)
        {
            try
            {
                _dbcontext.Medias.Remove(media);
                await _dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"error while deleting the media with media id {media.MediaId} from the database", ex);
            }
        }

        public async Task<List<Media>> SearchMediaByAudioType(int playlistId, AudioType type, string searchStr)
        {
            try
            {
                return await _dbcontext.Medias
                    .Where(m => m.PlaylistId == playlistId)
                   .Include(m => m.Playlist)
                   .Where(m => m.AudioType == type &&
                    m.Name.Contains(searchStr) || m.Title.Contains(searchStr))
                   .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while searching for media items with search string: {searchStr}", ex);
            }
        }

        public async Task<List<Media>> GetMediaFor(int playlistId)
        {
            try
            {
                return await _dbcontext.Medias
                    .Where(m => m.PlaylistId == playlistId)
                    .Include(m => m.Playlist)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while retrieving the media for playlist ID {playlistId} from the database", ex);
            }
        }
    }
}
