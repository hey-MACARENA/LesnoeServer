using System.ComponentModel.DataAnnotations;

namespace LesnoeServer.Tables
{
    public class Leave_types
    {
        [Key]
        public int leave_type_id { get; set; }
        public string leave_type { get; set; } = string.Empty;
    }
}
