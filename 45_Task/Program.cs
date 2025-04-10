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
            const int BeginFightMenu = 1;
            const int ExitMenu = 2;

            bool isWork = true;

            while (isWork)
            {
                Console.Clear();

                Print(
                    $"Меню:\n" +
                    $"{BeginFightMenu} - Начать подготовку к битве\n" +
                    $"{ExitMenu} - Покинуть поле битвы.\n" +
                    $"Введите команду для продолжения: ");

                switch (ReadInputNumber())
                {
                    case BeginFightMenu:
                        RunToFight();
                        break;

                    case ExitMenu:
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
            Print($"\nВы выбрали в качестве первого гладиатора:");
            Print($"\n{_gladiatorOne.GetInfo()}\n", ConsoleColor.Green);

            _gladiatorTwo = ChooseFighter($"\nВведите номер, для выбора второго гладиатора: ");
            Print($"\nВы выбрали в качестве второго гладиатора:");
            Print($"\n{_gladiatorTwo.GetInfo()}", ConsoleColor.Green);
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

            return _fightersCatalog[--indexGlagiator].Clone();
        }

        private void DrowLots()
        {
            Fighter tempFighter;
            int minNumber = 0;
            int maxNumber = 9;
            int lotsNumber = 5;
            int randomNumber = GenerateRandomNumber(minNumber, maxNumber);

            Print($"\n\nПроводится жеребьевка");

            if (randomNumber >= 5)
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
        private List<Fighter> _fighters;

        private int _minHealth;
        private int _maxHealth;
        private int _minArmor;
        private int _maxArmor;
        private int _minDamage;
        private int _maxDamage;

        private string _nameWarrior;
        private string _nameMage;
        private string _nameDruid;
        private string _nameAssasign;
        private string _nameBerserk;

        private string _descriptionWarrior;
        private string _descriptionMage;
        private string _descriptionDruid;
        private string _descriptionAssasign;
        private string _descriptionBerserk;

        private FighterSpecification _warriorSpecification;
        private FighterSpecification _mageSpecification;
        private FighterSpecification _druidSpecification;
        private FighterSpecification _assasignSpecification;
        private FighterSpecification _berserkSpecification;

        public FightersFactory()
        {
            ConfigurateFightersSpecification();
            FillFighters();
        }

        private void ConfigurateFightersSpecification()
        {
            _minHealth = 80;
            _maxHealth = 120;
            _minArmor = 2;
            _maxArmor = 10;
            _minDamage = 12;
            _maxDamage = 20;

            _nameWarrior = "Воин";
            _nameMage = "Маг";
            _nameDruid = "Друид";
            _nameAssasign = "Разбойник";
            _nameBerserk = "Берсерк";

            _descriptionWarrior = "Имеет шанс нанести удвоенный урон";
            _descriptionMage = "Использует огненный шар пока есть мана";
            _descriptionDruid = "Каждую третью атаку наносит урон дважды";
            _descriptionAssasign = "Имеет шанс уклониться от атаки";
            _descriptionBerserk = "При получении урона накапливает ярость, достигнув максимума использует лечение";

            _warriorSpecification = new FighterSpecification(_nameWarrior,
                                                             GenerateRandomNumber(_minHealth, _maxHealth),
                                                             GenerateRandomNumber(_minArmor, _maxArmor),
                                                             GenerateRandomNumber(_minDamage, _maxDamage),
                                                             _descriptionWarrior);

            _mageSpecification = new FighterSpecification(_nameMage,
                                                             GenerateRandomNumber(_minHealth, _maxHealth),
                                                             GenerateRandomNumber(_minArmor, _maxArmor),
                                                             GenerateRandomNumber(_minDamage, _maxDamage),
                                                             _descriptionMage);

            _druidSpecification = new FighterSpecification(_nameDruid,
                                                             GenerateRandomNumber(_minHealth, _maxHealth),
                                                             GenerateRandomNumber(_minArmor, _maxArmor),
                                                             GenerateRandomNumber(_minDamage, _maxDamage),
                                                             _descriptionDruid);

            _assasignSpecification = new FighterSpecification(_nameAssasign,
                                                             GenerateRandomNumber(_minHealth, _maxHealth),
                                                             GenerateRandomNumber(_minArmor, _maxArmor),
                                                             GenerateRandomNumber(_minDamage, _maxDamage),
                                                             _descriptionAssasign);

            _berserkSpecification = new FighterSpecification(_nameBerserk,
                                                             GenerateRandomNumber(_minHealth, _maxHealth),
                                                             GenerateRandomNumber(_minArmor, _maxArmor),
                                                             GenerateRandomNumber(_minDamage, _maxDamage),
                                                             _descriptionBerserk);
        }

        private void FillFighters()
        {
            _fighters = new List<Fighter>()
            {
                new Warrior(_warriorSpecification),
                new Mage(_mageSpecification),
                new Druid(_druidSpecification),
                new Assasign(_assasignSpecification),
                new Berserk(_berserkSpecification)
            };
        }

        public List<Fighter> GetFighters()
        {
            return new List<Fighter>(_fighters);
        }
    }

    abstract class Fighter : IDamageProvider, IDamageable, IHealable, IClone
    {
        private protected FighterSpecification _baseSpecification;
        private int _health;

        protected Fighter(FighterSpecification baseSpecification)
        {
            _baseSpecification = baseSpecification;
            Name = _baseSpecification.Name;
            _health = _baseSpecification.Health;
            Armor = _baseSpecification.Armor;
            Damage = _baseSpecification.Damage;
            Description = _baseSpecification.Description;
        }

        public string Name { get; private set; }

        public int Health
        {
            get =>
                _health;
            private set =>
                _health = value < 0 ? 0 : value;
        }

        public int Armor { get; private set; }
        public int Damage { get; private set; }
        public string Description { get; private set; }
        public bool IsAlive => _health > 0;

        public virtual void Attack(IDamageable target)
        {
            if (IsAlive)
            {
                Print($"\n[{Name}] ударил противника, нанеся ему [{Damage}] урона.");
                target.TryTakeDamage(Damage);
            }
            else
            {
                Print($"\n[{Name}] побежденный не может атаковать.");
            }
        }

        public virtual bool TryHealing(int healthPoint)
        {
            if (IsAlive == false || healthPoint < 0)
            {
                Print($"\n[{Name}] не смог вылечить здоровье на [{healthPoint}] единиц.");
                return false;
            }

            Health += healthPoint;
            Print($"\n[{Name}] вылечил здоровье на [{healthPoint}] единиц.");
            return true;
        }

        public virtual bool TryTakeDamage(int damage)
        {
            int totalDamage;

            if (IsAlive)
            {
                totalDamage = damage - Armor;
                Health -= totalDamage;
                Print($"\n[{Name}] получил урон [{totalDamage}], заблокировав своей броней [{Armor}] урона.");
                return true;
            }

            Print($"\n[{Name}] не получил урона.");
            return false;
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
                target.TryTakeDamage(totalDamage);
            }
            else
            {
                base.Attack(target);
            }
        }

        public override Fighter Clone() => 
            new Warrior(_baseSpecification);

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
            _mana = 90;
            _damageMultiplier = 2;
            _manaCostFireBall = 30;
        }

        public override void Attack(IDamageable target)
        {
            if (_mana - _manaCostFireBall >= 0 && IsAlive)
            {
                target.TryTakeDamage(ApplyFireBall());
                _mana -= _manaCostFireBall;
            }
            else
            {
                base.Attack(target);
            }
        }

        public int ApplyFireBall()
        {
            int totalDamage = Damage * _damageMultiplier;
            Print($"\n[{Name}] нанёс удар огненным шаром, нанеся противнику [{totalDamage}] урона.");

            return totalDamage;
        }

        public override Fighter Clone() => 
            new Mage(_baseSpecification);

        public override string GetInfo() => 
            base.GetInfo() + $", Мана [{_mana}] |";
    }

    class Druid : Fighter
    {
        private int _attackCount;
        private int _attackCountForDoubleAttack;

        public Druid(FighterSpecification specification) : base(specification)
        {
            _attackCount = 0;
            _attackCountForDoubleAttack = 2;
        }

        public override void Attack(IDamageable target)
        {
            if (_attackCount < _attackCountForDoubleAttack)
            {
                base.Attack(target);
                _attackCount++;
            }
            else
            {
                Print($"\n[{Name}] изловчается для нанесения двух ударов");
                base.Attack(target);
                base.Attack(target);
                _attackCount = 0;
            }
        }

        public override Fighter Clone() => 
            new Druid(_baseSpecification);

        public override string GetInfo() => 
            base.GetInfo() + $", Счётчик атак [{_attackCount}/{_attackCountForDoubleAttack}] |";
    }

    class Assasign : Fighter
    {
        private int _dodgeChancePercent;

        public Assasign(FighterSpecification specification) : base(specification)
        {
            _dodgeChancePercent = 35;
        }

        public override Fighter Clone() => 
            new Assasign(_baseSpecification);

        public override string GetInfo() => 
            base.GetInfo() + $", Уклонение [{_dodgeChancePercent}%] |";

        public override bool TryTakeDamage(int damage)
        {
            if (IsPositiveChance(_dodgeChancePercent))
            {
                Print($"\n[{Name}] уклонился от удара своего противника.");
                return false;
            }

            return base.TryTakeDamage(damage);
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
            _rageLevel = 0;
            _maxRageLevel = 90;
            _rageDamageValue = 30;
            _healthDivider = 5;
            _healingPoint = specification.Health / _healthDivider;
        }

        public override Fighter Clone() => 
            new Berserk(_baseSpecification);

        public override string GetInfo() => 
            base.GetInfo() + $", Ярость [{_rageLevel}/{_maxRageLevel}] |";

        public override bool TryTakeDamage(int damage)
        {
            if (_rageLevel >= _maxRageLevel)
            {
                base.TryHealing(_healingPoint);
                _rageLevel = 0;
            }

            _rageLevel += _rageDamageValue;
            return base.TryTakeDamage(damage);
        }
    }

    class FighterSpecification
    {
        private int _health;
        private int _damage;
        private int _armor;
        private string _name;
        private string _description;

        public FighterSpecification(string name, int health, int armor, int damage, string description)
        {
            Name = name;
            Health = health;
            Armor = armor;
            Damage = damage;
            Description = description;
        }

        public string Name
        {
            get =>
                _name;
            private set =>
                _name = GetNotEmptyString(value, "Ошибка! Имя не может быть пустым");
        }

        public int Health
        {
            get =>
                _health;
            private set =>
                _health = GetPositiveValue(value, "Ошибка! Значение здоровья не может быть меньше 0");
        }

        public int Armor
        {
            get =>
                _armor;
            private set =>
                _armor = GetPositiveValue(value, "Ошибка! Значение брони не может быть меньше 0");
        }

        public int Damage
        {
            get =>
                _damage;
            private set =>
                _damage = GetPositiveValue(value, "Ошибка! Значение урона не может быть меньше 0");
        }

        public string Description
        {
            get =>
                _description;
            private set =>
                _description = GetNotEmptyString(value, "Ошибка! Описание не может быть пустым");
        }
    }

    interface IDamageable
    {
        bool IsAlive { get; }
        bool TryTakeDamage(int damage);
    }

    interface IHealable
    {
        bool TryHealing(int health);
    }

    interface IDamageProvider
    {
        void Attack(IDamageable target);
    }

    interface IClone
    {
        Fighter Clone();
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

        public static int GetPositiveValue(int value, string message) =>
            value = value < 0 ? throw new Exception(message) : value;

        public static string GetNotEmptyString(string value, string message) =>
            value = value.Length <= 0 ? throw new Exception(message) : value;

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