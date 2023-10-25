using Microsoft.AspNetCore.Mvc;

[ApiController]
public class TranslateController
{
    public TranslateController()
    {
    }

    [HttpPost]
    [Route("api/SaveTranslate")]
    public string SaveTranslate()
    {
        return "Saved";
    }

    [HttpPost]
    [Route("api/GetTranslate")]
    public void GetTranslate()
    {        
    }
}