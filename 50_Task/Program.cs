namespace _50_Task
{
    public class Programm
    {
        public static void Main()
        {
            Console.Title = "ДЗ: Автосервис";
            CarService carService = new CarService();
            carService.Work();
        }
    }

    public class CarService
    {
        private int _balanceMoney;
        private Warehouse _warehouse;
        private PriceList _priceList;

        public CarService()
        {
            _balanceMoney = 0;
            _warehouse = new Warehouse();
            _priceList = new PriceList();
        }

        public void Work()
        {
            const string ServeCarCommand = "1";
            const string ShowCarsQueueCommand = "2";
            const string ShowWarhouseCommand = "3";
            const string ShowPriceListCommand = "4";
            const string ExitCommand = "5";

            Queue<Car> cars = new CarFactory().CreateCarsQueue();
            bool isWork = true;

            while (isWork)
            {
                Console.Clear();
                UserUtils.Print($"Баланс автосервиса: <{_balanceMoney}> $", ConsoleColor.DarkYellow);

                UserUtils.Print($"\n\nКоманды:");
                UserUtils.Print($"\n{ServeCarCommand} - Обслужить автомобиль" +
                                $"\n{ShowCarsQueueCommand} - Посмотреть очередь автомобилей" +
                                $"\n{ShowWarhouseCommand} - Посмотреть складские запасы запчастей" +
                                $"\n{ShowPriceListCommand} - Посмотреть прайслист на запчасти и работу" +
                                $"\n{ExitCommand} - Закрыть автосервис");
                UserUtils.Print($"\n\nВведите команду: ", ConsoleColor.DarkYellow);

                switch (Console.ReadLine())
                {
                    case ServeCarCommand:
                        ServeCars(cars);
                        break;
                    case ShowCarsQueueCommand:
                        UserUtils.Print(cars, "\nОчередь автомобилей: ");
                        break;
                    case ShowWarhouseCommand:
                        _warehouse.ShowCells();
                        break;
                    case ShowPriceListCommand:
                        ShowPriceList();
                        break;
                    case ExitCommand:
                        isWork = false;
                        break;
                    default:
                        UserUtils.Print($"Нет такой команды!", ConsoleColor.Red);
                        break;
                }

                Console.ReadKey();
            }
        }

        private void ShowPriceList()
        {
            _priceList.ShowPriceListDetails();
            UserUtils.Print($"\n{new string('-', 70)}");
            _priceList.ShowPriceListWork();
        }

        private void ServeCars(Queue<Car> cars)
        {
            int refuseToRepairCommand;
            int showWarehouseCommand;
            bool isSurveCar = true;
            bool isDuringRepair = false;
            Car car;
            List<Detail> brokenDetails;
            List<Detail> repairedDetails = new();
            int userInput;
            int totalCost = 0;

            CheckingCarsQueue(cars);
            car = cars.Dequeue();
            brokenDetails = GetBrokenDetails(car);

            while (isSurveCar && brokenDetails.Count != 0)
            {
                ShowMenuToRepairCar(out refuseToRepairCommand, out showWarehouseCommand, car, brokenDetails);
                userInput = UserUtils.ReadInputNumber();

                if (userInput == refuseToRepairCommand)
                {
                    RefuseToReparCar(out isSurveCar, isDuringRepair, car, brokenDetails, repairedDetails);
                }
                else if (userInput == showWarehouseCommand)
                {
                    _warehouse.ShowCells();
                }
                else if (userInput > 0 && userInput <= brokenDetails.Count)
                {
                    RepairCar(ref isDuringRepair, car, brokenDetails, repairedDetails, userInput, ref totalCost);
                }
                else
                {
                    UserUtils.Print($"\nНет такой команды!!");
                }

                brokenDetails = GetBrokenDetails(car);
                UserUtils.Print($"\nДля продолжения нажмите любую клавишу", ConsoleColor.Green);
                Console.ReadKey();
            }

            CheckingCarHealth(brokenDetails, totalCost);
            UserUtils.Print($"\nАвтомобиль <{car.GetInfo()}> выехал из сервиса", ConsoleColor.Green);
        }

        private void CheckingCarsQueue(Queue<Car> cars)
        {
            if (cars.Count == 0)
            {
                UserUtils.Print($"\n\nОчередь пуста!", ConsoleColor.Red);
                return;
            }
        }

        private void ShowMenuToRepairCar(out int refuseToRepairCommand, out int showWarehouseCommand, Car car, List<Detail> brokenDetails)
        {
            showWarehouseCommand = brokenDetails.Count() + 1;
            refuseToRepairCommand = showWarehouseCommand + 1;

            Console.Clear();
            UserUtils.Print($"Автомобиль <{car.GetInfo()}> заехал на обслуживание", ConsoleColor.DarkYellow);
            UserUtils.Print(brokenDetails, "\nСписок сломанных деталей в автомобиле: ");
            UserUtils.Print("\n\nКоманды: ", ConsoleColor.Green);

            ShowBrokenDetails(brokenDetails);

            UserUtils.Print($"\n{showWarehouseCommand}. Посмотреть запасы деталей на складе", ConsoleColor.DarkYellow);
            UserUtils.Print($"\n{refuseToRepairCommand}. Отказать в ремонте", ConsoleColor.Red);
            UserUtils.Print($"\n\nВведите команду: ", ConsoleColor.Green);
        }

        private void CheckingCarHealth(List<Detail> brokenDetails, int totalCost)
        {
            if (brokenDetails.Count == 0)
            {
                _balanceMoney += totalCost;
                UserUtils.Print($"\nЗа успешный ремонт автомобиля вы получили <{totalCost}> $", ConsoleColor.DarkYellow);
            }
        }

        private void RepairCar(ref bool isDuringRepair, Car car, List<Detail> brokenDetails, List<Detail> repairedDetails, int userInput, ref int totalCost)
        {
            Detail brokenDetail = brokenDetails[--userInput];

            if (TryRepairCar(car, brokenDetail))
            {
                totalCost += _priceList.GetTotalCost(brokenDetail.DetailType);
                isDuringRepair = true;
                brokenDetails.Remove(brokenDetail);
                repairedDetails.Add(brokenDetail);
            }
        }

        private void RefuseToReparCar(out bool isSurveCar, bool isDuringRepair, Car car, List<Detail> brokenDetails, List<Detail> repairedDetails)
        {
            int totalCost = 0;
            int penaltyForRefusal = 100;
            isSurveCar = false;

            if (isDuringRepair)
            {
                int penaltyCost = CalculatelCost(brokenDetails);
                int repairedCost = CalculatelCost(repairedDetails);
                totalCost = repairedCost - penaltyCost;
                _balanceMoney += totalCost;
                UserUtils.Print($"\nВы заплатили штраф за отказ в процессе ремонта машины - <{penaltyCost}>" +
                                $"\nЗа ремонт деталей автомобиля <{car.GetInfo()}> получили - <{repairedCost}>");
            }
            else
            {
                _balanceMoney -= penaltyForRefusal;
                UserUtils.Print($"\nВы заплатили фиксированный штраф за отказ ремонтировать машину - <{penaltyForRefusal}>");
            }
        }

        private int CalculatelCost(List<Detail> details)
        {
            int totalCost = 0;

            foreach (Detail brokenDetail in details)
            {
                totalCost += _priceList.GetPriceDetail(brokenDetail.DetailType);
            }

            return totalCost;
        }

        private static List<Detail> GetBrokenDetails(Car car) =>
            car.Details.Where(detail => detail.IsBroken).ToList();

        private void ShowBrokenDetails(List<Detail> brokenDetails)
        {
            int index = 0;

            foreach (Detail detail in brokenDetails)
            {
                UserUtils.Print($"\n{++index}. Отремонтировать <{detail.Name}>");
            }
        }

        private bool TryRepairCar(Car car, Detail brokenDetail)
        {
            Detail newDetail = _warehouse.TryTakeDetail(brokenDetail.DetailType);

            if (newDetail == null)
            {
                UserUtils.Print($"\n<{brokenDetail.Name}> - заменить не получится, её нет в наличии", ConsoleColor.Red);
                return false;
            }

            car.Repair(newDetail);
            UserUtils.Print($"\nВ машине <{car.GetInfo()}> была заменена деталь <{brokenDetail.Name}> на новую");
            return true;
        }
    }

    public class PriceList
    {
        private Dictionary<DetailType, int> _detailsPrice;
        private Dictionary<DetailType, int> _workPrice;

        public PriceList()
        {
            int minPriceDetail = 100;
            int maxPriceDetail = 300;
            int minPriceWork = 200;
            int maxPriceWork = 400;

            _detailsPrice = FillPriceList(minPriceDetail, maxPriceDetail);
            _workPrice = FillPriceList(minPriceWork, maxPriceWork);
        }

        public int GetPriceDetail(DetailType detailType) =>
            GetPrice(_detailsPrice, detailType);

        public int GetTotalCost(DetailType detailType) =>
             GetPrice(_detailsPrice, detailType) + GetPrice(_workPrice, detailType);

        public void ShowPriceListDetails() =>
            ShowPrice(_detailsPrice, "\nПрайслист на запчасти: ");

        public void ShowPriceListWork() =>
            ShowPrice(_workPrice, "\nПрайслист за работу: ");

        public void ShowPrice(Dictionary<DetailType, int> priceList, string message)
        {
            int index = 0;
            UserUtils.Print($"{message}", ConsoleColor.Green);

            for (int i = 0; i < priceList.Count; i++)
            {
                string detailName = DetailsData.GetName(priceList.Keys.ToArray()[i]);
                int price = priceList.Values.ToArray()[i];
                UserUtils.Print($"\n<{detailName}> - цена <{price}> $");
            }
        }

        private Dictionary<DetailType, int> FillPriceList(int minPrice, int maxPrice)
        {
            List<DetailType> detailTypes = DetailsData.GetDetailsType();
            Dictionary<DetailType, int> priceList = new();

            for (int i = 0; i < detailTypes.Count; i++)
            {
                priceList.Add(detailTypes[i], UserUtils.GenerateRandomNumber(minPrice, maxPrice));
            }

            return priceList;
        }

        private int GetPrice(Dictionary<DetailType, int> priceList, DetailType detailType)
        {
            int priceValue = 0;

            if (priceList.TryGetValue(detailType, out priceValue))
            {
                return priceValue;
            }

            return priceValue;
        }
    }

    public class Warehouse
    {
        private List<Cell> _cells;

        public Warehouse() =>
            _cells = new CellFactory().GetCells();

        public void ShowCells() =>
            UserUtils.Print(_cells, "\nСкладские запасы:");

        public Detail TryTakeDetail(DetailType detailType)
        {
            Cell cell = _cells.Where(cell => cell.Detail.DetailType == detailType).First();


            if (cell.TryGetDetail(out Detail detail))
            {
                UserUtils.Print($"\nСо склада взята запчасть: {detail.GetInfo()}", ConsoleColor.Green);
                return detail;
            }
            else
            {
                UserUtils.Print($"\nНа складе нет нужной запчасти: {DetailsData.GetName(detailType)}", ConsoleColor.Red);
                return null;
            }
        }
    }

    public class Detail : IHaveInfo
    {
        public Detail(DetailType detailType, bool isBroken = false)
        {
            DetailType = detailType;
            IsBroken = isBroken;
        }

        public DetailType DetailType { get; }

        public string Name =>
            DetailsData.GetName(DetailType);

        public bool IsBroken { get; }

        public string BrokenStatus =>
            IsBroken == true ? "Сломана" : "Исправна";

        public string GetInfo() =>
            $"{Name} - {BrokenStatus}";
    }

    public class CellFactory
    {
        private DetailFactory _detailFactory;

        public CellFactory() =>
            _detailFactory = new DetailFactory();

        public List<Cell> GetCells()
        {
            List<Cell> cells = new();
            List<Detail> details = _detailFactory.GetDetails();

            for (int i = 0; i < details.Count; i++)
            {
                cells.Add(CreatrCell(details[i]));
            }

            return cells;
        }

        private Cell CreatrCell(Detail detail)
        {
            int maxAmount = 10;
            int randomDetailAmount = UserUtils.GenerateRandomNumber(0, maxAmount);

            return new Cell(detail, randomDetailAmount);
        }
    }

    public class Cell : IHaveInfo
    {
        private int _amount;
        private int _maxCapacity;

        public Cell(Detail detail, int amount)
        {
            _maxCapacity = UserUtils.GenerateRandomNumber(0, 10);
            Detail = detail;
            Amount = amount;
        }

        public Detail Detail { get; }

        public int Amount
        {
            get => _amount;
            private set => _amount = Math.Clamp(value, 0, _maxCapacity);
        }

        public bool TryGetDetail(out Detail detail)
        {
            if (Amount > 0)
            {
                detail = new Detail(Detail.DetailType);
                --Amount;
                return true;
            }
            else
            {
                detail = null;
                return false;
            }
        }

        public string GetInfo() =>
            $"<{Detail.Name}>, количество: <{Amount}>";
    }

    public class CarFactory
    {
        private DetailFactory _detailFactory;

        public CarFactory() =>
            _detailFactory = new DetailFactory();

        public Queue<Car> CreateCarsQueue()
        {
            int minCarCount = 5;
            int maxCarCount = 10;
            int randomCarCount = UserUtils.GenerateRandomNumber(minCarCount, maxCarCount);
            Queue<Car> cars = new();

            for (int i = 0; i < randomCarCount; i++)
            {
                cars.Enqueue(CreateCar());
            }

            return cars;
        }

        private Car CreateCar()
        {
            List<string> modelName = new()
            {
                "Shkoda Octavia", "Audi Q5", "Audi A6", "BMW X3", "BMW X6",
                "Chevrolet Cruze", "Chevrolet Malibu", "Ford Mondeo", "Ford Mustang",
                "Honda Accord","Honda Civic", "Huyndai Solaris","Huyndai Creta",
                "Kia Ceed", "Kia Sorento", "Mazda CX-5", "Mazda 6", "Opel Corsa",
                "Opel Astra", "Nissan Murano", "Nissan X-Trail","Toyota Camry",
                "Toyota RAV4", "Volkswagen Tiguan", "Volkswagen Golf"
            };

            int minBrokenDetail = 1;
            int maxBrokenDetail = 3;
            int randomBrokenCount = UserUtils.GenerateRandomNumber(minBrokenDetail, maxBrokenDetail);
            List<Detail> details = _detailFactory.CreateDetailsWithBroken(randomBrokenCount);
            string randomName = modelName[UserUtils.GenerateRandomNumber(0, modelName.Count - 1)];

            return new Car(details, randomName);
        }
    }

    public class DetailFactory
    {
        public List<Detail> CreateDetailsWithBroken(int brokenDetailCount = 1)
        {
            List<DetailType> detailsType = DetailsData.GetDetailsType();
            List<DetailType> brokenDetailTypes = GetBrokenDetailsTypes(brokenDetailCount, detailsType);
            List<Detail> details = new();
            bool isDetailBroken;

            for (int i = 0; i < detailsType.Count; i++)
            {
                isDetailBroken = brokenDetailTypes.Contains(detailsType[i]);
                details.Add(new Detail(detailsType[i], isDetailBroken));
            }

            return details;
        }

        public List<Detail> GetDetails()
        {
            List<Detail> details = new();

            for (int i = 0; i < DetailsData.GetDetailsType().Count; i++)
            {
                details.Add(new Detail(DetailsData.GetDetailsType()[i]));
            }

            return details;
        }

        private List<DetailType> GetBrokenDetailsTypes(int brokenDetailCount, List<DetailType> detailsTypes)
        {
            List<DetailType> brokenDetailsTypes = new();

            for (int i = 0; i < brokenDetailCount; i++)
            {
                DetailType randomDetailType = detailsTypes[UserUtils.GenerateRandomNumber(0, detailsTypes.Count - 1)];
                brokenDetailsTypes.Add(randomDetailType);
            }

            return brokenDetailsTypes;
        }
    }

    public class Car : IHaveInfo
    {
        private List<Detail> _details;

        public Car(List<Detail> details, string model)
        {
            _details = details;
            Name = model;
        }

        public string Name { get; }

        public IEnumerable<Detail> Details => _details;

        public void Repair(Detail newDetail)
        {
            _details.Remove(GetReplaceableDetail(newDetail));
            _details.Add(newDetail);
        }

        public string GetInfo() =>
            $"{Name}";

        private Detail GetReplaceableDetail(Detail newDetail) =>
            _details.Where(detail => detail.DetailType == newDetail.DetailType).First();
    }

    public enum DetailType
    {
        OilFilter, SparkPlugs, AirFilter, Battery, Generator,
        TimingBelt, Wheel, BrakePads, Antifreeze, Headlight
    }

    public interface IHaveInfo
    {
        string GetInfo();
    }

    public static class DetailsData
    {
        private static IEnumerable<DetailType> s_detailTypes;

        static DetailsData() => 
            s_detailTypes = s_detailsName.Keys.ToList();

        private static Dictionary<DetailType, string> s_detailsName = new()
        {
            {DetailType.OilFilter,  "Масляный фильтр"},
            {DetailType.SparkPlugs, "Свечи зажигания"},
            {DetailType.AirFilter, "Воздушный фильтр"},
            {DetailType.Battery, "Аккумулятор"},
            {DetailType.Generator, "Генератор"},
            {DetailType.TimingBelt, "Ремень ГРМ"},
            {DetailType.Wheel, "Колесо"},
            {DetailType.BrakePads, "Тормозные колодки"},
            {DetailType.Antifreeze, "Антифриз"},
            {DetailType.Headlight, "Фара"}
        };

        public static string GetName(DetailType type)
        {
            s_detailsName.TryGetValue(type, out string name);
            return name;
        }

        public static List<DetailType> GetDetailsType() =>
            new List<DetailType>(s_detailTypes);
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
            Console.ForegroundColor = consoleColor;
            Print(message);
            Console.ResetColor();
        }

        public static void Print<T>(IEnumerable<T> items, string message) where T : IHaveInfo
        {
            int index = 0;
            Print($"{message}", ConsoleColor.Green);

            foreach (var item in items)
            {
                Print($"\n{++index}. {item.GetInfo()}");
            }
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