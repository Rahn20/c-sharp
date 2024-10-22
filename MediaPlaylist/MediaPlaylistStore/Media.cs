using System;
using System.ComponentModel.DataAnnotations;

namespace MediaPlaylistStore
{
    /// <summary>
    ///  The Media Audio Types enum.
    /// </summary>
    public enum AudioType
    {
        Song,
        Podcast,
        Audiobook,
    }

    /// <summary>
    ///   An abstract Media class, Base class that contains all the common properties of Song, Podcast, and Audiobook.
    ///   The DTO (Data Transfer Object) layer only has data properties, which are used for transferring data between layers.
    /// </summary>
    public abstract class Media
    {
        /// <summary>
        ///   The media id
        /// </summary>
        [Key]
        public int MediaId { get; set; }

        /// <summary>
        ///   Name of the media (file name).
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }


        /// <summary>
        ///   Title of the media (track) ex: song name, podcast series title, audiobook title.
        /// </summary>
        [Required]
        [MaxLength(25)]
        public string Title { get; set; }

        /// <summary>
        ///   Length of the media.
        /// </summary>
        [Required]
        public TimeSpan Duration { get; set; }


        /// <summary>
        ///  The file/media size
        /// </summary>
        [Required]
        public float Size { get; set; }

        /// <summary>
        ///   the type of media
        /// </summary>
        [Required]
        public AudioType AudioType { get; set; }


        /// <summary>
        ///   The full path of the media 
        /// </summary>
        [Required]
        public string FullPath { get; set; }

        /// <summary>
        ///    Realated table, Foreign Key property: Each Media belongs to a Playlist (Many-to-One relationship)
        /// </summary>
        [Required]
        public int PlaylistId { get; set; }

        /// <summary>
        ///    reference navigation 
        /// </summary>
        public Playlist Playlist { get; set; }
    }


    /// <summary>
    ///   Derived class for Song contains Song properties.
    /// </summary>
    public class Song : Media
    {
        /// <summary>
        ///   Artist who performed the track.
        /// </summary>
        [MaxLength(30)]
        public string? Artist { get; set; }

        /// <summary>
        ///   The musical genre (Ex.Rock, Pop, Jazz).
        /// </summary>
        [MaxLength(25)]
        public string? Genre { get; set; }

        /// <summary>
        ///   The collection or album the song belongs to.
        /// </summary>
        [MaxLength(30)]
        public string? Album { get; set; }
    }


    /// <summary>
    ///   Derived class for Podcast contains Podcast properties.
    /// </summary>
    public class Podcast : Media
    {
        /// <summary>
        ///   The person hosting the podcast.
        /// </summary>
        [MaxLength(30)]
        [Required]
        public string Host { get; set; }

        /// <summary>
        ///   The specific episode in the podcast series.
        /// </summary>
        public int EpisodeNumber { get; set; }

        /// <summary>
        ///   Special guests featured in the episode.
        /// </summary>
        [MaxLength(50)]
        public string? Guests { get; set; }
    }


    /// <summary>
    ///   Derived class for Audiobook contains Audiobook properties.
    /// </summary>
    public class Audiobook : Media
    {
        /// <summary>
        ///   The writer of the book.
        /// </summary>
        [MaxLength(30)]
        public string? Author { get; set; }

        /// <summary>
        ///   The literary genre (ex: Fiction, Non-Fiction, Mystery).
        /// </summary>
        [MaxLength(30)]
        public string? Genre { get; set; }

        /// <summary>
        ///   The company that published the audiobook.
        /// </summary>
        [MaxLength(40)]
        [Required]
        public string Publisher { get; set; }
    }
}