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
    public FileResult ImportXmlToCsv([FromBody] XliffFile xliffFile)
    {
        StringBuilder listCSV = new StringBuilder();
        if (xliffFile != null)
        {   
            foreach(var item in xliffFile.file.body.group.transUnit)
            {    
                listCSV.AppendLine(string.Format("\"{0}\",\"{1}\"", item.source, item.target));
            }        
        }
        
        byte[] fileBytes  = Encoding.ASCII.GetBytes(listCSV.ToString());
        return File(fileBytes, "text/csv");
    }
}