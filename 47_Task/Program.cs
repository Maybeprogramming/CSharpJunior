namespace _47_Task
{
    public class Program
    {
        static void Main()
        {
            Console.Title = "ДЗ: Война";
            Battlegraund battlegraund = new Battlegraund();
            battlegraund.Work();
            Console.ReadLine();
        }
    }

    public class Battlegraund
    {
        public void Work() 
        {
            //For TEST -------------------------------------------------------------------------------
            List<Fighter> fighters = new List<Fighter>()
            {
                new Soldier("Пехотинец", 10, 2, 3),
                new Sniper("Снайпер", 10, 1, 4),
                new Bomber("Гранатометчик", 10, 0, 5),
                new MachineGunner("Пулеметчик", 10, 3, 3)
            };

            foreach (Fighter fighter in fighters)
            {
                UserUtils.Print($"\n{fighter.GetInfo()}");
            }

            Console.ReadKey();

            Soldier soldier = new Soldier("Пехотинец", 10, 2, 3);
            Sniper sniper = new Sniper("Снайпер", 10, 1, 4);
            Bomber bomber = new Bomber("Гранатометчик", 10, 0, 5);
            MachineGunner machineGunner = new MachineGunner("Пулеметчик", 10, 3, 3);

            soldier.Attack(sniper);
            sniper.Attack(bomber);
            bomber.Attack(machineGunner);
            machineGunner.Attack(soldier);
            //END TEST -------------------------------------------------------------------------------

            Console.ReadKey();
        }
    }

    public class Squad
    {

    }

    public abstract class Fighter : IDamageable
    {
        private int _health;

        protected Fighter(string name, int health, int armor, int damage)
        {
            Name = name;
            Health = health;
            Armor = armor;
            Damage = damage;
        }

        public string Name { get; }
        public int Health
        {
            get => _health;
            private set => SetHealth(value);
        }

        public int Armor { get; private set; }
        public int Damage { get; private set; }

        public bool IsAlive => Health >= 0;

        public virtual void Attack(IDamageable target)
        {
            if (IsAlive && target.IsAlive)
            {
                UserUtils.Print($"\n<{Name}> атакует <{target.Name}> и наносит [{Damage}] урона");
                target.TakeDamage(Damage);
            }
            else
            {
                UserUtils.Print($"\n<{Name}> не может атаковать <{target.Name}>. Цель уже погибла.");
            }
        }

        public virtual void TakeDamage(int damage)
        {
            if (IsAlive)
            {
                int totalDamage = damage - Armor;
                Health -= totalDamage;
                UserUtils.Print($"\n<{Name}> получает [{totalDamage}] урона, блокируя [{Armor}] урона");
            }
            else
            {
                UserUtils.Print($"\n<{Name}> уже погиб.");
            }
        }

        public string GetInfo() =>
            $"<{Name}> ХП [{Health}], ARMOR [{Armor}], DMG [{Damage}]";

        private void SetHealth(int healthValue)
        {
            if (healthValue < 0)
                _health = 0;
            else
                _health = healthValue;
        }
    }

    //Первый - обычный солдат, без особенностей.
    public class Soldier : Fighter
    {
        public Soldier(string name, int health, int armor, int damage) : base(name, health, armor, damage)
        {
        }
    }

    //Второй - атакует только одного, но с множителем урона.
    public class Sniper : Fighter
    {
        public Sniper(string name, int health, int armor, int damage) : base(name, health, armor, damage)
        {
        }
    }

    //Третий - атакует сразу нескольких, без повторения атакованного за свою атаку.
    public class Bomber : Fighter
    {
        public Bomber(string name, int health, int armor, int damage) : base(name, health, armor, damage)
        {
        }
    }

    //Четвертый - атакует сразу нескольких, атакованные солдаты могут повторяться.
    public class MachineGunner : Fighter
    {
        public MachineGunner(string name, int health, int armor, int damage) : base(name, health, armor, damage)
        {
        }
    }


    public interface IDamageable
    {
        string Name { get; }
        bool IsAlive { get; }
        void TakeDamage(int damage);
    }

    public static class UserUtils
    {
        private static Random s_random = new Random();

        public static int GenerateRandomNumber(int minNumber, int maxNumber) =>
            s_random.Next(minNumber, maxNumber);

        public static void Print<T>(T message) =>
            Console.Write(message.ToString());

        public static void Print<T>(T message, ConsoleColor consoleColor = ConsoleColor.White)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = consoleColor;
            Print(message);
            Console.ForegroundColor = defaultColor;
        }
    }
}