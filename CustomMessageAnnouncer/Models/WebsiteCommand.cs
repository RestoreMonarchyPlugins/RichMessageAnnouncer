using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombiesia.CustomMessageAnnouncer.Models
{
    public class WebsiteCommand
    {
        public WebsiteCommand(string name, string help, string url, string desc)
        {
            Name = name;
            Help = help;
            Url = url;
            Description = desc;
        }
        public WebsiteCommand() { }
        public string Name { get; set; }
        public string Help { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }        
    }
}
