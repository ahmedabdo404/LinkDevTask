using Newtonsoft.Json;

namespace LinkDevTask.Domain.Models
{
    public class Job : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? Responsibilities { get; set; }
        public string Skills { get; set; } = null!;
        public string CategoryId { get; set; } = null!;
        public Category Category { get; set; }
        public List<UserJob> UserJob = new();
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public short? MaximumApplications { get; set; }
        public byte[]? Picture { get; set; }
    }
}
