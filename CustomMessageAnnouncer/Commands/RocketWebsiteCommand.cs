using Rocket.API;
using Rocket.Unturned.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.Unturned.Player;

namespace Zombiesia.CustomMessageAnnouncer.Commands
{
    public class RocketWebsiteCommand : IRocketCommand
    {
        private readonly string url;
        private readonly string desc;
        public RocketWebsiteCommand(string name, string help, string url, string desc)
        {
            Name = name;
            Help = help;
            this.url = url;
            this.desc = desc;
        }
        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name { get; set; }

        public string Help { get; set; }

        public string Syntax => "";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>();

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (caller.Id == "Console")
            {
                UnturnedChat.Say(caller, $"{desc} {url}");
            } else
            {
                var player = (UnturnedPlayer)caller;
                player.Player.sendBrowserRequest(desc, url);
            }
        }
    }
}
