using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace MyNotes
{
	public partial class Form1 : Form
	{
		private EntryList entries; //список с заметками
		public Form1()
		{
			InitializeComponent();
			// инициализация списка несколькими записями
			entries = new EntryList();
			//entries.AddEntry(new Entry("Встреча с куратором", "по поводу задолженностей", new DateTime(2024, 5, 6)));
			//entries.AddEntry(new Entry("Федорова А.И.", "+79192223311", new DateTime(2024, 3, 12)));
			//entries.AddEntry(new Entry("На салат", "Горошек, лук, яйца, капуста", new DateTime(2024, 4, 17)));
			//entries.PrintToListbox(listBox1);
		}

		private void button4_Click(object sender, EventArgs e)
		{
			if (listBox1.SelectedIndex != -1)
			{
				EntryEditForm editForm = new EntryEditForm(entries, entries.Entries()[listBox1.SelectedIndex]);
				editForm.Show();
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (listBox1.SelectedIndex != -1)
			{
				entries.RemoveEntry(listBox1.SelectedIndex);
				entries.PrintToListbox(listBox1);
			}
		}

		private void button5_Click(object sender, EventArgs e)
		{
			entries.SortByTitle();
			entries.PrintToListbox(listBox1);
		}

		private void button3_Click(object sender, EventArgs e)
		{
			entries.SortByDate();
			entries.PrintToListbox(listBox1);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			EntryEditForm editForm = new EntryEditForm(entries);
			editForm.Show();
		}

		// метод для записи списка в текстовый файл
		public void WriteToFile(string filePath)
		{
			using (StreamWriter writer = new StreamWriter(filePath))
			{
				writer.WriteLine(entries.Entries().Count); // записываем количество записей
				foreach (var entry in entries.Entries())
				{
					string entryString = $"{entry.Title};{entry.Description};{entry.Date.ToString("dd.MM.yyyy")}";
					writer.WriteLine(entryString); // записываем каждую запись в формате "название;описание;дата"
				}
			}
		}

		// метод для восстановления списка из текстового файла
		public EntryList ReadFromFile(string filePath)
		{
			EntryList newList = new EntryList();
			using (StreamReader reader = new StreamReader(filePath))
			{
				int count = int.Parse(reader.ReadLine()); // считываем количество записей
				for (int i = 0; i < count; i++)
				{
					string[] entryData = reader.ReadLine().Split(';'); // считываем и разбираем строку с данными о записи
					string title = entryData[0];
					string description = entryData[1];
					DateTime date = DateTime.ParseExact(entryData[2], "dd.MM.yyyy", null);
					Entry newEntry = new Entry(title, description, date);
					newList.AddEntry(newEntry); // добавляем новую запись в список
				}
			}
			return newList;
		}

		//загрузить файл с заметками через меню
		private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
			{
				try
				{
					entries = ReadFromFile(openFileDialog1.FileName); //загрузка списка
					entries.PrintToListbox(listBox1); //вывод на форму
				}
				catch (Exception exc)
				{
					MessageBox.Show("Ошибка чтения файла: " + exc.Message);
				}
			}
		}

		//сохранить файл с заметками через меню
		private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
			{
				try
				{
					WriteToFile(saveFileDialog1.FileName); //сохранение списка
				}
				catch (Exception exc)
				{
					MessageBox.Show(exc.Message);
				}
			}
		}

		//вывести все записи
		private void button7_Click(object sender, EventArgs e)
		{
			entries.PrintToListbox(listBox1);
		}

		//выборка записей по ключевому слову
		private void button6_Click(object sender, EventArgs e)
		{
			string keyword = textBox1.Text;
			List<Entry> selected = entries.SearchByKeyword(keyword); //выборка записей
			if (selected.Count != 0) //если записи были выбраны
			{
				listBox1.Items.Clear();
				foreach (Entry entry in selected)
				{
					listBox1.Items.Add(entry);
				}
			}
			else
				MessageBox.Show("Нет записей по заданному слову");
		}
	}
}
