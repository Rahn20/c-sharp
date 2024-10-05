using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using MediaPlaylistStore;
using MediaPlaylistDAL.DbContexts;
using System.Diagnostics;

namespace MediaPlaylistDAL
{
    /// <summary>
    ///  Provides data access layer (DAL) operations for the Playlist entity.
    ///  Implements the IDatabaseOperations interface to perform CRUD operations on playlist records in the database.
    /// </summary>
    public class PlaylistDAL : IDatabaseOperations<Playlist>
    {

        // Gets all the playlist entities from the database
        public async Task<List<Playlist>> GetAll()
        {
            List<Playlist> playlists = new List<Playlist>();

            using (var db = new MediaPlaylistContext())
            {
                try
                {
                    playlists = await db.Playlists
                        .Include(p => p.Medias)
                        .ToListAsync();
                }
                catch (Exception ex)
                {
                    //Debug.WriteLine(ex);
                    throw new Exception("Error while retrieving the playlist from the database.", ex);
                }
            }

            return playlists;
        }


        // Gets a specific playlist entity from the database based on the playlist ID
        public async Task<Playlist> GetById(int playlistId)
        {
            using (var db = new MediaPlaylistContext())
            {
                try
                {
                    Playlist? playlist = await db.Playlists
                        .Include(p => p.Medias)
                        .FirstOrDefaultAsync<Playlist>(p => p.PlaylistId == playlistId);

                    if (playlist == null) throw new Exception("Playlist nor found");
                    return playlist;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error while retrieving the specific playlist with specific ID {playlistId} from the database", ex);
                }
            }
        }


        // Adds a new playlist entity to the database. No id parameter needed.
        public async Task Add(Playlist playlist, int? id = null) 
        {
            using (var db = new MediaPlaylistContext())
            {
                try
                {
                    db.Playlists.Add(playlist);
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    //Debug.WriteLine(ex);
                    throw new Exception("Error while adding a new playlist to the database", ex);
                }
            }
        }


        // Updates an existing playlist entity in the database.
        public async Task Update(Playlist playlist)
        {
            using (var db = new MediaPlaylistContext())
            {
                try
                {
                    Playlist? existingPlaylist = await db.Playlists.FindAsync(playlist.PlaylistId);

                    if (existingPlaylist == null) throw new Exception("Playlist not found!");

                    existingPlaylist.Title = playlist.Title;
                    existingPlaylist.Description = playlist.Description;
                    existingPlaylist.LastModifiedDate = playlist.LastModifiedDate;

                    db.Playlists.Update(existingPlaylist);
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error while updating the playlist with playlist ID {playlist.PlaylistId} in the database.", ex);
                }
            }
        }

        // Deletes an existing playlist entity from the database.
        public async Task Delete(Playlist playlist)
        {
            using (var db = new MediaPlaylistContext())
            {
                try
                {
                    db.Playlists.Remove(playlist);
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error while deleting the playlist with playlist ID {playlist.PlaylistId} from the database", ex);
                }
            }
        }



        /// <summary>
        ///  Retrieves the last created playlist entity from the database.
        /// </summary>
        /// <returns> A task representing the asynchronous operation, containing the last created Playlist </returns>
        /// <exception cref="Exception"> Thrown if no playlist is found or if an error occurs while retrieving the playlist </exception>
        public async Task<Playlist> GetLastCreatedPlaylist()
        {
            using (var db = new MediaPlaylistContext())
            {
                try
                {
                    Playlist? playlist = await db.Playlists
                        .Include(p => p.Medias)
                        .OrderByDescending(p => p.PlaylistId)
                        .FirstOrDefaultAsync();

                    if (playlist == null) throw new Exception("Playlist not found");
                    return playlist;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error while retrieving the last created playlist from the database", ex);
                }
            }
        }

    }
}
