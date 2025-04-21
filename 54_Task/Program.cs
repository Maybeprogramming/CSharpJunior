namespace _54_Task
{
    public class Program
    {
        static void Main()
        {
            Console.Title = "ДЗ: Топ игроков сервера";
            LeaderBoard leaderBoard = new LeaderBoard();
            leaderBoard.Work();
        }
    }

    public class LeaderBoard
    {
        public void Work()
        {
            const int ShowTopByLevelCommand = 1;
            const int ShowTopByStrengthCommand = 2;
            const int ShowPlayersCommand = 3;
            const int ExitCommand = 4;

            bool isWork = true;
            int playerCount = 30;
            int playerTopCount = 3;
            List<Player> players = new PlayerFactory().GetPlayers(playerCount);

            while (isWork)
            {
                ShowMenu(ShowTopByLevelCommand, ShowTopByStrengthCommand, ShowPlayersCommand, ExitCommand);

                switch (UserUtils.ReadInputNumber())
                {
                    case ShowTopByLevelCommand:
                        ShowTopByLevel(players, playerTopCount);
                        break;
                    case ShowTopByStrengthCommand:
                        ShowTopByStrenght(players, playerTopCount);
                        break;
                    case ShowPlayersCommand:
                        ShowPacients(players);
                        break;
                    case ExitCommand:
                        isWork = false;
                        break;
                    default:
                        break;
                }

                UserUtils.Print($"\nДля продолжения нажмите любую клавишу", ConsoleColor.Green);
                Console.ReadKey();
            }
        }

        private void ShowTopByStrenght(List<Player> players, int playerCount) =>
            ShowTopByParametr(players, playerCount, player => player.Strength);

        private void ShowTopByLevel(List<Player> players, int playerCount) =>
            ShowTopByParametr(players, playerCount, player => player.Level);

        private void ShowTopByParametr(List<Player> players, int playerCount, Func<Player, int> sortParametr)
        {
            List<Player> playersTop = players.OrderByDescending(sortParametr).Take(playerCount).ToList();
            ShowPacients(playersTop);
        }

        private void ShowPacients(List<Player> players)
        {
            int index = 0;
            UserUtils.Print($"Список игроков: ", ConsoleColor.Green);

            players.ForEach((player) =>
                UserUtils.Print($"\n{++index}. {player.GetInfo()}"));
        }

        private void ShowMenu(int showTopByLevelCommand, int showTopByStrengthCommand, int showPlayersCommand, int exitCommand)
        {
            Console.Clear();
            UserUtils.Print($"Команды:", ConsoleColor.Green);
            UserUtils.Print($"\n{showTopByLevelCommand}. Показать топ 3 по уровню" +
                            $"\n{showTopByStrengthCommand}. Показать топ 3 по силе" +
                            $"\n{showPlayersCommand}. Показать всех игроков");
            UserUtils.Print($"\n{exitCommand}. Закрыть программу", ConsoleColor.Red);

            UserUtils.Print($"\nВведите команду: ", ConsoleColor.Green);
        }
    }

    public class Player
    {
        public Player(string name, int level, int strength)
        {
            Name = name;
            Level = level;
            Strength = strength;
        }

        public string Name { get; }
        public int Level { get; }
        public int Strength { get; }

        public string GetInfo() =>
            $"{Name}, - Уровень {Level}, - Сила {Strength}";
    }

    public class PlayerFactory
    {
        public List<Player> GetPlayers(int count)
        {
            List<Player> players = new();

            for (int i = 0; i < count; i++)
            {
                players.Add(CreatePlayer());
            }

            return players;
        }

        private Player CreatePlayer()
        {
            string name = PlayerData.TakeRandomName();
            int minLevel = 1;
            int maxLevel = 100;
            int minStrength = 100;
            int maxStrength = 500;
            int level = UserUtils.GenerateRandomNumber(minLevel, maxLevel);
            int strength = UserUtils.GenerateRandomNumber(minStrength, maxStrength);

            return new Player(name, level, strength);
        }
    }

    public static class PlayerData
    {
        private static string[] s_names;

        static PlayerData()
        {
            s_names = new string[]
            {
                "Павел", "Иван", "Сергей", "Олег", "Константин",
                "Анатолий", "Аркадий", "Петр", "Вячеслав", "Николай",
                "Владислав", "Роман", "Дмитрий", "Василий", "Михаил",
                "Руслан", "Равиль", "Фёдор", "Валерий", "Евгений"
            };
        }

        public static string TakeRandomName() =>
             s_names[UserUtils.GenerateRandomNumber(0, s_names.Length - 1)];
    }
    public static class UserUtils
    {
        private static Random s_random = new Random();

        public static int GenerateRandomNumber(int minNumber, int maxNumber) =>
            s_random.Next(minNumber, ++maxNumber);

        public static void Print<T>(T message) =>
            Console.Write(message.ToString());

        public static void Print<T>(T message, ConsoleColor consoleColor = ConsoleColor.White)
        {
            Console.ForegroundColor = consoleColor;
            Print(message);
            Console.ResetColor();
        }

        public static int ReadInputNumber()
        {
            int result;

            while (int.TryParse(Console.ReadLine(), out result) == false)
            {
                Print($"\nВы ввели не число!\nПопробуйте снова: ", ConsoleColor.DarkYellow);
            }

            return result;
        }
    }
}