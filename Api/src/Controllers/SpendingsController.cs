using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.src.Models;

namespace WebApi.src.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize] // Only authenticated users
  public class SpendingsController : ControllerBase
  {
    private readonly ApplicationDbContext _context;

    public SpendingsController(ApplicationDbContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Spending>>> GetAll()
    {
      var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      if (userIdString == null) return Unauthorized();

      var userId = Guid.Parse(userIdString);

      var spendings = await _context.Spendings
          .Where(s => s.UserId == userId)
          .Include(s => s.CostCategory)
          .ToListAsync();

      return spendings;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Spending>> GetById(int id)
    {
      var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      if (userIdString == null) return Unauthorized();

      var userId = Guid.Parse(userIdString);

      var spending = await _context.Spendings
          .Include(s => s.CostCategory)
          .FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);

      if (spending == null) return NotFound();

      return spending;
    }

    [HttpGet("from-to")]
    public async Task<ActionResult<IEnumerable<Spending>>> GetFromTo([FromQuery] DateTime from, [FromQuery] DateTime to)
    {
      var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      if (userIdString == null) return Unauthorized();

      var userId = Guid.Parse(userIdString);

      var spendings = await _context.Spendings
          .Where(s => s.UserId == userId && s.CreatedAt >= from && s.CreatedAt <= to)
          .Include(s => s.CostCategory)
          .ToListAsync();

      return spendings;
    }

    [HttpPost]
    public async Task<ActionResult<Spending>> Create(Spending spending)
    {
      var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      if (userIdString == null) return Unauthorized();

      var userId = Guid.Parse(userIdString);

      spending.UserId = userId;
      spending.CreatedAt = DateTime.UtcNow;

      _context.Spendings.Add(spending);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetById), new { id = spending.Id }, spending);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] SpendingUpdateDto updated)
    {
      var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      if (userIdString == null) return Unauthorized();

      var userId = Guid.Parse(userIdString);

      if (id != updated.Id) return BadRequest("Mismatched ID");

      var spending = await _context.Spendings.FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);
      if (spending == null) return NotFound("Spending not found");

      spending.Comment = updated.Comment;
      spending.CreatedAt = updated.CreatedAt;
      spending.CostCategoryId = updated.CostCategoryId;

      await _context.SaveChangesAsync();

      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      if (userIdString == null) return Unauthorized();

      var userId = Guid.Parse(userIdString);

      var spending = await _context.Spendings.FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);
      if (spending == null) return NotFound();

      _context.Spendings.Remove(spending);
      await _context.SaveChangesAsync();

      return NoContent();
    }
  }
}
