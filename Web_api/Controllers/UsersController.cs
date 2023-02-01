using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_api.Model;
using Web_api.Services;

namespace WebApi_User.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly UsersContext _usercontext;
    private readonly ICacheService _cacheservice;

    public UsersController(UsersContext userContext, ICacheService cacheService)
    {
        this._usercontext = userContext;
        this._cacheservice = cacheService;
    }

    [HttpGet("GetALL")]
    public async Task<IActionResult> Get()
    {
        var cacheData = _cacheservice.GetData<IEnumerable<UserInfo>>("UserInfo");
        if (cacheData != null && cacheData.Count() > 0)
            return Ok(cacheData);

        cacheData = await _usercontext.UserInfos.ToListAsync();

        // Set expiry time
        var expiryTime = DateTimeOffset.Now.AddSeconds(30);
        _cacheservice.SetData<IEnumerable<UserInfo>>("UserInfo", cacheData, expiryTime);
        return Ok(cacheData);
    }
    /* [HttpGet("GetbyCode")]
    public IActionResult GetbyCode(int id)
    {
        var people = this._usercontext.People.FirstOrDefault(o => o.Id == id);
        return Ok(people);
    } */
    [HttpDelete("Remove")]
    public async Task<IActionResult> Remove(int id)
    {
        var exist = await _usercontext.UserInfos.FirstOrDefaultAsync(o => o.Id == id);
        if (exist != null)
        {
            _usercontext.Remove(exist);
            _cacheservice.RemoveData($"UserInfo{id}");
            await _usercontext.SaveChangesAsync();
            return NoContent();
        }
        return NotFound();
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] UserInfo Users)
    {
        var addedObject = await _usercontext.UserInfos.AddAsync(Users);

        var expiryTime = DateTimeOffset.Now.AddSeconds(30);
        _cacheservice.SetData<UserInfo>($"UserInfo{Users.Id}",addedObject.Entity,expiryTime);

        await _usercontext.SaveChangesAsync();

        return Ok(addedObject.Entity);
    }
}