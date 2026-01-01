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
}