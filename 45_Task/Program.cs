namespace _45_Task
{
    using static UserUtils;

    public class Program
    {
        public static void Main()
        {
            Console.Title = "ДЗ: Гладиаторские бои";

            FightersFactory fightersFactory = new FightersFactory();
            List<Fighter> fighters = fightersFactory.GetFighters();

            foreach (Fighter fighter in fighters)
            {
                Console.WriteLine(fighter.GetInfo());
            }


            Console.ReadLine();
        }
    }

    public class Arena
    {
        private FightersFactory _fightersFactory;

        public Arena()
        {
            _fightersFactory = new FightersFactory();
        }

        public void Work()
        {
            const string BeginFightMenu = "1";
            const string ExitMenu = "2";

            bool isRun = true;
            //BattleField battleField = new();

            while (isRun)
            {
                Console.Clear();

                Print(
                    $"Меню:\n" +
                    $"{BeginFightMenu} - Начать подготовку к битве\n" +
                    $"{ExitMenu} - Покинуть поле битвы.\n" +
                    $"Введите команду для продолжения: ");

                switch (Console.ReadLine())
                {
                    case BeginFightMenu:
                        //battleField.Work();
                        break;

                    case ExitMenu:
                        isRun = false;
                        break;

                    default:
                        Console.WriteLine("Такой команды нет!!!");
                        Console.ReadKey();
                        break;
                }
            }

            Print("\nРабота программы завершена!");
            Console.ReadKey();
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

        public virtual void Attack(IDamageable target) =>
            target.TryTakeDamage(Damage);

        public virtual bool TryHealing(int healthPoint)
        {
            if (IsAlive == false || healthPoint < 0)
            {
                return false;
            }

            Health += healthPoint;
            return true;
        }

        public virtual bool TryTakeDamage(int damage)
        {
            if (IsAlive)
            {
                Health -= damage - Armor;
                return true;
            }

            return false;
        }

        public virtual string GetInfo()
        {
            return $"<{Name}> | СТАТЫ - Жизни [{Health}] Урон [{Damage}] Броня [{Armor}]";
        }

        public abstract Fighter Clone();
    }

    class Warrior : Fighter
    {
        private int _critChancePercent = 40;
        private int _critDamageMultiplier = 2;

        public Warrior(FighterSpecification specification) : base(specification) { }

        public override void Attack(IDamageable target)
        {
            if (IsPositiveChance(_critChancePercent))
            {
                target.TryTakeDamage(Damage * _critDamageMultiplier);
            }

            base.Attack(target);
        }

        public override Fighter Clone()
        {
            return new Warrior(_baseSpecification);
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $" Крит [{_critChancePercent}] |";
        }
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
            if (_mana - _manaCostFireBall >= 0)
            {
                target.TryTakeDamage(ApplyFireBall());
                _mana -= _manaCostFireBall;
            }

            base.Attack(target);
        }

        public int ApplyFireBall()
        {
            return Damage * _damageMultiplier;
        }

        public override Fighter Clone()
        {
            return new Mage(_baseSpecification);
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $" Мана [{_mana}] |";
        }
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
                base.Attack(target);
                base.Attack(target);
                _attackCount = 0;
            }
        }

        public override Fighter Clone()
        {
            return new Druid(_baseSpecification);
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $"Счётчик атак [{_attackCount}/{_attackCountForDoubleAttack}] |";
        }
    }

    class Assasign : Fighter
    {
        private int _dodgeChancePercent;

        public Assasign(FighterSpecification specification) : base(specification)
        {
            _dodgeChancePercent = 35;
        }

        public override Fighter Clone()
        {
            return new Assasign(_baseSpecification);
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $" Уклонение [{_dodgeChancePercent}%] |";
        }

        public override bool TryTakeDamage(int damage)
        {
            if (IsPositiveChance(_dodgeChancePercent))
            {
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
            _healthDivider = 3;
            _healingPoint = specification.Health / _healthDivider;
        }

        public override Fighter Clone()
        {
            return new Berserk(_baseSpecification);
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $" Ярость [{_rageLevel}/{_maxRageLevel}] |";
        }

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

    //public class BattleField
    //{
    //    private List<Fighter>? _fightersCatalog;
    //    private Fighter? _fighter1;
    //    private Fighter? _fighter2;

    //    public BattleField()
    //    {
    //        _fightersCatalog = new()
    //        {
    //            new Fighter(),
    //            new Warrior(),
    //            new Assasign(),
    //            new Hunter(),
    //            new Wizzard()
    //        };
    //    }

    //    public void Work()
    //    {
    //        ClearFighters();

    //        while (IsChoosedFigters() == false)
    //        {
    //            Console.Clear();

    //            ShowAllFighters();

    //            ChooseFighter(ReadNumber("Введите номер бойца: ") - 1);
    //        }

    //        AnnouncingFightersReadyForFight();
    //        Fight();
    //        AnnouncingWinner();
    //    }

    //    private bool IsChoosedFigters()
    //    {
    //        return _fighter1 != null && _fighter2 != null;
    //    }

    //    private void ShowAllFighters()
    //    {
    //        Print($"Список доступных бойцов для выбора:\n");

    //        for (int i = 0; i < _fightersCatalog.Count; i++)
    //        {
    //            Print($"{i + 1} - {_fightersCatalog[i].GetInfo()}\n");
    //        }
    //    }

    //    private void ClearFighters()
    //    {
    //        _fighter1 = null;
    //        _fighter2 = null;
    //    }

    //    private void AnnouncingFightersReadyForFight()
    //    {
    //        Print("\n\nГотовые к бою отважные герои:\n");
    //        Print($"1. {_fighter1.ClassName} ({_fighter1.Name}): DMG: {_fighter1.Damage}, HP: {_fighter1.Health}\n");
    //        Print($"2. {_fighter2.ClassName} ({_fighter2.Name}): DMG: {_fighter2.Damage}, HP: {_fighter2.Health}\n");
    //    }

    //    private void Fight()
    //    {
    //        int delayMiliseconds = 1000;

    //        Print("\nНачать битву?\nДля продолжения нажмите любую клавишу...\n\n");
    //        Console.ReadKey();

    //        while (_fighter1.IsAlive && _fighter2.IsAlive)
    //        {
    //            _fighter1.ToAttack(_fighter2);
    //            _fighter2.ToAttack(_fighter1);

    //            Print("\n " + new string('-', 70));
    //            Task.Delay(delayMiliseconds).Wait();
    //        }
    //    }

    //    private void AnnouncingWinner()
    //    {
    //        if (_fighter1.IsAlive == false && _fighter2.IsAlive == false)
    //        {
    //            Print("\nНичья! Оба героя пали на поле боя!");
    //        }
    //        else if (_fighter1.IsAlive == true && _fighter2.IsAlive == false)
    //        {
    //            Print($"\nПобедитель - {_fighter1.ClassName} ({_fighter1.Name})!");
    //        }
    //        else
    //        {
    //            Print($"\nПобедитель - {_fighter2.ClassName} ({_fighter2.Name})!");
    //        }

    //        Console.ReadKey();
    //    }

    //    private void ChooseFighter(int number)
    //    {
    //        if (number >= _fightersCatalog.Count || number < 0)
    //        {
    //            Print("\nНет такого бойца в каталоге!");
    //            Print("\nДля продолжения нажмите любую клавишу...");
    //            Console.ReadKey();
    //            return;
    //        }

    //        if (_fighter1 == null)
    //        {
    //            _fighter1 = (Fighter?)_fightersCatalog[number].Clone();
    //            Print($"\nВы выбрали: {_fighter1.GetInfo()}");
    //        }
    //        else if (_fighter2 == null)
    //        {
    //            _fighter2 = (Fighter?)_fightersCatalog[number].Clone();
    //            Print($"\nВы выбрали: {_fighter2.GetInfo()}");
    //        }

    //        Print($"\nДля продолжения нажмите любую клавишу...");
    //        Console.ReadKey();
    //    }
    //}

    public static class UserUtils
    {
        private static Random s_random = new Random();

        public static int GenerateRandomNumber(int minNumber, int maxNumber) =>
            s_random.Next(minNumber, ++maxNumber);

        public static bool IsPositiveChance(int currentChancePercent, int minChancePercent = 0, int maxChancePercent = 100)
        {
            int generatedChancePercent = GenerateRandomNumber(minChancePercent, maxChancePercent);

            return generatedChancePercent <= currentChancePercent;
        }

        public static int GetPositiveValue(int value, string message)
        {
            if (value < 0)
                throw new Exception(message);
            else
                return value;
        }

        public static string GetNotEmptyString(string value, string message)
        {
            if (value.Length <= 0)
                throw new Exception(message);
            else
                return value;
        }

        public static void Print<T>(T message)
        {
            Console.Write(message.ToString());
        }

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