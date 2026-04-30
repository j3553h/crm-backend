using crm_backend.Enums;

namespace crm_backend.Dtos;

public class ActivityCreateDto
{
    public int CustomerId { get; set; }
    public required ActivityType Type { get; set; }

    public string? Description { get; set; }    
}