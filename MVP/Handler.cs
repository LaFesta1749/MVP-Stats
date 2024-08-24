using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using System;
using System.Runtime.Remoting.Messaging;

namespace MVP
{
    internal class Handler
    {
        public void OnRoundStarted()
        {
            Plugin.TimeStart = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

        public void OnSpawned(SpawnedEventArgs ev)
        {
            if (!ev.Player.IsScp)
            {
                Plugin.PlayerKills.TryAdd(ev.Player.Id, 0);
            }
            else
            {
                Plugin.ScpKills.TryAdd(new(ev.Player.Id, ev.Player.Role.Type), 0);
            }

            Plugin.EscapeTime.TryAdd(new(ev.Player.Id, ev.Player.Role.Type), 0);
        }

        public void OnKilled(DyingEventArgs ev)
        {
            if (!ev.IsAllowed)
                return;

            if (ev.Attacker is null)
                return;

            if (ev.Attacker.IsScp)
            {
                Plugin.ScpKills[new(ev.Attacker.Id, ev.Attacker.Role.Type)]++;
            }
            else
            {
                Plugin.PlayerKills[ev.Attacker.Id]++;
            }
        }

        public void OnEscape(EscapingEventArgs ev)
        {
            if (!ev.IsAllowed)
                return;

            Plugin.EscapeTime[new(ev.Player.Id, ev.Player.Role.Type)] = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - Plugin.TimeStart;

            Log.Debug($"DBGG: A: {Plugin.EscapeTime.Count} + B: {Plugin.EscapeTime[new(ev.Player.Id, ev.Player.Role.Type)]}");
        }

        public void OnRoundEnded(RoundEndedEventArgs _) => Map.Broadcast(Plugin.Instance.Config.FinalBroadcastDuration, Utils.BuildMessage());

        public void OnVerified(VerifiedEventArgs ev) => Plugin.CachedNames.AddOrSet(ev.Player.Id, ev.Player.Nickname);
    }
}
