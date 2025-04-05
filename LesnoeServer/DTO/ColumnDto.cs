namespace LesnoeServer.DTO
{
    public class ColumnDto
    {
        public string Name { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public bool Required = true;
        public SettingsDto Settings { get; set; } = new SettingsDto();

        public ColumnDto(string name, string label, string type, bool required, SettingsDto settingsDto)
        {
            Name = name;
            Label = label;
            Type = type;
            Required = required;
            Settings = settingsDto;
        }
    }
}
