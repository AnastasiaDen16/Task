using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Task1
{
    partial class Payers : IComparable
    {
        public NamePayers Name { get; set; }
        public double Energy { get; set; }
        public double sum { get; set; }
        public enum NamePayers
        {
            Simple,
            withLimit,
            Preferential1,
            Preferential2
        }

        protected internal void Print(Payers[] payers)
        {
            foreach (Payers p in payers)
                Console.WriteLine($"Клиент: {p.Name}, Потреблённой эенергии: {p.Energy}, Сумма оплаты: {p.sum}");
        }
        public int CompareTo(object o)
        {
            Payers p = o as Payers;
            if (p != null)
                return this.Energy.CompareTo(p.Energy);
            else throw new Exception("Невозможно сравнить два объекта");
        }
        protected internal Payers[] TakePrayers(Payers[] payers)
        {
            string path = "Payers.txt";
            string text = "";
            string[] temp;
            try
            {
                using (StreamReader sr = new StreamReader(path)) { text = sr.ReadToEnd(); }
                temp = text.Split(',');

                for (int i = 0; i < payers.Length; i++)
                {
                    payers[i] = new Payers();
                    if (i < 3) payers[i].Name = Payers.NamePayers.Simple;
                    else
                        if (i > 2 && i < 6) payers[i].Name = Payers.NamePayers.withLimit;
                    else
                        if (i > 5 && i < 9) payers[i].Name = Payers.NamePayers.Preferential1;
                    else
                        if (i > 8) payers[i].Name = Payers.NamePayers.Preferential2;
                    payers[i].Energy = Convert.ToDouble(temp[i]);
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            return payers;
        }
        protected internal void Sort(Payers [] payers)
        {
            Array.Sort(payers);
            Array.Reverse(payers);
            Print(payers);
        }
    }
}
