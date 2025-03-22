using System.ComponentModel.DataAnnotations;

namespace LesnoeServer.Tables
{
    public class Order_types
    {
        [Key]
        public int order_type_id { get; set; }
        public int order_type { get; set; }
    }
}
