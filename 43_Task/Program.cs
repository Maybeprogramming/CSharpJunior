namespace _43_Task
{
    public class Program
    {
        public static void Main()
        {
            Console.Title = "ДЗ: Магазин";
            Console.WindowWidth = 110;
            Shop shop = new();
            shop.Work();
        }
    }

    public class Shop
    {
        public void Work()
        {
            const string ShowSellerProductsCommand = "1";
            const string ShowBuyerProductsCommand = "2";
            const string BuyProductCommand = "3";
            const string ExitCommand = "4";

            int buyerMoney = 1000;
            int sellerMoney = 0;
            string sellerName = "NPC";
            string buyerName = "Игрок";
            int leftPositionInfo = 60;
            int topPositionBuyerInfo = 0;
            int topPositionSellerInfo = 4;

            PersonInfoConfig buyerInfoConfig = new PersonInfoConfig(
                "Имя покупателя:",
                "Баланс покупателя:",
                "Количество товаров у покупателя:",
                leftPositionInfo,
                topPositionBuyerInfo);

            PersonInfoConfig sellerInfoConfig = new PersonInfoConfig(
                "Имя продавца:",
                "Баланс продавца:",
                "Количество товаров у продавца:",
                leftPositionInfo,
                topPositionSellerInfo);

            Seller seller = new(sellerName, sellerMoney, sellerInfoConfig);
            Buyer buyer = new(buyerName, buyerMoney, buyerInfoConfig);
            bool isWork = true;

            string menu = $"Меню:" +
                          $"\n{ShowSellerProductsCommand} - Показать товары продавца." +
                          $"\n{ShowBuyerProductsCommand} - Показать купленные товары покупателя." +
                          $"\n{BuyProductCommand} - Купить товар." +
                          $"\n{ExitCommand} - Закончить покупки и выйти из магазина.";
            string requestMessage = "\nВведите команду: ";
            string continueMesaage = "\nНажмите любую клавишу чтобы продолжить...";
            string errorCommandMessage = "Такой команды нет! Попробуйте снова.";
            string userInput;

            while (isWork)
            {
                Console.Clear();
                buyer.ShowInfo();
                seller.ShowInfo();
                Display.Print(menu);
                Display.Print(requestMessage, ConsoleColor.Green);
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case ShowSellerProductsCommand:
                        seller.ShowProducts();
                        break;

                    case ShowBuyerProductsCommand:
                        buyer.ShowProducts();
                        break;

                    case BuyProductCommand:
                        seller.SellProduct(buyer);
                        break;

                    case ExitCommand:
                        isWork = false;
                        break;

                    default:
                        Display.Print($"{userInput} - {errorCommandMessage}", ConsoleColor.DarkRed);
                        break;
                }

                Display.Print(continueMesaage, ConsoleColor.DarkYellow);
                Console.ReadLine();
            }
        }
    }

    public class Product
    {
        public Product(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; }
        public int Price { get; }

        public override string ToString() =>
            $"Товар: {Name}, цена: {Price}";
    }

    public class Person
    {
        private protected List<Product> Products;
        private PersonInfoConfig _showInfoConfig;

        public Person(string name, int money, PersonInfoConfig showInfoConfig)
        {
            Name = name;
            Money = money;
            Products = new();
            _showInfoConfig = showInfoConfig;
        }

        public string Name { get; }
        public int Money { get; private protected set; }

        public void ShowProducts()
        {
            int index = 0;

            if (Products.Count <= 0)
            {
                Display.Print($"Нет товаров для отображения.", ConsoleColor.DarkRed);
                return;
            }

            Display.Print($"\nСписок продуктов у {Name}:", ConsoleColor.Green);

            foreach (Product product in Products)
            {
                Display.Print($"\n{++index}. {product}");
            }
        }

        public void ShowInfo()
        {
            int currentPositionLeft = Console.CursorLeft;
            int currentPositionTop = Console.CursorTop;
            int positionLeft = _showInfoConfig.PositionLeft;
            int positionTop = _showInfoConfig.PositionTop;

            Console.SetCursorPosition(positionLeft, positionTop++);
            Display.Print($"# {_showInfoConfig.NameText} [{Name}]", ConsoleColor.Green);
            Console.SetCursorPosition(positionLeft, positionTop++);
            Display.Print($"# {_showInfoConfig.BalanceText} [{Money}] рублей.", ConsoleColor.Green);
            Console.SetCursorPosition(positionLeft, positionTop);
            Display.Print($"# {_showInfoConfig.ProductAmountText} [{Products.Count}].", ConsoleColor.Green);

            Console.SetCursorPosition(currentPositionLeft, currentPositionTop);
        }
    }

    public class Seller : Person
    {
        public Seller(string name, int money, PersonInfoConfig showInfoConfig) : base(name, money, showInfoConfig)
        {
            Products = new()
            {
                new Product("Апельсин", 100),
                new Product("Клубника", 120),
                new Product("Манго", 150),
                new Product("Хлеб", 120),
                new Product("Масло", 200),
                new Product("Огурцы", 90),
                new Product("Помидоры", 115),
                new Product("Петрушка", 20),
                new Product("Вода", 50)
            };
        }

        public void SellProduct(Buyer buyer)
        {
            int inputIndex;
            Console.Clear();
            ShowProducts();
            buyer.ShowInfo();
            ShowInfo();

            do
            {
                Display.Print($"\nВведите номер товара из списка для покупки: ");
                inputIndex = ReadInputNumber();

            } while (inputIndex <= 0 || inputIndex > Products.Count);

            Product product = Products[--inputIndex];

            if (buyer.TryBuyProduct(product))
            {
                Money += product.Price;
                Products.Remove(product);
                Display.Print($"{buyer.Name} купил {product.Name} по цене {product.Price}");
            }
            else
            {
                Display.Print($"У {buyer.Name} не хватает денег чтобы купить товар {product.Name}");
            }
        }

        private int ReadInputNumber()
        {
            int result;

            while (int.TryParse(Console.ReadLine(), out result) == false)
            {
                Display.Print($"\nВы ввели не число!\nПопробуйте снова: ", ConsoleColor.DarkRed);
            }

            return result;
        }
    }

    public class Buyer : Person
    {
        public Buyer(string name, int money, PersonInfoConfig showInfoConfig) : base(name, money, showInfoConfig) { }

        public bool TryBuyProduct(Product product)
        {
            if (Money >= product.Price)
            {
                Money -= product.Price;
                Products.Add(product);
                return true;
            }

            return false;
        }
    }

    public class PersonInfoConfig
    {
        public PersonInfoConfig(string name, string balance, string productAmount, int positionLeft, int positionTop)
        {
            NameText = name;
            BalanceText = balance;
            ProductAmountText = productAmount;
            PositionLeft = positionLeft;
            PositionTop = positionTop;
        }

        public string NameText { get; }
        public string BalanceText { get; }
        public string ProductAmountText { get; }
        public int PositionLeft { get; }
        public int PositionTop { get; }
    }

    public static class Display
    {
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
    }
}