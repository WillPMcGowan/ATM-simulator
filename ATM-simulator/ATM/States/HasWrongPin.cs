using System;
using System.Threading;

namespace stateExample
{
    public class HasWrongPin : State
    {
        public HasWrongPin(ITeller atm) : base(atm)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            _teller.Message.SetMessage("Wrong PIN");
        }
        public override void EjectCard()
        {
            _teller.Message.SetMessage("Card Ejecting...");
            Thread.Sleep(3000);
            _teller.SetState(new NoCard(_teller));
        }

        public override void InsertCard()
        {
            _teller.Message.SetMessage("There is already a card");
        }
        public override void InsertPin()
        { }

        public override void RequestCash()
        {
            _teller.Message.SetMessage("Pin already wrong");
        }
    }
}