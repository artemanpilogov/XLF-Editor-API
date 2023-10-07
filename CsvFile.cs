using CsvHelper.Configuration.Attributes;

public class CsvFile
{
    [Index(0)]
    public string? Id { get; set; }

    [Index(1)]
    public string? Source { get; set; }

    [Index(2)]
    public string? Target { get; set; }
}