namespace LesnoeServer.DTO
{
    public class FiltersDto
    {
        public string Name { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public SettingsDto Settings { get; set; } = new SettingsDto();

        public FiltersDto(string name, string label, string type, SettingsDto settingsDto)
        {
            Name = name;
            Label = label;
            Type = type;
            Settings = settingsDto;
        }
    }
}
