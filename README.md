# Rich Message Announcer
Message announcer, text and web commands with rich text options and icons.

## Features
- Automatic message announcements (with shuffle option)
- Text commands
- Web commands
- Welcome message
- Join link
- Rich text and variables support

## Commands
- **/commands** - Display a list of commands the player has permission for

## Permissions
```xml
<Permission Cooldown="0">commands</Permission>
```
Permissions of custom commands are the same as the command name.

## Variables
- `{player_name}` - player's display name
- `{player_id}` - player's steam id
- `{server_name}` - server name (Commands.dat)
- `{server_players}` - number of players online
- `{server_maxplayers}` - maximum number of players (Commands.dat)
- `{server_map}` - the map server is running (Commands.dat)
- `{server_mode}` - the server mode PVP or PVE (Commands.dat)
- `{server_thumbnail}` - the value of `Browser.Thumbnail` (Config.json)
- `{server_icon}` - the value  of `Browser.Icon` (Config.json)

## Rich Text
- **Bold** - `{b}Bold{/b}`
- **Italic** - `{i}Italic{/i}`
- **Underline** - `{u}Underline{/u}`
- **Strikethrough** - `{s}Strikethrough{/s}`
- **Color** - `{color=#3498db}Color{/color}`  
- **Size** - `{size=20}Size{/size}`
- **Line Break** - `{br}`

## Configuration
```xml
<?xml version="1.0" encoding="utf-8"?>
<RichMessageAnnouncerConfiguration xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <MessageColor>white</MessageColor>
  <MessageIconUrl>{server_icon}</MessageIconUrl>
  <MessageInterval>300</MessageInterval>
  <MessagesShuffle>false</MessagesShuffle>
  <Messages>
    <Message Text="You are playing on {b}{server_name}{/b}." IconUrl="{server_icon}" Color="white" />
    <Message Text="Use {color=#3498db}/commands{/color} to see available commands!" IconUrl="{server_icon}" Color="white" />
    <Message Text="{size=15}Tip: You can customize these messages in the config.{/size}" IconUrl="{server_icon}" Color="white" />
    <Message Text="Format examples: {b}Bold{/b}, {color=#e74c3c}Color{/color}, {size=20}Size{/size}" IconUrl="{server_icon}" Color="white" />
  </Messages>
  <TextCommands>
    <TextCommand>
      <Name>rules</Name>
      <Color>white</Color>
      <Message>Server Rules:{br}1. {color=#e74c3c}Be respectful{/color}{br}2. {color=#2ecc71}No cheating{/color}{br}3. {color=#f1c40f}Have fun!{/color}</Message>
      <IconUrl>{server_icon}</IconUrl>
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
  <WelcomeMessage Text="{size=18}Welcome to the server {b}{player_name}{/b}!" IconUrl="{server_icon}" Color="white" />
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