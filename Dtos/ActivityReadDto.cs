using crm_backend.Enums;

namespace crm_backend.Dtos;

public class ActivityReadDto
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public ActivityType Type { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }
}