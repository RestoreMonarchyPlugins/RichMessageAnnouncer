# Rich Message Announcer


## Commands
- **/commands** - Display a list of permissions (works the same as `/p`)

## Configuration
```xml
<?xml version="1.0" encoding="utf-8"?>
<RichMessageAnnouncerConfiguration xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <MessageColor>yellow</MessageColor>
  <MessageInterval>180</MessageInterval>
  <Messages>
    <Message Text="Thank You for playing on our server!" IconUrl="https://i.imgur.com/pKphzxH.png" Color="yellow" />
    <Message Text="Check out RestoreMonarchy.com!" IconUrl="https://i.imgur.com/pKphzxH.png" Color="yellow" />
  </Messages>
  <TextCommands>
    <TextCommand>
      <Name>rules</Name>
      <Help>Shows rules</Help>
      <Color>orange</Color>
      <Message>There's no rules, just chill and have fun!</Message>
      <IconUrl>https://i.imgur.com/pKphzxH.png</IconUrl>
    </TextCommand>
  </TextCommands>
  <WebsiteCommands>
    <WebCommand>
      <Name>web</Name>
      <Help>Shows web url</Help>
      <Url>https://restoremonarchy.com</Url>
      <Description>Restore Monarchy Website</Description>
    </WebCommand>
  </WebsiteCommands>
  <EnableWelcomeMessage>true</EnableWelcomeMessage>
  <WelcomeMessage Text="Welcome to the server!" IconUrl="https://i.imgur.com/pKphzxH.png" Color="yellow" />
</RichMessageAnnouncerConfiguration>
```