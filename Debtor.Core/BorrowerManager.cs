using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace Debtor.Core
{
    public class BorrowerManager
    {
        private List<Borrower> Borrowers { get; set; }

        private string FileName { get; set; } = "borrowers.txt";

        public BorrowerManager()
        {
            Borrowers = new List<Borrower>();

            if (!File.Exists(FileName))
            {
                return;
            }

            var fileLines = File.ReadAllLines(FileName);

            foreach (var line in fileLines)
            {
                var lineItems = line.Split(';');

                if (decimal.TryParse(lineItems[1], out var amountInDecimal))
                {
                    AddBorrower(lineItems[0], amountInDecimal, false);
                }
            }
        }

        public void AddBorrower(string name, decimal amount, bool shouldSaveToFile = true)
        {
            var borrower = new Borrower
            {
                Name = name,
                Amount = amount,
                Interest = DateTime.Now
            };

            Borrowers.Add(borrower);

            if (shouldSaveToFile)
            {
                File.AppendAllLines(FileName, new List<string> { borrower.ToString() });
            }
        }

        public void DeleteBorrower(string name, bool shouldSaveToFile = true)
        {
            foreach (var borrower in Borrowers)
            {
                if (borrower.Name == name)
                {
                    Console.WriteLine("Podaj kwotę, którą Ci zwrócono:");

                    var userInputAmount = Console.ReadLine();

                    if (decimal.TryParse(userInputAmount, out var userInputAmountDecimal))
                    {
                        if (userInputAmountDecimal < borrower.Amount)
                        {
                            var rest = borrower.Amount - userInputAmountDecimal;
                            borrower.Amount = borrower.Amount - userInputAmountDecimal;

                            Console.WriteLine("Usunięto " + userInputAmountDecimal + " zł długu, pozostało " + rest + " zł");
                        }
                        else if (userInputAmountDecimal == borrower.Amount)
                        {
                            Borrowers.Remove(borrower);
                            Console.WriteLine("Udało się usunąć dłużnika");
                            break;
                        }
                        else
                        {
                            var restForBorrower = borrower.Amount - userInputAmountDecimal;
                            Borrowers.Remove(borrower);
                            Console.WriteLine("Udało się usunąć dłużnika");
                            Console.WriteLine($"Zwróć dłużnikowi {Math.Abs(restForBorrower)} zł reszty");
                            break;
                        }
                    }
                }
            }

            if (shouldSaveToFile)
            {
                var borrowersToSave = new List<string>();

                foreach (var borrower in Borrowers)
                {
                    borrowersToSave.Add(borrower.ToString());
                }

                File.Delete(FileName);
                File.WriteAllLines(FileName, borrowersToSave);
            }
        }

        public List<string> ListBorrowers()
        {
            var borrowersStrings = new List<string>();
            var indexer = 1;

            foreach (var borrower in Borrowers)
            {
                var borrowerString = indexer + ". " + borrower.Name + " - " + borrower.Amount + " zł" + " " + borrower.Interest + " " + "Kwota do oddania za miesiąc to: " + Convert.ToDouble(borrower.Amount) * 1.03;
                indexer++;

                borrowersStrings.Add(borrowerString);
            }
            return borrowersStrings;
        }

        public void SumBorrowers()
        {
            var sum = Borrowers.Sum(borrowers => borrowers.Amount);
            Console.WriteLine(sum + " zł");
        }
    }
}
