using System;
using System.IO;

namespace Task1
{
    class Program
    {
        public static double Price { get; set; }
        public static double Limit { get; set; }
        public static double Preferential { get; set; }

        static void Main(string[] args)
        {
            Operations oper = new Operations();
            Payers pay = new Payers();
            Payers [] payers = new Payers[12];
            pay.TakePrayers(payers);
            string Key;
            while (true)
            {
                Console.WriteLine(
                    "--------------------------------------------------------------------------" + '\n' +
                    "1 - Вывести расчет стоимостей за потребленную энергию." + '\n' +
                    "2 - Отсортировать клиентов по количеству потребленной энергии по убыванию.(Метод Sort)" + '\n' +
                    "3 - Отсортировать клиентов по количеству потребленной энергии по убыванию.(Собственный код)" + '\n' +
                    "4 - Отсортировать клиентов по величине оплаты по возрастанию." + '\n' +
                    "5 - Упорядочить клиентов по типу." + '\n' +
                    "6 - Вычислить общую сумму оплаты всех клиентов за потребленную энергию." + '\n' +
                    "8 - Вычислить общий размер льготы." + '\n' +
                    "8 - Изменить коэффициенты, лимиты и тариф." + '\n' +
                    "9 - Очистить консоль." + '\n' +
                    "0 - Выход." + '\n' +
                    "--------------------------------------------------------------------------");
                Key = Console.ReadLine();
                for (int i = 0; i < payers.Length; i++)
                {
                    payers[i].sum = oper.Operation(payers[i].Name, payers[i].Energy);
                }
                switch (Key)
                {
                    case "1":
                        pay.Print(payers);
                        break;
                    case "2":
                        pay.Sort(payers);
                        break;
                    case "3":
                        oper.SortEnergy(payers);
                        break;
                    case "4":
                        oper.SortSum(payers);
                        break;
                    case "5":
                        oper.SortPayers(payers);
                        break;
                    case "6":
                        Console.WriteLine("Сумма оплаты всех клиетов за потреблённую энергию: {0}\n", oper.SUM(payers));
                        break;
                    case "7":
                        Console.WriteLine("Сумма льгот клиетов за потреблённую энергию: {0}\n", oper.LG(payers)); ;
                        break;
                    case "8":
                        Console.Clear();
                        Console.Write("Введите новое значение тарифа: ");
                        Price = double.Parse(Console.ReadLine());
                        Console.Write("Введите новое значение коэффициента для клиентов с лимитом: ");
                        Limit = double.Parse(Console.ReadLine());
                        Console.Write("Введите новое значение коэффициента для клиентов с льготами №2: ");
                        Preferential = double.Parse(Console.ReadLine());
                        oper.ChangeSettings(Price, Limit, Preferential);
                        Console.WriteLine("Изменения сохранены.");
                        Console.WriteLine();
                        break;
                    case "9":
                        Console.Clear();
                        break;
                    case "0":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Вы выбрали неизвестный пункт меню.");
                        break;
                }
            }
        }
    }
}