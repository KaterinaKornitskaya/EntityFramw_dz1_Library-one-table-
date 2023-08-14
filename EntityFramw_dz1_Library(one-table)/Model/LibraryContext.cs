using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFramw_dz1_Library_one_table_.Model
{
    // класс-посредник LibraryContext - отвечает за взаимодействие с БД
    internal class LibraryContext : DbContext  // наследуется от DbContext
    {
        // в конструкторе вызываем конструктор базового класса DbContext
        // и передаем в него строку подключения
        public LibraryContext() : base("MyLibrary") { }

        // создаем в оперативной памяти коллекцию books, которая будет
        // содержать объекты класса BookInfo
        // теперь DbContext по коллекции books будет создавать таблицу в БД
        public DbSet<BookInfo> books { get; set; }

        // метод Добавление книги
        public string AddBook(string title, string cat, string publish, int num, string author)
        {
            // в коллекцию books добавляем новый объект Книга, инициализируем
            // книгу информацией из текстбоксов
            books.Add(new BookInfo(title, cat, publish, num, author));
            // синхронизация изменений с БД
            SaveChanges();
            // возвращаю название, чтобы потом вывести в МеседжБокс
            return title;
        }

        // метод Удаление книги
        public void DeleteBook(BookInfo obj)
        {
            // в коллекции books удаляем выбранный в листбокс объект,
            // который в коллекции ищем по Id
            books.Remove(books.Find(obj.Id));
            // синхронизация изменений с БД
            SaveChanges();
        }

        // метод Редактирование книги
        public void EditBook(BookInfo obj, string t, string c, string p, int n, string a)
        {
            // выбранный в листбокс объект ищем в коллекции books по Id
            obj = books.Find(obj.Id);
            // меняем значения полей выбранного объекта
            obj.Title = t;
            obj.Category = c;
            obj.Publish = p;
            obj.PagesNumber = n;
            obj.Author = a;
            SaveChanges();
        }

        // метод для поиска книги по автору
        public List<BookInfo> SearchByAuthor(string author)
        {
            // создаем список книг
            List<BookInfo> list = new List<BookInfo>();
            foreach (BookInfo book in books)  // в цикле по массиву книг
            {
                if (book.Author == author)    // если автор книги == переданному в текстбоксе
                    list.Add(book);           // добавляем эту книгу в список
            }
            if (list.Count == 0)  // если список по итогу пустой - выводим сообщение
                MessageBox.Show($"Нет книги с автором '{author}'");
            return list;  // метод возвращаем список с автором
        }

        // метод для поиска книги по названию
        public List<BookInfo> SearchByTitle(string title)
        {
            // аналогично методу выше
            List<BookInfo> list = new List<BookInfo> ();
            foreach(BookInfo book in books)
            {
                if (book.Title == title)
                    list.Add(book);
            }
            return list;
        }

        // метод для поиска книги по категории
        public List<BookInfo> SearchByCategory(string cat)
        {
            // аналогично методу выше
            List<BookInfo> list = new List<BookInfo>();
            foreach (BookInfo book in books)
            {
                if (book.Category == cat)
                    list.Add(book);

            }
            return list;
        }

        // метод для поиска книги по издательству
        public List<BookInfo> SearchByPublish(string pub)
        {
            // аналогично методу выше
            List<BookInfo> list = new List<BookInfo>();
            foreach (BookInfo book in books)
            {
                if (book.Publish == pub)
                    list.Add(book);
            }
            return list;
        }
    }
}
