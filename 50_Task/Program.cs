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
        public CarService()
        {
        }

        internal void Work()
        {
            #region For Test #########################################################################################

            Queue<Car> cars = new CarFactory().CreateCarsQueue(10);

            foreach (Car car in cars)
            {
                car.ShowInfo();
            }

            #endregion ###############################################################################################

            Console.ReadKey();
        }
    }

    public class Warehouse
    {
        //Склад деталей
    }

    public class Detail
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

        public override string ToString() =>
            $"{Name}";
    }

    public class Cell
    {
        private Detail _detail;
        private int _amount;
        private int _maxCapacity;

        public Cell(Detail detail, int amount)
        {
            _detail = detail;
            _amount = amount;
            _maxCapacity = UserUtils.GenerateRandomNumber(0, 10);
        }

        public Detail Detail => _detail;

        public int Amount
        {
            get => _amount;
            private set => _amount = Math.Clamp(value, 0, _maxCapacity);
        }

        public Detail GetDetail()
        {
            bool isBroken = false;
            Amount--;

            return new Detail(_detail.DetailType, isBroken);
        }

        public override string ToString() =>
            $"<{_detail.Name}>, количество: <{Amount}>";
    }

    public class CarFactory
    {
        public Queue<Car> CreateCarsQueue(int carCount)
        {
            Queue<Car> cars = new();

            for (int i = 0; i < carCount; i++)
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
            List<DetailType> brokenDetailTypes = GetBrokenDetailTypes(brokenDetailCount, detailsType);
            List<Detail> details = new();
            bool isDetailBroken;            

            for (int i = 0; i < detailsType.Count; i++)
            {
                if (brokenDetailTypes.Contains(detailsType[i]))
                {
                    isDetailBroken = true;
                    details.Add(new Detail(detailsType[i], true));
                }
                else
                {
                    isDetailBroken = false;
                    details.Add(new Detail(detailsType[i], isDetailBroken));
                }
            }

            return details;
        }

        private List<DetailType> GetBrokenDetailTypes(int brokenDetailCount, List<DetailType> detailsType)
        {
            List<DetailType> brokenDetailTypes = new();

            for (int i = 0; i < brokenDetailCount; i++)
            {
                DetailType randomDetailType = detailsType[UserUtils.GenerateRandomNumber(0, detailsType.Count - 1)];
                brokenDetailTypes.Add(randomDetailType);
            }

            return brokenDetailTypes;
        }
    }

    public class Car
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

        public void ShowInfo()
        {
            int index = 0;

            UserUtils.Print($"\nИнформация об машине:" +
                            $"\nМодель <{Name}>");

            _details.ForEach((detail) =>
                UserUtils.Print($"\n{++index}. Деталь <{detail.Name}> - {detail.IsBrokenStatus}"));
        }

        private Detail GetReplaceableDetail(Detail newDetail) =>
            _details.Where(detail => detail.DetailType == newDetail.DetailType).First();
    }

    public enum DetailType
    {
        OilFilter,
        SparkPlugs,
        AirFilter,
        Battery,
        Generator,
        TimingBelt,
        Wheel,
        BrakePads,
        Antifreeze,
        Headlight
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