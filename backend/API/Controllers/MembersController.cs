using API.Data;
using backend.API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
// [Authorize]
public class MembersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public MembersController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetMembers()
    {
        List<ApplicationUser>? members = await _context.Users.ToListAsync();

        if (members is null)
        {
            return NotFound();
        }

        return Ok(members);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetMember(string id)
    {
        ApplicationUser? member = await _context.Users.FindAsync(id);

        if (member is null)
        {
            return NotFound();
        }

        return Ok(member);
    }
}