using System.ComponentModel.DataAnnotations;

namespace LesnoeServer.Tables
{
    public class Sections
    {
        [Key]
        public int section_id { get; set; }
        public string section_name { get; set; } = string.Empty;
        public int territory_id { get; set; }
        public double section_area { get; set; }
        public int fire_hazard_level_id { get; set; }
        public int cutting_age { get; set; }
    }

    public class SectionsDetails
    {
        public int section_id { get; set; }
        public string section_name { get; set; } = string.Empty;
        public int territory_id { get; set; }
        public string territory_type { get; set; } = string.Empty;
        public double section_area { get; set; }
        public int cutting_age { get; set; }
        public int fire_hazard_level_id { get; set; }
        public string fire_hazard_level { get; set; } = string.Empty;
        public int employee_id { get; set; }
        public string employee_name { get; set; } = string.Empty;
    }

    public class SectionsFire
    {
        public int section_id { get; set; }
        public string section_name { get; set; } = string.Empty;
        public string territory_type { get; set; } = string.Empty;
        public string fire_hazard_level { get; set; } = string.Empty;
        public double section_area { get; set; }
        public int cutting_age { get; set; }
        public DateOnly work_date { get; set; }
        public string work_description { get; set; } = string.Empty;
    }
}
