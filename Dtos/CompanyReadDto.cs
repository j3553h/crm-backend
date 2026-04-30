namespace crm_backend.Dtos;

public class CompanyReadDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Industry { get; set; }
}