using System.ComponentModel.DataAnnotations;

namespace LesnoeServer.Tables
{
    public class Reports
    {
        [Key]
        public int report_id { get; set; }
        public int work_id { get; set; }
        public int hourly_rate { get; set; }
        public int norm_hours { get; set; }
        public int overtime_hours { get; set; }
    }

    public class ReportsDetails
    {
        public int report_id { get; set; }
        public int work_id { set; get; }
        public int hourly_rate { get; set; }
        public int norm_hours { get; set; }
        public int overtime_hours { get; set; }
    }
}
