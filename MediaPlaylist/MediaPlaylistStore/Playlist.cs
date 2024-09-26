// The DTO (Data Transfer Object) layer contains the classes that represent the core data of MediaPlaylist.
// These classes only have data properties, which are used for transferring data between layers.
using System.ComponentModel.DataAnnotations;

namespace MediaPlaylistStore
{
    /// <summary>
    ///   Represents a media playlist containing a collection of tracks and relevant data.
    /// </summary>
    public class Playlist
    {
        public int Id { get; set; }                     // The Playlist ID

        [MaxLength(15)]
        [Required]
        public string Title { get; set; }               // The name of the playlist

        [MaxLength(70)]
        public string? Description { get; set; }        // A brief description of the playlist, can be null

        [Required]
        public DateTime CreationDate { get; set; }      // The date and time the playlist was created.

        public DateTime? LastModifiedDate { get; set; } // The last time the playlist was updated (ex. songs added/removed), can be null

        [Required]
        public List<Media> Medias { get; set; }         // List of tracks in the playlist.
    }
}
