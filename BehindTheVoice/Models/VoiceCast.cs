namespace BehindTheVoice.Models
{
    public class VoiceCast
    {
        public int Id { get; set; }

        public int VoiceActorId { get; set; }
        public VoiceActor VoiceActor { get; set; }

        public int CharacterId { get; set; }
        public Character Character { get; set; }

        public int ProductionId { get; set; }
        public Production Production { get; set; }

        public string LanguageCode { get; set; }

        public string? CharacterAppearanceUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
