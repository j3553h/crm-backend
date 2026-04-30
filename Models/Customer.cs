namespace crm_backend.Models;
public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = string.Empty;

    public List<Activity> Activities { get; set; } = new();

    public int? CompanyId { get; set; }
    public Company? Company { get; set;} = null!;
}