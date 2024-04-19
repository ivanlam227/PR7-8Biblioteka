using System;
using System.Linq;

namespace Bibl
{
    class Program
    {
        static void Main(string[] args)
        {
            LibraryCatalog libraryCatalog = new LibraryCatalog();

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Создать/удалить книгу");
                Console.WriteLine("2. Добавить/удалить книгу в каталог");
                Console.WriteLine("3. Вывести информацию по книге");
                Console.WriteLine("4. Поиск книги в каталоге по названию и по автору с выдачей идентификатора");
                Console.WriteLine("5. Выдача книги читателю");
                Console.WriteLine("6. Возврат книги читателем");
                Console.WriteLine("7. Вывод списка читателей, не вернувших книги в течение года");
                Console.WriteLine("8. Выйти из программы");
                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Ошибка ввода. Пожалуйста, введите число от 1 до 8.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Введите название книги:");
                        string title = Console.ReadLine();
                        Console.WriteLine("Введите авторов книги (через запятую):");
                        string[] authors = Console.ReadLine().Split(',');
                        Console.WriteLine("Введите год издания:");
                        uint year;
                        if (!uint.TryParse(Console.ReadLine(), out year))
                        {
                            Console.WriteLine("Ошибка ввода. Пожалуйста, введите целое положительное число.");
                            continue;
                        }
                        Console.WriteLine("Введите издательство:");
                        string publisher = Console.ReadLine();
                        Console.WriteLine("Введите количество страниц:");
                        uint pageCount;
                        if (!uint.TryParse(Console.ReadLine(), out pageCount))
                        {
                            Console.WriteLine("Ошибка ввода. Пожалуйста, введите целое положительное число.");
                            continue;
                        }
                        Console.WriteLine("Введите количество экземпляров:");
                        uint quantity;
                        if (!uint.TryParse(Console.ReadLine(), out quantity))
                        {
                            Console.WriteLine("Ошибка ввода. Пожалуйста, введите целое положительное число.");
                            continue;
                        }
                        libraryCatalog.CreateBook(title, authors.ToList(), year, publisher, pageCount, quantity);
                        break;
                    case 2:
                        Console.WriteLine("Введите ID книги:");
                        uint bookID;
                        if (!uint.TryParse(Console.ReadLine(), out bookID))
                        {
                            Console.WriteLine("Ошибка ввода. Пожалуйста, введите целое положительное число.");
                            continue;
                        }
                        Console.WriteLine("Введите количество экземпляров для добавления или удаления:");
                        uint quantityToAddOrRemove;
                        if (!uint.TryParse(Console.ReadLine(), out quantityToAddOrRemove))
                        {
                            Console.WriteLine("Ошибка ввода. Пожалуйста, введите целое положительное число.");
                            continue;
                        }
                        Console.WriteLine("Введите 'add' для добавления книги или 'remove' для удаления:");
                        string action = Console.ReadLine();
                        if (action.ToLower() == "add")
                        {
                            libraryCatalog.AddBookToCatalog(bookID, quantityToAddOrRemove);
                        }
                        else if (action.ToLower() == "remove")
                        {
                            libraryCatalog.RemoveBookFromCatalog(bookID, quantityToAddOrRemove);
                        }
                        else
                        {
                            Console.WriteLine("Некорректное действие.");
                        }
                        break;
                    case 3:
                        Console.WriteLine("Введите ID книги:");
                        if (!uint.TryParse(Console.ReadLine(), out bookID))
                        {
                            Console.WriteLine("Ошибка ввода. Пожалуйста, введите целое положительное число.");
                            continue;
                        }
                        libraryCatalog.ShowBookInfo(bookID);
                        break;
                    case 4:
                        Console.WriteLine("Введите название или автора книги для поиска:");
                        string searchQuery = Console.ReadLine();
                        var foundBooks = libraryCatalog.FindBooksByTitleOrAuthor(searchQuery);
                        if (foundBooks.Count > 0)
                        {
                            Console.WriteLine("Найденные книги:");
                            foreach (var book in foundBooks)
                            {
                                Console.WriteLine($"ID: {book.ID}, Название: {book.Title}, Автор(ы): {string.Join(", ", book.Authors)}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Книги с указанными критериями не найдены.");
                        }
                        break;
                    case 5:
                        Console.WriteLine("Введите ID книги:");
                        if (!uint.TryParse(Console.ReadLine(), out bookID))
                        {
                            Console.WriteLine("Ошибка ввода. Пожалуйста, введите целое положительное число.");
                            continue;
                        }
                        Console.WriteLine("Введите имя читателя:");
                        string readerName = Console.ReadLine();
                        Reader reader = new Reader { Name = readerName, Date = DateTime.Now };
                        libraryCatalog.IssueBookToReader(bookID, reader);
                        break;
                    case 6:
                        Console.WriteLine("Введите ID книги:");
                        if (!uint.TryParse(Console.ReadLine(), out bookID))
                        {
                            Console.WriteLine("Ошибка ввода. Пожалуйста, введите целое положительное число.");
                            continue;
                        }
                        Console.WriteLine("Введите имя читателя:");
                        string readerNameForReturn = Console.ReadLine();
                        libraryCatalog.ReturnBookFromReader(bookID, readerNameForReturn);
                        break;
                    case 7:
                        var overdueReaders = libraryCatalog.GetOverdueReaders();
                        if (overdueReaders.Count > 0)
                        {
                            Console.WriteLine("Список читателей, не вернувших книги в течение года:");
                            foreach (var overdueReader in overdueReaders)
                            {
                                Console.WriteLine($"Имя: {overdueReader.Name}, Дата выдачи: {overdueReader.Date}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Нет читателей, не вернувших книги в течение года.");
                        }
                        break;
                    case 8:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Пожалуйста, выберите действие от 1 до 8.");
                        break;
                }
            }
        }
    }
}
