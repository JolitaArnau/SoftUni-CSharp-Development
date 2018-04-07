using Twitter.Contracts;
using Twitter.IO;
using Twitter.Models;
using Twitter.Repositories;

namespace Twitter
{
    public class StartUp
    {
        public static void Main()
        {
            IWriter writer = new ConsoleWriter();
            ITweetRepository tweetRepo = new TweetRepository();

            var client = new Client(writer, tweetRepo);
            var tweet = new Tweet(client);

            tweet.ReceiveMessage("This is a tweet");
            tweet.ReceiveMessage("Yet another tweet");
            tweet.ReceiveMessage("More dummy messages");
            tweet.ReceiveMessage("Dummy message 4");
            tweet.ReceiveMessage("Last dummy message");
        }
    }
}