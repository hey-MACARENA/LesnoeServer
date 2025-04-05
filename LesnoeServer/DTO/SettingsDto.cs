namespace LesnoeServer.DTO
{
    public class SettingsDto
    {
        public int MaxChar { get; set; } = 0;
        public int MinNum { get; set; } = 0;
        public int MaxNum { get; set; } = 0;
        public string Url { get; set; } = string.Empty;

        public SettingsDto() { }

        public SettingsDto(int maxChar = 0, int minNum = 0, int maxNum = 0, string url = "") {
            MaxChar = maxChar;
            MinNum = minNum;
            MaxNum = maxNum;
            Url = url;
        }
    }
}
