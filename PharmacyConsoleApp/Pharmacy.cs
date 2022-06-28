using System.Collections.Generic;
using System.Threading;
using static PharmacyConsoleApp.Drug;
using static PharmacyConsoleApp.Helpers;
using static System.Console;

namespace PharmacyConsoleApp
{
    class Pharmacy
    {
        private static int _ID = 1;
        public int ID { get; set; }
        public string Name { get; set; }
        public double MinSalary { get; set; }
        public double Budget { get; set; }
        public string Location;
        public List<Employee> employees;
        public List<Drug> drugs;
        public Pharmacy()
        {
            ID = _ID;
            ++_ID;
            employees = new List<Employee>();
            drugs = new List<Drug>();
        }
        public void Sell(string name, double count, DrugType drugType, Pharmacy pharmacy)
        {
            foreach (var item in drugs)
            {
                if (item.Name.ToUpper() == name.ToUpper() && drugType == item.drugType)
                {
                    if (item.Count < count)
                    {
                        PrintColor($"We have only {item.Count} pieces", System.ConsoleColor.DarkRed);
                        PrintColor($"Would you like? yes/no", System.ConsoleColor.DarkRed);
                        string asksell = ReadLine();
                        if (asksell.ToUpper() == "YES")
                        {
                            pharmacy.Budget += item.Count * item.SalePrice;
                            item.Count -= item.Count;
                            PrintColor($"{item.Name} sold ", System.ConsoleColor.DarkRed);
                            if (item.Count == 0)
                            {
                                pharmacy.drugs.Remove(item);
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        if (item.Count == 0)
                        {
                            PrintColor($"{item.Name} sold out", System.ConsoleColor.DarkRed);
                        }
                        PrintColor($"Would you like to sell? yes/no", System.ConsoleColor.DarkRed);
                        string askselling = ReadLine();
                        if (askselling.ToUpper() == "YES")
                        {
                            pharmacy.Budget += item.Count * item.SalePrice;
                            item.Count -= item.Count;
                            PrintSlideText($"{item.Name} sold...", System.ConsoleColor.DarkRed);
                            Thread.Sleep(1000);
                            if (item.Count == 0)
                            {
                                pharmacy.drugs.Remove(item);
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    PrintSlideText($"{item.Name} type of {item.drugType} sold out", System.ConsoleColor.DarkRed);
                }
            }
        }
    }
}
