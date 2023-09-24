using System.Xml;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class ImportCSVController: ControllerBase
{
    [HttpPost]
    [Route("api/ImportXmlToCsv")]    
    [Consumes("application/xml")]
    public List<string> ImportXmlToCsv([FromBody] XliffFile xliffFile)
    {
        List<string> csv = new List<string>();
        if (xliffFile != null)
        {   
            foreach(var item in xliffFile.file.body.group.transUnit)
            {                              
                csv.Add(string.Join(";", item.source + ':' + item.target));
            }
            return csv;
        }
        return csv;
    }
}