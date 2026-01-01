using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Interfaces;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ITokenService _tokenService;

    public AccountController(ApplicationDbContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (await EmailExists(registerDTO.Email))
        {
            return BadRequest("Email Already Exists");
        }

        using var hmac = new HMACSHA512();

        var user = new ApplicationUser
        {
            Name = registerDTO.Name,
            Email = registerDTO.Email,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
            PasswordSalt = hmac.Key
        };

        user.Id = Guid.NewGuid().ToString();

        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return Ok(user.toUserDTO(_tokenService));
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        ApplicationUser? user = await _context.Users.FirstOrDefaultAsync(x => x.Email == loginDTO.Email);

        if (user is null)
        {
            return Unauthorized("Unauthorized");
        }

        if (user.PasswordSalt is null)
        {
            return Unauthorized("Unauthorized");
        }

        var hmac = new HMACSHA512(user.PasswordSalt);

        var passwordHashed = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

        for (var i = 0; i < passwordHashed.Length; ++i)
        {
            if (passwordHashed[i] != user.PasswordHash![i])
                return Unauthorized("Invalid Password");
        }

        return Ok(user.toUserDTO(_tokenService));
    }

    private async Task<bool> EmailExists(string email)
    {
        return await _context.Users.AnyAsync(x => x.Email == email);
    }
}