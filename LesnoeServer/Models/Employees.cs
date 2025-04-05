using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LesnoeServer.Tables
{
    public class Employees
    {
        [Key]
        public int employee_id { get; set; }
        public string name { get; set; } = string.Empty;
        public int position_id { get; set; }
        public int? section_id { get; set; }
        public int? team_id { get; set; }
        public int work_experience { get; set; }
        public string residence { get; set; } = string.Empty;
    }

    public class EmployeeDetails
    {
        public int employee_id { get; set; }
        public string name { get; set; } = string.Empty;
        public int position_id { get; set; }
        public string position_name { get; set; } = string.Empty;
        public int? section_id { get; set; }
        public string? section_name { get; set; } = string.Empty;
        public int? team_id { get; set; }
        public string? team_name { get; set; } = string.Empty;
        public int work_experience { get; set; }
        public string residence { get; set; } = string.Empty;
    }
}
