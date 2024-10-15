namespace _34_Task
{
    class Program
    {
        static void Main()
        {
            Console.Title = "ДЗ: Очередь в магазине";
            WorkShop();
        }

        private static void WorkShop()
        {
            Random random = new();
            string requestMessage = "Нажмите любую клавишу для продолжения";
            int shopBalance = 0;
            int queueCustomerCount = 15;
            int minMoneyCustomer = 50;
            int maxMoneyCustomer = 500;
            int currentCustomer = 0;
            int paidMoney;

            Queue<int> customers = FillingQueueCustomers(random, queueCustomerCount, minMoneyCustomer, maxMoneyCustomer);

            while (customers.Count > 0)
            {
                Console.Clear();
                Console.WriteLine($"Баланс магазина : [{shopBalance}] рублей");

                paidMoney = customers.Dequeue();
                currentCustomer++;

                Console.WriteLine($"Обслуживается клиент №{currentCustomer}:");
                Console.WriteLine($"Совершена покупка товара на сумму {paidMoney} рублей");

                shopBalance += paidMoney;

                Console.WriteLine("\n" + requestMessage);
                Console.ReadLine();
            }

            Console.Clear();
            Console.WriteLine($"Баланс магазина от продаж за прошедший день составляет: {shopBalance} рублей");
            Console.ReadLine();
        }

        private static Queue<int> FillingQueueCustomers(Random random, int queueCustomerCount, int minMoneyCustomer, int maxMoneyCustomer)
        {
            Queue<int> customers = new Queue<int>();

            for (int i = 0; i < queueCustomerCount; i++)
            {
                customers.Enqueue(random.Next(minMoneyCustomer, maxMoneyCustomer + 1));
            }

            return customers;
        }
    }
}