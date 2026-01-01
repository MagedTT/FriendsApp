using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using API.DTOs;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class SeedData
{
    public static async Task SeedUsersData(ApplicationDbContext context)
    {
        if (await context.Users.AnyAsync())
            return;

        var membersData = await File.ReadAllTextAsync("Data/users.json");
        var members = JsonSerializer.Deserialize<List<SeedUsersDTO>>(membersData);

        if (members is null)
        {
            return;
        }

        using var hmac = new HMACSHA512();
        foreach (var member in members)
        {
            var user = new ApplicationUser
            {
                Id = member.Id,
                Name = member.Name,
                Email = member.Email,
                ImageUrl = member.ImageUrl,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Admin@123")),
                Member = new Member
                {
                    Id = member.Id,
                    Name = member.Name,
                    Description = member.Description,
                    DateOfBirth = member.DateOfBirth,
                    ImageUrl = member.ImageUrl,
                    Gender = member.Gender,
                    City = member.City,
                    Country = member.Country,
                    LastActive = member.LastActive,
                    Created = member.Created
                }
            };

            user.Member.Photos.Add(new Photo
            {
                Url = member.ImageUrl!,
                MemberId = member.Id,
            });

            context.Users.Add(user);

            await context.SaveChangesAsync();
        }
    }
}