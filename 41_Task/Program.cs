namespace _41_Task
{
    class Program
    {
        static void Main()
        {
            Console.Title = "ДЗ: Колода карт";
            Croupier croupier = new();
            croupier.RunGame();
        }
    }

    class Croupier
    {
        public void RunGame()
        {
            const string CommandTakeCards = "1";
            const string CommandStopGame = "2";

            string menu = $"Крупье:" +
                          $"\n{CommandTakeCards} - взять несколько карт" +
                          $"\n{CommandStopGame} - завершить партию" +
                          $"\nВведите комадну: ";
            string userInput;
            bool isRun = true;
            Player player = new();
            Deck deck = new();

            while (isRun == true)
            {
                Console.Clear();
                Console.WriteLine(menu);

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandTakeCards:
                        TakeCards(player, deck);
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

        private static void TakeCards(Player player, Deck deck)
        {
            int amountCards = DesiredNumberCards();
            player.TakeSomeCards(deck.GiveCards(amountCards));
        }

        public static int DesiredNumberCards()
        {
            Console.WriteLine("\nВведите количество карт: ");
            int disireNumberCards = ReadInputNumber();
            return disireNumberCards;
        }

        private static int ReadInputNumber()
        {
            bool isNumber = false;
            string userInput;
            int result = 0;

            while (isNumber == false)
            {
                userInput = Console.ReadLine();
                isNumber = int.TryParse(userInput, out result);

                if (isNumber == false)
                {
                    Console.WriteLine($"Вы ввели не число: {userInput}");
                }

                if (result < 0)
                {
                    Console.Write($"Ошибка! Введеное число должно быть больше 0!\nПопробуйте снова: ");
                    isNumber = false;
                }
            }

            return result;
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

    class Deck
    {
        private Stack<Card> _cards;

        public Deck()
        {
            _cards = Fill();
        }

        public List<Card> GiveCards(int cardsAmount)
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

        private Stack<Card> Fill()
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