using System.Globalization;
using System.Text;
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
        listCsv.AppendLine("\"id\",\"source\",\"target\"");
        if (xmlFile != null)
        {
            foreach(var item in xmlFile.file.body.group.transUnit)
            {    
                listCsv.AppendLine(string.Format("\"{0}\",\"{1}\",\"{2}\"", item.id, item.source, item.target));
            }        
        }
        
        byte[] fileBytes  = Encoding.ASCII.GetBytes(listCsv.ToString());
        return File(fileBytes, "text/csv");
    }

    [HttpPost]
    [Route("api/ImportCsvToXml")]
    [Produces("application/xml")]
    public async Task<FileResult> ImportCsvToXml()
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(XmlFile));
        XmlFile newXmlFile = new XmlFile();

        using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
        //using (StreamReader reader = new StreamReader("D:\\test.txt", Encoding.UTF8))
        {
            string stringFiles = await reader.ReadToEndAsync();
            string[] file = stringFiles.Split("xml_csv_stop");

            List<CsvFile> csvFile = StringToCsv(file[0]);
            XmlFile xmlFile = StringToXml(file[1]);
            newXmlFile = UpdateXmlFile(csvFile, xmlFile);                
        }

        using(var fs = new MemoryStream())
        {
            xmlSerializer.Serialize(fs, newXmlFile);
            byte[] fileBytes  = fs.ToArray();
            return File(fileBytes, "application/xml");     
        }       
    }

    private XmlFile UpdateXmlFile(List<CsvFile> csvFile, XmlFile xmlFile)
    {
        XmlFile newXmlFile = new XmlFile();

        foreach(var item in csvFile)
        {
            if (xmlFile.file.body.group.transUnit.Where(w => w.id == item.Id).FirstOrDefault() != null)
            {
                xmlFile.file.body.group.transUnit.Where(w => w.id == item.Id).FirstOrDefault().target = item.Target;
            }
        }

        return xmlFile;
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
        using (var sReader = new StringReader(file))
        using (var csvReader = new CsvReader(sReader, CultureInfo.InvariantCulture))
        {                    
            List<CsvFile> csvFile = csvReader.GetRecords<CsvFile>().ToList();
            return csvFile;
        }        
    }
}