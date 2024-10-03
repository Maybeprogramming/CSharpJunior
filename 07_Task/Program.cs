namespace _07_Task
{
    public class Program
    {
        static void Main()
        {
            Console.Title = "ДЗ: Поликлиника";

            int patientsInLine;
            int amountReceptionTime = 10;
            int minutesInHour = 60;
            int waitingTime;
            int waitingHourCount;
            int waitingMinutesCount;

            Console.WriteLine("Вы пришли в больницу на приём к врачу и видите большую очередь.");
            Console.WriteLine("Сколько пациентов стоит перед вами в очереди?");

            patientsInLine = Convert.ToInt32(Console.ReadLine());

            waitingTime = patientsInLine * amountReceptionTime;
            waitingHourCount = waitingTime / minutesInHour;
            waitingMinutesCount = waitingTime % minutesInHour;

            Console.WriteLine("Немного прикинув, вы определили сколько вам ждать.");
            Console.WriteLine($"Вы должны отстоять в очереди {waitingHourCount} часа и {waitingMinutesCount} минут.");

            Console.ReadKey();
        }
    }
}
