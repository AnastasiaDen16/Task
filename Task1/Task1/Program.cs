using System;
using System.IO;

namespace Task1
{
    class Program
    {
        int Sum = 0;
        static void Main(string[] args)
        {
            Program prog = new Program();
            Payers pay = new Payers();
            Payers [] payers = new Payers[12];
            prog.TakePrayers(payers);
            
            for (int i = 0; i < payers.Length; i++)
            {
                payers[i].sum = payers[i].Operation(payers[i].Name, payers[i].Energy);
            }
            pay.Print(payers);
            Console.WriteLine("Сумма оплаты всех клиетов за потреблённую энергию: {0}\n", prog.SUM(payers));
            Console.WriteLine("Сумма льгот клиетов за потреблённую энергию: {0}\n", prog.LG(payers)); ;
            Array.Sort(payers);
            Array.Reverse(payers);
            Console.WriteLine("\n Сортировка потреблённой энергии по убыванию - Array\n");
            pay.Print(payers);
            Array.Sort(payers);
            prog.SortEnergy(payers);
            prog.SortSum(payers);
            prog.SortPayers(payers);
        }
        public Payers [] TakePrayers(Payers [] payers)
        {
            string path = "Payers.txt";
            string text="";
            string[] temp;
            try
            {
                using (StreamReader sr = new StreamReader(path)) {text = sr.ReadToEnd();}
                temp = text.Split(',');
                
                for (int i = 0; i < payers.Length; i++)
                {
                    payers[i] = new Payers();
                    if (i<3) payers[i].Name = Payers.NamePayers.Simple; else
                        if(i>2 && i < 6) payers[i].Name = Payers.NamePayers.withLimit;
                    else
                        if (i > 5 && i< 9) payers[i].Name = Payers.NamePayers.Preferential1;
                    else
                        if (i> 8) payers[i].Name = Payers.NamePayers.Preferential2;
                    payers[i].Energy = Convert.ToDouble(temp[i]);
                }   
            }
            catch (Exception e) {Console.WriteLine(e.Message);}
            return payers;
        }

        public int SUM(Payers [] payers)
        {
            try
            {
                for (int i = 0; i < payers.Length; i++)
                {
                    Sum += Convert.ToInt32(payers[i].sum);
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            return Sum;
        }
        public int LG(Payers[] payers)
        {
            int Sum0 = 0;
            try
            {
                for (int i = 0; i < payers.Length; i++)
                {
                    Sum0 += Convert.ToInt32(payers[i].Energy)*15;
                }
                Sum0 -= Sum; 
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            return Sum0;
        }
        public void SortEnergy(Payers [] p)
        {
            Payers temp;
            Payers pay = new Payers();
            for (int i = 0; i < p.Length; i++)
            {
                for (int j = i + 1; j < p.Length; j++)
                {
                    if (p[i].Energy < p[j].Energy)
                    {
                        temp = p[i];
                        p[i] = p[j];
                        p[j] = temp;
                    }
                }
            }
            Console.WriteLine("\n Сортировка потреблённой энергии по убыванию - код\n");
            pay.Print(p);
        }
        public void SortSum(Payers[] p)
        {
            Payers temp;
            Payers pay = new Payers();
            for (int i = 0; i < p.Length; i++)
            {
                for (int j = i + 1; j < p.Length; j++)
                {
                    if (p[i].sum > p[j].sum)
                    {
                        temp = p[i];
                        p[i] = p[j];
                        p[j] = temp;
                    }
                }
            }
            Console.WriteLine("\n Сортировка суммы оплаты по возрастанию - код\n");
            pay.Print(p);
        }

        public void SortPayers(Payers[] p)
        {
            Payers temp;
            Payers pay = new Payers();
            for (int i = 0; i < p.Length; i++)
            {
                for (int j = i + 1; j < p.Length; j++)
                {
                    if (p[i].Name > p[j].Name)
                    {
                        temp = p[i];
                        p[i] = p[j];
                        p[j] = temp;
                    }
                }
            }
            Console.WriteLine("\n Сортировка по типу клиента - код\n");
            pay.Print(p);
        }
    }
    class Payers : IComparable
    {
        public NamePayers Name { get; set; }
        private int price = 15;
        public double Energy { get; set; }
        public double sum { get; set; }
        public enum NamePayers
        {
            Simple,
            withLimit,
            Preferential1,
            Preferential2
        }
        public double Operation(NamePayers name, double energy)
        {
            try
            {
                switch (name)
                {
                    case NamePayers.Simple:
                        sum = energy * price;
                        break;
                    case NamePayers.withLimit:
                        if (energy > 150) sum = (150 * price) + ((energy - 150) * (price + price / 3));
                        else if (energy <= 150) sum = energy * price;
                        break;
                    case NamePayers.Preferential1: sum = energy * (price / 3 * 2);
                        break;
                    case NamePayers.Preferential2:
                        if (energy > 50) sum = (energy-50) * price;
                        else if (energy <= 50) sum = 0;
                        break;
                } 
            }
            catch (Exception) { Console.WriteLine("Error");}
            return sum;
        }
        public void Print(Payers [] payers)
        {
            foreach(Payers p in payers)
            Console.WriteLine($"Клиент: {p.Name}, Потреблённой эенергии: {p.Energy}, Сумма оплаты: {p.sum}");
        }

        public int CompareTo(object o)
        {
            Payers p = o as Payers;
            if (p != null)
                return this.Energy.CompareTo(p.Energy);
            else throw new Exception("Невозможно сравнить два объекта");
        }
    }
    public interface IComparable<Payers>
    {
        int CompareTo(object o);
    }
}