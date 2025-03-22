using System.ComponentModel.DataAnnotations;

namespace LesnoeServer.Tables
{
    public class Fire_hazard_levels
    {
        [Key]
        public int fire_hazard_level_id { get; set; }
        public string fire_hazard_level { get; set; } = string.Empty;
    }
}
