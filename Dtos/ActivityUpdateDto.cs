using crm_backend.Enums;

namespace crm_backend.Dtos;

public class ActivityUpdateDto
{
    public ActivityType Type { get; set; }
    public string? Description { get; set; }
}