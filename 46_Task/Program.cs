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
        private List<Product> _products;

        public Shop()
        {
            _moneyBalance = 0;

            ClientFactory clientFactory = new();
            _clients = clientFactory.GetRandomClients();

            ProductFactory productFactory = new();
            _products = productFactory.GetProducts();
        }

        public void Work()
        {
            const int ShopOpenCommand = 1;
            const int ShowClientsQueueCommand = 2;
            const int ShowProductsCommand = 3;
            const int ExitCommand = 4;

            int userInput;
            bool isWork = true;

            while (isWork)
            {
                Console.Clear();
                UserUtils.Print($"Баланс магазина: [{_moneyBalance}] $\n\n", ConsoleColor.DarkYellow);

                UserUtils.Print($"Команды:" +
                    $"\n{ShopOpenCommand} - Открыть магазин" +
                    $"\n{ShowClientsQueueCommand} - Посмотреть очередь покупателей" +
                    $"\n{ShowProductsCommand} - Посмотреть ассортимент продуктов" +
                    $"\n{ExitCommand} - Выйти из магазина");

                UserUtils.Print($"\n\nВведите команду: ", ConsoleColor.Green);

                switch (UserUtils.ReadInputNumber())
                {
                    case ShopOpenCommand:
                        UserUtils.Print($"Тут будет открытие магазина", ConsoleColor.Green);
                        break;
                    case ShowClientsQueueCommand:
                        ShowClientQueue();
                        break;
                    case ShowProductsCommand:
                        ShowProducts();
                        break;
                    case ExitCommand:
                        isWork = false;
                        break;
                    default:
                        UserUtils.Print($"Нет такой команды! Попробуйте снова", ConsoleColor.Red);
                        break;
                }

                Console.ReadLine();
            }

            //----------------------------- For Test
            //ShowProducts();

            //Console.WriteLine();

            //ShowClientQueue();
            //----------------------------- End Test
        }

        private void ShowProducts()
        {
            int index = 0;

            UserUtils.Print($"\nАссортимент товаров:", ConsoleColor.Green);

            foreach (Product product in _products)
            {
                UserUtils.Print($"\n{++index}. {product.GetInfo()}");
            }
        }

        private void ShowClientQueue()
        {
            int index = 0;

            UserUtils.Print($"\nОчередь покупателей:", ConsoleColor.Green);

            foreach (Client client in _clients)
            {
                UserUtils.Print($"\n{++index}. {client.GetInfo()}");
            }
        }
    }

    public class ClientFactory
    {
        public Queue<Client> GetRandomClients()
        {
            int minClientCount = 3;
            int maxClientCount = 9;
            int clientCount = UserUtils.GenerateRandomNumber(minClientCount, ++maxClientCount);

            Queue<Client> clients = new();

            for (int i = 0; i < clientCount; i++)
            {
                clients.Enqueue(CreateClient());
            }

            return clients;
        }

        private Client CreateClient()
        {
            return new Client(GetRandomName());
        }

        private string GetRandomName()
        {
            List<string> names = new()
            {
                "Василий","Анна","Петр","Оля","Иван","Марина","Николай","Снежана","Алексей","Катя","Гена","Света",
                "Данил","Василиса","Арсений","Карина", "Дима", "Галина","Максим", "Наташа", "Олег", "Дарья"
            };

            return names[UserUtils.GenerateRandomNumber(0, names.Count)];
        }
    }

    public class ProductFactory
    {
        public List<Product> GetProducts()
        {
            List<string> productName = new()
            {
                "Печенье","Лимонад","Апельсины","Клубника","Огурцы","Помидоры",
                "Хлеб","Колбаса","Сахар","Кофе","Шоколад","Пельмени","Котлеты","Арбуз"
            };

            List<Product> products = new();

            for (int i = 0; i < productName.Count; i++)
            {
                products.Add(CreateProduct(productName[i]));
            } 

            return products;
        }

        private Product CreateProduct(string name)
        {
            int minPrice = 50;
            int maxPrice = 150;

            return new Product(name, UserUtils.GenerateRandomNumber(minPrice, maxPrice));
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
            return $"<{Name}> - цена: [{Price}] $";
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

        public Client(string name)
        {
            Name = name;
            _wallet = new Wallet(UserUtils.GenerateRandomNumber(300, 701));
            _bag = new Bag("Сумка");
            _cart = new Bag("Корзина");
        }

        public string Name { get; }

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

        public string GetInfo()
        {
            return $"<{Name}>, у меня есть наличные: {_wallet.Money} $";
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