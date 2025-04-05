using System.ComponentModel.DataAnnotations;

namespace LesnoeServer.Tables
{
    public class Work_Employee
    {
        [Key]
        public int work_employee_id { get; set; }
        public int work_id { get; set; }
        public int employee_id { get; set; }
    }
}
