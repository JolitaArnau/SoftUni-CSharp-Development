namespace Twitter.Contracts
{
    public interface ITweet
    {
        void ReceiveMessage(string message);
    }
}