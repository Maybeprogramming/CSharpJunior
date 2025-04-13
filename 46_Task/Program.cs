namespace _46_Task
{
    public class Program
    {
        static void Main()
        {
            Console.Title = "ДЗ: Супермаркет";

            Shop shop = new Shop();
            shop.Work();

            Console.ReadLine();
        }
    }

    public class Shop
    {
        private int _moneyBalance;
        private Queue<Client> _clients;

        public Shop()
        {
            _moneyBalance = 0;
            ClientFactory clientFactory = new ClientFactory();
            _clients = clientFactory.FillClients();
        }

        public void Work()
        {
        }
    }

    public class ClientFactory
    {
        public Queue<Client>? FillClients()
        {
            throw new NotImplementedException();
        }
    }

    public class Product
    {
        public Product(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; private set; }
        public int Price { get; private set; }

        public string GetInfo()
        {
            return $"<{Name}> - цена: [{Price}]";
        }
    }

    public class Seller
    {

    }

    public class Client
    {
        private Wallet _wallet;
        private Bag _bag;
        private Bag _cart;

        public Client()
        {
            _wallet = new Wallet(1000);
            _bag = new Bag("Сумка");
            _cart = new Bag("Корзина");
        }

        public bool TryBuyProducts()
        {
            int totalCost = 0;

            foreach (Product product in _cart.Products)
            {
                totalCost += product.Price;
            }

            if (_wallet.TryRemoveMoney(totalCost))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class Bag
    {
        private List<Product> _products;

        public Bag(string name)
        {
            _products = new List<Product>();
            Name = name;
        }

        public string Name { get; }
        public IEnumerable<Product> Products => _products;

        public void PutProduct(Product product)
        {
            _products.Add(product);
        }

        public void ShowProducts()
        {
            int index = 0;

            UserUtils.Print($"\nВ <{Name}> следующие продукты:", ConsoleColor.Green);

            foreach (Product product in Products)
            {
                UserUtils.Print($"\n{++index}. {product.GetInfo()}");
            }
        }
    }

    public class Wallet
    {
        private int _money;

        public Wallet(int money)
        {
            _money = money;
        }

        public int Money => _money;

        public void AddMoney(int value)
        {
            if (value > 0)
            {
                _money += value;
            }
            else
            {
                UserUtils.Print($"\nОшибка! Количество денег не может быть отрицательным значением!", ConsoleColor.Red);
            }
        }

        public bool TryRemoveMoney(int value)
        {
            if (_money - value < 0)
            {
                return false;
            }

            _money -= value;
            return true;
        }
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