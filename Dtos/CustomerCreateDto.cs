
namespace crm_backend.Dtos;
using System.ComponentModel.DataAnnotations;

public class CustomerCreateDto
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public required string Name { get; set; }
    
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    public int? CompanyId { get; set; }

}