using Manage.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Text;

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
    public bool Autorization(AutorizationInfo autorizationInfo)
    {
        if (IsValid(autorizationInfo.Email))
            return true;

        return false;
    }

    [HttpPost]
    [Route("api/Register")]
    public bool Register([FromBody] Register register)
    {
        if (!IsValid(register.Email))
            return false;

        ASCIIEncoding encryptpwd = new ASCIIEncoding();
        byte[] passwordArray = encryptpwd.GetBytes(register.Password);
        Manages manages = new Manages(_dbContext);
        manages.RegisterUser(register.Email, passwordArray);
        return true;
    }

    public static bool IsValid(string email)
    {
        var valid = true;

        try
        {
            var emailAddress = new MailAddress(email);
        }
        catch
        {
            valid = false;
        }

        return valid;
    }
}