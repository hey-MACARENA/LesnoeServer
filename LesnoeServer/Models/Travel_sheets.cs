using System.ComponentModel.DataAnnotations;

namespace LesnoeServer.Tables
{
    public class Travel_sheets
    {
        [Key]
        public int travel_sheet_id { get; set; }
        public int driver_id { get; set; }
        public string vehicle_name { get; set; } = string.Empty;
        public DateOnly departure_date { get; set; }
        public int departure_mileage { get; set; }
        public int return_mileage { get; set; }
        public int departure_fuel { get; set; }
        public int return_fuel { get; set; }
        public double fuel_rate { get; set; }
        public double actual_fuel_consumption { get; set; }
    }

    public class Travel_sheetsDetails
    {
        public int travel_sheet_id { get; set; }
        public DateOnly departure_date { get; set; }
        public string vehicle_name { get; set; } = string.Empty;
        public int driver_id { get; set; }
        public string driver_name { get; set; } = string.Empty;
        public int departure_mileage { get; set; }
        public int return_mileage { get; set; }
        public int departure_fuel { get; set; }
        public int return_fuel { get; set; }
        public double fuel_rate { get; set; }
        public double actual_fuel_consumption { get; set; }
    }
}
