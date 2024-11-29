# Rich Message Announcer
Message announcer, text and web commands with rich text options and icons.

## Commands
- **/commands** - Display a list of commands the player has permission for

## Configuration
```xml
<?xml version="1.0" encoding="utf-8"?>
<RichMessageAnnouncerConfiguration xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <MessageColor>white</MessageColor>
  <MessageIconUrl>https://i.imgur.com/tLPIfuf.png</MessageIconUrl>
  <MessageInterval>300</MessageInterval>
  <Messages>
    <Message Text="Welcome! This server uses {b}RichMessageAnnouncer{/b}." IconUrl="https://i.imgur.com/tLPIfuf.png" Color="white" />
    <Message Text="Use {color=#3498db}/commands{/color} to see available commands!" IconUrl="https://i.imgur.com/tLPIfuf.png" Color="white" />
    <Message Text="{size=15}Tip: You can customize these messages in the config.{/size}" IconUrl="https://i.imgur.com/tLPIfuf.png" Color="white" />
    <Message Text="Format examples: {b}Bold{/b}, {color=#e74c3c}Color{/color}, {size=20}Size{/size}" IconUrl="https://i.imgur.com/tLPIfuf.png" Color="white" />
  </Messages>
  <TextCommands>
    <TextCommand>
      <Name>rules</Name>
      <Color>white</Color>
      <Message>Server Rules:{br}1. {color=#e74c3c}Be respectful{/color}{br}2. {color=#2ecc71}No cheating{/color}{br}3. {color=#f1c40f}Have fun!{/color}</Message>
      <IconUrl>https://i.imgur.com/tLPIfuf.png</IconUrl>
    </TextCommand>
  </TextCommands>
  <WebsiteCommands>
    <WebCommand>
      <Name>website</Name>
      <Url>https://restoremonarchy.com</Url>
      <Description>Platform for Unturned server owners, players and developers.</Description>
    </WebCommand>
  </WebsiteCommands>
  <EnableWelcomeMessage>true</EnableWelcomeMessage>
  <WelcomeMessage Text="{size=18}Welcome to the server!{/size}{br}Rules: {color=#e74c3c}Be respectful{/color} • {color=#2ecc71}No cheating{/color} • {color=#f1c40f}Have fun!{/color}{br}{color=#3498db}Type /commands for commands{/color}" IconUrl="https://i.imgur.com/tLPIfuf.png" Color="white" />
  <EnableJoinLink>false</EnableJoinLink>
  <JoinLinkUrl>https://restoremonarchy.com</JoinLinkUrl>
  <JoinLinkMessage>Platform for Unturned server owners, players and developers.</JoinLinkMessage>
</RichMessageAnnouncerConfiguration>
```

## Translations
```xml
<?xml version="1.0" encoding="utf-8"?>
<Translations xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <Translation Id="Commands" Value="[[b]]Your commands:[[/b]] {0}" />
</Translations>
```