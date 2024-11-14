using Rocket.API;
using Rocket.Core.Logging;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System.Collections.Generic;

namespace RestoreMonarchy.RichMessageAnnouncer.Commands
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
                Logger.Log($"{desc} {url}");
            } else
            {
                var player = (UnturnedPlayer)caller;
                player.Player.sendBrowserRequest(desc, url);
            }
        }
    }
}
