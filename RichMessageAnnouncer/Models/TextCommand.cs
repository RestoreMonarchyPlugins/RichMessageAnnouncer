namespace RestoreMonarchy.RichMessageAnnouncer.Models
{
    public class TextCommand
    {
        public TextCommand() { }
        public TextCommand(string name, string help, string color, string message, string iconUrl)
        {
            Name = name;
            Help = help;
            Color = color;
            Message = message;
            IconUrl = iconUrl;
        }
        
        public string Name { get; set; }
        public string Help { get; set; }
        public string Color { get; set; }
        public string Message { get; set; }
        public string IconUrl { get; set; }
    }
}