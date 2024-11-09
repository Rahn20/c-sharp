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
    public class PlaylistDAL : IPlaylistOperations
    {
        private readonly MediaPlaylistContext _dbContext;


        public PlaylistDAL(MediaPlaylistContext context)
        {
            _dbContext = context;
        }

        public async Task<List<Playlist>> GetAll()
        {
            try
            {
                return await _dbContext.Playlists
                    .Include(p => p.Medias)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex);
                throw new Exception("Error while retrieving the playlist from the database.", ex);
            }
        }

        public async Task<Playlist> GetById(int playlistId)
        {
            try
            {
                Playlist? playlist = await _dbContext.Playlists
                    .Include(p => p.Medias)
                    .FirstOrDefaultAsync<Playlist>(p => p.PlaylistId == playlistId);

                if (playlist == null) throw new Exception("Playlist not found");
                return playlist;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while retrieving the specific playlist with specific ID {playlistId} from the database", ex);
            }

        }

        public async Task Add(Playlist playlist) 
        {
            try
            {
                _dbContext.Playlists.Add(playlist);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex);
                throw new Exception("Error while adding a new playlist to the database", ex);
            }

        }

        public async Task Update(Playlist playlist)
        {
            try
            {
                Playlist? existingPlaylist = await _dbContext.Playlists.FindAsync(playlist.PlaylistId);

                if (existingPlaylist == null) throw new Exception("Playlist not found!");

                existingPlaylist.Title = playlist.Title;
                existingPlaylist.Description = playlist.Description;
                existingPlaylist.LastModifiedDate = playlist.LastModifiedDate;

                _dbContext.Playlists.Update(existingPlaylist);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while updating the playlist with playlist ID {playlist.PlaylistId} in the database.", ex);
            }

        }

        public async Task Delete(Playlist playlist)
        {
            try
            {
                _dbContext.Playlists.Remove(playlist);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while deleting the playlist with playlist ID {playlist.PlaylistId} from the database", ex);
            }

        }

        public async Task<Playlist> GetLastCreatedPlaylist()
        {
            try
            {
                Playlist? playlist = await _dbContext.Playlists
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
