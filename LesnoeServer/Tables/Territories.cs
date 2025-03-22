using System.ComponentModel.DataAnnotations;

namespace LesnoeServer.Tables
{
    public class Territories
    {
        [Key]
        public int territory_id { get; set; }
        public string territory_type { get; set; } = string.Empty;
    }
}
