namespace _43_Task
{
    public class Program
    {
        public static void Main()
        {
            Console.Title = "ДЗ: Магазин";
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

            Random random = new();
            int maxBuyerMoney = 1000;
            int sellerMoney = 0;
            string sellerName = "[NPC]";
            string buyerName = "[Игрок]";
            Seller seller = new(sellerName, sellerMoney);
            Buyer buyer = new(buyerName, random.Next(maxBuyerMoney));
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

            while (isWork == true)
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
        protected List<Product> Products;

        public Person(string name, int money = 0)
        {
            Name = name;
            Money = money;
            Products = new();
        }

        public string Name { get;}
        public int Money { get; private protected set; }

        public void ShowProducts()
        {
            int index = 0;

            if (Products.Count <= 0)
            {
                Display.Print($"Нет товаров для отображения.", ConsoleColor.DarkRed);
                return;
            }

            Display.Print($"Список продуктов у {Name}:", ConsoleColor.Green);

            foreach (Product product in Products)
            {
                Display.Print($"\n{++index}. {product}");
            }
        }
    }

    public class Seller : Person
    {
        public Seller(string name, int money) : base(name, money)
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
            Console.Clear();
            ShowProducts();
            buyer.ShowInfo();
            ShowInfo();

            Display.Print($"\nВведите номер желаемого продукта для покупки: ");

            int inputProductIndex = CheckProductIndex(Products.Count);
            Product product = Products[inputProductIndex];

            if (buyer.TruBuyProduct(product) == true)
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

        private int CheckProductIndex(int collectionCount)
        {
            bool isTryParse = false;
            string userInput;
            int productIndex = 0;

            while (isTryParse == false)
            {
                userInput = Console.ReadLine();

                if (int.TryParse(userInput, out int result) == true)
                {
                    if (result > 0 && result <= collectionCount)
                    {
                        productIndex = result;
                        isTryParse = true;
                    }
                    else
                    {
                        Display.Print($"\nОшибка! Такого товара нет!\nпопробуйте снова: ", ConsoleColor.DarkRed);
                    }
                }
                else
                {
                    Display.Print($"\nОшибка! Вы ввели не число: {userInput}!\nПопробуйте снова: ", ConsoleColor.DarkRed);
                }
            }

            return productIndex - 1;
        }

        public void ShowInfo()
        {
            int currentPositionLeft = Console.CursorLeft;
            int currentPositionTop = Console.CursorTop;
            int positionLeft = 60;
            int positionTopName = 4;
            int positionTopBalance = 5;
            int positionTopCountProducts = 6;

            Console.SetCursorPosition(positionLeft, positionTopName);
            Display.Print($"# Имя продавца: {Name}", ConsoleColor.Green);
            Console.SetCursorPosition(positionLeft, positionTopBalance);
            Display.Print($"# Баланс продавца: {Money} рублей.", ConsoleColor.Green);
            Console.SetCursorPosition(positionLeft, positionTopCountProducts);
            Display.Print($"# Количество товаров у продавца: {Products.Count}.", ConsoleColor.Green);

            Console.SetCursorPosition(currentPositionLeft, currentPositionTop);
        }
    }

    public class Buyer : Person
    {
        public Buyer(string name, int money) : base(name, money) { }

        public bool TruBuyProduct(Product product)
        {
            if (Money >= product.Price)
            {
                Money -= product.Price;
                Products.Add(product);
                return true;
            }

            return false;
        }

        public void ShowInfo()
        {
            int currentPositionLeft = Console.CursorLeft;
            int currentPositionTop = Console.CursorTop;
            int positionLeft = 60;
            int positionTopName = 0;
            int positionTopBalance = 1;
            int positionTopCountProducts = 2;

            Console.SetCursorPosition(positionLeft, positionTopName);
            Display.Print($"# Имя покупателя: {Name}", ConsoleColor.Green);
            Console.SetCursorPosition(positionLeft, positionTopBalance);
            Display.Print($"# Баланс покупателя: {Money} рублей.", ConsoleColor.Green);
            Console.SetCursorPosition(positionLeft, positionTopCountProducts);
            Display.Print($"# Количество товаров у покупателя: {Products.Count}.", ConsoleColor.Green);

            Console.SetCursorPosition(currentPositionLeft, currentPositionTop);
        }
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