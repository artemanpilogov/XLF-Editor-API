using System.Text;
using System.Xml;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class ImportCSVController: ControllerBase
{
    [HttpPost]
    [Route("api/ImportXmlToCsv")]    
    [Consumes("application/xml")]
    [Produces("text/csv")]
    public async Task<FileResult> ImportXmlToCsv([FromBody] XliffFile xliffFile)
    {
        StringBuilder listCsv = new StringBuilder();
        if (xliffFile != null)
        {   
            foreach(var item in xliffFile.file.body.group.transUnit)
            {    
                listCsv.AppendLine(item.source + ',' + item.target + ";");                          
            }        
        }
        
        byte[] fileBytes  = Encoding.ASCII.GetBytes(listCsv.ToString());
        return File(fileBytes, "text/csv", "text.csv");
    }
}