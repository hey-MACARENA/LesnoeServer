using System;
using System.ComponentModel.DataAnnotations;

namespace LesnoeServer.Tables
{
    public class Leaves
    {
        [Key]
        public int leave_id { get; set; }
        public int employee_id { get; set; }
        public int leave_type_id { get; set; }
        public DateOnly start_date { get; set; }
        public DateOnly end_date { get; set; }
    }

    public class LeavesDetails
    {
        public int leave_id { get; set; }
        public int employee_id { get; set; }
        public string employee_name { get; set; } = string.Empty;
        public int leave_type_id { get; set; }
        public string leave_type_name { get; set; } = string.Empty;
        public DateOnly start_date { get; set; }
        public DateOnly end_date { get; set; }
    }
}
