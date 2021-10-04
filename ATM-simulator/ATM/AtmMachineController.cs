namespace stateExample
{
    public class AtmMachineControl : ITeller
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
    }
}
