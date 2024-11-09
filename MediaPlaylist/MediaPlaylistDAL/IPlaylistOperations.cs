using MediaPlaylistStore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlaylistDAL
{
    /// <summary>
    ///   Defines a set of asynchronous database operations (CRUD) for Playlist type in the database.
    /// </summary>
    public interface IPlaylistOperations
    {
        /// <summary>
        ///   Retrieves all entities of Playlist type from the database.
        /// </summary>
        /// <returns> A list of all attributes in the entity </returns>
        Task<List<Playlist>> GetAll();

        /// <summary>
        ///   Retrieves an entity of type Playlist from the database by its unique identifier.
        /// </summary>
        /// <param name="playlistId"> The unique identifier of the Playlist. </param>
        /// <returns> The entity with the specified ID and all its attributes/columns </returns>
        Task<Playlist> GetById(int playlistId);

        /// <summary>
        ///  Adds a new entity of type Playlist to the database.
        /// </summary>
        /// <param name="entity"> The entity to add. </param>
        Task Add(Playlist entity);

        /// <summary>
        ///    Updates an existing entity of type Playlist in the database.
        /// </summary>
        /// <param name="entity"> The entity to update. </param>
        Task Update(Playlist entity);

        /// <summary>
        ///   Deletes an existing entity of type Playlist from the database.
        /// </summary>
        /// <param name="entity"> The entity to delete </param>
        Task Delete(Playlist entity);


        /// <summary>
        ///    Retrieves the last created playlist entity from the database.
        /// </summary>
        /// <returns>  The last created entity </returns>
        Task<Playlist> GetLastCreatedPlaylist();
    }
}