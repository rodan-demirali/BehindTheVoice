namespace BehindTheVoice.Models
{
    public class VoiceActor
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public ICollection<VoiceCast> VoiceCasts { get; set; }

        public string Description { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string BirthPlace { get; set; }
        
    }
}
