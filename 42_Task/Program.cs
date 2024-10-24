

namespace _42_Task
{
    class Program
    {
        static void Main()
        {
            Console.Title = "ДЗ: Хранилище книг";
            Console.WindowWidth = 120;
            LibraryBooks libraryBooks = new();
            ViewLibrary viewLibrary = new(libraryBooks);
            viewLibrary.Work();

            Console.ReadLine();
        }
    }

    class ViewLibrary
    {
        LibraryBooks _libraryBooks;

        public ViewLibrary(LibraryBooks libraryBooks)
        {
            _libraryBooks = libraryBooks;
        }

        public void Work()
        {
            const string ShowAllBooksCommand = "1";
            const string AddBookCommand = "2";
            const string RemoveBookCommand = "3";
            const string ShowByParameter = "4";
            const string ExitProgramm = "5";

            string menu = $"Меню:" +
                          $"\n{ShowAllBooksCommand} - показать все книги в хранилище" +
                          $"\n{AddBookCommand} - добавить книгу в хранилище" +
                          $"\n{RemoveBookCommand} - убрать книгу из хранилища" +
                          $"\n{ShowByParameter} - показать книги по заданному параметру" +
                          $"\n{ExitProgramm} - закрыть хранилище книг" +
                          $"\nВведите команду: ";
            bool isRun = true;

            while (isRun)
            {
                Console.Clear();
                Display.Print(menu);

                switch (Console.ReadLine())
                {
                    case ShowAllBooksCommand:
                        ShowAllBook();
                        break;

                    case AddBookCommand:
                        ShowMenuAddBook();
                        break;

                    case RemoveBookCommand:
                        ShowMenuRemoveBook();
                        break;

                    case ShowByParameter:
                        ShowFindMenu();
                        break;

                    case ExitProgramm:
                        isRun = false;
                        break;

                    default:
                        Console.WriteLine("Нет такой команды!");
                        break;
                }

                Display.Print($"\n\nНажмите любую клавишу чтобы продолжить", ConsoleColor.Green);
                Console.ReadLine();
            }
        }

        private void ShowMenuAddBook()
        {
            Display.Print("\nВведите название книги: ");
            string inputTitleName = Console.ReadLine();
            Display.Print("Введите автора книги: ");
            string inputAuthor = Console.ReadLine();
            Display.Print("Введите год первой публикации книги: ");
            int inputPublicationYear = ReadInputNumber();
            Display.Print("Введите жанр книги: ");
            string inputGenre = Console.ReadLine();

            _libraryBooks.Add(inputTitleName, inputAuthor, inputPublicationYear, inputGenre);
        }

        private void ShowMenuRemoveBook()
        {
            int inputIndex;
            ShowAllBook();

            do
            {
                Display.Print("\nВведите номер книги для удаления из хранилища: ");
                inputIndex = ReadInputNumber() - 1;
            }
            while (_libraryBooks.TryRemove(inputIndex) == false);
        }

        private void ShowAllBook()
        {
            int indexNumber = 0;
            Display.Print("\nСписок всех книг:", ConsoleColor.Blue);

            foreach (Book book in _libraryBooks.Books)
            {
                Display.Print($"\n{++indexNumber}. {book}");
            }
        }

        public void ShowFindMenu()
        {
            const string TitleMenu = "1";
            const string AuthorMenu = "2";
            const string YearMenu = "3";
            const string GenreMenu = "4";

            string menu = $"\n{TitleMenu} - по названию" +
                          $"\n{AuthorMenu} - по автору" +
                          $"\n{YearMenu} - по году публикации" +
                          $"\n{GenreMenu} - по жанру";

            Console.Clear();
            Display.Print("По какому параметру хотите показать книги в хранилище?");
            Display.Print(menu);

            int indexNumber = 0;
            string userInput;
            PropertyBook propertyBook;

            Display.Print("\nВведите команду: ");

            propertyBook = (PropertyBook)ReadInputNumber() - 1;

            Display.Print($"Конкретизируйте параметр для показа: ");
            userInput = Console.ReadLine();

            switch (propertyBook)
            {
                case PropertyBook.TitleName:
                    {
                        foreach (var book in _libraryBooks.Books)
                        {
                            if (book.TitleName.Equals(userInput))
                            {
                                Display.Print($"\n{++indexNumber}. " + book);
                            }
                        }

                        break;
                    }

                case PropertyBook.Author:
                    {
                        foreach (var book in _libraryBooks.Books)
                        {
                            if (book.Author.Equals(userInput))
                            {
                                Display.Print($"\n{++indexNumber}. " + book);
                            }
                        }

                        break;
                    }

                case PropertyBook.Year:
                    {
                        if (int.TryParse(userInput, out int result) == false)
                        {
                            Display.Print($"Вы ввели не число!", ConsoleColor.Red);
                            break;
                        }

                        foreach (var book in _libraryBooks.Books)
                        {
                            if (book.FirstPublicationYear == result)
                            {
                                Display.Print($"\n{++indexNumber}. " + book);
                            }
                        }

                        break;
                    }

                case PropertyBook.Genre:
                    {
                        foreach (var book in _libraryBooks.Books)
                        {
                            if (book.Genre.Equals(userInput))
                            {
                                Display.Print($"\n{++indexNumber}. " + book);
                            }
                        }

                        break;
                    }

                default:
                    Console.WriteLine("Нет такого параметра!");
                    break;
            }
        }

