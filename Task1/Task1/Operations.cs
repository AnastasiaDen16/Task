using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Task1
{
    partial class Operations
    {
        double price;
        double limit;
        double preferential;
        string Path = "settings";
        StreamWriter sw = null;
        protected internal void ChangeSettings(double price, double limit, double preferential)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("Settings.txt", false, System.Text.Encoding.Default))
                {
                    //sw  = new StreamWriter(Assembly.GetExecutingAssembly().Location.Replace("Task1.exe", "Settings.txt"), false, Encoding.Default); ;
                    sw.WriteLine(price.ToString() + ";" + limit.ToString() + ";" + preferential);
                }
            }
            catch (Exception e) {Console.WriteLine("Ошибка: {0}", e);}
            finally{ if (sw != null) sw.Close();}
        }
        protected internal double Operation(Payers.NamePayers name, double energy)
        {
            double sum=0;
            try
            {
                TakeSetting();
                switch (name)
                {
                    case Payers.NamePayers.Simple:
                        sum = energy * price;
                        break;
                    case Payers.NamePayers.withLimit:
                        if (energy > limit) sum = (limit * price) + ((energy - limit) * (price + price / 3));
                        else if (energy <= limit) sum = energy * price;
                        break;
                    case Payers.NamePayers.Preferential1:
                        sum = energy * (price / 3 * 2);
                        break;
                    case Payers.NamePayers.Preferential2:
                        if (energy > preferential) sum = (energy - preferential) * price;
                        else if (energy <= preferential) sum = 0;
                        break;
                }
            }
            catch (Exception) { Console.WriteLine("Error"); }
            return sum;
        }
        protected internal void TakeSetting ()
        {
            string text = "";
            try
            {
                using (StreamReader sr = new StreamReader("Settings.txt"))
                {
                    text = sr.ReadToEnd();
                
                price = Double.Parse(text.Split(';')[0]);
                limit = Double.Parse(text.Split(';')[1]);
                preferential = Double.Parse(text.Split(';')[2]);
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
        protected internal double SUM(Payers[] payers)
        {
            double Sum = 0;
            try
            {
                for (int i = 0; i < payers.Length; i++)
                {
                    Sum += payers[i].sum;
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            return Sum;
        }
        protected internal double LG(Payers[] payers)
        {
            double Sum0 = 0;
            double Sum = SUM(payers);
            try
            {
                for (int i = 0; i < payers.Length; i++)
                {
                    Sum0 += Convert.ToInt32(payers[i].Energy) * 15;
                }
                Sum0 -= Sum;
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            return Sum0;
        }
        protected internal void SortEnergy(Payers[] p)
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
        protected internal void SortSum(Payers[] p)
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

        protected internal void SortPayers(Payers[] p)
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
}
