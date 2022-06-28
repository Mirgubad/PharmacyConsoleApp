using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using static PharmacyConsoleApp.Drug;
using static System.Console;
using Console = Colorful.Console;


namespace PharmacyConsoleApp

{
    static class Helpers
    {

        public static string EnteredVal = "";
        public static void PrintColor(string text, ConsoleColor color = ConsoleColor.DarkGray)
        {
            ForegroundColor = color;
            WriteLine(text, color);
            ResetColor();
        }
        public static void PrintColorWrite(string text, ConsoleColor color = ConsoleColor.DarkYellow)
        {
            ForegroundColor = color;
            Write(text, color);

        }
        public static void PrintSlideText(string text, ConsoleColor color = ConsoleColor.DarkGray)
        {
            for (int i = 0; i < text.Length; i++)
            {
                Thread.Sleep(50);
                ForegroundColor = color;
                Write($"{text[i]}", color);
                ResetColor();
            }
        }
        public static void AdminStartMenu()
        {
            Clear();
            PrintSlideText("You are on the admin panel\n", ConsoleColor.Yellow);
            PrintColor($"<1>Admin panel:", ConsoleColor.Green);
            PrintColor($"<2>Sell Drug:", ConsoleColor.Green);
            PrintColor($"<3>Change information:", ConsoleColor.Green);
            PrintColor($"<4>See Budget", ConsoleColor.Green);
            PrintColor($"<5>See Employees Information:", ConsoleColor.Green);
            PrintColor($"<6>Log out:", ConsoleColor.Green);
        }
        public static void AdminCaseMenu()
        {
            Clear();
            PrintSlideText("Choose an operation\n", ConsoleColor.Yellow);
            PrintColor($"<1>Add employee:", ConsoleColor.Green);
            PrintColor($"<2>Add drug:", ConsoleColor.Green);
            PrintColor($"<3>Remove drug:", ConsoleColor.Green);
            PrintColor($"<4>Edit drug:", ConsoleColor.Green);
            PrintColor($"<5>Remove employee:", ConsoleColor.Green);
            PrintColor($"<6>Edit employee:", ConsoleColor.Green);
            PrintColor($"<7>Log out:", ConsoleColor.Green);


        }
        public static bool IsValidPassword(string password)
        {
            CapsLockActive();
            Regex validateGuidRegex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{5,}$");
            return validateGuidRegex.IsMatch(password);
        }
        public static bool IsSwitchCaseAdmin(string choose)
        {
            bool switchcase = int.TryParse(choose, out int choosen);
            if (choosen <= 7 || choosen >= 1)
            {
                return true;
            }
            return false;
        }
        public static bool IsSwitchCaseEmployee(string choose)
        {
            bool switchcase = int.TryParse(choose, out int choosen);
            if (choosen < 4 || choosen > 0)
            {
                return true;
            }
            return false;
        }
        public static bool isNullOrWhiteSpace(string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }
        public static void DrugTypeMenu()
        {
            PrintColor($"Choose Drug Type", ConsoleColor.DarkYellow);
            PrintColor($"<1> Powder", ConsoleColor.DarkYellow);
            PrintColor($"<2> Syrop", ConsoleColor.DarkYellow);
            PrintColor($"<3> Tablet", ConsoleColor.DarkYellow);
            PrintColor($"<4> Exit", ConsoleColor.DarkYellow);
        }
        public static void EmployeeStartMenu()
        {
            Clear();
            PrintSlideText("You are on the stuff panel\n", ConsoleColor.Yellow);
            PrintColor($"<1>Sell Drug:", ConsoleColor.Green);
            PrintColor($"<2>Change information:", ConsoleColor.Green);
            PrintColor($"<3>Log Out:", ConsoleColor.Green);
        }
        public static void AdminChangeİnformation(Pharmacy pharmacy, string username)
        {
            Clear();
            PrintSlideText($"You are changing personal info!!!\n", ConsoleColor.DarkRed);
            PrintColor($"Would you like to continue? yes/no", ConsoleColor.DarkRed);
            string askforchange = ReadLine();
            if (askforchange.ToUpper() == "YES")
            {
            newadminname:
                PrintColor($"Name", ConsoleColor.Green);
                string newadminname = ReadLine();
                if (string.IsNullOrWhiteSpace(newadminname))
                {
                    PrintSlideText($"Please fill the name!!!\n", ConsoleColor.DarkRed);
                    goto newadminname;
                }
            newadminsurname:
                PrintColor($"SurName", ConsoleColor.Green);
                string newadminsurname = ReadLine();
                if (string.IsNullOrWhiteSpace(newadminsurname))
                {
                    PrintSlideText($"Please fill the surname!!!\n", ConsoleColor.DarkRed);
                    goto newadminsurname;
                }
            isParsablenewDateadmin:
                PrintColor($"New Birthdate write like MM/dd/yyyy:", ConsoleColor.Green);
                string newbirthdateadmin = ReadLine();
                bool isParsablenewDateadmin = DateTime.TryParseExact(newbirthdateadmin, "MM/dd/yyyy", null, 0, out DateTime newdatetimeadmin);
                if (!isParsablenewDateadmin)
                {
                    PrintSlideText($"Write birthday correct type\n", ConsoleColor.DarkRed);
                    goto isParsablenewDateadmin;
                }
            newadminusername:
                PrintColor($"UserName", ConsoleColor.Green);
                string newadminusername = ReadLine();
                if (string.IsNullOrWhiteSpace(newadminusername))
                {
                    PrintSlideText($"Please fill the username!!!\n", ConsoleColor.DarkRed);
                    goto newadminusername;
                }
                else
                {
                    foreach (var adminusername in pharmacy.employees)
                    {
                        if (adminusername.UserName == newadminusername)
                        {
                            PrintSlideText($"Username already has taken!!!\n", ConsoleColor.DarkRed);
                            goto newadminusername;
                        }
                    }
                }
            newadminpass:
                PrintColor($"Password", ConsoleColor.Green);
                string newadminpass = ReadLine();
                if (!IsValidPassword(newadminpass))
                {
                    PrintSlideText($"Password was weak!!!\n", ConsoleColor.DarkRed);
                    goto newadminpass;
                }
                foreach (var items in pharmacy.employees)
                {
                    if (items.UserName == username)
                    {
                        items.Name = newadminname;
                        items.SurName = newadminsurname;
                        items.UserName = newadminusername;
                        items.Password = newadminpass;
                        items.BirthDate = newdatetimeadmin;
                        ShowSimplePercentage();
                        PrintSlideText($"Admin information has changed!!!\n", ConsoleColor.DarkRed);
                        Thread.Sleep(500);

                    }
                }

            }
            else
            {
                AdminStartMenu();
            }
        }
        public static void AddEmploye(Pharmacy pharmacy)
        {
            Clear();
            PrintSlideText("You are adding new employee\n", ConsoleColor.Yellow);
            PrintColor($"Press/BackSpace to exit..", ConsoleColor.Cyan);
            PrintColor($"Press/Enter to continue..", ConsoleColor.Cyan);
            if (ReadKey().Key == ConsoleKey.Backspace)
            {
                return;
            }
        nameisTrue:
            PrintColor($"Name:", ConsoleColor.Green);
            string empname = ReadLine();
            if (isNullOrWhiteSpace(empname))
            {
                PrintSlideText("You can't skip the name\n", ConsoleColor.DarkRed);
                goto nameisTrue;
            }
        surnameisTrue:
            PrintColor($"SurName:", ConsoleColor.Green);
            string empsurname = ReadLine();
            if (isNullOrWhiteSpace(empsurname))
            {
                PrintSlideText("You can't skip the surname\n", ConsoleColor.DarkRed);
                goto surnameisTrue;
            }
        isParsableDate:
            PrintColor($"Birthdate write like MM/dd/yyyy:", ConsoleColor.Green);
            string birthdate = ReadLine();
            bool isParsableDate = DateTime.TryParseExact(birthdate, "MM/dd/yyyy", null, 0, out DateTime datetime);
            if (!isParsableDate)
            {
                PrintSlideText("Write birthday correct type\n", ConsoleColor.DarkRed);
                goto isParsableDate;
            }
        salary:
            PrintColor($"Salary:", ConsoleColor.Green);
            string salarystr = ReadLine();
            bool isTrueSalary = double.TryParse(salarystr, out double salary);
            if (!isTrueSalary)
            {
                PrintSlideText("Salary input was incorrect\n", ConsoleColor.DarkRed);
                goto salary;
            }
            if (pharmacy.MinSalary > salary)
            {
                PrintSlideText($"Salary isn't match,min.salary must be {pharmacy.MinSalary}\n", ConsoleColor.DarkRed);
                goto salary;
            }
        empusernameisTrue:
            PrintColor($"UserName:", ConsoleColor.Green);
            string empusername = ReadLine();
            if (pharmacy.employees.Any(x => x.UserName == empusername))
            {
                PrintSlideText("Username has already created\n", ConsoleColor.DarkRed);
                goto empusernameisTrue;
            }
            if (isNullOrWhiteSpace(empusername))
            {
                PrintSlideText("You can't skip the username\n", ConsoleColor.DarkRed);
                goto empusernameisTrue;
            }
        emppasswordisTrue:
            PrintColor($"Password:", ConsoleColor.Green);
            string emppassword = ReadLine();
            if (!IsValidPassword(emppassword))
            {
                PrintSlideText($"Password was weak:\n", ConsoleColor.DarkRed);
                goto emppasswordisTrue;
            }
            bool emppasswordisTrue = string.IsNullOrWhiteSpace(emppassword);
            if (emppasswordisTrue)
            {
                PrintSlideText("Choose a password\n", ConsoleColor.DarkRed);
                goto emppasswordisTrue;
            }
            Employee employee = new Employee();
            employee.Name = empname;
            employee.SurName = empsurname;
            employee.UserName = empusername;
            employee.Password = emppassword;
            employee.Salary = salary;
            employee.BirthDate = datetime;
            PrintColor($"Choose roletype:", ConsoleColor.Green);
            PrintColor($"<1>Admin <2>Employee", ConsoleColor.Green);
            string chooseRoleType = ReadLine();
            switch (chooseRoleType)
            {

                case "1":
                    pharmacy.employees.Add(employee);
                    employee.roleType = Employee.RoleType.Admin;
                    ShowSimplePercentage();
                    PrintSlideText("\nAdmin is added successfully to employee list\n", ConsoleColor.Green);
                    Thread.Sleep(500);
                    break;
                case "2":
                    pharmacy.employees.Add(employee);
                    employee.roleType = Employee.RoleType.Stuff;
                    ShowSimplePercentage();
                    PrintSlideText("\nStuff is added successfully to employee list\n", ConsoleColor.Green);
                    Thread.Sleep(500);
                    break;
                default:
                    break;

            }
        }
        public static void AddDrug(Pharmacy pharmacy, Boolean exit)
        {
            Clear();
            if (pharmacy.Budget <= 1000)
            {
                PrintSlideText($"Budget is low!!!\n", ConsoleColor.DarkRed);
            }
            PrintSlideText($"You are adding new drug...\n", ConsoleColor.Green);
            PrintColor($"Press/BackSpace to exit..", ConsoleColor.Cyan);
            PrintColor($"Press/Enter to continue..", ConsoleColor.Cyan);
            if (ReadKey().Key == ConsoleKey.Backspace)
            {
                return;
            }
        drnameisTrue:
            PrintColor($"Drug name:", ConsoleColor.Green);
            string drname = ReadLine();
            if (isNullOrWhiteSpace(drname))
            {
                PrintSlideText($"Please fill the name!!!\n", ConsoleColor.DarkRed);
                goto drnameisTrue;
            }
        isdrCoutnParsable:
            PrintColor($"Count:", ConsoleColor.Green);
            string drcount = ReadLine();
            bool isdrCoutnParsable = double.TryParse(drcount, out double drugcount);
            if (!isdrCoutnParsable)
            {
                PrintSlideText($"Count input was incorrect!!!\n", ConsoleColor.DarkRed);
                goto isdrCoutnParsable;
            }
        ppriceisParsable:
            PrintColor($"Purchase price:", ConsoleColor.Green);
            string pprice = ReadLine();
            bool ppriceisParsable = double.TryParse(pprice, out double purchprice);
            if (!ppriceisParsable)
            {
                PrintSlideText($"Price input was incorrect!!!\n", ConsoleColor.DarkRed);
                goto ppriceisParsable;
            }
        salepriceisParsable:
            PrintColor($"Sale price:", ConsoleColor.Green);
            string sprice = ReadLine();
            bool spriceisParsable = double.TryParse(sprice, out double saleprice);
            if (!spriceisParsable)
            {
                PrintSlideText($"Price input was incorrect!!!\n", ConsoleColor.DarkRed);
                goto salepriceisParsable;
            }
            Drug drug = new Drug()
            {
                Name = drname,
                Count = drugcount,
                PurchasePrice = purchprice,
            };
            if (saleprice <= purchprice)
            {
                PrintSlideText($"Are you sure? yes/no sale price is lower than purchase price \n", ConsoleColor.DarkRed);
                string asksure = ReadLine();
                if (asksure.ToUpper() == "YES")
                {
                    drug.SalePrice = saleprice;
                }
                else
                {
                    goto salepriceisParsable;
                }
            }
            else
            {
                drug.SalePrice = saleprice;
            }
        chooseDrugType:
            DrugTypeMenu();
            string chooseDrugType = ReadLine();
            switch (chooseDrugType)
            {
                case "1":
                    if (pharmacy.Budget == 0)
                    {
                        PrintSlideText($"There is no cash!!!\n", ConsoleColor.DarkRed);
                        Thread.Sleep(500);
                        AdminStartMenu();
                    }
                    foreach (var item1 in pharmacy.drugs)
                    {
                        if (drname == item1.Name && item1.drugType == DrugType.Powder)
                        {
                            PrintSlideText($"You can't add same name drug\n", ConsoleColor.DarkRed);
                            goto chooseDrugType;
                        }
                    }
                    if (drugcount * purchprice > pharmacy.Budget)
                    {
                        PrintSlideText($"There is no enough cash!!!\n", ConsoleColor.DarkRed);
                        PrintColor($"There is {pharmacy.Budget} currency", ConsoleColor.DarkYellow);
                        double canbuy = pharmacy.Budget / purchprice;
                        int youbuy = (int)canbuy;
                        PrintColor($"Your cash is enough for {youbuy} pieces", ConsoleColor.DarkYellow);
                        PrintColor($"Would you like to buy? yes/no", ConsoleColor.DarkYellow);
                        string wantobuy = ReadLine();
                        if (wantobuy == "yes")
                        {
                            drug.Count = youbuy;
                            pharmacy.Budget = pharmacy.Budget - youbuy * purchprice;
                            pharmacy.drugs.Add(drug);
                            drug.drugType = DrugType.Powder;
                            ShowSimplePercentage();
                            PrintSlideText($"\nDrug added Powder List...\n", ConsoleColor.Green);
                            Thread.Sleep(500);
                            AdminStartMenu();
                        }
                        else
                        {
                            AdminStartMenu();
                        }
                    }
                    else
                    {
                        pharmacy.Budget = pharmacy.Budget - drugcount * purchprice;
                        pharmacy.drugs.Add(drug);
                        drug.drugType = DrugType.Powder;
                        ShowSimplePercentage();
                        PrintSlideText($"\nDrug added Powder List...\n", ConsoleColor.Green);
                        Thread.Sleep(500);
                    }
                    break;
                case "2":
                    if (pharmacy.Budget == 0)
                    {
                        PrintSlideText($"There is no cash!!!\n", ConsoleColor.DarkRed);
                        Thread.Sleep(500);
                        AdminStartMenu();
                    }
                    foreach (var item1 in pharmacy.drugs)
                    {
                        if (drname == item1.Name && item1.drugType == DrugType.Syrop)
                        {
                            PrintSlideText($"You can't add same name drug\n", ConsoleColor.DarkRed);
                            goto chooseDrugType;
                        }
                    }
                    if (drugcount * purchprice > pharmacy.Budget)
                    {
                        PrintSlideText($"There is no enough cash!!!\n", ConsoleColor.DarkRed);
                        PrintColor($"There is {pharmacy.Budget} currency", ConsoleColor.DarkYellow);
                        double canbuy = pharmacy.Budget / purchprice;
                        int youbuy = (int)canbuy;
                        PrintColor($"Your cash is enough for {youbuy} pieces", ConsoleColor.DarkYellow);
                        PrintColor($"Would you like to buy? yes/no", ConsoleColor.DarkYellow);
                        string wantobuy = ReadLine();
                        if (wantobuy == "yes")
                        {
                            drug.Count = youbuy;
                            pharmacy.Budget = pharmacy.Budget - youbuy * purchprice;
                            pharmacy.drugs.Add(drug);
                            drug.drugType = DrugType.Syrop;
                            ShowSimplePercentage();
                            PrintSlideText($"\nDrug added Syrop List...\n", ConsoleColor.Yellow);
                            Thread.Sleep(500);
                            AdminStartMenu();
                        }
                        else
                        {
                            AdminStartMenu();
                        }
                    }
                    else
                    {
                        pharmacy.Budget = pharmacy.Budget - drugcount * purchprice;
                        pharmacy.drugs.Add(drug);
                        drug.drugType = DrugType.Syrop;
                        ShowSimplePercentage();
                        PrintSlideText($"\nDrug added Syrop List...\n", ConsoleColor.Green);
                        Thread.Sleep(500);
                    }
                    break;
                case "3":
                    if (pharmacy.Budget == 0)
                    {
                        PrintSlideText($"There is no cash!!!\n", ConsoleColor.DarkRed);
                        Thread.Sleep(500);
                        AdminStartMenu();
                    }
                    foreach (var item1 in pharmacy.drugs)
                    {
                        if (drname == item1.Name && item1.drugType == DrugType.Tablet)
                        {
                            PrintSlideText($"You can't add same name drug!!!\n", ConsoleColor.DarkRed);
                            goto chooseDrugType;
                        }
                    }
                    if (drugcount * purchprice > pharmacy.Budget)
                    {
                        PrintSlideText($"There is no enough cash!!!\n", ConsoleColor.DarkRed);
                        PrintColor($"There is {pharmacy.Budget} currency!!!", ConsoleColor.DarkYellow);
                        double canbuy = pharmacy.Budget / purchprice;
                        int youbuy = (int)canbuy;
                    youbuy:
                        PrintColor($"Your cash is enough for {youbuy} pieces", ConsoleColor.DarkYellow);
                        PrintColor($"Would you like to buy? yes/no", ConsoleColor.DarkYellow);
                        string wantobuy = ReadLine();
                        if (isNullOrWhiteSpace(wantobuy))
                        {
                            PrintSlideText($"Incorrect choose!!!\n", ConsoleColor.Yellow);
                            goto youbuy;
                        }
                        if (wantobuy == "yes")
                        {
                            drug.Count = youbuy;
                            pharmacy.Budget = pharmacy.Budget - youbuy * purchprice;
                            pharmacy.drugs.Add(drug);
                            drug.drugType = DrugType.Tablet;
                            ShowSimplePercentage();
                            PrintSlideText($"\nDrug added Tablet List...\n", ConsoleColor.Yellow);
                            Thread.Sleep(500);
                            AdminStartMenu();
                        }
                        else
                        {
                            AdminStartMenu();
                        }
                    }
                    else
                    {
                        pharmacy.Budget = pharmacy.Budget - drugcount * purchprice;
                        pharmacy.drugs.Add(drug);
                        drug.drugType = DrugType.Tablet;
                        ShowSimplePercentage();
                        PrintSlideText($"\nDrug added Tablet List...\n", ConsoleColor.Green);
                        Thread.Sleep(500);
                    }

                    break;
                case "4":
                    exit = false;
                    break;
                default:
                    break;
            }


        }
        public static void RemoveDrug(Pharmacy pharmacy)
        {
            Clear();
            if (pharmacy.drugs.Count == 0)
            {
                PrintSlideText($"Drug List is empty\n", ConsoleColor.DarkRed);
                Thread.Sleep(500);
                return;
            }

            PrintSlideText($"You Are removing drug\n ", ConsoleColor.DarkRed);
            PrintColor($"Press/BackSpace to exit..", ConsoleColor.Cyan);
            PrintColor($"Press/Enter to continue..", ConsoleColor.Cyan);
            if (ReadKey().Key == ConsoleKey.Backspace)
            {
                return;
            }
            PrintColor($"Search drug", ConsoleColor.DarkCyan);
            string removedrug = ReadLine();
            List<Drug> result = pharmacy.drugs.FindAll(d => d.Name.ToUpper().Contains(removedrug.ToUpper()));
            ShowSimplePercentage();
            PrintColor($"\nSearch results", ConsoleColor.DarkCyan);
            if (result.Count == 0)
            {
                PrintColor($"\nDrug can't found...", ConsoleColor.DarkCyan);
                return;
            }
            foreach (var search in result)
            {

                PrintColor($"<<Name is {search.Name}>><<ID is {search.ID}>><<Count is{search.Count}>><<{search.drugType}>>", ConsoleColor.DarkCyan);
            }
        isParsableRemdrugId:
            PrintColor($"Remove Drug With ID ", ConsoleColor.DarkCyan);
            string removedrugId = ReadLine();
            bool isParsableRemdrugId = int.TryParse(removedrugId, out int remdrId);
            if (!isParsableRemdrugId)
            {
                PrintSlideText($"ID was incorrect format\n", ConsoleColor.DarkRed);
                goto isParsableRemdrugId;
            }
            foreach (var remId in pharmacy.drugs)
            {
                if (remdrId == remId.ID)
                {
                yousure:
                    PrintColor($"Are you Sure? yes/no", ConsoleColor.DarkCyan);
                    string yousure = ReadLine();
                    if (isNullOrWhiteSpace(yousure))
                    {
                        PrintSlideText($"Incorrect choose!!!\n", ConsoleColor.DarkRed);
                        goto yousure;
                    }
                    if (yousure == "yes")
                    {
                        pharmacy.drugs.Remove(remId);
                        ShowSimplePercentage();
                        PrintSlideText($"\n{remId.Name} drug was deleted\n", ConsoleColor.DarkRed);
                        pharmacy.Budget = remId.PurchasePrice * remId.Count + pharmacy.Budget;
                        Thread.Sleep(500);
                        break;
                    }
                    else
                    {
                        PrintSlideText($"Process cancelled\n", ConsoleColor.DarkRed);
                        AdminStartMenu();
                    }
                }
            }
        }
        public static void EditDrug(Pharmacy pharmacy)
        {

            Clear();
            if (pharmacy.drugs.Count == 0)
            {
                PrintSlideText($"Drug List is empty\n", ConsoleColor.DarkRed);
                Thread.Sleep(500);
                return;
            }

            PrintSlideText("You are editing drug..\n", ConsoleColor.DarkRed);
            PrintColor($"Press/BackSpace to exit..", ConsoleColor.Cyan);
            PrintColor($"Press/Enter to continue..", ConsoleColor.Cyan);
            if (ReadKey().Key == ConsoleKey.Backspace)
            {
                return;
            }
        editdrug:
            PrintColor($"Search Drug ", ConsoleColor.DarkCyan);
            string editdrug = ReadLine();
            List<Drug> editresult = pharmacy.drugs.FindAll(d => d.Name.ToUpper().Contains(editdrug.ToUpper()));
            ShowSimplePercentage();
            if (editresult.Count == 0)
            {
                PrintColor($"\nDrug can't found", ConsoleColor.DarkCyan);
                return;
            }
            PrintColor("\nSearch Results", ConsoleColor.DarkCyan);
            foreach (var editing in editresult)
            {

                PrintColor($"<<Name is {editing.Name}>><<ID is {editing.ID}>><<Count is{editing.Count}>><<{editing.drugType}>>", ConsoleColor.DarkCyan);
            }
        isParsableEditdrugId:
            PrintColor($"Edit Drug With ID ", ConsoleColor.DarkCyan);
            string editIdDrug = ReadLine();
            bool isParsableEditdrugId = int.TryParse(editIdDrug, out int editdrugId);
            if (!isParsableEditdrugId)
            {
                PrintSlideText($"ID was incorrect format\n", ConsoleColor.DarkRed);
                goto isParsableEditdrugId;
            }
            foreach (var editId in pharmacy.drugs)
            {
                if (editdrugId == editId.ID)
                {
                    PrintColor($"Are you Sure? yes/no", ConsoleColor.DarkCyan);
                    string yousure = ReadLine();
                    if (yousure == "yes")
                    {
                    drnameisTrue:
                        PrintColor($"Drug's new name", ConsoleColor.DarkCyan);
                        string drnewname = ReadLine();
                        bool drnewnameisTrue = string.IsNullOrWhiteSpace(drnewname);
                        if (drnewnameisTrue)
                        {
                            PrintSlideText($"Please fill the name:\n", ConsoleColor.DarkRed);
                            goto drnameisTrue;
                        }
                        PrintColor($"Drug's new Purchaseprice", ConsoleColor.DarkCyan);
                        string drnewpurchaseprice = ReadLine();
                    newppriceisParsable:
                        bool newppriceisParsable = double.TryParse(drnewpurchaseprice, out double newpurchprice);
                        if (!newppriceisParsable)
                        {
                            PrintSlideText($"Price input was incorrect:\n", ConsoleColor.DarkRed);
                            goto newppriceisParsable;
                        }
                        PrintColor($"Drug's new Saleprice", ConsoleColor.DarkCyan);
                        string drnewSaleprice = ReadLine();
                    newsalepriceisParsable:
                        bool newspriceisParsable = double.TryParse(drnewSaleprice, out double newsaleprice);
                        if (!newspriceisParsable)
                        {
                            PrintSlideText($"Price input was incorrect:\n", ConsoleColor.DarkRed);
                            goto newsalepriceisParsable;
                        }
                        PrintColor($"Drug's new Count", ConsoleColor.DarkCyan);
                    isdrnewCoutnParsable:
                        string drnewcount = ReadLine();
                        bool isdrnewCoutnParsable = int.TryParse(drnewcount, out int newdrugcount);
                        if (!isdrnewCoutnParsable)
                        {
                            PrintSlideText($"Count input was incorrect:\n", ConsoleColor.DarkRed);
                            goto isdrnewCoutnParsable;
                        }
                        double oldbudget = pharmacy.Budget + editId.Count * editId.PurchasePrice;
                        editId.Name = drnewname;
                        editId.Count = newdrugcount;
                        editId.PurchasePrice = newpurchprice;
                        editId.SalePrice = newsaleprice;
                        oldbudget -= newdrugcount * newpurchprice;
                        pharmacy.Budget = oldbudget;
                        PrintColor($"Choose new roletype", ConsoleColor.DarkCyan);
                        PrintColor($"<1>Powder<2>Syrop<3>Tablet", ConsoleColor.DarkCyan);
                        string newtype = ReadLine();
                        if (newtype == "1")
                        {
                            editId.drugType = DrugType.Powder;
                        }
                        else if (newtype == "2")
                        {
                            editId.drugType = DrugType.Syrop;
                        }
                        else if (newtype == "3")
                        {
                            editId.drugType = DrugType.Tablet;
                        }
                        ShowSimplePercentage();
                        PrintSlideText($"\n{editId.Name} drug was edited\n", ConsoleColor.DarkRed);
                        Thread.Sleep(500);
                    }
                    else
                    {
                        PrintSlideText($"Process cancelled\n", ConsoleColor.DarkRed);
                        goto editdrug;
                    }
                }
            }
        }
        public static void RemoveEmployee(Pharmacy pharmacy)
        {
            Clear();
            if (pharmacy.employees.Count == 1)
            {
                PrintSlideText($"Employee List is empty\n", ConsoleColor.DarkRed);
                Thread.Sleep(500);
                return;
            }
            PrintSlideText($"You are removing employee!!!\n", ConsoleColor.DarkRed);
            PrintColor($"Press/BackSpace to exit..", ConsoleColor.Cyan);
            PrintColor($"Press/Enter to continue..", ConsoleColor.Cyan);
            if (ReadKey().Key == ConsoleKey.Backspace)
            {
                return;
            }
        removeemp:
            PrintColor($"Search input for Employee ", ConsoleColor.DarkCyan);
            string removename = ReadLine();
            List<Employee> removeemp = pharmacy.employees.FindAll(n => n.Name.ToUpper().Contains(removename.ToUpper()));
            ShowSimplePercentage();
            if (removeemp.Count == 0)
            {
                PrintColor($"\nEmployee can't found", ConsoleColor.DarkCyan);
                return;
            }
            PrintColor($"\nSearch results ", ConsoleColor.DarkCyan);
            foreach (var removeempitem in removeemp)
            {

                PrintColor($"<<Name is {removeempitem.Name}>><<ID is {removeempitem.ID}>><<{removeempitem.roleType}>>", ConsoleColor.DarkCyan);
            }
        isParsableremId:
            PrintColor($"Remove employee With ID ", ConsoleColor.DarkCyan);
            string remempId = ReadLine();
            bool isParsableremId = int.TryParse(remempId, out int removedmpId);
            if (!isParsableremId)
            {
                PrintSlideText($"ID was incorrect format\n", ConsoleColor.DarkRed);
                goto isParsableremId;
            }
            foreach (var removedempitem in removeemp)
            {
                if (removedmpId == removedempitem.ID)
                {
                    PrintColor($"Are you Sure? yes/no", ConsoleColor.DarkCyan);
                    string areyousure = ReadLine();
                    if (areyousure == "yes")
                    {
                        pharmacy.employees.Remove(removedempitem);
                        ShowSimplePercentage();
                        PrintSlideText($"\nEmployee is deleted\n", ConsoleColor.DarkRed);
                        Thread.Sleep(500);
                        AdminCaseMenu();
                    }
                    else
                    {
                        PrintSlideText($"Process cancelled\n", ConsoleColor.DarkRed);
                        goto removeemp;
                    }
                }
            }
        }
        public static void EditEmployee(Pharmacy pharmacy)
        {
            Clear();
            if (pharmacy.employees.Count == 1)
            {
                PrintSlideText($"Employee List is empty\n", ConsoleColor.DarkRed);
                Thread.Sleep(500);
                return;
            }
            PrintSlideText($"You are editing employee\n", ConsoleColor.DarkRed);
            PrintColor($"Press/BackSpace to exit..", ConsoleColor.Cyan);
            PrintColor($"Press/Enter to continue..", ConsoleColor.Cyan);
            if (ReadKey().Key == ConsoleKey.Backspace)
            {
                return;
            }
        editemp:
            PrintColor($"Edit Employee ", ConsoleColor.DarkCyan);
            string searchname = ReadLine();
            List<Employee> resultemp = pharmacy.employees.FindAll(n => n.Name.ToUpper().Contains(searchname.ToUpper()));
            ShowSimplePercentage();
            if (resultemp.Count == 0)
            {
                PrintColor($"\nEmployee can't found", ConsoleColor.DarkCyan);
                return;
            }
            PrintColor($"\nSearch results ", ConsoleColor.DarkCyan);
            foreach (var searchemp in resultemp)
            {

                PrintColor($"<<Name is {searchemp.Name}>><<ID is {searchemp.ID}>><<{searchemp.roleType}>>", ConsoleColor.DarkCyan);
            }
        isParsableempeditId:
            PrintColor($"Edit employee With ID ", ConsoleColor.DarkCyan);
            string editempId = ReadLine();
            bool isParsableeditId = int.TryParse(editempId, out int editedmpId);
            if (!isParsableeditId)
            {
                PrintSlideText($"ID was incorrect format\n", ConsoleColor.DarkRed);
                goto isParsableempeditId;
            }
            foreach (var editempitem in resultemp)
            {
                if (editedmpId == editempitem.ID)
                {
                    PrintColor($"Are you Sure? yes/no", ConsoleColor.DarkCyan);
                    string yousure = ReadLine();
                    if (yousure == "yes")
                    {
                    newname:
                        PrintColor($"New Name", ConsoleColor.DarkCyan);
                        string newempname = ReadLine();
                        if (string.IsNullOrEmpty(newempname))
                        {
                            PrintSlideText($"Fill the name\n", ConsoleColor.DarkRed);
                            goto newname;
                        }
                    newsurname:
                        PrintColor($"New SurName", ConsoleColor.DarkCyan);
                        string newempsurname = ReadLine();
                        if (string.IsNullOrEmpty(newempsurname))
                        {
                            PrintSlideText($"Fill the surname\n", ConsoleColor.DarkRed);
                            goto newsurname;
                        }
                    isParsablenewDate:
                        PrintColor($"New Birthdate write like MM/dd/yyyy:", ConsoleColor.Green);
                        string newbirthdate = ReadLine();
                        bool isParsablenewDate = DateTime.TryParseExact(newbirthdate, "MM/dd/yyyy", null, 0, out DateTime newdatetime);
                        if (!isParsablenewDate)
                        {
                            PrintSlideText("Write birthday correct type\n", ConsoleColor.DarkRed);
                            goto isParsablenewDate;
                        }
                    newsalary:
                        PrintColor($"Salary:", ConsoleColor.Green);
                        string newsalarystr = ReadLine();
                        bool isTruenewSalary = double.TryParse(newsalarystr, out double newsalary);
                        if (!isTruenewSalary)
                        {
                            PrintSlideText("Salary input was incorrect\n", ConsoleColor.DarkRed);
                            goto newsalary;
                        }
                        if (pharmacy.MinSalary > newsalary)
                        {
                            PrintSlideText($"Salary isn't match,min.salary must be {pharmacy.MinSalary}\n", ConsoleColor.DarkRed);
                            goto newsalary;
                        }
                    newusername:
                        PrintColor($"New UserName", ConsoleColor.DarkCyan);
                        string newempusername = ReadLine();
                        if (string.IsNullOrEmpty(newempusername))
                        {
                            PrintSlideText($"Fill the username\n", ConsoleColor.DarkRed);
                            goto newusername;
                        }
                        foreach (var usernameunique in pharmacy.employees)
                        {
                            if (usernameunique.Name == newempsurname)
                            {
                                PrintSlideText("Username has already taken\n", ConsoleColor.DarkRed);
                                PrintSlideText("Enter another username\n", ConsoleColor.DarkRed);
                                goto newusername;
                            }
                        }
                    newemppassword:
                        PrintColor($"New Password", ConsoleColor.DarkCyan);
                        string newempassword = ReadLine();
                        if (IsValidPassword(newempassword))
                        {
                            PrintSlideText($"Password was weak\n", ConsoleColor.DarkRed);
                            goto newemppassword;
                        }
                        PrintColor($"<1>Admin<2>Stuff", ConsoleColor.DarkCyan);
                        PrintColor($"New RoleType", ConsoleColor.DarkCyan);
                        string newroletype = ReadLine();
                        if (newroletype == "1")
                        {
                            editempitem.roleType = Employee.RoleType.Admin;
                        }
                        else if (newroletype == "2")
                        {
                            editempitem.roleType = Employee.RoleType.Stuff;
                        }
                        editempitem.Name = newempname;
                        editempitem.SurName = newempsurname;
                        editempitem.UserName = newempusername;
                        editempitem.Salary = newsalary;
                        editempitem.BirthDate = newdatetime;
                        editempitem.Password = newempassword;
                        ShowSimplePercentage();
                        PrintSlideText($"\n{editempitem.Name} employee information was changed\n", ConsoleColor.DarkRed);
                        Thread.Sleep(500);
                        AdminCaseMenu();
                    }
                    else
                    {
                        PrintSlideText($"Process cancelled\n", ConsoleColor.DarkRed);
                        goto editemp;
                    }
                }
            }

        }
        public static void SeeBudget(Pharmacy pharmacy)
        {
            Clear();
            ShowSimplePercentage();
            PrintColor($"\nCurrent budget:{pharmacy.Budget}", ConsoleColor.Green);
            PrintColor($"Would you like to change budget? yes/no", ConsoleColor.DarkRed);
            string changecrbudget = ReadLine();
            if (changecrbudget.ToUpper() == "YES")
            {
                PrintColor($"Are you sure? yes/no", ConsoleColor.DarkRed);
                string budgetask = ReadLine();
                if (budgetask.ToUpper() == "YES")
                {
                budgetisTrue:
                    PrintColor($"Enter new budget", ConsoleColor.Green);
                    string newbudget = ReadLine();
                    bool budgetisTrue = double.TryParse(newbudget, out double changedbudget);
                    if (!budgetisTrue)
                    {
                        PrintSlideText($"Budget type was incorrect!!!\n", ConsoleColor.DarkRed);
                        goto budgetisTrue;
                    }
                    else
                    {
                        double oldbudget = pharmacy.Budget;
                        pharmacy.Budget = changedbudget;
                        ShowSimplePercentage();
                        PrintSlideText($"\nBudget changed successfully...\n", ConsoleColor.Green);
                        PrintSlideText($"Old Budget:{oldbudget} new budget:{changedbudget}\n", ConsoleColor.DarkCyan);
                        Thread.Sleep(500);
                    }
                }
                else
                {
                    AdminStartMenu();
                }
            }

        }
        public static void ChangeEmployeeInfo(Pharmacy pharmacy, string username)
        {
            Clear();
            PrintSlideText($"You are changing personal info!!!\n", ConsoleColor.DarkRed);
            PrintColor($"Would you like to continue? yes/no", ConsoleColor.DarkRed);
            string askforchange = ReadLine();
            if (askforchange.ToUpper() == "YES")
            {
                PrintSlideText($"You are changing information\n", ConsoleColor.DarkRed);
            cempname:
                PrintColor($"New name", ConsoleColor.Green);
                string cempname = ReadLine();
                if (string.IsNullOrWhiteSpace(cempname))
                {
                    PrintSlideText($"Please fill the name!!!\n", ConsoleColor.DarkRed);
                    goto cempname;
                }
            cempsurname:
                PrintColor($"New surname", ConsoleColor.Green);
                string cempsurname = ReadLine();
                if (string.IsNullOrWhiteSpace(cempsurname))
                {
                    PrintSlideText($"Please fill the surname!!!\n", ConsoleColor.DarkRed);
                    goto cempsurname;
                }
            cbirthemp:
                PrintColor($"New Birthdate write like MM/dd/yyyy", ConsoleColor.Green);
                string newbirthemp = ReadLine();
                bool isParsablecempdate = DateTime.TryParseExact(newbirthemp, "MM/dd/yyyy", null, 0, out DateTime cbirthemp);
                if (!isParsablecempdate)
                {
                    PrintSlideText($"Write birthday correct type\n", ConsoleColor.DarkRed);
                    goto cbirthemp;
                }
            cempusername:
                PrintColor($"New employee username", ConsoleColor.Green);
                string cempusername = ReadLine();
                if (string.IsNullOrWhiteSpace(cempusername))
                {
                    PrintSlideText($"Please fill the usrname\n", ConsoleColor.DarkRed);
                    goto cempusername;
                }
            cemppass:
                PrintColor($"New employee password", ConsoleColor.Green);
                string cemppass = ReadLine();
                if (!IsValidPassword(cemppass))
                {
                    PrintSlideText($"Password was weak!!!\n", ConsoleColor.DarkRed);
                    goto cemppass;
                }
                foreach (var cemp in pharmacy.employees)
                {
                    if (cemp.UserName == username)
                    {
                        cemp.Name = cempname;
                        cemp.SurName = cempsurname;
                        cemp.UserName = cempusername;
                        cemp.Password = cemppass;
                        cemp.BirthDate = cbirthemp;
                        ShowSimplePercentage();
                        PrintSlideText($"\nEmployee information changed successfully\n", ConsoleColor.DarkRed);
                        Thread.Sleep(500);
                    }
                }
            }
            else
            {
                EmployeeStartMenu();
            }
        }
        public static void CheckPassword(string EnterText, ConsoleColor color = ConsoleColor.DarkYellow)
        {
            CapsLockActive();
            ForegroundColor = color;
            try
            {
                Write(EnterText);
                EnteredVal = "";
                do
                {

                    ConsoleKeyInfo key = ReadKey(true);
                    // Backspace Should Not Work  
                    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                    {
                        EnteredVal += key.KeyChar;
                        Write("*");
                    }
                    else
                    {
                        if (key.Key == ConsoleKey.Backspace && EnteredVal.Length > 0)
                        {
                            EnteredVal = EnteredVal.Substring(0, (EnteredVal.Length - 1));
                            Write("\b \b");
                        }
                        else if (key.Key == ConsoleKey.Enter)
                        {
                            if (string.IsNullOrWhiteSpace(EnteredVal))
                            {
                                WriteLine("");
                                PrintSlideText("Empty value not allowed!!\n", ConsoleColor.DarkRed);
                                CheckPassword(EnterText);
                                break;
                            }
                            else
                            {
                                WriteLine("");
                                break;
                            }
                        }
                    }
                } while (true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ResetColor();
        }
        public static void ShowSimplePercentage(ConsoleColor color = ConsoleColor.DarkGreen)
        {
            ForegroundColor = color;
            for (int i = 0; i <= 100; i++)
            {

                Write($"\rProcessing: {i}%  ", color);

                Thread.Sleep(25);
            }

            ResetColor();
        }
        public static void SeeEmployeeList(Pharmacy pharmacy)
        {
            Clear();
            ShowSimplePercentage();

            foreach (var item in pharmacy.employees)
            {
                PrintColor($"\n<Name:{item.Name}> <Surname:{item.SurName}> <Role Type:{item.roleType}><Birthday:{item.BirthDate}> <Salary:{item.Salary}>", ConsoleColor.Blue);

            }
            ReadKey();
        }
        public static void AsciiText()
        {
            Console.WriteAscii("          Pharmacy ", System.Drawing.Color.FromArgb(255, 58, 50));
        }
        public static void ConsoleDraw(IEnumerable<string> lines, int x, int y)
        {
            if (x > Console.WindowWidth) return;
            if (y > Console.WindowHeight) return;

            var trimLeft = x < 0 ? -x : 0;
            int index = y;

            x = x < 0 ? 0 : x;
            y = y < 0 ? 0 : y;

            var linesToPrint =
                from line in lines
                let currentIndex = index++
                where currentIndex > 0 && currentIndex < Console.WindowHeight
                select new
                {
                    Text = new String(line.Skip(trimLeft).Take(Math.Min(Console.WindowWidth - x, line.Length - trimLeft)).ToArray()),
                    X = x,
                    Y = y++
                };

            Console.Clear();
            foreach (var line in linesToPrint)
            {
                Console.SetCursorPosition(line.X, line.Y);
                Console.Write(line.Text);
            }
        }
        public static void Start()
        {
            ForegroundColor = ConsoleColor.Magenta;
            var arr = new[]
            {

 @"    ___                       ____             ____  __               __    _",
 @"   / _ |   ___    ___        /  _/  ___       / __/ / /_ ___ _  ____ / /_  (_)  ___   ___ _",
 @"  / __ |  / _ \  / _ \      _/ /   (_-<      _\ \  / __// _ `/ / __// __/ / /  / _ \ / _ `/",
 @" /_/ |_| / .__/ / .__/     /___/  /___/     /___/  \__/ \_,_/ /_/   \__/ /_/  /_//_/ \_, /",
 @"        /_/    /_/                                                                  /___/"    ,

            };
            Console.WindowWidth = 150;
            Console.WriteLine("\n\n");
            var maxLength = arr.Aggregate(0, (max, line) => Math.Max(max, line.Length));
            var x = Console.BufferWidth / 2 - maxLength / 2;
            for (int y = -arr.Length; y < Console.WindowHeight + arr.Length; y++)
            {
                ConsoleDraw(arr, x, y);
                Thread.Sleep(100);

            }
            ResetColor();
        }
        public static void CapsLockActive()
        {
            if (Console.CapsLock == true)
            {
                PrintColor("CAPS LOCK IS on", ConsoleColor.DarkRed);
            }
        }
    }
}
