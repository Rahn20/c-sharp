// The DTO (Data Transfer Object) layer contains the classes that represent the core data of MediaPlaylist.
using System.ComponentModel.DataAnnotations;

namespace MediaPlaylistStore
{
    // The AudioTypes
    public enum AudioType
    {
        Song,
        Podcast,
        Audiobook,
    }

    /// <summary>
    ///  An abstract Media class that contains all the common properties of Song, Podcast, and Audiobook.
    /// </summary>
    public abstract class Media
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }           // Name of the media (file name).

        [Required]
        [MaxLength(25)]
        public string Title { get; set; }           // Title of the media (track) ex: song name, podcast series title, audiobook title.

        [Required]
        public TimeSpan Duration { get; set; }      // Length of the media.
        
        [Required]
        public float Size { get; set; }             //  The file/media size

        [Required]
        public AudioType AudioType { get; set; }    // the type of media

        [Required]
        public string FullPath { get; set; }       // The full path of the media 
    }

    public class Song : Media
    {
        [MaxLength(30)]
        public string Artist { get; set; }      // Artist who performed the track.

        [MaxLength(25)]
        public string? Genre { get; set; }     //  The musical genre (Ex.Rock, Pop, Jazz).

        [MaxLength(30)]
        public string Album { get; set; }       // The collection or album the song belongs to.
    }

    public class Podcast : Media
    {
        [MaxLength(30)]
        [Required]
        public string Host { get; set; }        // The person hosting the podcast.

        public int EpisodeNumber { get; set; }  // The specific episode in the podcast series.

        [MaxLength(50)]
        public string Guests { get; set; }      //  Special guests featured in the episode.
    }

    public class Audiobook : Media
    {
        [MaxLength(30)]
        public string Author { get; set; }      // The writer of the book.

        [MaxLength(30)]
        public string Genre { get; set; }       // The literary genre (ex: Fiction, Non-Fiction, Mystery).


        [MaxLength(40)]
        [Required]
        public string Publisher { get; set; }   // The company that published the audiobook.
    }
}
