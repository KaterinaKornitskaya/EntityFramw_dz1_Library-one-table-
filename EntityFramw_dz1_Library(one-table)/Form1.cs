using EntityFramw_dz1_Library_one_table_.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFramw_dz1_Library_one_table_
{
    // 1) используем фреймфорк EntityFramework (название проекта-
    //    -Manage NuGet Packages-подключаем EntityFramework)
    // 2) используем Code first
    // 3) в app.config создаем строку подключения
    public partial class Form1 : Form
    {
        // выделяем память для объекта LibraryContext
        private LibraryContext db = new LibraryContext();
        public Form1()
        {
            InitializeComponent();
            listBox1.DataSource = db.books.ToList();
            listBox1.HorizontalScrollbar = true;
            listBox2.HorizontalScrollbar = true;
        }

        // метод для очистки текстбоксов
        private void ClearTextBoxes()
        {
            textBoxTitle.Text = string.Empty;
            textBoxCat.Text = string.Empty;
            textBoxPublish.Text = string.Empty;
            textBoxNum.Text = string.Empty;
            textBoxAuthor.Text = string.Empty;
        }

        // обработчик кнопки Добавить книгу
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            using (db = new LibraryContext())
            {
                // вызываю метод Добавить книгу, и инициализирую строку
                // возвращаемым значением
                string str = 
                db.AddBook(textBoxTitle.Text, textBoxCat.Text, textBoxPublish.Text,
                    Convert.ToInt32(textBoxNum.Text), textBoxAuthor.Text);

                // инициализируем элемент ЛистБокс коллекцией books
                listBox1.DataSource = db.books.ToList();
                // выводим в сообщение инфо о том, какая книга добавлена
                MessageBox.Show($"Книга {str} добавлена");
                // вызываем метод очистки текстбоксов
                ClearTextBoxes();
            }
        }

        // обработчик кнопки Удалить книгу
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            using (db = new LibraryContext())
            {
                // создаем объект, инициализируем его выбранным объектов в листБокс
                BookInfo obj = (BookInfo)listBox1.SelectedItem;
                // вызываем метод Удалить, передаем выделенный объект
                db.DeleteBook(obj);
                // инициализируем элемент ЛистБокс коллекцией books
                listBox1.DataSource = db.books.ToList();
                // вывод сообщения об удалении
                MessageBox.Show($"Книга {obj.Title} удалена");
            }
        }

        // обработчик кнопки Редактировать книгу
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            using (db = new LibraryContext())
            {
                // создаем объект, инициализируем его выбранным объектов в листБокс
                BookInfo obj = (BookInfo)listBox1.SelectedItem;
                // вызываем метод Редактировать, передаем выделенный объект и
                // значения текстбоксов, которыми заполним поля объекта для изменения
                db.EditBook(obj, textBoxTitle.Text, textBoxCat.Text, textBoxPublish.Text,
                    Convert.ToInt32(textBoxNum.Text), textBoxAuthor.Text);
                // инициализируем элемент ЛистБокс коллекцией books
                listBox1.DataSource = db.books.ToList();
            }
        }

        // обработчик кнопки Поиск
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            using (db = new LibraryContext())
            {
                // в зависимости от выбранного пункта в КомбоБокс
                switch (comboBox1.SelectedIndex)
                {
                    // инициализируем второй ЛистБокс
                    case 0:
                        listBox2.DataSource = null;
                        listBox2.DataSource = db.SearchByAuthor(textBoxForSearch.Text);
                        break;
                    case 1:
                        listBox2.DataSource = null;
                        listBox2.DataSource = db.SearchByTitle(textBoxForSearch.Text);
                        break;
                    case 2:
                        listBox2.DataSource = null;
                        listBox2.DataSource = db.SearchByCategory(textBoxForSearch.Text);
                        break;
                    case 3:
                        listBox2.DataSource = null;
                        listBox2.DataSource = db.SearchByPublish(textBoxForSearch.Text);
                        break;
                }
            }
        }
        // метод для отображения выбранного элемента в ЛистБоксе
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // создаем объект и инициализируем его выбранным объектом из ЛистБокса
            BookInfo obj = (BookInfo)listBox1.SelectedItem;
            if (obj != null)
            {
                // присваиваем текстбоксам информацию из полей выбранного объекта
                textBoxTitle.Text = obj.Title.ToString();
                textBoxCat.Text = obj.Category.ToString();
                textBoxPublish.Text = obj.Publish.ToString();
                textBoxNum.Text = obj.PagesNumber.ToString();
                textBoxAuthor.Text = obj.Author.ToString();
            }
        }
    }
}
