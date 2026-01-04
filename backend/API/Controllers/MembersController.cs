using System.Security.Claims;
using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MembersController : ControllerBase
{
    private readonly IMemberRepository _memberRepository;

    public MembersController(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetMembers()
    {
        return Ok(await _memberRepository.GetMembersAsync());
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetMember(string id)
    {
        var member = await _memberRepository.GetMemberByIdAsync(id);

        if (member is null)
            return NotFound();

        return Ok(member);
    }

    [HttpGet]
    [Route("{id}/photos")]
    public async Task<IActionResult> GetMemberPhotos(string id)
    {
        return Ok(await _memberRepository.GetPhotosForMemberAsync(id));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateMembeR(MemberUpdateDTO memberUpdateDTO)
    {
        var memberId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (memberId == null)
            return BadRequest("no id found in the token");

        var member = await _memberRepository.GetMemberByIdAsync(memberId);

        if (member is null)
            return BadRequest("Counld get member");

        member.Name = memberUpdateDTO.Name ?? member.Name;
        member.Description = memberUpdateDTO.Description ?? member.Description;
        member.City = memberUpdateDTO.City ?? member.City;
        member.Country = memberUpdateDTO.Country ?? member.Country;

        member.User.Name = memberUpdateDTO.Name ?? member.User.Name;

        _memberRepository.Update(member);

        if (await _memberRepository.SaveAllAsync())
            return NoContent();

        return BadRequest("Failed to update member");
    }
}