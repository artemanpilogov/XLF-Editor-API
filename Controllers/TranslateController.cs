using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class TranslateController
{
    public TranslateController()
    {
    }

    [Authorize]
    [HttpPost]
    [Route("api/SaveTranslate")]
    public string SaveTranslate()
    {
        return "Saved";
    }

    [Authorize]
    [HttpPost]
    [Route("api/GetTranslate")]
    public string GetTranslate()
    {        
        return "Got Translation";
    }
}