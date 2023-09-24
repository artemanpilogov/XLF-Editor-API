using System.Text;
using System.Xml;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class ImportExportController: ControllerBase
{
    [HttpPost]
    [Route("api/ExportXmlToCsv")]
    [Consumes("application/xml")]
    [Produces("text/csv")]
    public FileResult ExportXmlToCsv([FromBody] XliffFile xliffFile)
    {
        StringBuilder listCsv = new StringBuilder();
        if (xliffFile != null)
        {   
            foreach(var item in xliffFile.file.body.group.transUnit)
            {    
                listCsv.AppendLine(string.Format("\"{0}\",\"{1}\"", item.source, item.target));
            }        
        }
        
        byte[] fileBytes  = Encoding.ASCII.GetBytes(listCsv.ToString());
        return File(fileBytes, "text/csv");
    }
}