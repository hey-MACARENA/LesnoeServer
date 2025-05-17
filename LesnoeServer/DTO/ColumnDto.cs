namespace LesnoeServer.DTO
{
    public class ColumnDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public bool Required { get; set; } = true;
        public bool SortOn { get; set; } = false;
        public string Filter { get; set; } = string.Empty;
        public SettingsDTO Settings { get; set; } = new SettingsDTO();

        public ColumnDTO(string name, string id, string label, string type, bool required, bool sortOn, string filter, SettingsDTO settingsDto)
        {
            Name = name;
            Id = id;
            Label = label;
            Type = type;
            Required = required;
            SortOn = sortOn;
            Filter = filter;
            Settings = settingsDto;
        }
    }
}
