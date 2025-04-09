namespace LesnoeServer.DTO
{
    public class FiltersDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public SettingsDTO Settings { get; set; } = new SettingsDTO();

        public FiltersDTO(string name, string label, string type, SettingsDTO settingsDto)
        {
            Name = name;
            Label = label;
            Type = type;
            Settings = settingsDto;
        }
    }
}
