using Microsoft.AspNetCore.Mvc;
using crm_backend.Data;
using Microsoft.EntityFrameworkCore;
using crm_backend.Models;
using crm_backend.Dtos;
namespace crm_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompaniesController : ControllerBase
{
    private readonly AppDbContext _context;

    public CompaniesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompanyReadDto>>> GetCompanies()
    {
        var companies = await _context.Companies.ToListAsync();

        var result = companies.Select(c => new CompanyReadDto
        {
            Id = c.Id,
            Name = c.Name,
            Industry = c.Industry
        });

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CompanyReadDto>> GetCompany(int id)
    {
        var company = await _context.Companies.FindAsync(id);

        if (company == null)
            return NotFound();

        var result = new CompanyReadDto
        {
            Id = company.Id,
            Name = company.Name,
            Industry = company.Industry
        };

        return Ok(result);
    }   

    [HttpPost]
   public async Task<ActionResult<Company>> CreateCompany(CompanyCreateDto dto)
    {
        var company = new Company
        {
            Name = dto.Name,
            Industry = dto.Industry
        };
 

        _context.Companies.Add(company);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCompanies), new { id = company.Id }, company);
    }      

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCompany(int id, [FromBody] CompanyUpdateDto dto)
    {
        var company = await _context.Companies.FindAsync(id);

        if (company == null)
        {
            return NotFound();
        }

        // Map allowed fields only
        company.Name = dto.Name;
        company.Industry = dto.Industry;

        await _context.SaveChangesAsync();

        return NoContent();
    }       

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompany(int id)
    {
        var company = await _context.Companies.FindAsync(id);

        if (company == null)
            return NotFound();

        _context.Companies.Remove(company);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}