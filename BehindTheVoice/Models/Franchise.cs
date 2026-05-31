using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BehindTheVoice.Models
{
    public class Franchise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        [ValidateNever]
        public ICollection<Production> Productions { get; set; }
    }
}
