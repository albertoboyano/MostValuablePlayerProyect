using CodeTestMostValuablePlayerProyect.Interface;

namespace CodeTestMostValuablePlayerProyect.Model.Players.Basketball
{
    public class BasketballPlayer : Player, IPlayer
    {
        public string PlayerName { get; set; }
        public string Nickname { get; set; }
        public int Number { get; set; }
        public string TeamName { get; set; }
        public string Position { get; set; }
        public int ScoredPoints { get; set; }
        public int Rebounds { get; set; }
        public int Assists { get; set; }
        public int PointCounter
        {
            get
            {
                return ScoredPoints * 2 + Rebounds * 1 + Assists * 3 + MatchWinCounter * 10;
            }
        }
    }
}
