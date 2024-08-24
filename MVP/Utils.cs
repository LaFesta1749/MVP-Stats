using Exiled.API.Extensions;
using Exiled.API.Features;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVP
{
    internal class Utils
    {
        public static KeyValuePair<KeyValuePair<int, RoleTypeId>, int> GetMostScpKills()
        {
            KeyValuePair<KeyValuePair<int, RoleTypeId>, int> Element = new(new(-1, RoleTypeId.None), 0);

            foreach (KeyValuePair<KeyValuePair<int, RoleTypeId>, int> Data in Plugin.ScpKills)
            {
                if (Data.Value > Element.Value)
                {
                    Element = Data;
                }
            }

            return Element;
        }

        public static KeyValuePair<int, int> GetMostKills()
        {
            KeyValuePair<int, int> Element = new(-1, 0);

            foreach (KeyValuePair<int, int> Data in Plugin.PlayerKills)
            {
                if (Data.Value > Element.Value)
                {
                    Element = Data;
                }
            }

            return Element;
        }

        public static KeyValuePair<KeyValuePair<int, RoleTypeId>, long> GetFirstExitTime()
        {
            KeyValuePair<KeyValuePair<int, RoleTypeId>, long> Element = new(new(-1, RoleTypeId.None), 0);

            foreach (KeyValuePair<KeyValuePair<int, RoleTypeId>, long> Data in Plugin.EscapeTime)
            {
                if (Data.Value > Element.Value)
                {
                    Element = Data;
                }
            }

            return Element;
        }

        public static string BuildTime(long time)
        {
            Log.Debug($"Evaluating time received at {DateTimeOffset.UtcNow.ToUnixTimeSeconds()}: {time}");
            int m = 0;

            for (int i = 0; i < time / 60; i += 60)
            {
                m++;
            }

            return $"{AdjustNumber(m)}:{AdjustNumber(time - (m * 60))}";
        }

        public static string AdjustNumber(long number)
        {
            if (number.ToString().Length < 2)
            {
                return $"0{number}";
            }

            return number.ToString();
        }

        public static string BuildMessage()
        {
            string Result = string.Empty;

            foreach (KeyValuePair<DataType, string> Element in Plugin.Instance.Config.FinalBroadcast)
            {
                Log.Debug($"Evaluating info for {Element.Key}@{Element.Value}");
                Result += $"{BuildMessage(Element.Key)}\n";
                Log.Debug($"Results: {Result}");
            }

            return Result;
        }

        public static string BuildMessage(DataType type)
        {
            return BuildMessage(type, RetriveMessage(type), RetriveMessage(type, true));
        }

        public static string BuildMessage(DataType type, string baseMessage, string errorMessage = null)
        {
            switch (type)
            {
                case DataType.Human:
                    KeyValuePair<int, int> Kills = GetMostKills();

                    if (Kills.Key < 1 || Kills.Value < 1)
                    {
                        return errorMessage ?? string.Empty;
                    }

                    return baseMessage            
                        .Replace("%mostkillshumanplayer%", RetrivePlayer(Kills.Key))
                        .Replace("%mostkillshumancount%", Kills.Value.ToString());

                case DataType.Scp:
                    KeyValuePair<KeyValuePair<int, RoleTypeId>, int> ScpKill = GetMostScpKills();

                    if (ScpKill.Value < 1)
                    {
                        return errorMessage ?? string.Empty;
                    }

                    return baseMessage
                        .Replace("%mostkillscpplayer%", RetrivePlayer(ScpKill.Key.Key))
                        .Replace("%mostkillscprole%", RetriveRoleName(ScpKill.Key.Value))
                        .Replace("%mostkillsscpcount%", ScpKill.Value.ToString());

                case DataType.Escape:
                    KeyValuePair<KeyValuePair<int, RoleTypeId>, long> Escape = GetFirstExitTime();

                    if (Escape.Value < 3)
                    {
                        return errorMessage ?? string.Empty;
                    }

                    return baseMessage
                        .Replace("%firstescapeplayer%", RetrivePlayer(Escape.Key.Key))
                        .Replace("%firstescaperole%", RetriveRoleName(Escape.Key.Value))
                        .Replace("%firstescaperolecolor%", Escape.Key.Value.GetColor().ToString())
                        .Replace("%firstescapetime%", BuildTime(Escape.Value));

                default:
                    return string.Empty;
            }
        }

        public static string RetriveMessage(DataType type, bool error = false)
        {
            string Message = Plugin.Instance.Config.FinalBroadcast[type] ?? "";
            
            if (Message.Contains(" ?? "))
            {
                List<string> MessageBase = Message.Split(new string[] { " ?? " }, System.StringSplitOptions.RemoveEmptyEntries).ToList();

                if (error)
                {
                    return (MessageBase.Count > 1) ? MessageBase[1] : string.Empty;
                }
                return MessageBase[0];
            }

            return Message;
        }

        public static string RetrivePlayer(int Id)
        {
            return Plugin.CachedNames?[Id] ?? string.Empty;
        }

        public static string RetriveRoleName(RoleTypeId element) => Plugin.Instance.Config.CustomRoleNames.ContainsKey(element) ? Plugin.Instance.Config.CustomRoleNames[element] : element.GetFullName();
    }
}
