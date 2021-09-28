namespace stateExample
{
   
    public abstract class State
    {
        protected ITeller _teller { get; set; }
        public State(ITeller teller)
        {
            _teller = teller;
        }
        public abstract void InsertCard();
        public abstract void EjectCard();
        public abstract void InsertPin();
        public abstract void RequestCash();
    }
}
