using CodeTestMostValuablePlayerProyect.Model.Players.Basketball;
using CodeTestMostValuablePlayerProyect.Model.Players.Handball;
using System.Xml.Linq;

namespace CodeTestMostValuablePlayerProyect.Tools
{
    public static class XMLHelper
    {
        private static List<BasketballPlayer> BasketballPlayersList = new List<BasketballPlayer>();
        private static List<HandballPlayer> HandballPlayersList = new List<HandballPlayer>();

        public static List<BasketballPlayer> GetBasketballPlayers(List<string> paths)
        {
            foreach (string path in paths)
            {
                XDocument xDocument = XDocument.Load(path);
                BasketballPlayersList.AddRange(xDocument.Root.Element("Basketball")
                    .Descendants("Player")
                    .Select(node => new BasketballPlayer
                    {
                        PlayerName = node.Element("PlayerName").Value,
                        Nickname = node.Element("Nickname").Value,
                        Number = int.Parse(node.Element("Number").Value),
                        TeamName = node.Element("TeamName").Value,
                        Position = node.Element("Position").Value,
                        ScoredPoints = int.Parse(node.Element("ScoredPoints").Value),
                        Rebounds = int.Parse(node.Element("Rebounds").Value),
                        Assists = int.Parse(node.Element("Assists").Value),
                    })
                .ToList());

                UpdatePointCounterBasketballPlayers();
            }

            var result = from item in BasketballPlayersList
                         group item by item.Nickname into g
                         select new BasketballPlayer()
                         {
                             Nickname = g.Key,
                             ScoredPoints = g.Sum(x => x.ScoredPoints),
                             Rebounds = g.Sum(x => x.Rebounds),
                             Assists = g.Sum(x => x.Assists)
                         };

            return result.ToList();

        }

        private static void UpdatePointCounterBasketballPlayers()
        {
            string winnerTeam = GetWinnerBasketballTeam(BasketballPlayersList);

            foreach (BasketballPlayer player in BasketballPlayersList)
            {
                if (player.TeamName == winnerTeam)
                {
                    player.MatchWin();
                }
            }
        }

        private static string GetWinnerBasketballTeam(List<BasketballPlayer> playersList)
        {
            List<string> teams = playersList.GroupBy(x => x.TeamName).Select(x => x.Key).ToList();

            int golTeamA = 0, golTeamB = 0;

            foreach (BasketballPlayer player in playersList)
            {
                if (player.TeamName == teams[0])
                {
                    golTeamA += player.ScoredPoints;
                }
                else if (player.TeamName == teams[1])
                {
                    golTeamB += player.ScoredPoints;
                }
            }
            return golTeamA > golTeamB ? teams[0] : teams[1];

        }

        public static List<HandballPlayer> GetHandballPlayers(List<string> paths)
        {


            foreach (string path in paths)
            {
                XDocument xDocument = XDocument.Load(path);
                HandballPlayersList.AddRange(xDocument.Root.Element("Handball")
                    .Descendants("Player")
                    .Select(node => new HandballPlayer
                    {
                        PlayerName = node.Element("PlayerName") != null ? node.Element("PlayerName").Value : string.Empty,
                        Nickname = node.Element("Nickname").Value,
                        Number = int.Parse(node.Element("Number").Value),
                        TeamName = node.Element("TeamName").Value,
                        Position = node.Element("Position").Value,
                        GoalsMade = int.Parse(node.Element("GoalsMade").Value),
                        GoalsReceived = int.Parse(node.Element("GoalsReceived").Value)
                    })
                    .ToList());

                UpdatePointCounterHandballPlayer();
            }

            var result = from item in HandballPlayersList
                         group item by item.Nickname into g
                         select new HandballPlayer()
                         {
                             Nickname = g.Key,
                             GoalsMade = g.Sum(x => x.GoalsMade),
                             GoalsReceived = g.Sum(x => x.GoalsReceived)
                         };

            return result.ToList();

        }

        private static void UpdatePointCounterHandballPlayer()
        {
            string winnerTeam = GetWinnerHandballTeams(HandballPlayersList);

            foreach (HandballPlayer player in HandballPlayersList)
            {
                if (player.TeamName == winnerTeam)
                {
                    player.MatchWin();
                }
            }
        }

        private static string GetWinnerHandballTeams(List<HandballPlayer> playersList)
        {
            List<string> teams = playersList.GroupBy(x => x.TeamName).Select(x => x.Key).ToList();

            int golTeamA = 0, golTeamB = 0;

            foreach (HandballPlayer player in playersList)
            {
                if (player.TeamName == teams[0])
                {
                    golTeamA += player.GoalsMade;
                }
                else if (player.TeamName == teams[1])
                {
                    golTeamB += player.GoalsMade;
                }
            }
            return golTeamA > golTeamB ? teams[0] : teams[1];
        }
    }
}
