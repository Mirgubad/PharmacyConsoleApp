using Colorful;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using static PharmacyConsoleApp.Drug;
using static PharmacyConsoleApp.Helpers;
using static System.Console;
using Console = Colorful.Console;

namespace PharmacyConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Start();
            AsciiText();
            string chooseMenuAdmin;
            Pharmacy pharmacy = new Pharmacy()
            {
                MinSalary = 450,
                Budget = 50000,
                Name = "Zeytun",
                Location = "Baku Af Mall"
            };
            Employee admin = new Employee
            {
                Name = "Ahmad",
                SurName = "ALiyev",
                UserName = "admin",
                Password = "admin123@",
                Salary = 1200,
                roleType = Employee.RoleType.Admin
            };
            pharmacy.employees.Add(admin);
            bool exit = true;
            while (exit)
            {
            Main:
                PrintColorWrite($"Username:");
                string username = ReadLine();
                CheckPassword("Password:");              
                foreach (var item in pharmacy.employees)
                {
                    if (username == item.UserName && EnteredVal == item.Password)
                    {  AdminStartMenu:
                        if (item.roleType == Employee.RoleType.Admin)
                        {
                            AdminStartMenu();
                            chooseMenuAdmin = ReadLine();
                            switch (chooseMenuAdmin)
                            {
                                case "1":
                                AdminCaseMenu:
                                    AdminCaseMenu();
                                    string chooseMenuFromAdmin = ReadLine();
                                    switch (chooseMenuFromAdmin)
                                    {
                                        case "1":
                                            AddEmploye(pharmacy);
                                            goto AdminStartMenu;
                                        case "2":
                                            AddDrug(pharmacy, exit);
                                            goto AdminStartMenu;
                                        case "3":
                                            RemoveDrug(pharmacy);
                                            goto AdminStartMenu;
                                        case "4":
                                            EditDrug(pharmacy);
                                            goto AdminStartMenu;
                                        case "5":
                                            RemoveEmployee(pharmacy);
                                            goto AdminStartMenu;
                                        case "6":
                                            EditEmployee(pharmacy);
                                            goto AdminStartMenu;
                                        case "7":
                                            #region Exit
                                            Clear();
                                            PrintColor($"Are you sure? yes/no", ConsoleColor.DarkRed);
                                            string lokask1 = ReadLine();
                                            if (lokask1.ToUpper() == "YES")
                                            {
                                                PrintSlideText("Logged out\n", ConsoleColor.DarkRed);
                                                goto Main;
                                            }
                                            else
                                            {

                                                goto AdminStartMenu;
                                            }
                                        #endregion
                                        default:
                                            break;
                                    }
                                    break;
                                default:
                                    break;
                                case "2":
                                    Clear();
                                search:
                                    if (pharmacy.drugs.Count == 0)
                                    {
                                        PrintSlideText("Drug list is empty\n", ConsoleColor.DarkRed);
                                        Thread.Sleep(500);
                                        goto AdminStartMenu;
                                    }
                                    PrintColor($"Press/BackSpace to exit..", ConsoleColor.Cyan);
                                    PrintColor($"Press/Enter to continue..", ConsoleColor.Cyan);
                                    if (ReadKey().Key == ConsoleKey.Backspace)
                                    {
                                        goto AdminCaseMenu;
                                    }
                                    PrintColor($"Search drug on list for sale:", ConsoleColor.DarkGreen);
                                    string searchdrug = ReadLine();
                                    List<Drug> searchdrugs = pharmacy.drugs.FindAll(x => x.Name.ToUpper().Contains(searchdrug.ToUpper()));
                                    foreach (var items in searchdrugs)
                                    {
                                        if (items.Count != 0)
                                        {
                                            PrintColor($"Name:{items.Name} Price:{items.SalePrice} ID:{items.ID} count:{items.Count} type:{items.drugType}", ConsoleColor.DarkGreen);
                                        }
                                        else
                                        {
                                            PrintSlideText("Drug can't found\n", ConsoleColor.DarkRed);
                                        }
                                    }
                                searchcount:
                                    PrintColor($"Drug name", ConsoleColor.DarkGreen);
                                    string drugsell = ReadLine();
                                    PrintColor($"Drug count:", ConsoleColor.DarkGreen);
                                    string searchdrugcount = ReadLine();
                                    if (!double.TryParse(searchdrugcount, out double srchcount))
                                    {
                                        PrintSlideText("Incorrect input\n", ConsoleColor.DarkRed);
                                        goto searchcount;
                                    }
                                    PrintColor($"Drug type:", ConsoleColor.DarkGreen);
                                    PrintColor($"<1>Powder<2>Syrop<3>Tablet", ConsoleColor.DarkGreen);
                                    string searchdrugtype = ReadLine();
                                    foreach (var drugs in pharmacy.drugs)
                                    {
                                        switch (searchdrugtype)
                                        {
                                            case "1":
                                                drugs.drugType = DrugType.Powder;
                                                break;
                                            case "2":
                                                drugs.drugType = DrugType.Syrop;
                                                break;
                                            case "3":
                                                drugs.drugType = DrugType.Tablet;
                                                break;
                                            default:
                                                break;
                                        }
                                        pharmacy.Sell(drugsell, srchcount, drugs.drugType, pharmacy);
                                        goto AdminStartMenu;
                                    }
                                    PrintSlideText("Drug can't found\n", ConsoleColor.DarkRed);
                                    Thread.Sleep(500);
                                    goto search;
                                case "3":
                                    AdminChangeİnformation(pharmacy, username);
                                    goto Main;

                                case "4":
                                    SeeBudget(pharmacy);
                                    goto AdminStartMenu;
                                case "5":
                                    SeeEmployeeList(pharmacy);
                                    goto AdminStartMenu;
                                case "6":
                                    Clear();
                                    PrintSlideText($"Are you sure? yes/no\n", ConsoleColor.DarkRed);
                                    string logask = ReadLine();
                                    if (logask.ToUpper() == "YES")
                                    {
                                        PrintSlideText("Logged out\n", ConsoleColor.DarkRed);
                                        goto Main;
                                    }
                                    else
                                    {
                                        goto AdminStartMenu;
                                    }
                            }
                            if (IsSwitchCaseAdmin(chooseMenuAdmin))
                            {
                                PrintSlideText("Please choose correct menu\n", ConsoleColor.DarkRed);
                                Thread.Sleep(500);
                                goto AdminStartMenu;
                            }  
                        }
                        else
                        {                    
                            foreach (var stuff in pharmacy.employees)
                            {
                                if (stuff.roleType == Employee.RoleType.Stuff)
                                {
                                    if (username == stuff.UserName && EnteredVal == stuff.Password)
                                    {  employeemain:
                                        Clear();
                                        EmployeeStartMenu();
                                        string empchoose = ReadLine();
                                        switch (empchoose)
                                        {
                                            case "1":
                                                Clear();
                                                if (pharmacy.drugs.Count == 0)
                                                {
                                                    PrintSlideText("Drug list is empty\n", ConsoleColor.DarkRed);
                                                    Thread.Sleep(500);
                                                    goto employeemain;
                                                }
                                                PrintColor($"Search drug on list for sale:", ConsoleColor.DarkGreen);
                                                string searchdrug = ReadLine();
                                                List<Drug> searchdrugs = pharmacy.drugs.FindAll(x => x.Name.ToUpper().Contains(searchdrug.ToUpper()));
                                                foreach (var items in searchdrugs)
                                                {
                                                    if (items.Count != 0)
                                                    {
                                                        PrintColor($"Name:{items.Name} Price:{items.SalePrice} ID:{items.ID} count:{items.Count} type:{items.drugType}", ConsoleColor.DarkGreen);
                                                    }
                                                    else
                                                    {
                                                        PrintSlideText("Drug can't found\n", ConsoleColor.DarkRed);
                                                    }
                                                }
                                            searchcount:
                                                PrintColor($"Drug name", ConsoleColor.DarkGreen);
                                                string drugsell = ReadLine();
                                                PrintColor($"Drug count:", ConsoleColor.DarkGreen);
                                                string searchdrugcount = ReadLine();
                                                if (!double.TryParse(searchdrugcount, out double srchcount))
                                                {
                                                    PrintSlideText("Incorrect input\n", ConsoleColor.DarkRed);
                                                    goto searchcount;
                                                }
                                                PrintColor($"Drug type:", ConsoleColor.DarkGreen);
                                                PrintColor($"<1>Powder<2>Syrop<3>Tablet", ConsoleColor.DarkGreen);
                                                string searchdrugtype = ReadLine();
                                                foreach (var drugs in pharmacy.drugs)
                                                {
                                                    switch (searchdrugtype)
                                                    {
                                                        case "1":
                                                            drugs.drugType = DrugType.Powder;
                                                            break;
                                                        case "2":
                                                            drugs.drugType = DrugType.Syrop;
                                                            break;
                                                        case "3":
                                                            drugs.drugType = DrugType.Tablet;
                                                            pharmacy.Sell(drugsell, srchcount, drugs.drugType, pharmacy);
                                                            goto employeemain;
                                                    }
                                                    break;
                                                }
                                                break;
                                            case "2":
                                                ChangeEmployeeInfo(pharmacy, username);
                                                goto employeemain;
                                            case "3":
                                                #region EmployeeExit
                                                Clear();
                                                PrintColor($"Are you sure? yes/no", ConsoleColor.DarkRed);
                                                string lokask1 = ReadLine();
                                                if (lokask1.ToUpper() == "YES")
                                                {
                                                    PrintSlideText("Logged out\n", ConsoleColor.DarkRed);
                                                    Thread.Sleep(500);
                                                    goto Main;
                                                }
                                                else
                                                {
                                                    goto AdminStartMenu;
                                                }
                                            default:
                                                break;
                                                #endregion
                                        }
                                        if (IsSwitchCaseEmployee(empchoose))
                                        {
                                            PrintSlideText("Please choose correct menu\n", ConsoleColor.DarkRed);
                                            Thread.Sleep(500);
                                            goto employeemain;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        PrintSlideText("Wrong Password or Username\n", ConsoleColor.DarkRed);
                        Thread.Sleep(500);
                    }
                }
            }
        }
    }
}

