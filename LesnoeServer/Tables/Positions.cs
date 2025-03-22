using System.ComponentModel.DataAnnotations;

namespace LesnoeServer.Tables
{
    public class Positions
    {
        [Key]
        public int position_id { get; set; }
        public string position_name { get; set; } = string.Empty;
        public int required_quantity { get; set; }
    }
}
