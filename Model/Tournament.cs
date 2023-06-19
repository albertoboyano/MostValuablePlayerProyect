using CodeTestMostValuablePlayerProyect.Interface;

namespace CodeTestMostValuablePlayerProyect.Model
{
    public static class Tournament
    {
        public static void GetMVPTournament(List<IPlayer> players)
        {
            var MVP = players.GroupBy(x => x.Nickname).Select(x => new
            {
                Nickname = x.Key,
                PointCounter = x.Sum(s => s.PointCounter)
            }).OrderByDescending(x => x.PointCounter).FirstOrDefault();

            Console.WriteLine($"El MVP del torneo es {MVP.Nickname} con una puntuación de {MVP.PointCounter}");
        }
    }
}
