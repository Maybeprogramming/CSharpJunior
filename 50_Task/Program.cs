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

        public CarService()
        {
            _balanceMoney = 0;
            _warehouse = new Warehouse();
        }

        public void Work()
        {
            const string ServeCarCommand = "1";
            const string ShowCarsQueueCommand = "2";
            const string ShowWarhouseCommand = "3";
            const string ExitCommand = "4";

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

        private void ServeCars(Queue<Car> cars)
        {
            const string DoRepairCommand = "1";
            const string RefuseToRepair = "2";

            bool isSurve = true;
            bool isDuringRepair = false;
            Car car;
            List<Detail> brokenDetail;

            if (cars.Count == 0)
            {
                UserUtils.Print($"\n\nОчередь пуста!", ConsoleColor.Red);
                return;
            }

            car = cars.Dequeue();
            brokenDetail = car.Details.Where(detail => detail.IsBroken).ToList();

            while (isSurve)
            {
                Console.Clear();
                UserUtils.Print($"Автомобиль <{car.GetInfo()}> заехал на обслуживание", ConsoleColor.DarkYellow);
                UserUtils.Print(brokenDetail, "\nСписок сломанных деталей в автомобиле: ");

                UserUtils.Print("\n\nКоманды: ", ConsoleColor.Green);
                UserUtils.Print($"\n{DoRepairCommand} - Отремонтировать одну деталь" +
                                $"\n{RefuseToRepair} - Отказать в ремонте");

                UserUtils.Print($"\n\nВведите команду: ", ConsoleColor.Green);

                switch (Console.ReadLine())
                {
                    case DoRepairCommand:
                        RepairCar(car, ref isSurve);
                        break;
                    case RefuseToRepair:
                        RefuseToRepairCar(isDuringRepair, ref isSurve);
                        break;
                    default:
                        UserUtils.Print($"\nНет такой команды!!");
                        break;
                }
            }

            UserUtils.Print($"\nОбслуживание авто когда нибудь");
        }

        private void RepairCar(Car car, ref bool isSurve)
        {
            isSurve = false;
        }

        private void RefuseToRepairCar(bool isDuringRepair, ref bool isServe)
        {
            int penaltyForRefusal = 100;
            int penaltyForRefusalDuringRepair = 50;
            isServe = false;

            if (isDuringRepair)
            {
                //заплатить штраф за каждую деталь
                //Нужен список деталей и цен на них
                _balanceMoney -= penaltyForRefusalDuringRepair;
            }
            else
            {
                _balanceMoney -= penaltyForRefusal;
            }
        }
    }

    public class Warehouse
    {
        private List<Cell> _cells;

        public Warehouse() =>
            _cells = new CellFactory().GetCells();

        public void ShowCells() =>
            UserUtils.Print(_cells, "\nСкладские запасы:");

        public Detail TryGetDetail(DetailType detailType)
        {
            Cell cell = _cells.Where(cell => cell.Detail.DetailType == detailType).First();
            Detail detail = null;

            if (cell.TryGetDetail(out detail))
            {
                UserUtils.Print($"\nСо склада взята запчасть: {detail.GetInfo()}", ConsoleColor.Green);
                return detail;
            }
            else
            {
                UserUtils.Print($"\nНа складе нет нужной запчасти: {DetailsData.GetName(detailType)}", ConsoleColor.Red);
                return detail;
            }
        }
    }

    public class Detail : Iinfoable
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

        public string IsBrokenStatus =>
            IsBroken == true ? "Сломана" : "Исправна";

        public string GetInfo() =>
            $"{Name} - {IsBrokenStatus}";
    }

    public class CellFactory
    {
        public List<Cell> GetCells()
        {
            List<Cell> cells = new();
            List<Detail> details = new DetailFactory().GetDetails();

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

    public class Cell : Iinfoable
    {
        private Detail _detail;
        private int _amount;
        private int _maxCapacity;

        public Cell(Detail detail, int amount)
        {
            _maxCapacity = UserUtils.GenerateRandomNumber(0, 10);
            _detail = detail;
            Amount = amount;
        }

        public Detail Detail => _detail;

        public int Amount
        {
            get => _amount;
            private set => _amount = Math.Clamp(value, 0, _maxCapacity);
        }

        public bool TryGetDetail(out Detail detail)
        {
            if (Amount > 0)
            {
                detail = new Detail(_detail.DetailType);
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
            $"<{_detail.Name}>, количество: <{Amount}>";
    }

    public class CarFactory
    {
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
            List<Detail> details = new DetailFactory().CreateDetailsWithBroken(randomBrokenCount);
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
                if (brokenDetailTypes.Contains(detailsType[i]))
                {
                    isDetailBroken = true;
                    details.Add(new Detail(detailsType[i], isDetailBroken));
                }
                else
                {
                    isDetailBroken = false;
                    details.Add(new Detail(detailsType[i], isDetailBroken));
                }
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

    public class Car : Iinfoable
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

    public interface Iinfoable
    {
        string GetInfo();
    }

    public static class DetailsData
    {
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

        public static List<DetailType> GetDetailsType()
        {
            return s_detailsName.Keys.ToList();
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
            Console.ForegroundColor = consoleColor;
            Print(message);
            Console.ResetColor();
        }

        public static void Print<T>(IEnumerable<T> items, string message) where T : Iinfoable
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