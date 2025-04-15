namespace _49_Task
{
    public class Program
    {
        static void Main()
        {
            Console.Title = "ДЗ: Зоопарк";
            Zoo zoo = new Zoo();
            zoo.Work();
            Console.ReadKey();
        }
    }

    public class Zoo
    {
        public void Work() { }
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
            //Что за вольер
            //Количество животных в вольере
            //Показать пол животных
            //Какой звук издают
        }
    }

    public class AviaryFactory 
    { 
        public Aviary Create()
        {
            return new Aviary(new List<Animal>());
        }
    }

    public class AnimalFactory 
    { 
        public List<Animal> CreateAnimals()
        {
            return new List<Animal>();
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
            $"{Name}, пол животного: <{Gender}>";
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