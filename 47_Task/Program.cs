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

            Squad squad = new Squad("Альфа");
            Squad squad2 = new Squad("Браво");

            int index = 0;

            while (squad.Fighters.Count() > 0 && squad2.Fighters.Count() > 0)
            {
                UserUtils.Print($"\n#{++index} {new string('-', 70)}", ConsoleColor.Red);
                squad.Attack(squad2);
                UserUtils.Print($"\n{new string('-', 70)}");
                squad2.Attack(squad);
                UserUtils.Print($"\n{new string('-', 70)}");
                squad.GetInfo();
                UserUtils.Print($"\n{new string('-', 70)}");
                squad2.GetInfo();
            }


            //END TEST -------------------------------------------------------------------------------

            Console.ReadKey();
        }
    }

    public class Squad
    {
        private List<Fighter> _fighters;

        public Squad(string name)
        {
            Name = name;

            _fighters = new List<Fighter>()
            {
                new Soldier("Пехотинец", 10, 2, 3, Name),
                new Sniper("Снайпер", 10, 1, 4, Name),
                new Bomber("Гранатометчик", 10, 0, 5, Name),
                new MachineGunner("Пулеметчик", 10, 2, 3, Name)
            };
        }

        public string Name { get; }
        public IEnumerable<Fighter> Fighters => _fighters;

        public void Attack(Squad targetSquad)
        {
            for (int i = 0; i < _fighters.Count; i++)
            {
                _fighters[i].Attack(targetSquad);
            }

            targetSquad.RemoveDeadFighters();
        }

        public void GetInfo()
        {
            UserUtils.Print($"\nБойцы в отряде <{Name}>:");

            foreach (Fighter fighter in _fighters)
            {
                UserUtils.Print($"\n{fighter.GetInfo()}");
            }
        }

        public void RemoveDeadFighters() =>
            _fighters.RemoveAll(fighter => fighter.IsAlive == false);
    }

    public abstract class Fighter : IDamageable
    {
        private int _health;

        protected Fighter(string name, int health, int armor, int damage, string squadName)
        {
            Name = name;
            SquadName = squadName;
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
        public string SquadName { get; }

        public bool IsAlive => Health > 0;

        public virtual void Attack(IDamageable target)
        {
            if (IsAlive && target.IsAlive)
            {
                UserUtils.Print($"\n<{Name} ({SquadName})> атакует <{target.Name} ({target.SquadName})> и наносит [{Damage}] урона");
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


        public virtual void Attack(Squad targetSquad)
        {
            int indexFighterTarget = UserUtils.GenerateRandomNumber(0, targetSquad.Fighters.Count());
            Fighter target = targetSquad.Fighters.ToList()[indexFighterTarget];
            Attack(target);
        }

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
        public Soldier(string name, int health, int armor, int damage, string squadName) : base(name, health, armor, damage, squadName)
        {
        }
    }

    //Второй - атакует только одного, но с множителем урона.
    public class Sniper : Fighter
    {
        public Sniper(string name, int health, int armor, int damage, string squadName) : base(name, health, armor, damage, squadName)
        {
        }
    }

    //Третий - атакует сразу нескольких, без повторения атакованного за свою атаку.
    public class Bomber : Fighter
    {
        public Bomber(string name, int health, int armor, int damage, string squadName) : base(name, health, armor, damage, squadName)
        {
        }
    }

    //Четвертый - атакует сразу нескольких, атакованные солдаты могут повторяться.
    public class MachineGunner : Fighter
    {
        public MachineGunner(string name, int health, int armor, int damage, string squadName) : base(name, health, armor, damage, squadName)
        {
        }
    }


    public interface IDamageable
    {
        string Name { get; }
        string SquadName { get; }
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