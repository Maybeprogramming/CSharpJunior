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
            //AnimalFactory animalFactory = new AnimalFactory();
            //List<Animal> animals = animalFactory.CreateAnimals(AnimalType.Rhinoceros, 10);

            //foreach (Animal animal in animals)
            //{
            //    UserUtils.Print($"\n{animal.GetInfo()}");
            //}

            List<Aviary> aviaries = new List<Aviary>();
            AviaryFactory aviaryFactory = new AviaryFactory();
            aviaries = aviaryFactory.CreateAviaries();

            foreach (Aviary aviary in aviaries)
            {
                aviary.ShowInfo();
                UserUtils.Print($"\n {new string('#', 70)}");
            }

            Console.ReadKey();
        }
    }

    public class Aviary
    {
        private List<Animal> _animals;

        public Aviary(List<Animal> animals)
        {
            _animals = animals;
        }

        public void ShowInfo()
        {
            UserUtils.Print($"\n");
            UserUtils.Print($"\n{_animals.Count}");
            UserUtils.Print($"\n{_animals.First().Name}");
            _animals.First().MakeSound();

            //Что за вольер
            //Количество животных в вольере
            //Показать пол животных
            //Какой звук издают
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
            AnimalFactory animalFactory = new();
            List<Animal> animals = animalFactory.CreateAnimals(animalType, UserUtils.GenerateRandomNumber(5, 15));

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
            List<string> genders = new() { "Самец", "Самка" };
            int newGenderIndex = UserUtils.GenerateRandomNumber(0, genders.Count - 1);

            return genders[newGenderIndex];
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

        public virtual string GetInfo() =>
            $"{Name}, гендер: <{Gender}>";
    }

    public class Bear : Animal
    {
        public Bear(string name, string gender) : base(name, gender)
        {
        }

        public override void MakeSound()
        {
            UserUtils.Print($"");
        }
    }

    public class Tiger : Animal
    {
        public Tiger(string name, string gender) : base(name, gender)
        {
        }

        public override void MakeSound()
        {
            UserUtils.Print($"");
        }
    }

    public class Rhinoceros : Animal
    {
        public Rhinoceros(string name, string gender) : base(name, gender)
        {
        }

        public override void MakeSound()
        {
            UserUtils.Print($"");
        }
    }

    public class Antiloup : Animal
    {
        public Antiloup(string name, string gender) : base(name, gender)
        {
        }

        public override void MakeSound()
        {
            UserUtils.Print($"");
        }
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