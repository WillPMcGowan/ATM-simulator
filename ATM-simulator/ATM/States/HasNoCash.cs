using System;
using System.Threading;

namespace stateExample
{
    public class HasNoCash : State
    {
        public HasNoCash(ITeller atm) : base(atm)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            _teller.Message.SetMessage("Sorry the ATM has no cash");
        }
        public override void EjectCard()
        {
            _teller.Message.SetMessage("Ejecting card...");
            Thread.Sleep(3000);
            _teller.SetState(new NoCard(_teller));
        }

        public override void InsertCard()
        {
            _teller.Message.SetMessage("Card already inserted");
        }

        public override void InsertPin()
        {
            _teller.Message.SetMessage("Pin already correct");
        }

        public override void RequestCash()
        {
            _teller.Message.SetMessage("Sorry the ATM has no cash");
        }
    }
}