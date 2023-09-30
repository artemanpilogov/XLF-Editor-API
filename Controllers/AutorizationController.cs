using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

[ApiController]
public class AutorizationController: ControllerBase
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
        Log.Models.LogEntity logEntity = (new Log.Models.LogEntity {
            Ip_Address = "192.168.1.1",
            Action_Type = 2,
            Created_Date = DateTime.UtcNow.Date,
            Created_Time = DateTime.UtcNow.TimeOfDay,
        });        
        
        _dbContext.LogsEntity.Add(logEntity);
        _dbContext.SaveChanges();

        var logsEntity = _dbContext.LogsEntity.ToList();

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