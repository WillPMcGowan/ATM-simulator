namespace stateExample
{
    public interface IMessage
    {
        void Init();
        void Connect(ITeller controller);
        void SetMessage(string s);
    }
}