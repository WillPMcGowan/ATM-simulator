using System;
using System.Threading;

namespace stateExample
{
    public class HasCorrectPin : State
    {
        public HasCorrectPin(ITeller atm) : base(atm)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            _teller.Message.SetMessage("Pin Correct");
        }
        public override void EjectCard()
        {
            _teller.Message.SetMessage("Card ejecting...");
            Thread.Sleep(5000);
            _teller.SetState(new NoCard(_teller));
        }

        public override void InsertCard()
        {
            _teller.Message.SetMessage("There is already a card");
        }

        public override void InsertPin()
        {
            _teller.Message.SetMessage("Pin already correct");
        }
        public override void RequestCash()
        {
            _teller.Message.SetMessage("Enter cash amount");
            Console.SetCursorPosition(1, 3);
            int cash = Convert.ToInt32(Console.ReadLine());
            if (cash < _teller.CashInATM)
            {
                _teller.Message.SetMessage("Here is your cash");
                _teller.CashInATM = -cash;
                Thread.Sleep(3000);
                _teller.SetState(new NoCard(_teller));
            }
            else
            {
                _teller.SetState(new HasNoCash(_teller));
            }

        }
    }
}