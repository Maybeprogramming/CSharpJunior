namespace _45_Task
{
    using static UserUtils;

    public class Program
    {
        public static void Main()
        {
            Console.Title = "ДЗ: Гладиаторские бои";
            Arena arena = new Arena();
            arena.Work();
            Console.ReadLine();
        }
    }

    public class Arena
    {
        private List<Fighter> _fightersCatalog;
        private FightersFactory _fightersFactory;
        private Fighter _gladiatorOne;
        private Fighter _gladiatorTwo;

        public Arena()
        {
            _fightersFactory = new FightersFactory();
            _fightersCatalog = _fightersFactory.GetFighters();
        }

        public void Work()
        {
            const int CommandBeginFight = 1;
            const int CommandExit = 2;

            bool isWork = true;

            while (isWork)
            {
                Console.Clear();

                Print(
                    $"Меню:\n" +
                    $"{CommandBeginFight} - Начать подготовку к битве\n" +
                    $"{CommandExit} - Покинуть поле битвы.\n" +
                    $"Введите команду для продолжения: ");

                switch (ReadInputNumber())
                {
                    case CommandBeginFight:
                        RunToFight();
                        break;

                    case CommandExit:
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("Такой команды нет! Попробуйте снова!");
                        Console.ReadKey();
                        break;
                }
            }

            Print("\nРабота программы завершена!");
            Console.ReadLine();
        }

        private void RunToFight()
        {
            ShowAllFighters();
            BeginFighters();
            DrowLots();
            Fight();
            AnnouncingFightResults();

            Print("\nЧтобы продолжить нажмите любую клавишу", ConsoleColor.Green);
            Console.ReadLine();
        }

        private void ShowAllFighters()
        {
            int index = 0;

            Print($"\n Список доступных гладиаторов:", ConsoleColor.Green);
            Print($"\n{new string('-', 90)}", ConsoleColor.Green);

            foreach (Fighter fighter in _fightersCatalog)
            {
                Print($"\n{++index}. {fighter.GetInfo()} " +
                      $"\nОписание: [{fighter.Description}]");
                Print($"\n{new string('-', 90)}", ConsoleColor.DarkRed);
            }
        }

        private void BeginFighters()
        {
            _gladiatorOne = ChooseFighter($"\nВведите номер, для выбора первого гладиатора: ");
            _gladiatorTwo = ChooseFighter($"\nВведите номер, для выбора второго гладиатора: ");
        }

        private Fighter ChooseFighter(string message)
        {
            int indexGlagiator;
            int minIndex = 1;
            int maxIndex = _fightersCatalog.Count;

            do
            {
                Print($"{message}");
                indexGlagiator = ReadInputNumber();
            } while (indexGlagiator < minIndex || indexGlagiator > maxIndex);

            Print($"\nВы выбрали следующего гладиатора:");
            Print($"\n{_fightersCatalog[indexGlagiator - 1].GetInfo()}\n", ConsoleColor.Green);

            return _fightersCatalog[indexGlagiator - 1].Clone();
        }

        private void DrowLots()
        {
            Fighter tempFighter;
            int minNumber = 0;
            int maxNumber = 9;
            int lotsNumber = 5;
            int randomNumber = GenerateRandomNumber(minNumber, maxNumber);

            Print($"\n\nПроводится жеребьевка");

            if (randomNumber >= lotsNumber)
            {
                tempFighter = _gladiatorOne;
                _gladiatorOne = _gladiatorTwo;
                _gladiatorTwo = tempFighter;
            }

            Print($"\nПервым начнёт атаку гладиатор:");
            Print($"\n{_gladiatorOne.GetInfo()}", ConsoleColor.Green);
        }

        private void Fight()
        {
            int fightStepCount = 0;
            Print($"\n\nНачинается битва!", ConsoleColor.Red);

            while (_gladiatorOne.IsAlive && _gladiatorTwo.IsAlive)
            {
                Print($"\n# {++fightStepCount} #\n{new string('-', 90)}", ConsoleColor.Red);
                _gladiatorOne.Attack(_gladiatorTwo);
                _gladiatorTwo.Attack(_gladiatorOne);

                Print($"\n{new string('-', 90)}", ConsoleColor.Red);
                Print($"\n{_gladiatorOne.GetInfo()}", ConsoleColor.Yellow);
                Print($"\n{_gladiatorTwo.GetInfo()}", ConsoleColor.DarkYellow);

                Print($"\nДля следующего хода нажмите любую клавишу", ConsoleColor.Green);
                Console.ReadLine();
            }
        }

        private void AnnouncingFightResults()
        {
            Print($"\n{new string('-', 90)}", ConsoleColor.Green);
            Print($"\nПодведём итоги боя гладиаторов", ConsoleColor.Green);

            if (_gladiatorOne.IsAlive && _gladiatorTwo.IsAlive == false)
            {
                Print($"\n{_gladiatorOne.Name} одержал победу над своим противником", ConsoleColor.DarkYellow);
            }
            else if (_gladiatorOne.IsAlive == false && _gladiatorTwo.IsAlive)
            {
                Print($"\n{_gladiatorTwo.Name} одержал победу над своим противником", ConsoleColor.DarkYellow);
            }
            else
            {
                Print($"\nВ этой битве нет победителей", ConsoleColor.Red);
            }
        }
    }

    class FightersFactory
    {
        private int _minHealth;
        private int _maxHealth;
        private int _minArmor;
        private int _maxArmor;
        private int _minDamage;
        private int _maxDamage;

        public FightersFactory()
        {
            _minHealth = 80;
            _maxHealth = 120;
            _minArmor = 2;
            _maxArmor = 10;
            _minDamage = 12;
            _maxDamage = 20;
        }

        public List<Fighter> GetFighters()
        {
            List<Fighter> _fighters = new List<Fighter>()
            {
                new Warrior(CreateRandomSpecification()),
                new Mage(CreateRandomSpecification()),
                new Druid(CreateRandomSpecification()),
                new Assasign(CreateRandomSpecification()),
                new Berserk(CreateRandomSpecification())
            };

            return _fighters;
        }

        private FighterSpecification CreateRandomSpecification()
        {
            return new FighterSpecification(GenerateRandomNumber(_minHealth, _maxHealth),
                                            GenerateRandomNumber(_minArmor, _maxArmor),
                                            GenerateRandomNumber(_minDamage, _maxDamage));
        }
    }

    abstract class Fighter : IDamageable
    {
        private int _health;

        public Fighter(FighterSpecification baseSpecification)
        {
            Name = "Безымянный";
            Description = "Отсутствует";
            Health = baseSpecification.Health;
            Armor = baseSpecification.Armor;
            Damage = baseSpecification.Damage;
        }

        public string Name { get; private protected set; }

        public int Health
        {
            get =>
                _health;
            private protected set =>
                _health = value < 0 ? 0 : value;
        }

        public int Armor { get; private set; }
        public int Damage { get; private set; }
        public string Description { get; private protected set; }
        public bool IsAlive => _health > 0;

        public virtual void Attack(IDamageable target)
        {
            if (IsAlive)
            {
                Print($"\n[{Name}] ударил противника, нанеся ему [{Damage}] урона.");
                target.TakeDamage(Damage);
            }
            else
            {
                Print($"\n[{Name}] побежденный не может атаковать.");
            }
        }

        public virtual void TakeDamage(int damage)
        {
            int totalDamage;

            if (IsAlive)
            {
                totalDamage = damage - Armor;
                Health -= totalDamage;
                Print($"\n[{Name}] получил урон [{totalDamage}], заблокировав своей броней [{Armor}] урона.");
            }
            else
            {
                Print($"\n[{Name}] не получил урона.");
            }
        }

        public virtual string GetInfo() =>
            $"<{Name}> | СТАТЫ: Жизни [{Health}], Урон [{Damage}], Броня [{Armor}]";

        public abstract Fighter Clone();
    }

    class Warrior : Fighter
    {
        private int _critChancePercent;
        private int _critDamageMultiplier;

        public Warrior(FighterSpecification specification) : base(specification)
        {
            Name = "Воин";
            Description = "Имеет шанс нанести удвоенный урон";
            _critChancePercent = 40;
            _critDamageMultiplier = 2;
        }

        public override void Attack(IDamageable target)
        {
            int totalDamage;

            if (IsPositiveChance(_critChancePercent) && IsAlive)
            {
                totalDamage = Damage * _critDamageMultiplier;
                Print($"\n[{Name}] нанёс критический удар, нанеся противнику [{totalDamage}] урона.");
                target.TakeDamage(totalDamage);
            }
            else
            {
                base.Attack(target);
            }
        }

        public override Fighter Clone() =>
            new Warrior(new FighterSpecification(Health, Armor, Damage));

        public override string GetInfo() =>
            base.GetInfo() + $", Крит [{_critChancePercent}] |";
    }

    class Mage : Fighter
    {
        private int _mana;
        private int _damageMultiplier;
        private int _manaCostFireBall;

        public Mage(FighterSpecification specification) : base(specification)
        {
            Name = "Маг";
            Description = "Использует огненный шар пока есть мана";
            _mana = 90;
            _damageMultiplier = 2;
            _manaCostFireBall = 30;
        }

        public override void Attack(IDamageable target)
        {
            if (_mana - _manaCostFireBall >= 0 && IsAlive)
            {
                target.TakeDamage(ApplyFireBall());
                _mana -= _manaCostFireBall;
            }
            else
            {
                base.Attack(target);
            }
        }

        public override Fighter Clone() =>
            new Mage(new FighterSpecification(Health, Armor, Damage));

        public override string GetInfo() =>
            base.GetInfo() + $", Мана [{_mana}] |";

        private int ApplyFireBall()
        {
            int totalDamage = Damage * _damageMultiplier;
            Print($"\n[{Name}] нанёс удар огненным шаром, нанеся противнику [{totalDamage}] урона.");

            return totalDamage;
        }
    }

    class Druid : Fighter
    {
        private int _attackCount;
        private int _attackCountForDoubleAttack;

        public Druid(FighterSpecification specification) : base(specification)
        {
            Name = "Друид";
            Description = "Каждую третью атаку наносит урон дважды";
            _attackCount = 0;
            _attackCountForDoubleAttack = 2;
        }

        public override void Attack(IDamageable target)
        {
            if (_attackCount >= _attackCountForDoubleAttack)
            {
                Print($"\n[{Name}] изловчается для нанесения двух ударов");
                base.Attack(target);
                _attackCount = 0;
            }

            base.Attack(target);
            _attackCount++;
        }

        public override Fighter Clone() =>
            new Druid(new FighterSpecification(Health, Armor, Damage));

        public override string GetInfo() =>
            base.GetInfo() + $", Счётчик атак [{_attackCount}/{_attackCountForDoubleAttack}] |";
    }

    class Assasign : Fighter
    {
        private int _dodgeChancePercent;

        public Assasign(FighterSpecification specification) : base(specification)
        {
            Name = "Разбойник";
            Description = "Имеет шанс уклониться от атаки";
            _dodgeChancePercent = 35;
        }

        public override Fighter Clone() =>
            new Assasign(new FighterSpecification(Health, Armor, Damage));

        public override string GetInfo() =>
            base.GetInfo() + $", Уклонение [{_dodgeChancePercent}%] |";

        public override void TakeDamage(int damage)
        {
            if (IsPositiveChance(_dodgeChancePercent))
            {
                Print($"\n[{Name}] уклонился от удара своего противника.");
            }
            else
            {
                base.TakeDamage(damage);
            }
        }
    }

    class Berserk : Fighter
    {
        private int _rageLevel;
        private int _maxRageLevel;
        private int _rageDamageValue;
        private int _healingPoint;
        private int _healthDivider;

        public Berserk(FighterSpecification specification) : base(specification)
        {
            Name = "Берсерк";
            Description = "При получении урона накапливает ярость, достигнув максимума использует лечение";
            _rageLevel = 0;
            _maxRageLevel = 90;
            _rageDamageValue = 30;
            _healthDivider = 5;
            _healingPoint = specification.Health / _healthDivider;
        }

        public override Fighter Clone() =>
            new Berserk(new FighterSpecification(Health, Armor, Damage));

        public override string GetInfo() =>
            base.GetInfo() + $", Ярость [{_rageLevel}/{_maxRageLevel}] |";

        public override void TakeDamage(int damage)
        {
            if (_rageLevel >= _maxRageLevel)
            {
                Healing(_healingPoint);
                _rageLevel = 0;
            }
            else
            {
                _rageLevel += _rageDamageValue;
                base.TakeDamage(damage);
            }
        }

        private void Healing(int healthPoint)
        {
            if (IsAlive == false || healthPoint < 0)
            {
                Print($"\n[{Name}] не смог вылечить здоровье на [{healthPoint}] единиц.");
            }
            {
                Health += healthPoint;
                Print($"\n[{Name}] вылечил здоровье на [{healthPoint}] единиц.");
            }
        }
    }

    class FighterSpecification
    {
        private int _health;
        private int _damage;
        private int _armor;

        public FighterSpecification(int health, int armor, int damage)
        {
            Health = health;
            Armor = armor;
            Damage = damage;
        }

        public int Health { get; private set; }
        public int Armor { get; private set; }
        public int Damage { get; private set; }
    }

    interface IDamageable
    {
        bool IsAlive { get; }
        void TakeDamage(int damage);
    }

    public static class UserUtils
    {
        private static Random s_random = new Random();

        public static int GenerateRandomNumber(int minNumber, int maxNumber) =>
            s_random.Next(minNumber, ++maxNumber);

        public static bool IsPositiveChance(int currentChancePercent, int minChancePercent = 0, int maxChancePercent = 100)
        {
            int randomNumber = GenerateRandomNumber(minChancePercent, maxChancePercent);

            return randomNumber <= currentChancePercent;
        }

        public static void Print<T>(T message) =>
            Console.Write(message.ToString());

        public static void Print<T>(T message, ConsoleColor consoleColor = ConsoleColor.White)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = consoleColor;
            Print(message);
            Console.ForegroundColor = defaultColor;
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