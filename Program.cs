using CodeTestMostValuablePlayerProyect.Interface;
using CodeTestMostValuablePlayerProyect.Model;
using CodeTestMostValuablePlayerProyect.Model.Players.Basketball;
using CodeTestMostValuablePlayerProyect.Model.Players.Handball;
using CodeTestMostValuablePlayerProyect.Tools;

try
{
    Console.WriteLine($"Cargando los partidos...");
    Console.WriteLine();
    if (Directory.Exists(Directory.GetCurrentDirectory() + "\\Data\\"))
    {
        List<string> paths = new List<string>();
        // Los ficheros xml de los partidos se guardaran en el outputDirectory\Data
        paths = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Data\\", "*.xml").ToList();

        if (paths.Count > 0)
        {
            List<IPlayer> players = new List<IPlayer>();
            List<BasketballPlayer> basketballPlayers = XMLHelper.GetBasketballPlayers(paths);
            List<HandballPlayer> handballPlayers = XMLHelper.GetHandballPlayers(paths);

            players.AddRange(basketballPlayers);
            players.AddRange(handballPlayers);

            if (basketballPlayers.Count > 0)
            {
                Console.WriteLine("Jugadores Baloncesto");
                foreach (var player in basketballPlayers)
                {
                    Console.WriteLine($"{player.Nickname} ha anotado {player.PointCounter}.");
                }
                BasketballPlayer basketballPlayersMVP = basketballPlayers.OrderByDescending(x => x.PointCounter).FirstOrDefault();
                if (basketballPlayersMVP != null)
                {
                    Console.WriteLine($"Jugador MVP de baloncesto {basketballPlayersMVP.Nickname}.");
                }
            }

            Console.WriteLine();

            if (handballPlayers.Count > 0)
            {
                Console.WriteLine("Jugadores Balonmano");
                foreach (var player in handballPlayers)
                {
                    Console.WriteLine($"{player.Nickname} ha anotado {player.PointCounter}.");
                }
                HandballPlayer handballPlayerMVP = handballPlayers.OrderByDescending(x => x.PointCounter).FirstOrDefault();
                if (handballPlayerMVP != null)
                {
                    Console.WriteLine($"Jugador MVP de balonmano {handballPlayerMVP.Nickname}.");
                }
            }

            Console.WriteLine();
            Tournament.GetMVPTournament(players);
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("No se han encontrado ficheros .xml.");
        }

        Console.ReadKey();
    }
    else
    {
        Console.WriteLine("La carpeta Data no existe en el directorio de salida.");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Message: {ex.Message}");
    Console.ReadKey();
}