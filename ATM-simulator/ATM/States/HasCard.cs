using System;
using System.Threading;

namespace stateExample
{
    public class HasCard : State
    {
        public HasCard(ITeller atm) : base(atm)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            _teller.Message.SetMessage("Card Inserted");
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
        {
            _teller.Message.SetMessage("Enter PIN");
            Console.SetCursorPosition(1, 3);
            int pin = Convert.ToInt32(Console.ReadLine());

            if (pin == _teller.Pin)
            {
                _teller.SetState(new HasCorrectPin(_teller));
                return;
            }

            _teller.SetState(new HasWrongPin(_teller));
        }

        public override void RequestCash()
        {
            _teller.Message.SetMessage("Enter PIN first");
        }
    }
}