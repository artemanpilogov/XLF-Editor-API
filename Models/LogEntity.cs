using System.ComponentModel.DataAnnotations;

namespace Log.Models
{
    public class LogEntity
    {
        [Key]
        public int EntryNo { get; set; }
        public string? Ip_Address { get; set; }
        public DateTime Created_Date { get; set; }
        public TimeSpan Created_Time { get; set; }
        public int Action_Type { get; set; }
    }
}