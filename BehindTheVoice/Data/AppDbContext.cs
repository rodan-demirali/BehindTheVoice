using BehindTheVoice.Models;
using Microsoft.EntityFrameworkCore;

namespace BehindTheVoice.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }


        public DbSet<VoiceActor> VoiceActors { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Production> Productions { get; set; }
        public DbSet<VoiceCast> VoiceCasts { get; set; }

        public DbSet<Franchise> Franchises { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public enum ProductionType { Movie = 1, TVShow = 2, VideoGame = 3 }
    }
}
