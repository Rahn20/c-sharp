using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaPlaylistDAL
{
    /// <summary>
    ///   Defines a set of asynchronous database operations (CRUD) for a specific entity type in the database.
    /// </summary>
    /// <typeparam name="T"> The type of entity the database operations will handle, ex: Playlist, Media </typeparam>
    public interface IDatabaseOperations<T>
    {
        /// <summary>
        ///  Retrieves all entities of type <T> from the database.
        /// </summary>
        /// <returns> A list of all attributes in the entity </returns>
        Task<List<T>> GetAll();

        /// <summary>
        ///  Retrieves an entity of type <T> from the database by its unique identifier.
        /// </summary>
        /// <param name="id"> The unique identifier of the entity. </param>
        /// <returns> The entity with the specified ID and all its attributes/columns </returns>
        Task<T> GetById(int id);


        /// <summary>
        ///  Adds a new entity of type T to the database.
        /// </summary>
        /// <param name="entity"> The entity to add. </param>
        /// <param name="id"> An optional identifier for the entity, if required. </param>
        Task Add(T entity, int? id = null);

        /// <summary>
        ///    Updates an existing entity of type T in the database.
        /// </summary>
        /// <param name="entity"> The entity to update. </param>
        Task Update(T entity);

        /// <summary>
        ///   Deletes an existing entity of type T from the database.
        /// </summary>
        /// <param name="entity"> The entity to delete </param>
        Task Delete(T entity);
    }
}
