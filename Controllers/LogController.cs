using System.Net;
using Manage.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class LogController : ControllerBase
{
    private readonly DBContext _dbContext;

    public LogController(DBContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    [Route("api/InsertLog")]
    [Consumes("application/xml")]
    [Produces("text/csv")]
    public void InsertLog(int entryType)
    {
        Manages manages = new Manages(_dbContext);
        manages.InsertLog(GetIpAddress(), (EntryType)Enum.ToObject(typeof(EntryType), entryType));
    }

    private string GetIpAddress()
    {
        string ip_address = Response.HttpContext.Connection.RemoteIpAddress.ToString();
        if (ip_address == "::1")
        {
            ip_address = Dns.GetHostEntry(Dns.GetHostName()).AddressList[3].ToString();
        }
        return ip_address;
    }
}