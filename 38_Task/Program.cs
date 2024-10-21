namespace _38_Task
{
    public class Program
    {
        private static void Main()
        {
            Console.Title = "ДЗ: Работа с классами";
            string name = "Maybeonce";
            string classType = "Воин";
            int health = 100;
            int damage = 25;

            Player player = new Player(name, classType, health, damage);
            player.ShowInfo();

            Console.ReadLine();
        }
    }

    public class Player
    {
        private string _name;
        private string _classType;
        private int _health;
        private int _damage;

        public Player(string name, string classType, int health, int damage)
        {
            _name = name;
            _classType = classType;
            _health = health;
            _damage = damage;
        }

        public string Name => _name;
        public string Class => _classType;
        public int Health => _health;
        public int Damage => _damage;

        public void ShowInfo()
        {
            Console.WriteLine($"" +
                $"Персонаж: {_name}" +
                $"\nКласс: {_classType}" +
                $"\nЗдоровье: [{_health}]" +
                $"\nНаносимый урон: [{_damage}]");
        }
    }
}