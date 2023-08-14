using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramw_dz1_Library_one_table_.Model
{
    // класс Книга - здесь реализуем те полями, которые
    // станут столбцами в будущем БД
    internal class BookInfo
    {
        public int Id { get; set; }           // Id
        public string Title { get; set; }     // название книги
        public string Category { get; set; }  // категория
        public string Publish { get; set; }   // издательство
        public int PagesNumber { get; set; }  // кол-во страниц
        public string Author { get; set; }    // автор

        // конструкторы
        public BookInfo() { }
        public BookInfo(string title, string category, string publish, int pagesNumber, string author)
        {
            Title=title;
            Category=category;
            Publish=publish;
            PagesNumber=pagesNumber;
            Author=author;
        }
        // переопределяем метод ToString() для вывода информации
        public override string ToString()
        {
            return $"Id: {Id} \nНазвание: {Title} \nКатегория: {Category} " +
                $"\nИздательство: {Publish} \nКол-во страниц: {PagesNumber} \nАвтор: {Author}" +
                $"\n";
        }
    }
}
