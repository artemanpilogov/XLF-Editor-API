using System.Text;
using Manage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

[ApiController]
public class AutorizationController : ControllerBase
{
    private readonly DBContext _dbContext;
    private readonly JwtSettings _jwtSettings;

    public AutorizationController(DBContext dbContext, IOptions<JwtSettings> options)
    {
        _dbContext = dbContext;
        _jwtSettings = options.Value;
    }

    [HttpPost]
    [Route("api/Autorization")]
    public string Autorization([FromBody] UserInfo userInfo)
    {
        Manages manages = new Manages(_dbContext);
        string result = manages.Autorization(userInfo);
        if (result != "Authorization was successful")
            return result;

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(_jwtSettings.securitykey);        
        var tokenDesc = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity
            (
                new Claim[]
                {
                    new Claim(ClaimTypes.Name, userInfo.Email)
                }
            ),
            Expires = DateTime.Now.AddMinutes(20),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256
            )
        };

        var token = tokenHandler.CreateToken(tokenDesc);
        string finaltoken = tokenHandler.WriteToken(token);

        return result;
    }

    [HttpPost]
    [Route("api/Register")]
    public string Register([FromBody] UserInfo userInfo)
    {
        Manages manages = new Manages(_dbContext);
        return manages.RegisterUser(userInfo);
    }
}