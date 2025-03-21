using CodeTestMostValuablePlayerProyect.Interface;
using CodeTestMostValuablePlayerProyect.Model.Players;
using System.Xml.Linq;

namespace CodeTestMostValuablePlayerProyect.Tools
{
    public static class XMLHelper
    {
        public static List<T> GetPlayers<T>(List<string> paths, string sportType) where T : class
        {
            List<T> playersList = new List<T>();

            foreach (string path in paths)
            {
                XDocument xDocument = XDocument.Load(path);
                var players = xDocument.Root.Element(sportType)
                    .Descendants("Player")
                    .Select(node => CreatePlayer<T>(node))
                    .Where(player => player != null)
                    .ToList();

                playersList.AddRange(players);
            }

            if (typeof(T) == typeof(BasketballPlayer))
            {
                UpdatePointCounter<BasketballPlayer>((List<BasketballPlayer>)(object)playersList);
            }
            else if (typeof(T) == typeof(HandballPlayer))
            {
                UpdatePointCounter<HandballPlayer>((List<HandballPlayer>)(object)playersList);
            }

            return AggregatePlayers<T>(playersList);
        }

        private static T CreatePlayer<T>(XElement node) where T : class
        {
            if (typeof(T) == typeof(BasketballPlayer))
            {
                return new BasketballPlayer
                {
                    PlayerName = node.Element("PlayerName")?.Value,
                    Nickname = node.Element("Nickname")?.Value,
                    Number = int.Parse(node.Element("Number")?.Value ?? "0"),
                    TeamName = node.Element("TeamName")?.Value,
                    Position = node.Element("Position")?.Value,
                    ScoredPoints = int.Parse(node.Element("ScoredPoints")?.Value ?? "0"),
                    Rebounds = int.Parse(node.Element("Rebounds")?.Value ?? "0"),
                    Assists = int.Parse(node.Element("Assists")?.Value ?? "0")
                } as T;
            }
            else if (typeof(T) == typeof(HandballPlayer))
            {
                return new HandballPlayer
                {
                    PlayerName = node.Element("PlayerName")?.Value ?? string.Empty,
                    Nickname = node.Element("Nickname")?.Value,
                    Number = int.Parse(node.Element("Number")?.Value ?? "0"),
                    TeamName = node.Element("TeamName")?.Value,
                    Position = node.Element("Position")?.Value,
                    GoalsMade = int.Parse(node.Element("GoalsMade")?.Value ?? "0"),
                    GoalsReceived = int.Parse(node.Element("GoalsReceived")?.Value ?? "0")
                } as T;
            }
            return null;
        }

        private static List<T> AggregatePlayers<T>(List<T> playersList) where T : class
        {
            if (typeof(T) == typeof(BasketballPlayer))
            {
                return ((List<BasketballPlayer>)(object)playersList)
                    .GroupBy(p => p.Nickname)
                    .Select(g => new BasketballPlayer
                    {
                        Nickname = g.Key,
                        ScoredPoints = g.Sum(x => x.ScoredPoints),
                        Rebounds = g.Sum(x => x.Rebounds),
                        Assists = g.Sum(x => x.Assists)
                    })
                    .ToList() as List<T>;
            }
            else if (typeof(T) == typeof(HandballPlayer))
            {
                return ((List<HandballPlayer>)(object)playersList)
                    .GroupBy(p => p.Nickname)
                    .Select(g => new HandballPlayer
                    {
                        Nickname = g.Key,
                        GoalsMade = g.Sum(x => x.GoalsMade),
                        GoalsReceived = g.Sum(x => x.GoalsReceived)
                    })
                    .ToList() as List<T>;
            }

            return playersList;
        }

        private static void UpdatePointCounter<T>(List<T> playersList) where T : class
        {
            if (typeof(T) == typeof(BasketballPlayer))
            {
                string winnerTeam = GetWinnerBasketballTeam((List<BasketballPlayer>)(object)playersList);

                foreach (var player in (List<BasketballPlayer>)(object)playersList)
                {
                    if (player.TeamName == winnerTeam)
                    {
                        player.MatchWin();
                    }
                }
            }
            else if (typeof(T) == typeof(HandballPlayer))
            {
                string winnerTeam = GetWinnerHandballTeam((List<HandballPlayer>)(object)playersList);

                foreach (var player in (List<HandballPlayer>)(object)playersList)
                {
                    if (player.TeamName == winnerTeam)
                    {
                        player.MatchWin();
                    }
                }
            }
        }

        private static string GetWinnerBasketballTeam(List<BasketballPlayer> playersList)
        {
            return GetWinnerTeam(playersList, p => p.ScoredPoints);
        }

        private static string GetWinnerHandballTeam(List<HandballPlayer> playersList)
        {
            return GetWinnerTeam(playersList, p => p.GoalsMade);
        }

        private static string GetWinnerTeam<T>(List<T> playersList, Func<T, int> scoreSelector) where T : IPlayer
        {
            var teams = playersList.GroupBy(p => p.TeamName).Select(g => g.Key).ToList();
            int teamAScore = playersList.Where(p => p.TeamName == teams[0]).Sum(scoreSelector);
            int teamBScore = playersList.Where(p => p.TeamName == teams[1]).Sum(scoreSelector);

            return teamAScore > teamBScore ? teams[0] : teams[1];
        }
    }
}
