using crm_backend.Enums;

namespace crm_backend.Models;
public class Activity
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    public ActivityType Type { get; set; } = ActivityType.Unknown;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}