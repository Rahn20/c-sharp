using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
//
using MediaPlaylistStore;
using MediaPlaylistDAL.DbContexts;

namespace MediaPlaylistDAL
{
    /// <summary>
    ///   Provides data access layer (DAL) operations for the Media entity.
    ///   Implements the IDatabaseOperations interface to perform CRUD operations on media records in the database.
    /// </summary>
    public class MediaDAL : IDatabaseOperations<Media>
    {
        public async Task<List<Media>> GetAll()
        {
            List<Media> medias = new List<Media>();

            using (var db = new MediaPlaylistContext())
            {
                try
                {
                    medias = await db.Medias.Include(m => m.Playlist).ToListAsync<Media>();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error while retrieving the media from the database.", ex);
                }
            }

            return medias;
        }

        public async Task<Media> GetById(int mediaId)
        {
            using (var db = new MediaPlaylistContext())
            {
                try
                {
                    Media? media = await db.Medias
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
        }

        public async Task Add(Media media, int? playlistId)
        {
            using (var db = new MediaPlaylistContext())
            {
                try
                {
                    Playlist? playlist = await db.Playlists
                        .Include(p => p.Medias)
                        .FirstOrDefaultAsync(m => m.PlaylistId == playlistId);

                    if (playlist == null) throw new Exception("Playlist not found");

                    playlist.Medias.Add(media);
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error while adding a new media to the playlist, with playlist Id {playlistId}", ex);
                }
            }
        }

        public async Task Update(Media media)
        {
            using (var db = new MediaPlaylistContext())
            {
                try
                {
                    Media? existingMedia = await db.Medias.FindAsync(media.MediaId);

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

                    db.Medias.Update(existingMedia);
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error while updating the media with media ID {media.MediaId} in the database.", ex);
                }
            }
        }

        public async Task Delete(Media media)
        {
            using (var db = new MediaPlaylistContext())
            {
                try
                {
                    db.Medias.Remove(media);
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error while deleting the media with media ID {media.MediaId} from the database", ex);
                }
            }
        }

        /// <summary>
        ///  Asynchronously searches for Media items in a specific playlist by their audio type and a search string,
        ///  and checks if the name or title contains the provided search string.
        /// </summary>
        /// <param name="playlistId"> The unique identifier of the playlist to search within. </param>
        /// <param name="type"> The audio type to filter the media items. </param>
        /// <param name="searchStr"> The search string used to match against the media item's name and title. </param>
        /// <returns> A list of matching Media items.</returns>
        /// <exception cref="Exception"> Thrown if an error occurs during the search operation. </exception>
        public async Task<List<Media>> SearchMediaByAudioType(int playlistId, AudioType type, string searchStr)
        {
            List<Media> medias = new List<Media>();

            using (var db = new MediaPlaylistContext())
            {
                try
                {
                    medias = await db.Medias
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

            return medias;
        }


        /// <summary>
        ///   Asynchronously retrieves a list of Media items associated with a specific playlist and includes related playlist information.
        /// </summary>
        /// <param name="playlistId"> The unique identifier of the playlist for which to retrieve media items. </param>
        /// <returns>A list of Media items for the specified playlist.</returns>
        /// <exception cref="Exception"> Thrown if an error occurs during the retrieval operation. </exception>
        public async Task<List<Media>> GetMediaFor(int playlistId)
        {
            List<Media> medias = new List<Media>();

            using (var db = new MediaPlaylistContext())
            {
                try
                {
                    medias = await db.Medias
                        .Where(m => m.PlaylistId == playlistId)
                        .Include(m => m.Playlist)
                        .ToListAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error while retrieving the media for playlist ID {playlistId} from the database", ex);
                }
            }

            return medias;
        }
    }
}
