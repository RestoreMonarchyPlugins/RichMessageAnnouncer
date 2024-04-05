using RestoreMonarchy.CustomMessageAnnouncer.Commands;
using Rocket.API.Collections;
using Rocket.Core;
using Rocket.Core.Plugins;
using Rocket.Core.Utils;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System.Timers;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace RestoreMonarchy.CustomMessageAnnouncer
{
    public class CustomMessageAnnouncerPlugin : RocketPlugin<CustomMessageAnnouncerConfiguration>
    {
        private Timer timer;
        private int index = 0;

        public static CustomMessageAnnouncerPlugin Instance { get; private set; }

        protected override void Load()
        {
            Instance = this;
            timer = new Timer(Configuration.Instance.MessageInterval * 1000);
            timer.AutoReset = true;
            timer.Elapsed += SendMessage;
            timer.Start();
            
            foreach (var textCommand in Configuration.Instance.TextCommands)
            {
                var cmd = new RocketTextCommand(textCommand);
                R.Commands.Register(cmd);
            }
            foreach (var webCommand in Configuration.Instance.WebsiteCommands)
            {
                var cmd = new RocketWebsiteCommand(webCommand.Name, webCommand.Help, webCommand.Url, webCommand.Description);
                R.Commands.Register(cmd);
            }

            U.Events.OnPlayerConnected += OnPlayerConnected;

            Logger.Log($"{Name} {Assembly.GetName().Version} has been loaded!");
        }

        public override TranslationList DefaultTranslations => new TranslationList()
        {
            { "Commands", "Your commands: {0}" }
        };

        protected override void Unload()
        {
            U.Events.OnPlayerConnected -= OnPlayerConnected;
            timer.Dispose();
            timer.Elapsed -= SendMessage;

            Logger.Log($"{Name} has been unloaded!");
        }

        private void SendMessage(object sender, ElapsedEventArgs e)
        {
            if (index >= Configuration.Instance.Messages.Count)
            {
                index = 0;
            }

            var msg = Configuration.Instance.Messages[index];

            TaskDispatcher.QueueOnMainThread(() => 
            {
                if (Provider.clients.Count <= 0)
                    return;

                Color color = UnturnedChat.GetColorFromName(msg.Color, Color.green);

                if (Configuration.Instance.UseRich)
                {
                    ChatManager.serverSendMessage(msg.Text.Replace('{', '<').Replace('}', '>'), color, null, null, EChatMode.SAY, msg.IconUrl, true);
                }
                else
                {
                    UnturnedChat.Say(msg.Text, color);
                }
            });
            
            index++;
        }

        private void OnPlayerConnected(UnturnedPlayer player)
        {
            if (!Configuration.Instance.EnableWelcomeMessage)
            {
                return;
            }

            Message msg = Configuration.Instance.WelcomeMessage;
            Color color = UnturnedChat.GetColorFromName(msg.Color, Color.green);


            if (Configuration.Instance.UseRich)
            {
                ChatManager.serverSendMessage(msg.Text.Replace('{', '<').Replace('}', '>'), color, null, player.SteamPlayer(), EChatMode.SAY, msg.IconUrl, true);
            }
            else
            {
                UnturnedChat.Say(player, msg.Text, color);
            }
        }
    }
}
