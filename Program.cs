using CodeTestMostValuablePlayerProyect.Constants;
using CodeTestMostValuablePlayerProyect.Interface;
using CodeTestMostValuablePlayerProyect.Model;
using CodeTestMostValuablePlayerProyect.Model.Players;
using CodeTestMostValuablePlayerProyect.Tools;

try
{
    Console.WriteLine("Cargando los partidos...\n");

    string dataDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Data");
    if (Directory.Exists(dataDirectory))
    {
        // Obtener los archivos XML
        var paths = Directory.GetFiles(dataDirectory, "*.xml").ToList();

        if (paths.Count != 0)
        {
            var players = new List<IPlayer>();

            // Obtener jugadores de baloncesto y balonmano
            var basketballPlayers = XMLHelper.GetPlayers<BasketballPlayer>(paths, SportsConstants.Basketball);
            var handballPlayers = XMLHelper.GetPlayers<HandballPlayer>(paths, SportsConstants.Handball);

            players.AddRange(basketballPlayers);
            players.AddRange(handballPlayers);

            // Mostrar jugadores y MVP de baloncesto
            DisplayPlayersStats(SportsConstants.Basketball, basketballPlayers);
            var basketballMVP = basketballPlayers.OrderByDescending(p => p.PointCounter).FirstOrDefault();
            if (basketballMVP != null)
            {
                Console.WriteLine($"Jugador MVP de baloncesto: {basketballMVP.Nickname}.");
            }

            Console.WriteLine();

            // Mostrar jugadores y MVP de balonmano
            DisplayPlayersStats(SportsConstants.Handball, handballPlayers);
            var handballMVP = handballPlayers.OrderByDescending(p => p.PointCounter).FirstOrDefault();
            if (handballMVP != null)
            {
                Console.WriteLine($"Jugador MVP de balonmano: {handballMVP.Nickname}.");
            }

            Console.WriteLine();

            // Mostrar MVP del torneo
            Tournament.GetMVPTournament(players);
        }
        else
        {
            Console.WriteLine("No se han encontrado ficheros .xml.");
        }
    }
    else
    {
        Console.WriteLine("La carpeta Data no existe en el directorio de salida.");
    }

    Console.ReadKey(); // Solo una vez al final
}
catch (Exception ex)
{
    Console.WriteLine($"Message: {ex.Message}");
    Console.ReadKey();
}

static void DisplayPlayersStats(string sportName, IEnumerable<IPlayer> players)
{
    if (players.Any())
    {
        Console.WriteLine($"Jugadores {sportName}");
        foreach (var player in players)
        {
            Console.WriteLine($"{player.Nickname} ha anotado {player.PointCounter}.");
        }
    }
}
