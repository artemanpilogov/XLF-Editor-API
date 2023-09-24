using System.Xml.Serialization;

[XmlRoot(ElementName = "xliff", Namespace = "urn:oasis:names:tc:xliff:document:1.2")]
public class XliffFile
{
    [XmlElement(ElementName = "file")]
    public File file {get; set;}
}

public class File
{
    [XmlElement(ElementName = "body")]
    public Body body {get; set;}
}

public class Body
{
    [XmlElement(ElementName = "group")]
    public Group group {get; set;}
}

public class Group
{
    [XmlElement(ElementName = "trans-unit")]
    public List<TransUnit> transUnit {get; set;}
}

public class TransUnit
{
    [XmlElement(ElementName = "source")]
    public string? source { get; set; }

    [XmlElement(ElementName = "target")]
    public string? target { get; set; }
}