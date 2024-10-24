﻿namespace _42_Task
{
    public class Program
    {
        public static void Main()
        {
            Console.Title = "ДЗ: Хранилище книг";
            Console.WindowWidth = 120;
            LibraryBooks libraryBooks = new();
            ViewLibrary viewLibrary = new(libraryBooks);
            viewLibrary.Work();

            Console.ReadLine();
        }
    }

    public class ViewLibrary
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
                        ShowBooks(_libraryBooks.Books);
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
            int inputPublicationYear = ReadInputNumber("Введите год первой публикации книги: ");
            Display.Print("Введите жанр книги: ");
            string inputGenre = Console.ReadLine();

            _libraryBooks.Add(inputTitleName, inputAuthor, inputPublicationYear, inputGenre);
        }

        private void ShowMenuRemoveBook()
        {
            int inputIndex;
            ShowBooks(_libraryBooks.Books);

            do
            {
                inputIndex = ReadInputNumber("\nВведите номер книги для удаления из хранилища: ") - 1;
            }
            while (_libraryBooks.TryRemove(inputIndex) == false);
        }

        private void ShowBooks(IEnumerable<Book> books)
        {
            int indexNumber = 0;
            Display.Print("\nСписок книг:", ConsoleColor.Blue);

            foreach (Book book in books)
            {
                Display.Print($"\n{++indexNumber}. {book}");
            }
        }

        private void ShowFindMenu()
        {
            const string TitleMenu = "1";
            const string AuthorMenu = "2";
            const string YearMenu = "3";
            const string GenreMenu = "4";

            string menu = $"\n{TitleMenu} - по названию" +
                          $"\n{AuthorMenu} - по автору" +
                          $"\n{YearMenu} - по году публикации" +
                          $"\n{GenreMenu} - по жанру";

            int indexNumber = 0;
            string userInput;
            PropertyBook inputPropertyBook;
            IEnumerable<Book> books;

            Console.Clear();
            Display.Print("По какому параметру хотите показать книги в хранилище?");
            Display.Print(menu);

            inputPropertyBook = (PropertyBook)ReadInputNumber("\nВведите команду: ") - 1;

            switch (inputPropertyBook)
            {
                case PropertyBook.TitleName:
                    {
                        userInput = ReadInput("\nВведите название книги: ");
                        books = _libraryBooks.Books.Where(book => book.TitleName.ToLower().Equals(userInput.ToLower()));
                        ShowBooks(books);

                        break;
                    }

                case PropertyBook.Author:
                    {
                        userInput = ReadInput("Введите Автора книга: ");
                        books = _libraryBooks.Books.Where(book => book.Author.ToLower().Equals(userInput.ToLower()));
                        ShowBooks(books);

                        break;
                    }

                case PropertyBook.Year:
                    {
                        int inputYear = ReadInputNumber("Введите год публикации книги: ");
                        books = _libraryBooks.Books.Where(book => book.FirstPublicationYear == inputYear);
                        ShowBooks(books);

                        break;
                    }

                case PropertyBook.Genre:
                    {
                        userInput = ReadInput("Введите жанр: ");
                        books = _libraryBooks.Books.Where(book => book.Genre.ToLower().Equals(userInput.ToLower()));
                        ShowBooks(books);

                        break;
                    }

                default:
                    Console.WriteLine("Нет такого параметра!");
                    break;
            }
        }

        private string ReadInput(string message)
        {
            string userInput;
            Display.Print(message);

            return Console.ReadLine();
        }

        private int ReadInputNumber(string message)
        {
            int result;

            while (int.TryParse(ReadInput(message), out result) == false)
            {
                Display.Print($"\nВы ввели не число:!\nПопробуйте снова: ");
            }

            return result;
        }
    }

    public class Book
    {
        public Book(string titeleName, string author, int firstPublicationYear, string genre)
        {
            TitleName = titeleName;
            Author = author;
            FirstPublicationYear = firstPublicationYear;
            Genre = genre;
        }

        public string TitleName { get; }
        public string Author { get; }
        public int FirstPublicationYear { get; }
        public string Genre { get; }

        public override string ToString()
        {
            return $"Название: \"{TitleName}\", Автор: \"{Author}\", Дата выхода: {FirstPublicationYear}, Жанр: \"{Genre}\"";
        }
    }

    public class LibraryBooks
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
                Display.Print("\nКниги с таким индексом нет!", ConsoleColor.Red);

                return false;
            }

            Display.Print($"\nКнига убрана с полки: {_books[index]}");
            _books.Remove(_books[index]);

            return true;
        }
    }

    public enum PropertyBook
    {
        TitleName,
        Author,
        Year,
        Genre
    }

    public static class Display
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