using System.ComponentModel.Design;

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
        private Queue<Customer> _customers;
        private List<Product> _products;

        public Shop()
        {
            _moneyBalance = 0;

            CustomerFactory customerFactory = new();
            _customers = customerFactory.GetRandomCustomers();

            ProductFactory productFactory = new();
            _products = productFactory.GetProducts();
        }

        public void Work()
        {
            const int ShopOpenCommand = 1;
            const int ShowCustomersCommand = 2;
            const int ShowProductsCommand = 3;
            const int ExitCommand = 4;

            bool isWork = true;

            while (isWork)
            {
                Console.Clear();
                UserUtils.Print($"Баланс магазина: [{_moneyBalance}] $\n\n", ConsoleColor.DarkYellow);

                UserUtils.Print($"Команды:" +
                    $"\n{ShopOpenCommand} - Открыть магазин" +
                    $"\n{ShowCustomersCommand} - Посмотреть очередь покупателей" +
                    $"\n{ShowProductsCommand} - Посмотреть ассортимент товаров" +
                    $"\n{ExitCommand} - Выйти из магазина");

                UserUtils.Print($"\n\nВведите команду: ", ConsoleColor.Green);

                switch (UserUtils.ReadInputNumber())
                {
                    case ShopOpenCommand:
                        OpenShop();
                        break;
                    case ShowCustomersCommand:
                        ShowCustomersQueue();
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
        }

        private void OpenShop()
        {
            if (_customers.Count > 0)
            {
                Customer customer = _customers.Dequeue();
                FillCustomerCart(customer);
                SellProducts(customer);
            }
            else
            {
                UserUtils.Print($"\nПокупателей нет!");
            }
        }

        private void SellProducts(Customer customer)
        {
            int totalCost;
            bool CanBuyProducts = false;

            while (CanBuyProducts == false)
            {
                totalCost = CalculateProductsCost(customer.ProductsInCart.ToList());

                if (customer.TryBuyProducts(totalCost))
                {
                    if (totalCost > 0)
                    {
                        UserUtils.Print($"\nПокупатель <{customer.Name}> купил товары на сумму [{totalCost}] $:");
                        customer.ShowProducts();
                        _moneyBalance += totalCost;
                    }
                    else
                    {
                        UserUtils.Print($"\nПокупатель <{customer.Name}> ничего не купил и вышел из магазина");
                    }

                    CanBuyProducts = true;
                }
                else
                {
                    customer.RemoveRandomProduct();
                }
            }
        }

        private int CalculateProductsCost(List<Product> products)
        {
            int totalCost = 0;

            foreach (Product product in products)
            {
                totalCost += product.Price;
            }

            return totalCost;
        }

        private void FillCustomerCart(Customer customer)
        {
            int buyProductCommand = _products.Count + 1;
            int userInput;
            bool isRun = true;

            while (isRun)
            {
                Console.Clear();
                UserUtils.Print($"Покупатель: <{customer.Name}> выбирает товары", ConsoleColor.DarkYellow);

                ShowProducts();
                UserUtils.Print($"\n{buyProductCommand}. - Завершить поход по магазину и идти на кассу", ConsoleColor.Green);

                UserUtils.Print($"\nВведите номер желаемого продукта, чтобы положить его в корзину: ", ConsoleColor.DarkYellow);
                userInput = UserUtils.ReadInputNumber();

                if (userInput == buyProductCommand)
                {
                    UserUtils.Print($"Покупатель <{customer.Name}> идёт на кассу", ConsoleColor.Green);
                    isRun = false;
                }
                else if (userInput <= 0 || userInput > buyProductCommand)
                {
                    UserUtils.Print($"\nОшибка! Такой команды нет! Попробуйте снова", ConsoleColor.Red);
                }
                else
                {
                    Product product = _products[--userInput].Clone();
                    customer.AddToCart(product);
                    UserUtils.Print($"\nПокупатель <{customer.Name}> кладёт в корзину <{product.Name}>");
                }

                Console.ReadLine();
            }
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

        private void ShowCustomersQueue()
        {
            int index = 0;

            UserUtils.Print($"\nОчередь покупателей:", ConsoleColor.Green);

            foreach (Customer customer in _customers)
            {
                UserUtils.Print($"\n{++index}. {customer.GetInfo()}");
            }
        }
    }

    public class CustomerFactory
    {
        public Queue<Customer> GetRandomCustomers()
        {
            int minCustomerCount = 3;
            int maxCustomerCount = 9;
            int customersCount = UserUtils.GenerateRandomNumber(minCustomerCount, ++maxCustomerCount);

            Queue<Customer> customers = new();

            for (int i = 0; i < customersCount; i++)
            {
                customers.Enqueue(CreateCustomer());
            }

            return customers;
        }

        private Customer CreateCustomer()
        {
            return new Customer(GetRandomName());
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

        public string GetInfo() =>
            $"<{Name}> - цена: [{Price}] $";

        public Product Clone() =>
            new Product(Name, Price);
    }

    public class Customer
    {
        private int _money;
        private Bag _bag;
        private Bag _cart;

        public Customer(string name)
        {
            Name = name;
            _money = UserUtils.GenerateRandomNumber(300, 701);
            _bag = new Bag();
            _cart = new Bag();
        }

        public string Name { get; }
        public int Money => _money;
        public IEnumerable<Product> ProductsInCart => _cart.Products;

        public bool TryBuyProducts(int costValue)
        {
            if (Money < costValue)
            {
                return false;
            }

            _money -= costValue;
            _bag = new Bag(_cart.Products);

            return true;
        }

        public string GetInfo() =>
            $"<{Name}>, у меня есть наличные: {Money} $";

        public void AddToCart(Product product) =>
            _cart.PutProduct(product);

        public void RemoveRandomProduct()
        {
            int randomProductIndex = UserUtils.GenerateRandomNumber(0, _cart.Products.Count());
            Product product = _cart.Products.ToList()[randomProductIndex];

            _cart.RemoveProduct(product);
        }

        public void ShowProducts() =>
            _bag.ShowProducts();
    }

    public class Bag
    {
        private List<Product> _products;

        public Bag() =>
            _products = new List<Product>();

        public Bag(IEnumerable<Product> products) =>
            _products = products.ToList();

        public IEnumerable<Product> Products => _products;

        public void ShowProducts()
        {
            int index = 0;

            UserUtils.Print($"\nСписок товаров:", ConsoleColor.Green);

            foreach (Product product in Products)
            {
                UserUtils.Print($"\n{++index}. {product.GetInfo()}");
            }
        }

        public void PutProduct(Product product) =>
            _products.Add(product);

        public void RemoveProduct(Product product) =>
            _products.Remove(product);
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
                Print($"\nВы ввели не число!\nПопробуйте снова: ", ConsoleColor.DarkYellow);

            return result;
        }
    }
}