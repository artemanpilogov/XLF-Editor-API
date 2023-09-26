using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class ImportExportController: ControllerBase
{
    [HttpPost]
    [Route("api/ExportXmlToCsv")]
    [Consumes("application/xml")]
    [Produces("text/csv")]
    public FileResult ExportXmlToCsv(XmlFile xmlFile)
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
    public async Task<IActionResult> ImportCsvToXml()
    {        
        try
        {
            //using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            using (StreamReader reader = new StreamReader("D:\\test.txt", Encoding.UTF8))
            {
                string stringFiles = await reader.ReadToEndAsync();
                string[] file = stringFiles.Split("artendstop");

                List<CsvFile> csvFile = StringToCsv(file[0]);
                XmlFile xmlFile = StringToXml(file[1]);

                return base.Ok();
            }
        }
        catch(Exception ex)
        {
            return base.Problem(ex.Message);
        }
    }

    private XmlFile StringToXml(string file)
    {
        using(TextReader sReader = new StringReader(file))
        {
            var serializer = new XmlSerializer(typeof(XmlFile));
            XmlFile xmlFile =  (XmlFile)serializer.Deserialize(sReader);            
            return xmlFile;
        }        
    }

    private List<CsvFile> StringToCsv(string file)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = false };
        using (var sReader = new StringReader(file))
        using (var csvReader = new CsvReader(sReader, config))
        {                    
            List<CsvFile> csvFile = csvReader.GetRecords<CsvFile>().ToList();
            return csvFile;
        }        
    }
}