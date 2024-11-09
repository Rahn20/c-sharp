using System;
using System.Collections.Generic;
using MediaPlaylistStore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

namespace MediaPlaylistDAL.DbContexts
{
    /// <summary>
    ///   Represents the Entity Framework database context for managing media playlists.
    ///   This class handles the interaction between the application and the database for playlist and media entities.
    /// </summary>
    public class MediaPlaylistContext : DbContext
    {
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Media> Medias { get; set; }


        /// <summary>
        ///   Specify Db connection string.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string? dbProvider = Environment.GetEnvironmentVariable("MEDIAPLAYLIST_DB_PROVIDER");

            if (dbProvider != null && dbProvider == "InMemory")
            {
                // Unit Testing with In-Memory Database
                optionsBuilder.UseInMemoryDatabase("TestDatabase");
                optionsBuilder.EnableSensitiveDataLogging();
            }
            else
            {
                optionsBuilder.UseSqlServer(
                    @"Server=(localdb)\mssqllocaldb;Database=MediaPlaylist;Trusted_Connection=True;");
            }
        }


        /// <summary>
        ///   Method to configure model relationships and table mappings.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Setting up Table-Per-Hierarchy (TPH) for Media class inheritance
            modelBuilder.Entity<Media>()
                .HasDiscriminator<AudioType>("AudioType")
                .HasValue<Song>(AudioType.Song)
                .HasValue<Audiobook>(AudioType.Audiobook)
                .HasValue<Podcast>(AudioType.Podcast);

            // Playlist to Media: One-to-Many relationship
            // Adds PlaylistId to Media to establish a relationship between Playlist and Media.
            modelBuilder.Entity<Playlist>()
                .HasMany(p => p.Medias)
                .WithOne(m => m.Playlist)
                .HasForeignKey(m => m.PlaylistId)
                .OnDelete(DeleteBehavior.Cascade); // When a Playlist is deleted, all associated Media records are automatically deleted.
        }
    }
}
