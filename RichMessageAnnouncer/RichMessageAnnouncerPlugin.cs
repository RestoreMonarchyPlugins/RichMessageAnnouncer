using RestoreMonarchy.RichMessageAnnouncer.Commands;
using RestoreMonarchy.RichMessageAnnouncer.Models;
using Rocket.API;
using Rocket.API.Collections;
using Rocket.Core;
using Rocket.Core.Plugins;
using Rocket.Core.Utils;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Timers;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace RestoreMonarchy.RichMessageAnnouncer
{
    public class RichMessageAnnouncerPlugin : RocketPlugin<RichMessageAnnouncerConfiguration>
    {
        private Timer timer;
        private int index = 0;

        public static RichMessageAnnouncerPlugin Instance { get; private set; }
        public Color MessageColor { get; set; }

        protected override void Load()
        {
            Instance = this;
            if (!string.IsNullOrEmpty(Configuration.Instance.MessageColor))
            {
                MessageColor = UnturnedChat.GetColorFromName(Configuration.Instance.MessageColor, Color.green);
            }
            else
            {
                MessageColor = Color.green;
            }            

            timer = new Timer(Math.Max(1, Configuration.Instance.MessageInterval) * 1000)
            {
                AutoReset = true
            };
            timer.Elapsed += SendMessage;
            timer.Start();

            foreach (TextCommand textCommand in Configuration.Instance.TextCommands)
            {
                RocketTextCommand cmd = new(textCommand);
                R.Commands.Register(cmd);
            }
            foreach (WebsiteCommand webCommand in Configuration.Instance.WebsiteCommands)
            {
                RocketWebsiteCommand cmd = new(webCommand.Name, webCommand.Help, webCommand.Url, webCommand.Description);
                R.Commands.Register(cmd);
            }

            U.Events.OnPlayerConnected += OnPlayerConnected;

            Logger.Log($"{Name} {Assembly.GetName().Version} has been loaded!", ConsoleColor.Yellow);
            Logger.Log($"Check out more Unturned plugins at restoremonarchy.com");
        }

        protected override void Unload()
        {
            U.Events.OnPlayerConnected -= OnPlayerConnected;

            timer.Dispose();
            timer.Elapsed -= SendMessage;
            timer.Enabled = false;
            timer = null;

            index = 0;
            Messages = null;

            Logger.Log($"{Name} has been unloaded!", ConsoleColor.Yellow);
        }

        public override TranslationList DefaultTranslations => new()
        {
            { "Commands", "[[b]]Your commands:[[/b]] {0}" }
        };
        
        private List<Message> Messages { get; set; }
        private Message lastMessage;
        private void ResetMessages()
        {
            if (Configuration.Instance.Messages == null || !Configuration.Instance.Messages.Any())
            {
                Messages = null;
                return;
            }

            List<Message> messagesList = Configuration.Instance.Messages.ToList();
            if (Configuration.Instance.MessagesShuffle)
            {
                System.Random random = new();
                int n = messagesList.Count;
                while (n > 1)
                {
                    n--;
                    int k = random.Next(n + 1);
                    Message temp = messagesList[k];
                    messagesList[k] = messagesList[n];
                    messagesList[n] = temp;
                }

                if (messagesList.Count >= 2 && lastMessage != null)
                {
                    if (lastMessage == messagesList[0])
                    {
                        Message temp = messagesList[0];
                        messagesList[0] = messagesList[1];
                        messagesList[1] = temp;
                    }
                }
            }

            Messages = messagesList;
        }

        private void SendMessage(object sender, ElapsedEventArgs e)
        {
            TaskDispatcher.QueueOnMainThread(() =>
            {
                if (Provider.clients.Count <= 0)
                {
                    return;
                }

                if (index >= (Messages?.Count ?? 0))
                {
                    index = 0;
                    lastMessage = Messages?.LastOrDefault();
                    Messages = null;
                }

                if (Messages == null)
                {
                    ResetMessages();
                    if (Messages.Count == 0)
                    {
                        return;
                    }
                }

                Message msg = Messages[index];
                index++;

                string iconUrl = msg.IconUrl ?? Configuration.Instance.MessageIconUrl;
                foreach (Player player in PlayerTool.EnumeratePlayers())
                {
                    SendMessageToPlayer(UnturnedPlayer.FromPlayer(player), msg.Text, null, iconUrl, msg.Color);
                }
            });
        }

        private void OnPlayerConnected(UnturnedPlayer player)
        {
            if (Configuration.Instance.EnableWelcomeMessage)
            {
                Message msg = Configuration.Instance.WelcomeMessage;
                string iconUrl = msg.IconUrl ?? Configuration.Instance.MessageIconUrl;
                SendMessageToPlayer(player, msg.Text, null, iconUrl, msg.Color);
            }

            if (Configuration.Instance.EnableJoinLink)
            {
                string url = Configuration.Instance.JoinLinkUrl;
                string message = Configuration.Instance.JoinLinkMessage;

                ReplaceVariables(ref message, player);
                ReplaceVariables(ref url, player);

                player.Player.sendBrowserRequest(message, url);
            }
        }

        internal void SendMessageToPlayer(IRocketPlayer player, string translationKey, object[] placeholder = null, string iconUrl = null, string color = null)
        {
            string msg;
            if (DefaultTranslations.Any(x => x.Id == translationKey))
            {
                if (placeholder == null)
                {
                    placeholder = [];
                }
                msg = Translate(translationKey, placeholder);
                ReplaceVariables(ref msg, player);
                msg = msg.Replace("[[", "<").Replace("]]", ">");
            }
            else
            {
                msg = translationKey;
                ReplaceVariables(ref msg, player);
                msg = msg.Replace("{", "<").Replace("}", ">");
            }            

            if (player is ConsolePlayer)
            {
                msg = msg.Replace("<b>", "").Replace("</b>", "");
                Logger.Log(msg);
                return;
            }

            SteamPlayer steamPlayer = null;
            if (player != null)
            {
                UnturnedPlayer unturnedPlayer = (UnturnedPlayer)player;
                steamPlayer = unturnedPlayer.SteamPlayer();
            }

            if (iconUrl == null)
            {
                iconUrl = Configuration.Instance.MessageIconUrl;
            }

            ReplaceVariables(ref iconUrl, player);

            Color messageColor;
            if (color != null)
            {
                messageColor = UnturnedChat.GetColorFromName(color, MessageColor);
            }
            else
            {
                messageColor = MessageColor;
            }

            ChatManager.serverSendMessage(msg, messageColor, null, steamPlayer, EChatMode.SAY, iconUrl, true);
        }

        internal void ReplaceVariables(ref string text, IRocketPlayer player)
        {
            text = text
                .Replace("{player_name}", player?.DisplayName ?? string.Empty)
                .Replace("{player_id}", player?.Id ?? string.Empty)
                .Replace("{server_name}", Provider.serverName)
                .Replace("{server_players}", Provider.clients.Count.ToString("N0"))
                .Replace("{server_maxplayers}", Provider.maxPlayers.ToString("N0"))
                .Replace("{server_map}", Level.info?.name ?? string.Empty)
                .Replace("{server_mode}", Provider.mode.ToString())
                .Replace("{server_thumbnail}", Provider.configData.Browser.Thumbnail)
                .Replace("{server_icon}", Provider.configData.Browser.Icon);
        }
    }
}
