namespace BehindTheVoice.Models
{
    public enum ProductionType 
    { 
        Movie = 1, 
        TvShow = 2,
        VideoGame = 3
    }

    public class Production
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string PosterUrl { get; set; }
        public ProductionType Type { get; set; }

        public DateTime ReleaseDate { get; set; }
        public string? Studio { get; set; }
        public int? Runtime { get; set; }
        public string? OriginalLanguage { get; set; }

        public int? FranchiseId { get; set; }
        public Franchise? Franchise { get; set; }

        public ICollection<Genre> Genres { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public ICollection<VoiceCast> VoiceCasts { get; set; }
    }
}
