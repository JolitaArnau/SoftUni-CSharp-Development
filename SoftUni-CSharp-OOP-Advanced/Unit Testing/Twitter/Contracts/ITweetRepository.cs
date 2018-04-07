namespace Twitter.Contracts
{
    public interface ITweetRepository
    {
        void SaveTweet(string content);
    }
}