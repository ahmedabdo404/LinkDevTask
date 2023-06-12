namespace LinkDevTask.Domain.Models
{
    public class BaseEntity
    {
        public string Id { get; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; } = DateTime.Now;
    }
}
