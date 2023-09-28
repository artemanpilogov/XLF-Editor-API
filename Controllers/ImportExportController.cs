using System.Globalization;
using System.Text;
using System.Xml;
using CsvHelper;
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
        listCsv.AppendLine("\"source\",\"target\"");
        if (xmlFile != null)
        {
            var groupSources = xmlFile.file.body.group.transUnit.GroupBy(g => g.source);

            foreach(var groupSource in groupSources)
            {
                string targetLine = string.Empty;
                foreach(var line in groupSource)
                {
                    if (string.IsNullOrEmpty(line.source))
                        continue;

                    if (string.IsNullOrEmpty(targetLine))
                        targetLine = line.target;
                    else
                        targetLine += ',' + line.target;
                }
                if (!string.IsNullOrEmpty(targetLine))
                    listCsv.AppendLine(string.Format("\"{0}\",\"{1}\"", groupSource.Key, targetLine));
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
        string newXmlFile = "";
        using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
        //using (StreamReader reader = new StreamReader("D:\\test.txt", Encoding.UTF8))
        {
            string stringFiles = await reader.ReadToEndAsync();
            string[] file = stringFiles.Split("xml_csv_stop");

            List<CsvFile> csvFile = StringToCsv(file[0]);
            newXmlFile = UpdateXmlFile(csvFile, file[1]);                
        }
        
        byte[] fileBytes  = Encoding.ASCII.GetBytes(newXmlFile);
        return File(fileBytes, "application/xml");       
    }

    private string UpdateXmlFile(List<CsvFile> csvFile, string xmlFile)
    {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(xmlFile);

        XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDocument.NameTable);    
        nsmgr.AddNamespace("ns", "urn:oasis:names:tc:xliff:document:1.2");

        XmlNodeList nodes = xmlDocument.SelectNodes("ns:xliff/ns:file/ns:body/ns:group/ns:trans-unit", nsmgr);
        foreach (XmlNode node in nodes)
        {
            CsvFile csvTarget = csvFile.Where(w => w.Id == node.Attributes["id"].InnerText).FirstOrDefault();
            if (csvTarget == null)
                continue;

            string target = "ns:xliff/ns:file/ns:body/ns:group/ns:trans-unit[@id="+ "'" + node.Attributes["id"].InnerText + "'" + "]/ns:target";
            xmlDocument.SelectSingleNode(target, nsmgr).InnerText = csvTarget.Target;
        }

        return xmlDocument.OuterXml;
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