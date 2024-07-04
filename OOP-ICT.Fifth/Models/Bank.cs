using OOP_ICT.Fifth.Exeptions;
using OOP_ICT.Fifth.Services;

namespace OOP_ICT.Fifth.Models
{
    public class Bank
    {
        private int totalChips;
        private TextGameUI gameUI;

        public int TotalChips => totalChips;

        public Bank()
        {
            totalChips = 0;
            gameUI = new TextGameUI();
        }

        public void AddToBank(int chips)
        {
            if (chips < 0)
            {
                throw new BankException("Cannot add negative chips to the bank.");
            }

            totalChips += chips;
            gameUI.DisplayMessage($"[yellow]{chips}[/] chips added to the bank. Total chips in the bank: [yellow]{totalChips}[/]");
        }

        public void ClearBank()
        {
            gameUI.DisplayMessage($"Clearing the bank. Total chips in the bank: [yellow]{totalChips}[/]");
            totalChips = 0;
        }

        public int GetTotalChips()
        {
            return totalChips;
        }
    }
}
