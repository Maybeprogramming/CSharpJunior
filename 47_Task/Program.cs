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
            Squad squadOne = new Squad("Альфа");
            Squad squadTwo = new Squad("Браво");

            UserUtils.Print($"Начинается бой между отрядами <{squadOne.Name}> и <{squadTwo.Name}>", ConsoleColor.DarkYellow);

            while (squadOne.Fighters.Count() > 0 && squadTwo.Fighters.Count() > 0)
            {
                GoFight(squadOne, squadTwo);
                ShowSquadsInfo(squadOne, squadTwo);
            }

            AnnouncingFightResults(squadOne, squadTwo);

            UserUtils.Print($"\n\nНажмите любую клавишу для продолжения");
            Console.ReadKey();
        }

        private void ShowSquadsInfo(Squad squadOne, Squad squadTwo)
        {
            squadOne.GetInfo();
            squadTwo.GetInfo();
        }

        private void GoFight(Squad squadOne, Squad squadTwo)
        {
            squadOne.Attack(squadTwo);
            squadTwo.Attack(squadOne);
        }

        private void AnnouncingFightResults(Squad squadOne, Squad squadTwo)
        {
            if (squadOne.Fighters.Count() > 0 && squadTwo.Fighters.Count() <= 0)
            {
                UserUtils.Print($"\n\nОтряд <{squadOne.Name}> одержал победу над отрядом <{squadTwo.Name}>", ConsoleColor.DarkYellow);
            }
            else if (squadOne.Fighters.Count() <= 0 && squadTwo.Fighters.Count() > 0)
            {
                UserUtils.Print($"\n\nОтряд <{squadTwo.Name}> одержал победу над отрядом <{squadOne.Name}>", ConsoleColor.DarkYellow);
            }
            else
            {
                UserUtils.Print($"\n\nНичья!", ConsoleColor.DarkYellow);
            }
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
                new Soldier("Пехотинец",Name),
                new Sniper("Снайпер",Name),
                new Bomber("Гранатометчик", Name),
                new MachineGunner("Пулеметчик", Name)
            };
        }

        public string Name { get; }
        public IEnumerable<Fighter> Fighters => _fighters;

        public void Attack(Squad targetSquad)
        {
            if (targetSquad.Fighters.Count() > 0)
            {
                UserUtils.Print($"\nАтаку начинает отряд <{Name}> {new string('-', 50)}", ConsoleColor.Red);

                for (int i = 0; i < _fighters.Count; i++)
                {
                    _fighters[i].GoFight(targetSquad.Fighters);
                    targetSquad.TakeDamage();
                }
            }
            else
            {
                UserUtils.Print($"\nВ отряде противника нет живых бойцов");
            }
        }

        public void GetInfo()
        {
            UserUtils.Print($"\nБойцы в отряде <{Name}> {new string('-', 56)}", ConsoleColor.Green);

            if (_fighters.Count > 0)
            {
                foreach (Fighter fighter in _fighters)
                {
                    UserUtils.Print($"\n{fighter.GetInfo()}");
                }
            }
            else
            {
                UserUtils.Print($"\nВ отряде нет живых бойцов", ConsoleColor.Red);
            }
        }

        public void TakeDamage() =>
            _fighters.RemoveAll(fighter => fighter.IsAlive == false);
    }

    public abstract class Fighter
    {
        private int _health;

        protected Fighter(string name, string squadName)
        {
            Name = name;
            SquadName = squadName;
            Health = UserUtils.GenerateRandomNumber(100,200);
            Armor = UserUtils.GenerateRandomNumber(0, 15);
            Damage = UserUtils.GenerateRandomNumber(30,60);
        }

        public string Name { get; }

        public int Health
        {
            get => _health;
            private set => SetHealth(value);
        }

        public int Armor { get; private set; }
        public int Damage { get; private protected set; }
        public string SquadName { get; }
        public bool IsAlive => Health > 0;

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


        public virtual void GoFight(IEnumerable<Fighter> targetSquad)
        {
            if (targetSquad.Count() > 0)
            {
                Attack(SelectTarget(targetSquad));
            }
            else
            {
                UserUtils.Print($"\nОтряд противника уничтожен!");
            }
        }

        private protected Fighter SelectTarget(IEnumerable<Fighter> targetSquad)
        {
            int indexFighterTarget = UserUtils.GenerateRandomNumber(0, targetSquad.Count());
            Fighter target = targetSquad.ToList()[indexFighterTarget];
            return target;
        }

        private protected void Attack(Fighter target)
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

        private void SetHealth(int healthValue) => 
            _health = healthValue < 0 ? 0 : healthValue;
    }

    public class Soldier : Fighter
    {
        public Soldier(string name, string squadName) : base(name, squadName){ }
    }

    public class Sniper : Fighter
    {
        private int _damageMultiplier;

        public Sniper(string name, string squadName) : base(name, squadName)
        {
            _damageMultiplier = 2;
            Damage *= _damageMultiplier;
        }

        public override void GoFight(IEnumerable<Fighter> targetSquad) =>
            base.GoFight(targetSquad);
    }

    public class Bomber : Fighter
    {
        public Bomber(string name, string squadName) : base(name, squadName) { }

        public override void GoFight(IEnumerable<Fighter> targetSquad)
        {
            for (int i = 0; i < targetSquad.Count(); i++)
            {
                base.Attack(targetSquad.ToArray()[i]);
            }
        }
    }

    public class MachineGunner : Fighter
    {
        public MachineGunner(string name, string squadName) : base(name, squadName) { }

        public override void GoFight(IEnumerable<Fighter> targetSquad)
        {
            for (int i = 0; i < targetSquad.Count(); i++)
            {
                base.Attack(base.SelectTarget(targetSquad));
            }
        }
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