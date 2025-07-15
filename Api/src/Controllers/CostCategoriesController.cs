using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.src.Models;

namespace WebApi.src.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class CostCategoriesController : ControllerBase
  {
    private readonly ApplicationDbContext _context;

    public CostCategoriesController(ApplicationDbContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CostCategory>>> GetAll()
    {
      var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      if (userIdString == null) return Unauthorized();

      var userId = Guid.Parse(userIdString);

      var categories = await _context.CostCategories
          .Where(c => c.UserId == userId)
          .ToListAsync();

      return categories;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CostCategory>> GetById(int id)
    {
      var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      if (userIdString == null) return Unauthorized();

      var userId = Guid.Parse(userIdString);

      var category = await _context.CostCategories
          .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

      if (category == null) return NotFound();

      return category;
    }

    [HttpPost]
    public async Task<ActionResult<CostCategory>> Create(CostCategory category)
    {
      var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      if (userIdString == null) return Unauthorized();

      var userId = Guid.Parse(userIdString);

      category.UserId = userId;

      _context.CostCategories.Add(category);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CostCategory updated)
    {
      var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      if (userIdString == null) return Unauthorized();

      var userId = Guid.Parse(userIdString);

      if (id != updated.Id) return BadRequest();

      // Make sure the category belongs to current user
      var exists = await _context.CostCategories.AnyAsync(c => c.Id == id && c.UserId == userId);
      if (!exists) return NotFound();

      updated.UserId = userId; // Ensure UserId remains unchanged to current user

      _context.Entry(updated).State = EntityState.Modified;
      await _context.SaveChangesAsync();

      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      if (userIdString == null) return Unauthorized();

      var userId = Guid.Parse(userIdString);

      var category = await _context.CostCategories.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
      if (category == null) return NotFound();

      _context.CostCategories.Remove(category);
      await _context.SaveChangesAsync();

      return NoContent();
    }
  }
}
