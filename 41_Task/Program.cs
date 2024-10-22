namespace _41_Task
{
    class Program
    {
        static void Main()
        {
            Console.Title = "ДЗ: Колода карт";
            GameTable gameTable = new();
            gameTable.RunGame();
        }
    }

    class GameTable
    {
        public void RunGame()
        {
            const string CommandTakeSomeCards = "1";
            const string CommandStopGame = "2";

            string menu = $"Крупье:" +
                          $"\n{CommandTakeSomeCards} - взять несколько карт" +
                          $"\n{CommandStopGame} - завершить партию" +
                          $"\nВведите комадну: ";
            string userInput;
            bool isRun = true;
            Player player = new();
            Croupier deck = new();

            while (isRun == true)
            {
                Console.Clear();
                Console.WriteLine(menu);

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandTakeSomeCards:
                        TakeSomeCards(player, deck);
                        break;

                    case CommandStopGame:
                        isRun = false;
                        break;

                    default:
                        Console.WriteLine($"\n\"{userInput}\" - Такой команды нет!");
                        break;
                }

                player.ShowCards();
                Console.WriteLine($"\nНажмите любую клавишу для продолжения");
                Console.ReadLine();
            }
        }

        private static void TakeSomeCards(Player player, Croupier deck)
        {
            int amountCards = player.DesiredNumberCards();
            player.TakeSomeCards(deck.GiveSomeCards(amountCards));
        }
    }

    class Player
    {
        private List<Card> _cards;

        public Player()
        {
            _cards = new();
        }

        public void TakeSomeCards(List<Card> cards)
        {
            if (cards != null && cards.Count != 0)
            {
                _cards.AddRange(cards);
                Console.WriteLine($"\nИгрок взял {cards.Count} карт.");
            }
            else
            {
                Console.WriteLine($"\nИгрок не смог взять несколько карт.\nКолода пуста или там меньше желаемого количества карт");
            }
        }

        public void ShowCards()
        {
            if (_cards.Count == 0)
            {
                Console.WriteLine("У игрока нет карт на руках");
                return;
            }

            Console.WriteLine("\nИгрок имеет на руках следующие карты:");

            foreach (Card card in _cards)
            {
                Console.WriteLine(card.GetInfo());
            }
        }

        public int DesiredNumberCards()
        {
            Console.WriteLine("\nВведите количество карт: ");
            int disireNumberCards = ReadInputNumber();
            return disireNumberCards;
        }

        private int ReadInputNumber()
        {
            bool isTryParse = false;
            string userInput;
            int result = 0;

            while (isTryParse == false)
            {
                userInput = Console.ReadLine();
                isTryParse = int.TryParse(userInput, out result);

                if (isTryParse == false)
                {
                    Console.WriteLine($"Вы ввели не число: {userInput}");
                }

                if (result < 0)
                {
                    Console.Write($"Ошибка! Введеное число должно быть больше 0!\nПопробуйте снова: ");
                    isTryParse = false;
                }
            }

            return result;
        }
    }

    class Card
    {
        public Card(string value, string suit)
        {
            Value = value;
            Suit = suit;
        }

        public string Value { get; }
        public string Suit { get; }

        public string GetInfo() => $"{Value} : {Suit}";
    }

    class Croupier
    {
        private Stack<Card> _cards;

        public Croupier()
        {
            _cards = FillDeck();
        }

        public List<Card> GiveSomeCards(int cardsAmount)
        {
            List<Card> givenCards = new();

            if (_cards.Count >= cardsAmount)
            {
                for (int i = 0; i < cardsAmount; i++)
                {
                    givenCards.Add(_cards.Pop());
                }

                return givenCards;
            }

            return null;
        }

        private Stack<Card> FillDeck()
        {
            Random random = new();
            List<Card> cards = CreateCards();
            cards = Shuffle(cards, random);
            Stack<Card> tempCards = new();

            for (int i = 0; i < cards.Count; i++)
            {
                tempCards.Push(cards[i]);
            }

            return tempCards;
        }

        private List<Card> CreateCards()
        {
            List<string> values = new()
                { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Валет", "Дама", "Король", "Туз" };
            List<string> suits = new()
                { "Червы", "Пики", "Бубны", "Трефы" };
            List<Card> cards = new();

            for (int suitIndex = 0; suitIndex < suits.Count; suitIndex++)
            {
                for (int valueIndex = 0; valueIndex < values.Count; valueIndex++)
                {
                    cards.Add(new Card(values[valueIndex], suits[suitIndex]));
                }
            }

            return cards;
        }

        private List<Card> Shuffle(List<Card> collection, Random random)
        {
            List<Card> tempCollection = new(collection);
            int elementIndex;

            for (int i = 0; i < tempCollection.Count; i++)
            {
                elementIndex = random.Next(collection.Count);
                tempCollection[i] = collection[elementIndex];
                collection.RemoveAt(elementIndex);
            }

            return tempCollection;
        }
    }
}