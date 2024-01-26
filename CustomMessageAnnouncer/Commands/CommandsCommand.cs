using Rocket.API;
using Rocket.Core;
using Rocket.Unturned.Chat;
using System.Collections.Generic;
using System.Linq;

namespace Zombiesia.CustomMessageAnnouncer.Commands
{
    public class CommandsCommand : IRocketCommand
    {
        private CustomMessageAnnouncerPlugin pluginInstance => CustomMessageAnnouncerPlugin.Instance;
        public AllowedCaller AllowedCaller => AllowedCaller.Both;
        public string Name => "commands";
        public string Help => "Shows all your available commands";
        public string Syntax => "";
        public List<string> Aliases => new List<string>() { "cmds" };
        public List<string> Permissions => new List<string>();

        public void Execute(IRocketPlayer caller, string[] command)
        {
            var permissions = R.Permissions.GetPermissions(caller);
            UnturnedChat.Say(caller, pluginInstance.Translate("Commands", string.Join(", ", permissions.Select(x => x.Name))), 
                UnturnedChat.GetColorFromName(pluginInstance.Configuration.Instance.CommandsMessageColor, UnityEngine.Color.green));
        }
    }
}
