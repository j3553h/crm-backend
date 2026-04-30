using Microsoft.AspNetCore.Mvc;
using crm_backend.Data;
using Microsoft.EntityFrameworkCore;
using crm_backend.Models;
using crm_backend.Dtos;
namespace crm_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly AppDbContext _context;

    public CustomersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerReadDto>>> GetCustomers()
    {
        var customers = await _context.Customers
            .Select(c => new CustomerReadDto
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                CompanyName = c.Company != null ? c.Company.Name : null
            })
            .ToListAsync();

        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerReadDto>> GetCustomer(int id)
    {
        var customer = await _context.Customers
            .Where(c => c.Id == id)
            .Select(c => new CustomerReadDto
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                CompanyName = c.Company != null ? c.Company.Name : null
            })
            .FirstOrDefaultAsync();

        if (customer == null)
            return NotFound();

        return Ok(customer);
    }


    [HttpPost]
   public async Task<ActionResult<Customer>> CreateCustomer(CustomerCreateDto dto)
    {
        var customer = new Customer
        {
            Name = dto.Name,
            Email = dto.Email,
            CompanyId = dto.CompanyId
        };

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        var result = await _context.Customers
            .Where(c => c.Id == customer.Id)
            .Select(c => new CustomerReadDto
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                CompanyName = c.Company != null ? c.Company.Name : null
            })
            .FirstOrDefaultAsync();

        return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, result);
    }    




    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerUpdateDto dto)
    {
        var customer = await _context.Customers.FindAsync(id);

        if (customer == null)
        {
            return NotFound();
        }

        // Map allowed fields only
        customer.Name = dto.Name;
        customer.Email = dto.Email;
        customer.CompanyId = dto.CompanyId;
        
        await _context.SaveChangesAsync();

        return NoContent();
    }       

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var customer = await _context.Customers.FindAsync(id);

        if (customer == null)
            return NotFound();

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}