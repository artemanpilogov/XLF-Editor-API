using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

[ApiController]
public class AutorizationController: ControllerBase
{
    public AutorizationController() {}

    [HttpPost]
    [Route("api/Autorization")]
    public bool Autorization(AutorizationInfo autorizationInfo)
    {
        if (IsValid(autorizationInfo.Email))
            return true;

        return false;
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