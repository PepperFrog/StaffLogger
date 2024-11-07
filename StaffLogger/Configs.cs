using Exiled.API.Interfaces;
using System.ComponentModel;
using static System.Net.WebRequestMethods;

namespace StaffLogger
{
    public class Config : IConfig
    {
        [Description("Indicates whether the plugin is enabled or not.")]
        public bool IsEnabled { get; set; } = true;

        [Description("Indicates whether the debug is enabled or not.")]
        public bool Debug { get; set; } = false;

        [Description("Indicate url for connection.")]
        public string Url { get; set; } = "https://pepperfrog.ddns.net/server/peppertracker/peppertrack.php?action=addsession";

    }

}