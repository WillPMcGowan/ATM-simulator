using System;
using System.Threading;

namespace stateExample
{
    partial class AtmMachineControl : ITeller
    {
        private State _atmState;
        private int _pin = 1234;
        public IMessage Message { get; set; }
        public int Pin { get => _pin; set => _pin = value; }
        public int CashInATM { get => _cashInATM; set => _cashInATM = value; }

        private int _cashInATM = 2000;


        public void SetState(State atmState)
        {
            _atmState = atmState;
        }
        public void Connect(IMessage message)
        {
            Message = message;
        }
        public void EjectCard()
        {
            _atmState.EjectCard();
        }

        public void InsertCard()
        {
            _atmState.InsertCard();
        }

        public void InsertPin()
        {
            _atmState.InsertPin();
        }

        public void RequestCash()
        {
            _atmState.RequestCash();
        }

        public void Init()
        {
            _atmState = new NoCard(this);
        }

        // NO CARD STATE
        private class NoCard : State
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


        //HAS CARD
        private class HasCard : State
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
        // HAS CORRECT PIN
        private class HasCorrectPin : State
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
                    _teller.CashInATM =- cash;
                    Thread.Sleep(3000);
                    _teller.SetState(new NoCard(_teller));
                }
                else
                {
                    _teller.SetState(new HasNoCash(_teller));
                }

            }
        }
        // HAS WRONG PIN
        private class HasWrongPin : State
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
        // HAS NO CASH
        private class HasNoCash : State
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
}
