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
using System.Linq;
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
            } else
            {
                MessageColor = Color.green;
            }            

            timer = new Timer(Configuration.Instance.MessageInterval * 1000)
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

            Logger.Log($"{Name} has been unloaded!", ConsoleColor.Yellow);
        }

        public override TranslationList DefaultTranslations => new()
        {
            { "Commands", "[[b]]Your commands:[[/b]] {0}" }
        };

        private void SendMessage(object sender, ElapsedEventArgs e)
        {
            if (index >= Configuration.Instance.Messages.Count)
            {
                index = 0;
            }

            Message msg = Configuration.Instance.Messages[index];

            TaskDispatcher.QueueOnMainThread(() => 
            {
                if (Provider.clients.Count <= 0) 
                {
                    return;
                }


                Color color;
                if (!string.IsNullOrEmpty(msg.Color))
                {
                    color = UnturnedChat.GetColorFromName(msg.Color, MessageColor);
                } else
                {
                    color = MessageColor;
                }
                
                string iconUrl = msg.IconUrl ?? Configuration.Instance.MessageIconUrl;
                string text = msg.Text.Replace('{', '<').Replace('}', '>');
                ChatManager.serverSendMessage(text, color, null, null, EChatMode.SAY, iconUrl, true);
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

            Color color;
            if (!string.IsNullOrEmpty(msg.Color))
            {
                color = UnturnedChat.GetColorFromName(msg.Color, MessageColor);
            }
            else
            {
                color = MessageColor;
            }
            string iconUrl = msg.IconUrl ?? Configuration.Instance.MessageIconUrl;
            string text = msg.Text.Replace('{', '<').Replace('}', '>');
            ChatManager.serverSendMessage(text, color, null, player.SteamPlayer(), EChatMode.SAY, iconUrl, true);
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
                msg = msg.Replace("[[", "<").Replace("]]", ">");
            } else
            {
                msg = translationKey;
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
    }
}
