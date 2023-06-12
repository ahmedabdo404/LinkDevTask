using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace LinkDevTask.Domain.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? SkillsAsString { get; set; }
        public List<string> Skills { get => JsonConvert.DeserializeObject<List<string>>(SkillsAsString) ?? new();
            set => SkillsAsString = JsonConvert.SerializeObject(value); }

        public List<UserJob> UserJob = new ();

    }
}
