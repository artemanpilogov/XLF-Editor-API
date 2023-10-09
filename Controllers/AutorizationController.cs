using Manage.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class AutorizationController : ControllerBase
{
    private readonly DBContext _dbContext;

    public AutorizationController(DBContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    [Route("api/Autorization")]
    public bool Autorization([FromBody] UserInfo userInfo)
    {
        Manages manages = new Manages(_dbContext);
        return manages.Autorization(userInfo);
    }

    [HttpPost]
    [Route("api/Register")]
    public bool Register([FromBody] UserInfo userInfo)
    {
        Manages manages = new Manages(_dbContext);
        return manages.RegisterUser(userInfo);
    }
}