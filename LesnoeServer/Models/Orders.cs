using System.ComponentModel.DataAnnotations;

namespace LesnoeServer.Tables
{
    public class Orders
    {
        [Key]
        public int order_id { get; set; }
        public int order_type_id { get; set; }
        public DateOnly order_date { get; set; }
        public string order_descriction { get; set; } = string.Empty;
    }

    public class OrdersDetails
    {
        public int order_id { get; set; }
        public int order_type_id { get; set; }
        public string order_type_name { get; set; } = string.Empty;
        public DateOnly order_date { get; set; }
        public string order_descriction { get; set; } = string.Empty;
        public int employee_id { get; set; }
        public string employee_name { get; set; } = string.Empty;
    }
}
