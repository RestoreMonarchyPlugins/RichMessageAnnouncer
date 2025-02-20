﻿using RestoreMonarchy.RichMessageAnnouncer.Models;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System.Collections.Generic;
using Rocket.Core.Logging;

namespace RestoreMonarchy.RichMessageAnnouncer.Commands
{
    public class RocketTextCommand : IRocketCommand
    {
        private RichMessageAnnouncerPlugin pluginInstance => RichMessageAnnouncerPlugin.Instance;

        public RocketTextCommand(TextCommand textCommand)
        {
            Name = textCommand.Name;
            Help = textCommand.Help;
            Color = textCommand.Color;
            Message = textCommand.Message;
            IconUrl = textCommand.IconUrl;
        }

        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name { get; set; }
        public string Help { get; set; }
        public string Color { get; set; }
        public string Message { get; set; }
        public string IconUrl { get; set; }

        public string Syntax => "";
        public List<string> Aliases => new List<string>();
        public List<string> Permissions => new List<string>();

        public void Execute(IRocketPlayer caller, string[] command)
        {
            pluginInstance.SendMessageToPlayer(caller, Message, null, IconUrl, Color);
        }
    }
}
