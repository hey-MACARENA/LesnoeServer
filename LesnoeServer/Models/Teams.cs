using System.ComponentModel.DataAnnotations;

namespace LesnoeServer.Tables
{
    public class Teams
    {
        [Key]
        public int team_id { get; set; }
        public string team_name { get; set; } = string.Empty;
    }
}
