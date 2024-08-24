using Exiled.API.Interfaces;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Rendering.HighDefinition;

namespace MVP
{
    internal class Config : IConfig
    {
        [Description("Do enable the plugin?")]
        public bool IsEnabled { get; set; } = true;

        [Description("Do enable the developer (debug) mode?")]
        public bool Debug { get; set; } = false;

        [Description("The broadcast message")]
        public Dictionary<DataType, string> FinalBroadcast { get; set; } = new()
        {
            {
                DataType.Escape,
                "Player %firstescapeplayer% was the first person to escape as %firstescaperole% in %firstescapetime% ?? No player escaped"
            },
            { 
                DataType.Scp, 
                "Player %mostkillscpplayer% was the %mostkillscprole% with the most kills: %mostkillsscpcount% ?? No kills from scp wtf" 
            },
            { 
                DataType.Human, 
                "%mostkillshumanplayer% was the human with the most kills: %mostkillshumancount% ?? Nah humans are not cruel here" 
            }
        };

        [Description("The broadcast duration")]
        public ushort FinalBroadcastDuration { get; set; } = 10;

        public Dictionary<RoleTypeId, string> CustomRoleNames { get; set; } = new()
        {
            {
                RoleTypeId.ClassD,
                "Class-D Personnel"
            }
        };
    }
}
