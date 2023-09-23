using System.Text;
using System.Xml;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class ImportCSVController: ControllerBase
{
    [HttpPost]
    [Route("api/importcsv")]    
    [Consumes("application/xml")]
    [Produces("application/xml")]
    public async Task<IActionResult> Importcsv([FromBody] FileContent fileContent)
    { 
        
        // if (fileContent.FileXML != null)
        // {
        //     //XmlDocument doc = new XmlDocument();
        //     //MemoryStream ms = new MemoryStream(fileContent.FileXML);
        //     //doc.Load(ms);
        //     return "ok";
        // }

        await Task.CompletedTask;
        return Ok(fileContent); // just echo out for now to show the output serialization
    }

    [HttpPost]
    [Route("api/importcsv2")]    
    [Consumes("application/json")]
    [Produces("application/json")]
    public string Importcsv2(string text1)
    { 
        
        // if (fileContent.FileXML != null)
        // {
        //     //XmlDocument doc = new XmlDocument();
        //     //MemoryStream ms = new MemoryStream(fileContent.FileXML);
        //     //doc.Load(ms);
        //     return "ok";
        // }
        
        return text1;
    }
}