using System.ComponentModel.DataAnnotations;

namespace LesnoeServer.Tables
{
    public class Order_Employee
    {
        [Key]
        public int order_employee_id { get; set; }
        public int order_id { get; set; }
        public int employee_id { get; set; }
    }
}
