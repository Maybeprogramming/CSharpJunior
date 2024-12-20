﻿namespace _45_Task
{
    using static UserInput;
    using static Randomaizer;
    using static Display;

    public class Program
    {
        static void Main()
        {
            const string BeginFightMenu = "1";
            const string ExitMenu = "2";

            Console.Title = "ДЗ: Гладиаторские бои";
            bool isRun = true;
            BattleField battleField = new();

            Console.WindowWidth = 90;

            while (isRun)
            {
                Console.Clear();

                Print(
                    $"Меню:\n" +
                    $"{BeginFightMenu} - Начать подготовку битвы\n" +
                    $"{ExitMenu} - Покинуть поле битвы.\n" +
                    $"Введите команду для продолжения: ");

                switch (Console.ReadLine())
                {
                    case BeginFightMenu:
                        battleField.Work();
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

    public class BattleField
    {
        private List<Fighter>? _fightersCatalog;
        private Fighter? _fighter1;
        private Fighter? _fighter2;

        public BattleField()
        {
            _fightersCatalog = new()
            {
                new Fighter(),
                new Warrior(),
                new Assasign(),
                new Hunter(),
                new Wizzard()
            };
        }

        public void Work()
        {
            ClearFighters();

            while (IsChoosedFigters() == false)
            {
                Console.Clear();

                ShowAllFighters();

                ChooseFighter(ReadNumber("Введите номер бойца: ") - 1);
            }

            AnnouncingFightersReadyForFight();
            Fight();
            AnnouncingWinner();
        }

        private bool IsChoosedFigters()
        {
            return _fighter1 != null && _fighter2 != null;
        }

        private void ShowAllFighters()
        {
            Print($"Список доступных бойцов для выбора:\n");

            for (int i = 0; i < _fightersCatalog.Count; i++)
            {
                Print($"{i + 1} - {_fightersCatalog[i].GetInfo()}\n");
            }
        }

        private void ClearFighters()
        {
            _fighter1 = null;
            _fighter2 = null;
        }

        private void AnnouncingFightersReadyForFight()
        {
            Print("\n\nГотовые к бою отважные герои:\n");
            Print($"1. {_fighter1.ClassName} ({_fighter1.Name}): DMG: {_fighter1.Damage}, HP: {_fighter1.Health}\n");
            Print($"2. {_fighter2.ClassName} ({_fighter2.Name}): DMG: {_fighter2.Damage}, HP: {_fighter2.Health}\n");
        }

        private void Fight()
        {
            int delayMiliseconds = 1000;

            Print("\nНачать битву?\nДля продолжения нажмите любую клавишу...\n\n");
            Console.ReadKey();

            while (_fighter1.IsAlive && _fighter2.IsAlive)
            {
                _fighter1.ToAttack(_fighter2);
                _fighter2.ToAttack(_fighter1);

                Print("\n " + new string('-', 70));
                Task.Delay(delayMiliseconds).Wait();
            }
        }

        private void AnnouncingWinner()
        {
            if (_fighter1.IsAlive == false && _fighter2.IsAlive == false)
            {
                Print("\nНичья! Оба героя пали на поле боя!");
            }
            else if (_fighter1.IsAlive == true && _fighter2.IsAlive == false)
            {
                Print($"\nПобедитель - {_fighter1.ClassName} ({_fighter1.Name})!");
            }
            else
            {
                Print($"\nПобедитель - {_fighter2.ClassName} ({_fighter2.Name})!");
            }

            Console.ReadKey();
        }

        private void ChooseFighter(int number)
        {
            if (number >= _fightersCatalog.Count || number < 0)
            {
                Print("\nНет такого бойца в каталоге!");
                Print("\nДля продолжения нажмите любую клавишу...");
                Console.ReadKey();
                return;
            }

            if (_fighter1 == null)
            {
                _fighter1 = (Fighter?)_fightersCatalog[number].Clone();
                Print($"\nВы выбрали: {_fighter1.GetInfo()}");
            }
            else if (_fighter2 == null)
            {
                _fighter2 = (Fighter?)_fightersCatalog[number].Clone();
                Print($"\nВы выбрали: {_fighter2.GetInfo()}");
            }

            Print($"\nДля продолжения нажмите любую клавишу...");
            Console.ReadKey();
        }
    }

    public class Fighter : IClone
    {
        private int _health;

        public Fighter()
        {
            ClassName = "Боец";
            Health = GenerateRandomNumber(150, 301);
            Damage = GenerateRandomNumber(10, 21);
            Name = GenerateRandomName();
        }

        public string ClassName { get; private protected set; }
        public int Health
        {
            get => _health;
            protected set => SetHealth(value);
        }
        public int Damage { get; private protected set; }
        public bool IsAlive => Health > 0;
        public string Name { get; private protected set; }

        public virtual void ToAttack(Fighter target)
        {
            if (IsAlive == true && target.IsAlive == true)
            {
                Print($"\n{ClassName} ({Name}) произвёл удар в сторону {target.ClassName} ({target.Name})");
                target.TryTakeDamage(Damage);
            }
        }

        public virtual void Heal(int healingPoint)
        {
            Health += healingPoint;
            Print($"\n{ClassName} ({Name}) подлечил здоровье на ({healingPoint}) ед. Здоровье : ({Health})", ConsoleColor.Green);
        }

        public virtual string GetInfo() =>
            $"{ClassName}";

        public virtual bool TryTakeDamage(int damage)
        {
            if (Health > 0)
            {
                Health -= damage;
                Print($"\n{ClassName} ({Name}) получил урон ({damage}) ед., осталось здоровья ({Health})", ConsoleColor.Red);
                return true;
            }

            return false;
        }

        public virtual IClone Clone() => new Fighter();


        private void SetHealth(int value)
        {
            if (value > 0)
            {
                _health = value;
            }
            else
            {
                _health = 0;
            }
        }
    }

    public class Warrior : Fighter
    {
        private readonly int _missDamagePercent;
        private readonly int _maxPercent;

        public Warrior()
        {
            _missDamagePercent = 30;
            _maxPercent = 100;
            ClassName = "Воин";
        }

        public override bool TryTakeDamage(int damage)
        {
            int missChance = GenerateRandomNumber(0, _maxPercent + 1);

            if (missChance < _missDamagePercent)
            {
                Print($"\n{ClassName} ({Name}) увернулся от удара, осталось здоровья ({Health})", ConsoleColor.DarkYellow);
                return false;
            }

            return base.TryTakeDamage(damage);
        }

        public override IClone Clone() => new Warrior();
    }

    public class Assasign : Fighter
    {
        public Assasign() =>
            ClassName = "Разбойник";

        public override void ToAttack(Fighter target)
        {
            if (IsAlive || target.IsAlive)
            {
                Print($"\n{ClassName} ({Name}) произвёл удар в сторону {target.ClassName} ({target.Name})");

                if (target.TryTakeDamage(Damage))
                {
                    int damageDivider = 10;
                    int healingPoint = Damage / damageDivider;
                    Heal(healingPoint);
                }
            }
        }

        public override IClone Clone() => new Assasign();
    }

    public class Hunter : Fighter
    {
        private readonly int _critPercent;
        private readonly int _maxPercent;
        private readonly int _damageModifyPercent;

        public Hunter()
        {
            _critPercent = 30;
            _maxPercent = 100;
            _damageModifyPercent = 150;
            ClassName = "Охотник";
        }

        public override void ToAttack(Fighter target)
        {
            if (IsAlive && target.IsAlive)
            {
                int currentDamage = CalculateCriteDamage(out bool isCritacalDamage);

                if (isCritacalDamage)
                {
                    Print($"\n{ClassName} ({Name}) произвёл критический удар в сторону {target.ClassName} ({target.Name})", ConsoleColor.DarkCyan);
                }
                else
                {
                    Print($"\n{ClassName} ({Name}) произвёл удар в сторону {target.ClassName} ({target.Name})");
                }

                target.TryTakeDamage(currentDamage);
            }
        }

        public override IClone Clone() => new Hunter();

        private int CalculateCriteDamage(out bool isCriticalDamage)
        {
            int critChance = GenerateRandomNumber(0, _maxPercent + 1);

            if (critChance < _critPercent)
            {
                isCriticalDamage = true;
                return Damage * _damageModifyPercent / _maxPercent;
            }

            isCriticalDamage = false;
            return Damage;
        }
    }

    public class Wizzard : Fighter
    {
        private readonly int _minMana;
        private readonly int _maxMana;
        private readonly int _castingManaCost;
        private readonly int _regenerationManaCount;
        private int _mana;

        public Wizzard()
        {
            _mana = GenerateRandomNumber(_minMana, _maxMana + 1);
            _minMana = 50;
            _maxMana = 100;
            _castingManaCost = 20;
            _regenerationManaCount = 10;
            ClassName = "Волшебник";
        }

        public int Mana => _mana;

        public override void ToAttack(Fighter target)
        {
            if (_mana >= _castingManaCost)
            {
                _mana -= _castingManaCost;
                base.ToAttack(target);
            }
            else
            {
                _mana += _regenerationManaCount;
                Print($"\n{ClassName} ({Name}) не хватает маны для удара {target.ClassName} ({target.Name})", ConsoleColor.DarkYellow);
            }
        }

        public override IClone Clone() => new Wizzard();

        public override bool TryTakeDamage(int damage)
        {
            _mana += _regenerationManaCount;
            return base.TryTakeDamage(damage);
        }
    }

    public interface IClone
    {
        IClone Clone();
    }

    public static class Randomaizer
    {
        private static readonly Random s_random;
        private static readonly string[] s_names;

        static Randomaizer()
        {
            s_random = new();
            s_names = new[]
            {
                "Варвар",
                "Космонафт",
                "Миледи",
                "Вульфич",
                "Страйк",
                "Герандич",
                "Фрея",
                "Крыса",
                "Нинка",
                "Царь",
                "Забота",
                "Прожариватель",
                "Овощ",
                "Имба",
                "Нагибатель",
                "Топчик",
                "Холивар",
                "Бывалый",
                "Пирожок",
                "Котейка",
                "Оливер",
                "Викрам",
                "Архидея",
                "Метрономщик",
                "Зимник",
                "Волкодав",
                "Богатырь",
                "Вафлич",
                "Вурдолакыч",
                "Зяблик",
                "Кудахта",
                "Чувиха",
                "Мордорка",
                "Куряха",
                "Смоляха",
                "Крендель",
                "Остряк",
                "Крушила",
                "Очкович",
                "Щавель",
                "Днище",
                "Нубичка",
                "Жираф",
                "Подлиза",
                "Лимурчик",
                "Попрыгун",
                "Тряпкович"
            };
        }

        public static string GenerateRandomName()
        {
            return s_names[s_random.Next(0, s_names.Length)];
        }

        public static int GenerateRandomNumber(int minValue, int maxValue)
        {
            return s_random.Next(minValue, maxValue);
        }
    }

    public static class UserInput
    {
        public static int ReadNumber(string message, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            int result;

            Console.Write(message);

            while (int.TryParse(Console.ReadLine(), out result) == false || result < minValue || result >= maxValue)
            {
                Console.Error.WriteLine("Ошибка!. Попробуйте снова!");
            }

            return result;
        }
    }

    public static class Display
    {
        public static void Print(string message, ConsoleColor consoleColor = ConsoleColor.White)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = consoleColor;
            Console.Write(message);
            Console.ForegroundColor = defaultColor;
        }
    }
}