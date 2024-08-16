using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MyNotes
{
    //класс для записи
    public class Entry
    {
        public string Title { get; set; } //название
        public string Description { get; set; } //описание
        public DateTime Date { get; set; } //дата

        public Entry(string title, string description, DateTime date)
		{
            this.Title = title;
            this.Description = description;
            this.Date = date;
		}

        //преобразование записи в строку
        public override string ToString()
        {
            return $"{Title}: {Description} ({Date.ToString("dd.MM.yyyy")})";
        }
    }

    //класс для работы со списком записей
    public class EntryList
    {
        private List<Entry> entries = new List<Entry>(); //список записей

        public List<Entry> Entries() { return entries; }

        // внести новую запись
        public void AddEntry(Entry entry)
        {
            entries.Add(entry);
        }

        // удалить запись по индексу
        public void RemoveEntry(int index)
        {
            if (index >= 0 && index < entries.Count) // проверка на корректность
            {
                entries.RemoveAt(index);
            }
        }

        // упорядочить по дате
        public void SortByDate()
        {
            entries = entries.OrderBy(e => e.Date).ToList();
        }

        // упорядочить по названию
        public void SortByTitle()
        {
            entries = entries.OrderBy(e => e.Title).ToList();
        }

        // поиск по ключевому слову (и в названии, и в описании)
        public List<Entry> SearchByKeyword(string keyword)
        {
            return entries.Where(e => e.Title.Contains(keyword) || e.Description.Contains(keyword)).ToList();
        }

        // метод для вывода всех записей в объект ListBox
        public void PrintToListbox(ListBox listBox)
        {
            listBox.Items.Clear();  // очищаем перед добавлением новых элементов
            foreach (var entry in entries)
            {
                listBox.Items.Add(entry.ToString());  // добавляем строковое представление записи
            }
        }
    }
}
