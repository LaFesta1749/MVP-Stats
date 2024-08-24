using Exiled.API.Features;
using PlayerRoles;
using System;
using System.Collections.Generic;
using PlayerEvent = Exiled.Events.Handlers.Player;
using ServerEvent = Exiled.Events.Handlers.Server;

namespace MVP
{
    internal class Plugin : Plugin<Config>
    {
        public override string Name => "MVP";

        public override string Prefix => Name;

        public override string Author => "LaFesta1749";

        public override Version RequiredExiledVersion => new(8, 8, 1);

        public override Version Version => new(0, 8, 1);

        internal static Plugin Instance;

        internal static Handler Handler;

        public static long TimeStart = 0;

        // List of cached player names based on the player Id
        public static Dictionary<int, string> CachedNames = new();

        // Key: Player ID
        public static Dictionary<int, int> PlayerKills = new();

        // Key: <Player ID, Player Role>
        public static Dictionary<KeyValuePair<int, RoleTypeId>, int> ScpKills = new();

        // Key: <Player ID, Player Role>
        public static Dictionary<KeyValuePair<int, RoleTypeId>, long> EscapeTime = new();

        public override void OnEnabled()
        {
            Instance = this;

            Handler = new();

            PlayerEvent.Spawned += Handler.OnSpawned;
            PlayerEvent.Dying += Handler.OnKilled;
            PlayerEvent.Escaping += Handler.OnEscape;
            PlayerEvent.Verified += Handler.OnVerified;

            ServerEvent.RoundEnded += Handler.OnRoundEnded;
            ServerEvent.RoundStarted += Handler.OnRoundStarted;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            PlayerEvent.Spawned -= Handler.OnSpawned;
            PlayerEvent.Dying -= Handler.OnKilled;
            PlayerEvent.Escaping -= Handler.OnEscape;
            PlayerEvent.Verified -= Handler.OnVerified;

            ServerEvent.RoundEnded -= Handler.OnRoundEnded;
            ServerEvent.RoundStarted -= Handler.OnRoundStarted;

            PlayerKills.Clear();
            EscapeTime.Clear();
            ScpKills.Clear();

            Handler = null;
            Instance = null;

            base.OnDisabled();
        }
    }
}
