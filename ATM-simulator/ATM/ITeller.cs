namespace stateExample
{
    public interface ITeller
    {
        IMessage Message { get; set; }
        public int CashInATM { get; set; }
        int Pin { get; set; }
        void SetState(State atmState);
        void Connect(IMessage message);
        void InsertCard();
        void EjectCard();
        void InsertPin();
        void RequestCash();
        void Init();
    }
}
