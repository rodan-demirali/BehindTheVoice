using Microsoft.AspNetCore.Http;

namespace BehindTheVoice.ViewModels
{
    public class VoiceActorCreateViewModel
    {
        public string FullName { get; set; }
        public string Description { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string BirthPlace { get; set; }
        public IFormFile ProfileImage { get; set; }
    }
}
