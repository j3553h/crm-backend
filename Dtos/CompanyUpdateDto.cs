namespace crm_backend.Dtos;

using System.ComponentModel.DataAnnotations;

public class CompanyUpdateDto
{
    [Required]
    [StringLength(100)]
    public required string Name { get; set; }

    public string? Industry { get; set; }
}