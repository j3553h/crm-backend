namespace crm_backend.Models;
public class Company
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Industry { get; set; }

    public List<Customer> Customers {get; set; } = new();
}