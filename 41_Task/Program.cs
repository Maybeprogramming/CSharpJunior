namespace _41_Task
{
    class Program
    {
        static void Main()
        {
            Console.Title = "ДЗ: Колода карт";
            Player player = new();
            Deck deck = new();
            Croupier croupier = new(player, deck);
            croupier.RunGame();
        }
    }

    class Croupier
    {
        private Player _player;
        private Deck _deck;

        public Croupier(Player player, Deck deck)
        {
            _player = player;
            _deck = deck;
        }

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

            while (isRun)
            {
                Console.Clear();
                Console.WriteLine(menu);

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandTakeCards:
                        TransferCards();
                        break;

                    case CommandStopGame:
                        isRun = false;
                        break;

                    default:
                        Console.WriteLine($"\n\"{userInput}\" - Такой команды нет!");
                        break;
                }

                _player.ShowCards();
                Console.WriteLine($"\nНажмите любую клавишу для продолжения");
                Console.ReadLine();
            }
        }

        private void TransferCards()
        {
            int amountCards = 0;

            while (amountCards <= 0)
            {
                Console.WriteLine("\nВведите положительное количество карт: ");
                amountCards = ReadInputNumber();
            }

            _player.TakeCards(_deck.GiveCards(amountCards));
        }

        private int ReadInputNumber()
        {
            int result;

            while (int.TryParse(Console.ReadLine(), out result) == false)
            {
                Console.WriteLine($"Вы ввели не число, повторите попытку");
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

        public void TakeCards(List<Card> cards)
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
                Console.WriteLine("\nУ игрока нет карт на руках");
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
        private string _value;
        private string _suit;

        public Card(string value, string suit)
        {
            _value = value;
            _suit = suit;
        }

        public string GetInfo() =>
            $"{_value} : {_suit}";
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
            int index;

            for (int i = 0; i < collection.Count; i++)
            {
                index = random.Next(collection.Count);
                Card card = collection[collection.Count - i - 1];
                collection[collection.Count - i - 1] = collection[index];
                collection[index] = card;
            }

            return collection;
        }
    }
}