using System.ComponentModel.DataAnnotations;

namespace Setup.Models
{
    public class SetupEntity
    {
        [Key]
        public string? Code { get; set; }
        public bool Insert_log_import_to_csv { get; set; }
        public bool Save_Changes { get; set; }
    }
}