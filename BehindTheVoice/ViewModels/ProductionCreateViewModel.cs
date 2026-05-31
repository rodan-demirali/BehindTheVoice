using BehindTheVoice.Models;

namespace BehindTheVoice.ViewModels
{
    public class ProductionCreateViewModel
    {
        public string Title { get; set; }
        public string? Description { get; set; }

        public ProductionType Type { get; set; }

        public IFormFile PosterImage { get; set; }

        public DateTime ReleaseDate { get; set; }
        public string? Studio { get; set; }
        public int? Runtime { get; set; }
        public string? OriginalLanguage { get; set; }

        public int? FranchiseId { get; set; }

        public List<int> SelectedGenreIds { get; set; } = new List<int>();
    }
}
