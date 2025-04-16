namespace _49_Task
{
    public class Program
    {
        static void Main()
        {
            Console.Title = "ДЗ: Зоопарк";
            Zoo zoo = new Zoo();
            zoo.Work();
        }
    }

    public class Zoo
    {
        public void Work()
        {
            List<Aviary> aviaries = new AviaryFactory().CreateAviaries();
            bool isWork = true;
            int userInput;
            int exitCommand = aviaries.Count + 1;

            while (isWork)
            {
                Console.Clear();
                UserUtils.Print($"Добро пожаловать в зоопарк!");

                ShowAviariesName(aviaries);

                UserUtils.Print($"\n{exitCommand}. Для выхода из зоопарка");
                UserUtils.Print($"\n\nВведите номер вальера, который хотите посетить: ", ConsoleColor.Green);

                userInput = UserUtils.ReadInputNumber();

                if (userInput > 0 && userInput <= aviaries.Count)
                {
                    aviaries[--userInput].ShowInfo();
                }
                else if (userInput == exitCommand)
                {
                    isWork = false;
                }
                else
                {
                    UserUtils.Print($"\nНет такой команды!!!", ConsoleColor.Red);
                }

                UserUtils.Print($"\nДля продолжения нажмите любую клавишу", ConsoleColor.Green);
                Console.ReadKey();
            }

            UserUtils.Print($"\n\nРабота программы завершена!", ConsoleColor.Green);
        }

        private void ShowAviariesName(List<Aviary> aviaries)
        {
            int index = 0;

            UserUtils.Print($"\nСписок вальеров:", ConsoleColor.Green);

            aviaries.ForEach((aviary) =>
                UserUtils.Print($"\n{++index}. {aviary.Name}"));
        }
    }

    public class Aviary
    {
        private List<Animal> _animals;

        public Aviary(List<Animal> animals) =>
            _animals = animals;

        public string Name =>
            $"Вальер с <{_animals.First().Name}>";

        public void ShowInfo()
        {
            UserUtils.Print($"\nИнформация:");
            UserUtils.Print($"\n{Name}");
            UserUtils.Print($"\nКоличество животных: {_animals.Count}");

            ShowAnimals();
            _animals.First().MakeSound();
        }

        private void ShowAnimals()
        {
            int index = 0;

            UserUtils.Print($"\nCписок животных:", ConsoleColor.Green);

            _animals.ForEach((animal) =>
                UserUtils.Print($"\n{++index}. {animal.GetInfo()}"));
        }
    }

    public class AviaryFactory
    {
        public List<Aviary> CreateAviaries()
        {
            List<Aviary> aviaries = new();

            List<AnimalType> animalTypes = new()
            {
                AnimalType.Bear,
                AnimalType.Rhinoceros,
                AnimalType.Tiger,
                AnimalType.Antiloup
            };

            for (int i = 0; i < animalTypes.Count; i++)
            {
                aviaries.Add(CreateAviary(animalTypes[i]));
            }

            return aviaries;
        }

        private Aviary CreateAviary(AnimalType animalType)
        {
            List<Animal> animals = new AnimalFactory().CreateAnimals(animalType, UserUtils.GenerateRandomNumber(5, 15));

            return new Aviary(animals);
        }
    }

    public class AnimalFactory
    {
        public List<Animal> CreateAnimals(AnimalType animalType, int animalCount)
        {
            List<Animal> animals = new();

            for (int i = 0; i < animalCount; i++)
            {
                animals.Add(CreateAnimal(animalType));
            }

            return animals;
        }

        private Animal CreateAnimal(AnimalType animalType)
        {
            Dictionary<AnimalType, Animal> animalsDictionary = new()
            {
                {AnimalType.Bear, new Bear("Медведь", GetRandomGender()) },
                {AnimalType.Tiger, new Tiger("Тигр", GetRandomGender())},
                {AnimalType.Rhinoceros, new Rhinoceros("Носорог", GetRandomGender())},
                {AnimalType.Antiloup, new Antiloup("Антилопа", GetRandomGender()) }
            };

            animalsDictionary.TryGetValue(animalType, out Animal animal);

            return animal;
        }

        private string GetRandomGender()
        {
            List<string> genders = new()
            {
                "Самец",
                "Самка"
            };

            int randomGenderIndex = UserUtils.GenerateRandomNumber(0, genders.Count - 1);

            return genders[randomGenderIndex];
        }
    }

    public abstract class Animal
    {
        protected Animal(string name, string gender)
        {
            Name = name;
            Gender = gender;
        }

        public string Name { get; }
        public string Gender { get; }

        public abstract void MakeSound();

        public string GetInfo() =>
            $"{Name}, гендер: <{Gender}>";
    }

    public class Bear : Animal
    {
        public Bear(string name, string gender) : base(name, gender) { }

        public override void MakeSound() =>
            UserUtils.Print($"\n<{Name}> издаёт пронзительный рёв!", ConsoleColor.DarkYellow);
    }

    public class Tiger : Animal
    {
        public Tiger(string name, string gender) : base(name, gender) { }

        public override void MakeSound() =>
            UserUtils.Print($"\n<{Name}> рычит, Р-р-р-р-р!", ConsoleColor.DarkYellow);
    }

    public class Rhinoceros : Animal
    {
        public Rhinoceros(string name, string gender) : base(name, gender) { }

        public override void MakeSound() =>
            UserUtils.Print($"\n<{Name}> пыхтит, пыффф пыффф..!", ConsoleColor.DarkYellow);
    }

    public class Antiloup : Animal
    {
        public Antiloup(string name, string gender) : base(name, gender) { }

        public override void MakeSound() =>
            UserUtils.Print($"\n<{Name}> блеет, Мм-уу-мм-уу", ConsoleColor.DarkYellow);
    }

    public enum AnimalType
    {
        Bear,
        Tiger,
        Rhinoceros,
        Antiloup
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