using Rocket.API;
using System.Collections.Generic;
using System.Xml.Serialization;
using RestoreMonarchy.RichMessageAnnouncer.Models;

namespace RestoreMonarchy.RichMessageAnnouncer
{
    public class RichMessageAnnouncerConfiguration : IRocketPluginConfiguration
    {
        public string MessageColor { get; set; }
        public double MessageInterval { get; set; }
        [XmlArrayItem("Message")]
        public List<Message> Messages { get; set; }
        [XmlArrayItem("TextCommand")]
        public List<TextCommand> TextCommands { get; set; }
        [XmlArrayItem("WebCommand")]
        public List<WebsiteCommand> WebsiteCommands { get; set; }
        public bool EnableWelcomeMessage { get; set; }
        public Message WelcomeMessage { get; set; }

        public void LoadDefaults()
        {
            MessageColor = "yellow";
            MessageInterval = 180;
            Messages = new List<Message>()
            {
                new Message("Thank You for playing on our server!", "https://i.imgur.com/pKphzxH.png", "yellow"),
                new Message("Check out RestoreMonarchy.com", "https://i.imgur.com/pKphzxH.png", "yellow")
            };
            TextCommands = new List<TextCommand>()
            {
                new TextCommand("rules", "Shows rules", "orange", "There's no rules, just chill and have fun!", "https://i.imgur.com/pKphzxH.png")
            };
            WebsiteCommands = new List<WebsiteCommand>()
            {
                new WebsiteCommand("web", "Shows web url", "https://restoremonarchy.com", "Restore Monarchy Website")
            };
            EnableWelcomeMessage = true;
            WelcomeMessage = new Message("Welcome to the server!", "https://i.imgur.com/pKphzxH.png", "yellow");
        }
    }

    public sealed class Message
    {
        public Message(string text, string iconUrl, string color)
        {
            Text = text;
            IconUrl = iconUrl;
            Color = color;
        }

        public Message() { }

        [XmlAttribute]
        public string Text { get; set; }
        [XmlAttribute]
        public string IconUrl { get; set; }
        [XmlAttribute]
        public string Color { get; set; }
    }
}
