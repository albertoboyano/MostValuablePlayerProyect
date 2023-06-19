namespace CodeTestMostValuablePlayerProyect.Model.Players
{
    public class Player
    {
        public int MatchWinCounter { get; set; }
        public void MatchWin()
        {
            MatchWinCounter++;
        }
    }
}
