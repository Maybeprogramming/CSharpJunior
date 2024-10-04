namespace _15_Task
{
    public class Program
    {
        static void Main()
        {
            Console.Title = "ДЗ: Программа под паролем";
            string password = "qwerty";
            string userInput;
            int tryPasswordCount = 3;
            int leftTryInputPassword;
            string requestInputMessage = $"Введите пароль для доступа: ";
            string continueMessege = "Нажмите клавишу чтобы продолжить";
            string topSecretMessege = "Менторы, вы молодцы! Так держать!";
            string incorrectInputPassworMessage = $"Пароль неверный!";

            for (int i = 0; i < tryPasswordCount; i++)
            {
                Console.Clear();
                Console.Write(requestInputMessage);
                userInput = Console.ReadLine();

                if (userInput == password)
                {
                    Console.WriteLine(topSecretMessege);
                    Console.WriteLine(continueMessege);
                    Console.ReadLine();
                }
                else
                {
                    leftTryInputPassword = tryPasswordCount - (i + 1);
                    Console.WriteLine(incorrectInputPassworMessage);
                    Console.WriteLine($"Осталось {leftTryInputPassword} попыток для ввода пароля.");
                    Console.WriteLine(continueMessege);
                    Console.ReadLine();
                }
            }
        }
    }
}