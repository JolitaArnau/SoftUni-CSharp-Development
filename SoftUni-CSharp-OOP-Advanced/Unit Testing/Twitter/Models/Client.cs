namespace Twitter.Models
{
    using Contracts;
    
    public class Client : IClient
    {
        private IWriter writer;
        private ITweetRepository tweetRepository;

        public Client(IWriter writer, ITweetRepository tweetRepository)
        {
            this.writer = writer;
            this.tweetRepository = tweetRepository;
        }
        
        public void WriteTweet(string message)
        {
            this.writer.WriteLine(message);
        }

        public void SendTweetToServer(string message)
        {
            this.tweetRepository.SaveTweet(message);
        }
    }
}