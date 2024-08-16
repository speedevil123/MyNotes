using System;
using System.Windows.Forms;

namespace MyNotes
{
	public partial class EntryEditForm : Form
	{
		EntryList entryList;
		Entry entry;
		public EntryEditForm(EntryList entryList, Entry entry=null)
		{
			InitializeComponent();
			this.entryList = entryList;
			if (entry != null)
			{
				button1.Text = "Изменить";
				textBox1.Text = entry.Title;
				textBox2.Text = entry.Description;
			}
		}

		//добавить запись
		private void button1_Click(object sender, EventArgs e)
		{
			string title = textBox1.Text;
			string description = textBox2.Text;
			DateTime date = dateTimePicker1.Value;
			if (entry == null)
			{
				//новая запись в списке
				Entry newEntry = new Entry(title, description, date);
				entryList.AddEntry(newEntry);
			}
			else
			{
				entry.Title = title;
				entry.Date = date;
				entry.Description = description;
			}
			this.Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
