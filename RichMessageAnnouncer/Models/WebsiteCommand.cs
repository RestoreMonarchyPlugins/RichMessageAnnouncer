namespace RestoreMonarchy.RichMessageAnnouncer.Models
{
    public class WebsiteCommand
    {
        public WebsiteCommand(string name, string help, string url, string desc)
        {
            Name = name;
            Help = help;
            Url = url;
            Description = desc;
        }
        public WebsiteCommand() { }
        public string Name { get; set; }
        public string Help { get; set; }
        public bool ShouldSerializeHelp() => !string.IsNullOrEmpty(Help);
        public string Url { get; set; }
        public string Description { get; set; }
        public bool ShouldSerializeDescription() => !string.IsNullOrEmpty(Description);
    }
}
