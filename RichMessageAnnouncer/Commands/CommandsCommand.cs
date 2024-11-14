using Rocket.API;
using Rocket.API.Serialisation;
using Rocket.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestoreMonarchy.RichMessageAnnouncer.Commands
{
    public class CommandsCommand : IRocketCommand
    {
        private RichMessageAnnouncerPlugin pluginInstance => RichMessageAnnouncerPlugin.Instance;
        public AllowedCaller AllowedCaller => AllowedCaller.Both;
        public string Name => "commands";
        public string Help => "Shows all players available permissions";
        public string Syntax => "";
        public List<string> Aliases => ["cmds"];
        public List<string> Permissions => [];

        public void Execute(IRocketPlayer caller, string[] command)
        {
            List<Permission> permissions = R.Permissions.GetPermissions(caller);

            List<string> commands = new();
            foreach (IRocketCommand cmd in R.Commands.Commands)
            {
                foreach (Permission permission in permissions)
                {
                    if (cmd.Permissions.Any(x => x.Equals(permission.Name, StringComparison.OrdinalIgnoreCase)))
                    {
                        commands.Add(cmd.Name);
                        break;
                    }

                    if (cmd.Aliases.Any(x => x.Equals(permission.Name, StringComparison.OrdinalIgnoreCase)))
                    {
                        commands.Add(permission.Name);
                        break;
                    }

                    if (cmd.Name.Equals(permission.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        commands.Add(cmd.Name);
                        break;
                    }
                }
            }

            string commandsString = string.Join(", ", commands.Select(x => x.ToLower()).Distinct());
            pluginInstance.SendMessageToPlayer(caller, "Commands", [commandsString]);
        }
    }
}
