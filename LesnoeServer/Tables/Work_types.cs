using System.ComponentModel.DataAnnotations;

namespace LesnoeServer.Tables
{
    public class Work_types
    {
        [Key]
        public int work_type_id { get; set; }
        public string work_type { get; set; } = string.Empty;
    }
}
