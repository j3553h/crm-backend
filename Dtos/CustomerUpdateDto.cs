using System.ComponentModel.DataAnnotations;
namespace crm_backend.Dtos;

public class CustomerUpdateDto
{
    [Required]
    public required string Name { get; set; }
    
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

     public int? CompanyId { get; set; }
}