using Debtor.Core;
using System;

namespace Debtor
{
    public class DebtorApp
    {
        public BorrowerManager BorrowerManager { get; set; } = new BorrowerManager();

        public void IntroduceDebtorApp()
        {
            Console.WriteLine("Witam w aplikacji dłużnik. Tutaj zapisujemy listę dłużników");
        }

        public void AddBorrower()
        {
            Console.WriteLine("Podaj nazwę dłużnika, którego chcesz dodać do listy");

            var userName = Console.ReadLine();

            Console.WriteLine("Podaj kwotę długu");

            var userAmount = Console.ReadLine();

            var amountInDecimal = default(decimal);

            while (!decimal.TryParse(userAmount, out amountInDecimal))
            {
                Console.WriteLine("Podano niepoprawną kwotę");

                Console.WriteLine("Podaj kwotę długu");

                userAmount = Console.ReadLine();
            }

            BorrowerManager.AddBorrower(userName, amountInDecimal);
        }

        public void DeleteBorrower()
        {
            Console.WriteLine("Podaj nazwę dłużnika, którego chcesz usunąć z listy");

            var userName = Console.ReadLine();

            BorrowerManager.DeleteBorrower(userName);
        }

        public void ListAllBorrowers()
        {
            Console.WriteLine("Oto lista Twoich dłużników:");

            foreach (var borrower in BorrowerManager.ListBorrowers())
            {
                Console.WriteLine(borrower);
            }

            if (BorrowerManager.ListBorrowers().Count  == 0)
            {
                Console.WriteLine("Lista jest pusta");
            }
        }

        public void SumOfBorrowers()
        {
            Console.WriteLine("Suma kwot twoich dłużników wynosi:");
            BorrowerManager.SumBorrowers();
        }

        public void AskForAction()
        {
            Console.WriteLine("Podaj czynność, którą chcesz wykonać:");

            var userInput = default(string);

            while (userInput != "exit")
            {
                Console.WriteLine("");
                Console.WriteLine("add - Dodawanie dłużnika");
                Console.WriteLine("del - Usuwanie dłużnika");
                Console.WriteLine("list - Wypisywanie listy dłużników");
                Console.WriteLine("sum - Suma długów");
                Console.WriteLine("exit - Wyjście z programu");
                Console.WriteLine("");

                userInput = Console.ReadLine();
                userInput = userInput.ToLower();

                switch (userInput)
                {
                    case "add":
                        AddBorrower();
                        break;
                    case "del":
                        DeleteBorrower();
                        break;
                    case "list":
                        ListAllBorrowers();
                        break;
                    case "sum":
                        SumOfBorrowers();
                        break;
                    case "exit":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Podano złą wartość");
                        break;
                }
            }
            
        }
    }
}
