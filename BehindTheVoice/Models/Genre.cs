using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BehindTheVoice.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ValidateNever]
        public ICollection<Production> Productions { get; set; }
    }
}
