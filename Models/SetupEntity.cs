using System.ComponentModel.DataAnnotations;

namespace Setup.Models
{
    public class SetupEntity
    {
        [Key]
        public string? Code { get; set; }
        public bool Import_To_CSV { get; set; }
        public bool Export_From_CSV { get; set; }
        public bool Save_Changes { get; set; }
        public bool Created_File { get; set; }
    }
}