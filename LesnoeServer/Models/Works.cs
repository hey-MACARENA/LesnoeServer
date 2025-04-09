using System.ComponentModel.DataAnnotations;

namespace LesnoeServer.Tables
{
    public class Works
    {
        [Key]
        public int work_id { get; set; }
        public int work_type_id { get; set; }
        public int section_id { get; set; }
        public DateOnly work_date { get; set; }
        public string work_description { get; set; } = string.Empty;
    }

    public class WorksDetails
    {
        public int work_id { get; set; }
        public DateOnly work_date { get; set; }
        public int work_type_id { get; set; }
        public string work_type { get; set; } = string.Empty;
        public string work_description { get; set; } = string.Empty;
        public int section_id { get; set; }
        public string section_name { get; set; } = string.Empty;
        public int? employee_id { get; set; }
        public string? employee_name { get; set; } = string.Empty;
    }
}
