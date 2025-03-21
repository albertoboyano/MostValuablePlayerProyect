using CodeTestMostValuablePlayerProyect.Interface;

namespace CodeTestMostValuablePlayerProyect.Model
{
    public static class Tournament
    {
        // Método público que invoca la lógica de cálculo del MVP del torneo
        public static void GetMVPTournament(List<IPlayer> players)
        {
            var mvp = CalculateMVP(players);

            if (mvp != null)
            {
                Console.WriteLine($"El MVP del torneo es {mvp.Nickname} con una puntuación de {mvp.PointCounter}");
            }
            else
            {
                Console.WriteLine("No se encontró un MVP.");
            }
        }

        // Método privado para calcular el MVP
        private static MVPResult CalculateMVP(List<IPlayer> players)
        {
            // Verifica si la lista de jugadores está vacía
            if (players == null || !players.Any())
            {
                return null;
            }

            var mvp = players
                .GroupBy(player => player.Nickname)
                .Select(group => new MVPResult
                {
                    Nickname = group.Key,
                    PointCounter = group.Sum(player => player.PointCounter)
                })
                .OrderByDescending(result => result.PointCounter)
                .FirstOrDefault();

            return mvp;
        }

        // Clase para almacenar el resultado del MVP
        private class MVPResult
        {
            public string Nickname { get; set; }
            public int PointCounter { get; set; }
        }
    }
}
