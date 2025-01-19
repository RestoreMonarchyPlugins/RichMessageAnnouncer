using Rocket.API;
using System.Collections.Generic;
using System.Xml.Serialization;
using RestoreMonarchy.RichMessageAnnouncer.Models;

namespace RestoreMonarchy.RichMessageAnnouncer
{
    public class RichMessageAnnouncerConfiguration : IRocketPluginConfiguration
    {
        public string MessageColor { get; set; }
        public string MessageIconUrl { get; set; }
        public double MessageInterval { get; set; }
        public bool MessagesShuffle { get; set; }
        [XmlArrayItem("Message")]
        public List<Message> Messages { get; set; }
        [XmlArrayItem("TextCommand")]
        public List<TextCommand> TextCommands { get; set; }
        [XmlArrayItem("WebCommand")]
        public List<WebsiteCommand> WebsiteCommands { get; set; }
        public bool EnableWelcomeMessage { get; set; }
        public Message WelcomeMessage { get; set; }
        public bool EnableJoinLink { get; set; } = false;
        public string JoinLinkUrl { get; set; } = "https://restoremonarchy.com";
        public string JoinLinkMessage { get; set; } = "Platform for Unturned server owners, players and developers.";

        public void LoadDefaults()
        {
            MessageColor = "white";
            MessageIconUrl = "{server_icon}";
            MessageInterval = 300;
            MessagesShuffle = false; 
            Messages =
            [
                new("You are playing on {b}{server_name}{/b}.", "{server_icon}", "white"),
                new("Use {color=#3498db}/commands{/color} to see available commands!", "{server_icon}", "white"),
                new("{size=15}Tip: You can customize these messages in the config.{/size}", "{server_icon}", "white"),
                new("Format examples: {b}Bold{/b}, {color=#e74c3c}Color{/color}, {size=20}Size{/size}", "{server_icon}", "white")
            ];
            TextCommands =
            [
                new("rules", null, "white", "Server Rules:{br}1. {color=#e74c3c}Be respectful{/color}{br}2. {color=#2ecc71}No cheating{/color}{br}3. {color=#f1c40f}Have fun!{/color}", "{server_icon}")
            ];
            WebsiteCommands =
            [
                new("website", null, "https://restoremonarchy.com", "Platform for Unturned server owners, players and developers.")
            ];
            EnableWelcomeMessage = true;
            WelcomeMessage = new Message("{size=18}Welcome to the server {b}{player_name}{/b}!", "{server_icon}", "white");
            EnableJoinLink = false;
            JoinLinkUrl = "https://restoremonarchy.com";
            JoinLinkMessage = "Platform for Unturned server owners, players and developers.";
        }
    }
}