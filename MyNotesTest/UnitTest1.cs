using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyNotes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Windows.Forms;

namespace MyNotesTest
{
    [TestClass]
    public class EntryTest
    {
        [TestMethod]
        public void TestToString()
        {
            Entry entry = new Entry("Title", "Description", new DateTime(2024, 8, 15));
            string expected = "Title: Description (15.08.2024)";
            string actual = entry.ToString();
            Assert.AreEqual(expected, actual);
        }
    }
    [TestClass]
    public class EntryListTest
    {
        [TestMethod]
        public void AddEntryTest()
        {
            List<Entry> entries = new List<Entry>();

            int previousLength = entries.Count;
            Entry entry = new Entry("Title", "Description", new DateTime(2024, 2, 2));
            entries.Add(entry);
            int actualLength = entries.Count;
            Assert.AreEqual(previousLength + 1, actualLength);
        }
        [TestMethod]
        public void RemoveEntryTest()
        {
            List<Entry> entries = new List<Entry>();

            Entry entry = new Entry("Title", "Description", new DateTime(2024, 2, 2));
            entries.Add(entry);
            int previousLength = entries.Count;

            entries.RemoveAt(0);
            int actualLength = entries.Count;
            Assert.AreEqual(previousLength - 1, actualLength);
        }
        [TestMethod]
        public void SortByDateTest()
        {
            EntryList actual_entries = new EntryList();
            actual_entries.AddEntry(new Entry("Title", "Description", new DateTime(2024, 1, 1)));
            actual_entries.AddEntry(new Entry("Title", "Description", new DateTime(2023, 1, 1)));
            actual_entries.AddEntry(new Entry("Title", "Description", new DateTime(2025, 1, 1)));
            actual_entries.SortByDate();

            List<Entry> actual_list = actual_entries.Entries();
            for (int i = 1; i < actual_list.Count; i++)
            {
                Assert.IsTrue(actual_list[i - 1].Date < actual_list[i].Date);
            }
        }
        [TestMethod]
        public void SortByTitleTest()
        {
            EntryList actual_entries = new EntryList();
            actual_entries.AddEntry(new Entry("B", "Description", new DateTime(2024, 1, 1)));
            actual_entries.AddEntry(new Entry("A", "Description", new DateTime(2023, 1, 1)));
            actual_entries.AddEntry(new Entry("C", "Description", new DateTime(2025, 1, 1)));
            actual_entries.SortByTitle();

            List<Entry> actual_list = actual_entries.Entries();
            for (int i = 1; i < actual_list.Count; i++)
            {
                Assert.IsTrue(String.Compare(actual_list[i - 1].Title, actual_list[i].Title) < 0);
            }
        }
        [TestMethod]
        public void PrintToListboxTest()
        {
            EntryList entries = new EntryList();
            entries.AddEntry(new Entry("Первая запись", "Это первая запись", DateTime.Now));
            entries.AddEntry(new Entry("Вторая запись", "Это вторая запись", DateTime.Now));
            entries.AddEntry(new Entry("Третья запись", "Это третья запись", DateTime.Now));

            ListBox listbox = new ListBox();
            entries.PrintToListbox(listbox);

            Assert.AreEqual(entries.Entries().Count, listbox.Items.Count);
            Assert.AreEqual(entries.Entries()[0].ToString(), listbox.Items[0]);
        }
        [TestMethod]
        public void SearchByKeywordTest()
        {
            EntryList entries = new EntryList();
            entries.AddEntry(new Entry("Первая запись", "Это первая запись", DateTime.Now));
            entries.AddEntry(new Entry("Вторая запись", "Это вторая запись", DateTime.Now));
            entries.AddEntry(new Entry("Третья запись", "Это третья запись", DateTime.Now));

            List<Entry> result = entries.SearchByKeyword("Первая");
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Первая запись", result.First().Title);

        }
    }
    [TestClass]
    public class  Form1Test
    {
        [TestMethod]
        public void WriteToFileTest()
        {
            Form1 form = new Form1();
            form.WriteToFile("notes_test_write.txt");
            Assert.IsTrue(File.Exists("notes_test_write.txt"));
        }
        [TestMethod]
        public void ReadFileTest()
        {
            EntryList entries = new EntryList();

            Form1 form = new Form1();
            entries = form.ReadFromFile("notes_test_read.txt");

            Assert.AreEqual(3, entries.Entries().Count);
            Assert.AreEqual("Встреча с куратором", entries.Entries()[0].Title);
        }
    }
}
