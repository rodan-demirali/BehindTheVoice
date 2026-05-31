namespace BehindTheVoice.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public ICollection<VoiceCast> VoiceCasts { get; set; }
    }
}
