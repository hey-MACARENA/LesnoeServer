namespace LesnoeServer.DTO
{
    public class ColumnDto
    {
        public string Name { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public bool Required { get; set; } = true;
        public SettingsDto Settings { get; set; } = new SettingsDto();

        public ColumnDto(string name, string id, string label, string type, bool required, SettingsDto settingsDto)
        {
            Name = name;
            Id = id;
            Label = label;
            Type = type;
            Required = required;
            Settings = settingsDto;
        }
    }
}