        private int ReadInputNumber()
        {
            int result;

            while (int.TryParse(Console.ReadLine(), out result) == false)
            {
                Display.Print($"\nВы ввели не число:!\nПопробуйте снова: ");
            }

            return result;
        }
    }

    class Book
    {
        public Book(string titeleName, string author, int firstPublicationYear, string genre)
        {
            TitleName = titeleName;
            Author = author;
            FirstPublicationYear = firstPublicationYear;
            Genre = genre;
        }

        public string TitleName { get; private set; }
        public string Author { get; private set; }
        public int FirstPublicationYear { get; private set; }
        public string Genre { get; private set; }

        public override string ToString()
        {
            return $"Название: \"{TitleName}\", Автор: \"{Author}\", Дата выхода: {FirstPublicationYear}, Жанр: \"{Genre}\"";
        }
    }

    class LibraryBooks
    {
        private List<Book> _books;

        public LibraryBooks()
        {
            _books = new()
            {
                new Book("Дубровский","Александр Пушкин", 1841, "Любовный роман"),
                new Book("Анна Коренина", "Лев Толстой",1875,"Любовный роман"),
                new Book("По ком звонит колокол", "Эрнест Хемингуэй", 1940, "Любовный роман"),
                new Book("Алмазная колесница","Борис Акунин",2002,"Детектив"),
                new Book("Убийство на улице Морг","Эдгар По",1841,"Детектив"),
                new Book("Девушка с татуировкой дракона","Стиг Ларссон",2004,"Детектив"),
                new Book("Конец вечности","Айзек Азимов",1955,"Фантастика"),
                new Book("Дюна","Фрэнк Герберт",1965,"Фантастика"),
                new Book("Звёздный десант","Роберт Хайнлайн",1959,"Фантастика"),
                new Book("Гадкий утёнок","Ганс Христиан Андерсен",1843,"Сказки"),
                new Book("Удивительный волшебник Страны Оз", "Ганс Христиан Андерсен",1900,"Сказки"),
                new Book("Снежная королева", "Ганс Христиан Андерсен",1844,"Сказки"),
                new Book("Руслан и Людмила", "Александр Пушкин",1820,"Сказки"),
                new Book("Кот в сапогах" , "Шарль Перро",1697,"Сказки"),
                new Book("Красная Шапочка" , "Шарль Перро",1697,"Сказки")
            };
        }

        public IReadOnlyList<Book> Books => _books;

        public void Add(string titleName, string author, int publicationYear, string genre)
        {
            Book book = new(titleName, author, publicationYear, genre);
            Display.Print($"{book} - добавлена в библеотеку", ConsoleColor.Green);
            _books.Add(book);
        }

        public bool TryRemove(int index)
        {
            if (index < 0 || index >= _books.Count)
            {
                Display.Print("Книги с таким индексом нет!");

                return false;
            }

            Display.Print($"\nКнига убрана с полки: {_books[index]}");
            _books.Remove(_books[index]);

            return true;
        }
    }

    enum PropertyBook
    {
        TitleName,
        Author,
        Year,
        Genre
    }

    static class Display
    {
        public static void Print(string message)
        {
            Console.Write(message.ToString());
        }

        public static void Print(string message, ConsoleColor consoleColor = ConsoleColor.White)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = consoleColor;
            Print(message);
            Console.ForegroundColor = defaultColor;
        }
    }
}