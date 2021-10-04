using System;

namespace stateExample
{
    public class NoCard : State
    {
        public NoCard(ITeller atm) : base(atm)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            _teller.Message.SetMessage("Insert Card");
        }

        public override void InsertCard()
        {
            _teller.SetState(new HasCard(_teller));
        }

        public override void EjectCard()
        {
            _teller.Message.SetMessage("There is no card");
        }

        public override void InsertPin()
        {
            _teller.Message.SetMessage("Insert card first");
        }

        public override void RequestCash()
        {
            _teller.Message.SetMessage("Insert card first");
        }
    }
}