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
    public FileResult ExportXmlToCsv([FromBody] XmlFile xmlFile)
    {
        StringBuilder listCsv = new StringBuilder();
        if (xmlFile != null)
        {   
            foreach(var item in xmlFile.file.body.group.transUnit)
            {    
                listCsv.AppendLine(string.Format("\"{0}\",\"{1}\"", item.source, item.target));
            }        
        }
        
        byte[] fileBytes  = Encoding.ASCII.GetBytes(listCsv.ToString());
        return File(fileBytes, "text/csv");
    }

    [HttpPost]
    [Route("api/ImportCsvToXml")]
    [Consumes("application/xml")]
    [Produces("text/csv")]
    public void ImportCsvToXml()
    {        

    }
}