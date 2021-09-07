using System;
using System.IO;

namespace BankManagement
{
    class LogIn
    {
        string username;
        string password;
        public void UserInterface()
        {
            //Clear login screen
            Console.Clear();
            //Render UI
            Console.WriteLine("\t\t ================================");
            Console.WriteLine("\t\t|     Welcome to My Bank        |");
            Console.WriteLine("\t\t ================================");
            Console.WriteLine("\t\t|     Login to start            |");
            Console.WriteLine("\t\t| \t\t\t        |");
            Console.Write("\t\t| Username: ");
            int loginCursorX = Console.CursorTop;
            int LoginCursorY = Console.CursorLeft;
            Console.WriteLine("\t\t\t|");
            Console.Write("\t\t| Password: ");
            int passwordCursorX = Console.CursorTop;
            int passwordCursorY = Console.CursorLeft;
            Console.WriteLine("\t\t\t|");
            Console.Write("\t\t| ");
            int errorCursorX = Console.CursorTop;
            int errorCursorY = Console.CursorLeft;
            Console.WriteLine("\t\t\t        |");
            Console.WriteLine("\t\t ===============================");
            Console.SetCursorPosition(LoginCursorY, loginCursorX);
            username = Console.ReadLine();
            Console.SetCursorPosition(passwordCursorY, passwordCursorX);
            password = Console.ReadLine();
            
            //Check username and password
            var loginFile = File.ReadAllText("login.txt");
            string[] loginDetails = loginFile.Split('|');
            //loginDetails[0] = username
            //loginDetails[1] = password
            if (loginDetails[0].Equals(username) && loginDetails[1].Equals(password))
            {
                Console.SetCursorPosition(errorCursorY, errorCursorX);
                Console.WriteLine("Valid Credentials \t\t| ");
                Console.ReadKey();
                MainMenu mainMenu = new MainMenu();
                mainMenu.UserInterface();
            }
            else
            {
                Console.SetCursorPosition(errorCursorY, errorCursorX);
                Console.WriteLine("Invalid Credentials \t\t| ");
                Console.WriteLine(password + loginDetails[1]);
                Console.ReadKey();
            }
            
        }
    }
    class MainMenu
    {
        public void UserInterface()
        {
            //Clear screen
            Console.Clear();
            //Render Main menu UI
            Console.WriteLine("\t\t ================================");
            Console.WriteLine("\t\t|     Welcome to My Bank        |");
            Console.WriteLine("\t\t ================================");
            Console.WriteLine("\t\t| \t\t\t        |");
            Console.Write("\t\t| 1. Create a new account");
            Console.WriteLine("       |");
            Console.Write("\t\t| 2. Search for an account");
            Console.WriteLine("      |");
            Console.Write("\t\t| 3. Deposit");
            Console.WriteLine("\t\t\t|");
            Console.Write("\t\t| 4. Withdraw");
            Console.WriteLine("\t\t\t|");
            Console.Write("\t\t| 5. A/C statement");
            Console.WriteLine("\t\t|");
            Console.Write("\t\t| 6. Delete account");
            Console.WriteLine("\t\t|");
            Console.Write("\t\t| 7. Exit");
            Console.WriteLine("\t\t\t|");
            Console.WriteLine("\t\t --------------------------------");
            Console.Write("\t\t| Enter your choice(1-7): ");
            int cursorX = Console.CursorTop;
            int cursorY = Console.CursorLeft;
            Console.WriteLine("      |");
            Console.SetCursorPosition(cursorY, cursorX);
            int userChoice = Int16.Parse(Console.ReadLine());
            Console.WriteLine(userChoice);
            //Navigation based on users choice
            Account account = new Account();
            switch (userChoice)
            {
                case 1:
                    account.Create();
                    break;
                case 2:
                    account.Search();
                    break;
                default:
                    UserInterface();
                    break;
            }
        }
    }
    class Account
    {
        public void Create()
        {
            //Clear screen
            Console.Clear();
            //Render create account UI
            Console.WriteLine("\t\t===================================");
            Console.WriteLine("\t\t|     Create a new account         |");
            Console.WriteLine("\t\t===================================");
            Console.Write("\t\t|    Enter the details");
            Console.WriteLine("             |");
            string[] userAccountFields = new string[5] {"First Name: ", "Last Name: ", "Address: ", "Phone: ", "Email: "};
            UserAccount newUser = new UserAccount();
            for (int i = 0; i < userAccountFields.Length; i++)
            {
                Console.Write("\t\t| {0}", userAccountFields[i]);
                int cursorX = Console.CursorTop;
                int cursorY = Console.CursorLeft;
                Console.WriteLine("\t\t\t   |");
                Console.SetCursorPosition(cursorY, cursorX);
                switch(userAccountFields[i])
                {
                    case "First Name: ":
                        newUser.FirstName = Console.ReadLine();
                        break;
                    case "Last Name: ":
                        newUser.LastName = Console.ReadLine();
                        break;
                    case "Address: ":
                        newUser.Address = Console.ReadLine();
                        break;
                    case "Phone: ":
                        newUser.Phone = Console.ReadLine();
                        break;
                    case "Email: ":
                        newUser.Email = Console.ReadLine();
                        break;
                    default:    
                        break;
                }
            }
            Console.WriteLine("\t\t===================================");
            Console.Write("\t\t|Is the information correct (y/n)?:");
            int confirmationCursorX = Console.CursorTop;
            int confirmationCursorY = Console.CursorLeft;
            Console.WriteLine("  |");
            Console.SetCursorPosition(confirmationCursorY, confirmationCursorX);
            char isInformationCorrect = Console.ReadKey().KeyChar;
            switch (isInformationCorrect)
            {
                case 'y':
                    Console.WriteLine("Account created ! Details will be provided by email");
                    Random accountNumber = new Random();
                    //Create unique 8 digit number
                    newUser.AccountNumber = accountNumber.Next(10000000, 99999999);
                    Console.WriteLine("Account number is: " + newUser.AccountNumber);
                    break;
                case 'n':
                    Create();
                    break;
                default:
                    break;
            }
            //Write account details to text file
            string[] accountDetails =
            {
                $"First Name|{newUser.FirstName}",$"Last Name |{newUser.LastName}",$"Address|{newUser.Address}",
                $"Phone|{newUser.Phone}",$"Email|{newUser.Email}",$"AccountNo|{newUser.AccountNumber}",
                $"Balance|{newUser.Balance}"
            };
            File.WriteAllLines($"{newUser.AccountNumber}.txt", accountDetails);
            Console.ReadKey();
            MainMenu mainMenu = new MainMenu();
            mainMenu.UserInterface();
        }
        public void Search()
        {
            //Clear screen
            Console.Clear();
            //Render search account UI
            Console.WriteLine("\t\t===================================");
            Console.WriteLine("\t\t|     Search an account           |");
            Console.WriteLine("\t\t===================================");
            Console.Write("\t\t|    Enter the details");
            Console.WriteLine("            |");
            Console.Write("\t\t| Account Number: ");
            int cursorX = Console.CursorTop;
            int cursorY = Console.CursorLeft;
            Console.WriteLine("\t\t  |");
            Console.SetCursorPosition(cursorY, cursorX);

            int accountNumber = Int32.Parse(Console.ReadLine());
            if (File.Exists($"{accountNumber}.txt"))
            {
                Console.Write("\t\t| Account found!");
                Console.WriteLine("                  |");
                Console.WriteLine("\t\t===================================");
                string[] accountInformation = File.ReadAllLines($"{accountNumber}.txt");
                for (int i = 0; i< accountInformation.Length; i++)
                {
                    //Get rid of delimiter and replace with :
                    accountInformation[i] = accountInformation[i].Replace("|", ":");
                    Console.WriteLine($"\t\t  {accountInformation[i]}");

                }
                Console.WriteLine("\t\t===================================");
            }
            else
            {
                Console.Write("\t\t| Account not found!");
                Console.WriteLine("              |");
            }
            Console.Write("\t\t| Check another account (y/n)?");
            int confirmationCursorX = Console.CursorTop;
            int confirmationCursorY = Console.CursorLeft;
            Console.SetCursorPosition(confirmationCursorY, confirmationCursorX);
            char yesOrNo = Console.ReadKey().KeyChar;
            switch (yesOrNo)
            {
                case 'y':
                    Search();
                    break;
                case 'n':
                    MainMenu menu = new MainMenu();
                    menu.UserInterface();
                    break;
                default:
                    break;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            LogIn logIn = new LogIn();
            //Create Login UI
            logIn.UserInterface();
        }
    }
}
