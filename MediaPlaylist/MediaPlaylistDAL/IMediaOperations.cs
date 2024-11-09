using MediaPlaylistStore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlaylistDAL
{
    /// <summary>
    ///   Defines a set of asynchronous database operations (CRUD) for Media type in the database.
    /// </summary>
    public interface IMediaOperations
    {
        /// <summary>
        ///   Retrieves all entities of Media type from the database.
        /// </summary>
        /// <returns> A list of all attributes in the entity </returns>
        Task<List<Media>> GetAll();

        /// <summary>
        ///   Retrieves an entity of type Media from the database by its unique identifier.
        /// </summary>
        /// <param name="mediaId"> The unique identifier of the Media. </param>
        /// <returns> The entity with the specified ID and all its attributes/columns </returns>
        Task<Media> GetById(int mediaId);

        /// <summary>
        ///  Adds a new entity of type Media to the database.
        /// </summary>
        /// <param name="entity"> The entity to add. </param>
        /// <param name="playlistId">  The playlist ID </param>
        Task Add(Media entity, int playlistId);

        /// <summary>
        ///    Updates an existing entity of type Media in the database.
        /// </summary>
        /// <param name="entity"> The entity to update. </param>
        Task Update(Media entity);

        /// <summary>
        ///   Deletes an existing entity of type Media from the database.
        /// </summary>
        /// <param name="entity"> The entity to delete </param>
        Task Delete(Media entity);


        /// <summary>
        ///  Searches for Media items in a specific playlist by their audio type and a search string,
        ///  and checks if the name or title contains the provided search string.
        /// </summary>
        /// <param name="playlistId"> The unique identifier of the playlist to search within. </param>
        /// <param name="type"> The audio type to filter the media items. </param>
        /// <param name="searchStr"> The search string used to match against the media item's name and title. </param>
        /// <returns> A list of matching Media items.</returns>
        Task<List<Media>> SearchMediaByAudioType(int playlistId, AudioType type, string searchStr);


        /// <summary>
        ///  Retrieves a list of Media items associated with a specific playlist and includes related playlist information.
        /// </summary>
        /// <param name="playlistId"> The unique identifier of the playlist for which to retrieve media items. </param>
        /// <returns> A list of Media items for the specified playlist. </returns>
        Task<List<Media>> GetMediaFor(int playlistId);
    }
}
