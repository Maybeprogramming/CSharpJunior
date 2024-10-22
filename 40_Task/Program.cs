namespace _40_Task
{

    public class Program
    {
        public static void Main()
        {
            Console.Title = "ДЗ: База данных игроков";

            Database playerDatabase = new();
            ViewData view = new();
            view.Work(playerDatabase);
        }
    }

    public class ViewData
    {
        public void Work(Database database)
        {
            const string CommandShowPlayersData = "1";
            const string CommandAddPlayerToDataBase = "2";
            const string CommandRemovePlayerInDataBase = "3";
            const string CommandBanPlayerById = "4";
            const string CommandUnBanPlayerById = "5";
            const string CommandExitProgramm = "6";

            string menu = $"Меню:" +
                           $"\n{CommandShowPlayersData} - Вывести информацию обо всех игроках" +
                           $"\n{CommandAddPlayerToDataBase} - Добавить нового игрока в базу" +
                           $"\n{CommandRemovePlayerInDataBase} - Удалить игрока из базы" +
                           $"\n{CommandBanPlayerById} - Забанить игрока по ID" +
                           $"\n{CommandUnBanPlayerById} - Разбанить игкрока по ID" +
                           $"\n{CommandExitProgramm} - Выход из программы" +
                           $"\n\nВведите команду: ";
            string userInput;
            bool isRun = true;
            Database playersDatabase = database;

            while (isRun)
            {
                Console.Clear();
                Console.Write(menu);

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandShowPlayersData:
                        playersDatabase.ShowPlayers();
                        break;

                    case CommandAddPlayerToDataBase:
                        playersDatabase.AddPlayer();
                        break;

                    case CommandRemovePlayerInDataBase:
                        playersDatabase.RemovePlayer();
                        break;

                    case CommandBanPlayerById:
                        playersDatabase.BanPlayer();
                        break;

                    case CommandUnBanPlayerById:
                        playersDatabase.UnbanPlayer();
                        break;

                    case CommandExitProgramm:
                        isRun = false;
                        break;

                    default:
                        Console.WriteLine($"{userInput} - такой команды нет! Повторите ещё раз.");
                        break;
                }

                PrintContinueMessage();
            }
        }

        private void PrintContinueMessage()
        {
            Console.WriteLine("Нажмите любую клавишу чтобы продолжить");
            Console.ReadLine();
        }
    }

    public class Database
    {
        private List<Player> _players;

        public Database()
        {
            _players = new()
            {
                new Player ("Свирепый", 20),
                new Player ("Шустрый", 30),
                new Player ("Проницательный", 25),
                new Player ("Великолепный", 45),
                new Player ("Важный", 80)
            };
        }

        public void ShowPlayers()
        {
            Console.Clear();
            Console.WriteLine($"Список игроков:");

            foreach (Player player in _players)
            {
                Console.WriteLine(player.GetInfo());
            }
        }

        public void AddPlayer()
        {
            Console.Clear();
            Console.WriteLine("Добавление нового игрока в базу");
            Console.Write("Введите ник: ");
            string inputName = Console.ReadLine();

            if (inputName == string.Empty)
            {
                Console.Write("Никнейм не может быть пыстым");
                return;
            }

            Console.Write("Введите уровень: ");
            int inputLevel = ReadInputNumber();
            _players.Add(new Player(inputName, inputLevel));
            Console.WriteLine($"В базу добавлен игрок: {inputName} с уровнем: {inputLevel}");
        }

        public void RemovePlayer()
        {
            ShowPlayers();

            Console.Write("Введите Id игрока для удаления с базы: ");

            if (TryGetPlayer(out Player player))
            {
                _players.Remove(player);
                Console.WriteLine($"Игрок {player.Name} с ID: {player.Id} - успешно удалён из базы");
            }
        }

        public void BanPlayer()
        {
            ShowPlayers();

            Console.Write("Введите Id для бана игрока: ");

            if (TryGetPlayer(out Player player))
            {
                player.Ban();
                Console.WriteLine($"Игрок {player.Name} с ID: {player.Id} - успешно забанен");
            }
        }

        public void UnbanPlayer()
        {
            ShowPlayers();

            Console.Write("Введите Id для разбана игрока: ");

            if (TryGetPlayer(out Player player))
            {
                player.Unban();
                Console.WriteLine($"Игрок {player.Name} с ID: {player.Id} - успешно разбанен");
            }
        }

        private bool TryGetPlayer(out Player desiredPlayer)
        {
            int playerId = ReadInputNumber();

            foreach (Player player in _players)
            {
                if (player.Id.Equals(playerId))
                {
                    desiredPlayer = player;
                    return true;
                }
            }

            desiredPlayer = null;
            return false;
        }

        private int ReadInputNumber()
        {
            bool isTryParse = false;
            string userInput;
            int result = 0;

            while (isTryParse == false)
            {
                userInput = Console.ReadLine();
                isTryParse = int.TryParse(userInput, out result);

                if (isTryParse == false)
                {
                    Console.WriteLine($"Вы ввели не число: {userInput}");
                }

                if (result < 0)
                {
                    Console.Write($"Ошибка! Введеное число должно быть больше 0!\nПопробуйте снова: ");
                    isTryParse = false;
                }
            }

            return result;
        }
    }

    public class Player
    {
        private static int s_lastId = 0;

        public Player(string name, int level, bool isBan = false)
        {
            ++s_lastId;
            Id = s_lastId;
            Level = SetLevel(level);
            Name = name;
            IsBanned = isBan;
        }

        public int Id { get; }
        public string Name { get; }
        public int Level { get; private set; }
        public bool IsBanned { get; private set; }
        public string BanStatus => IsBanned == true ? "забанен" : "не забанен";

        public string GetInfo() => $"#{Id} | ник: {Name} \t | уровень: {Level} \t | статус: {BanStatus}";

        public void Ban() => IsBanned = true;

        public void Unban() => IsBanned = false;

        private int SetLevel(int value)
        {
            if (value > 0)
                Level = value;
            else
                Level = 0;

            return Level;
        }
    }
}