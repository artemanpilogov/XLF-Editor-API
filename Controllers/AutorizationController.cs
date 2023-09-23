using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

[ApiController]
[Route("[controller]")]
public class AutorizationController: ControllerBase
{
    public AutorizationController() {}

    [HttpPost(Name = "CheckAutorizationInfo")]
    public bool CheckAutorizationInfo(AutorizationInfo autorizationInfo)
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