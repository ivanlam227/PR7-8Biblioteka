using System;
using System.Collections.Generic;
using System.Linq;

namespace Bibl
{
    // Класс библиотечного каталога
    class LibraryCatalog
    {
        private List<CatalogBook> catalog = new List<CatalogBook>();

        // Метод создания книги
        public void CreateBook(string title, List<string> authors, uint year, string publisher, uint pageCount, uint quantity)
        {
            CatalogBook newBook = new CatalogBook
            {
                Title = title,
                Authors = authors,
                Year = year,
                Publisher = publisher,
                PageCount = pageCount,
                ID = (uint)catalog.Count + 1,
                Quantity = quantity,
                Instances = quantity,
                Readers = new List<Reader>()
            };
            catalog.Add(newBook);
            Console.WriteLine("Книга успешно создана.");
        }

        // Метод удаления книги
        public void RemoveBook(uint bookID)
        {
            CatalogBook bookToRemove = catalog.Find(book => book.ID == bookID);
            if (bookToRemove != null)
            {
                catalog.Remove(bookToRemove);
                Console.WriteLine("Книга успешно удалена из каталога.");
            }
            else
            {
                Console.WriteLine("Книга с указанным ID не найдена.");
            }
        }

        // Метод добавления книги в каталог
        public void AddBookToCatalog(uint bookID, uint quantity)
        {
            CatalogBook bookToAdd = catalog.Find(book => book.ID == bookID);
            if (bookToAdd != null)
            {
                bookToAdd.Quantity += quantity;
                bookToAdd.Instances += quantity;
                Console.WriteLine("Книга успешно добавлена в каталог.");
            }
            else
            {
                Console.WriteLine("Книга с указанным ID не найдена.");
            }
        }

        // Метод удаления книги из каталога
        public void RemoveBookFromCatalog(uint bookID, uint quantity)
        {
            CatalogBook bookToRemove = catalog.Find(book => book.ID == bookID);
            if (bookToRemove != null)
            {
                if (quantity <= bookToRemove.Quantity && quantity <= bookToRemove.Instances)
                {
                    bookToRemove.Quantity -= quantity;
                    bookToRemove.Instances -= quantity;
                    Console.WriteLine("Книга успешно удалена из каталога.");
                }
                else
                {
                    Console.WriteLine("Указанное количество экземпляров превышает общее количество или количество экземпляров в наличии.");
                }
            }
            else
            {
                Console.WriteLine("Книга с указанным ID не найдена.");
            }
        }

        // Вывод информации по книге
        public void ShowBookInfo(uint bookID)
        {
            CatalogBook book = catalog.Find(b => b.ID == bookID);
            if (book != null)
            {
                Console.WriteLine($"Название: {book.Title}");
                Console.WriteLine($"Автор(ы): {string.Join(", ", book.Authors)}");
                Console.WriteLine($"Год издания: {book.Year}");
                Console.WriteLine($"Издательство: {book.Publisher}");
                Console.WriteLine($"Количество страниц: {book.PageCount}");
                Console.WriteLine($"Общее количество экземпляров: {book.Quantity}");
                Console.WriteLine($"Количество экземпляров в наличии: {book.Instances}");
                Console.WriteLine("Список читателей, взявших книгу:");
                foreach (var reader in book.Readers)
                {
                    Console.WriteLine($"Имя читателя: {reader.Name}, Дата выдачи: {reader.Date}");
                }
            }
            else
            {
                Console.WriteLine("Книга с указанным ID не найдена.");
            }
        }

        // Поиск книги по названию или автору
        public List<CatalogBook> FindBooksByTitleOrAuthor(string searchQuery)
        {
            return catalog.Where(book => book.Title.Contains(searchQuery) || book.Authors.Any(author => author.Contains(searchQuery))).ToList();
        }

        // Выдача книги читателю
        public void IssueBookToReader(uint bookID, Reader reader)
        {
            CatalogBook book = catalog.Find(b => b.ID == bookID);
            if (book != null)
            {
                if (book.Instances > 0)
                {
                    book.Readers.Add(reader);
                    book.Instances--;
                    Console.WriteLine($"Книга \"{book.Title}\" успешно выдана читателю {reader.Name}.");
                }
                else
                {
                    Console.WriteLine("Извините, данной книги в данный момент нет в наличии.");
                }
            }
            else
            {
                Console.WriteLine("Книга с указанным ID не найдена.");
            }
        }

        // Возврат книги читателем
        public void ReturnBookFromReader(uint bookID, string readerName)
        {
            CatalogBook book = catalog.Find(b => b.ID == bookID);
            if (book != null)
            {
                Reader reader = book.Readers.Find(r => r.Name == readerName);
                if (reader != null)
                {
                    book.Readers.Remove(reader);
                    book.Instances++;
                    Console.WriteLine($"Книга \"{book.Title}\" успешно возвращена читателем {readerName}.");
                }
                else
                {
                    Console.WriteLine($"Читатель {readerName} не взял данную книгу.");
                }
            }
            else
            {
                Console.WriteLine("Книга с указанным ID не найдена.");
            }
        }

        // Вывод списка читателей, не вернувших книги в течение года
        public List<Reader> GetOverdueReaders()
        {
            List<Reader> overdueReaders = new List<Reader>();
            foreach (var book in catalog)
            {
                foreach (var reader in book.Readers)
                {
                    if ((DateTime.Now - reader.Date).TotalDays > 365)
                    {
                        overdueReaders.Add(reader);
                    }
                }
            }
            return overdueReaders;
        }
    }
}
