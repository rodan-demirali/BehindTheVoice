using Microsoft.AspNetCore.Http;

namespace BehindTheVoice.ViewModels
{
    public class VoiceCastCreateViewModel
    {
        // İlişkili Tabloların Seçilen ID'leri
        public int VoiceActorId { get; set; }
        public int CharacterId { get; set; }
        public int ProductionId { get; set; }

        // Ekstra Bağlantı Detayları
        public string LanguageCode { get; set; } // Örn: TR, EN, JP

        // Karaktere özel resim (Opsiyonel)
        public IFormFile? CharacterAppearanceImage { get; set; }
    }
}