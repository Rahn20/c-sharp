using System;
using System.ComponentModel.DataAnnotations;

namespace MediaPlaylistStore
{
    /// <summary>
    ///   The DTO (Data Transfer Object) layer Represents a media playlist containing a collection of tracks and relevant data.
    ///   The class only has data properties, which are used for transferring data between layers.
    /// </summary>
    public class Playlist
    {
        /// <summary>
        ///   The Playlist ID
        /// </summary>
        [Key]
        public int PlaylistId { get; set; }

        /// <summary>
        ///   The name of the playlist
        /// </summary>
        [MaxLength(15)]
        [Required]
        public string Title { get; set; }

        /// <summary>
        ///  A brief description of the playlist, can be null
        /// </summary>
        [MaxLength(70)]
        public string? Description { get; set; }

        /// <summary>
        ///   The date and time the playlist was created.
        /// </summary>
        [Required]
        public DateTime CreationDate { get; set; }

        /// <summary>   
        ///   The last time the playlist was updated (ex. songs added/removed), can be null
        /// </summary>
        public DateTime? LastModifiedDate { get; set; }

        /// <summary>
        ///  List of tracks in the playlist. Collection navigation. 
        ///  A Playlist can have many Media entries (One-to-Many relationship).
        /// </summary>
        [Required]
        public List<Media> Medias { get; set; }
    }
}
