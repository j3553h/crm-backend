namespace crm_backend.Dtos;
using System.ComponentModel.DataAnnotations;
public class CustomerReadDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }

     public string? CompanyName { get; set; }
}