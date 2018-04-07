using Moq;
using NUnit.Framework;
using Twitter.Contracts;
using Twitter.Models;

namespace TwitterTests
{
    public class ClientTests
    {
        private const string TweetMessage = "This is just a tweet message test.";

        [Test]
        public void WriteTweetShouldWriteAStringToTheConsole()
        {
            var writer = new Mock<IWriter>();
            writer.Setup(w => w.WriteLine(It.IsAny<string>()));
            var tweetRepo = new Mock<ITweetRepository>();
            var client = new Client(writer.Object, tweetRepo.Object);

            client.WriteTweet(TweetMessage);

            writer.Verify(w => w.WriteLine(It.Is<string>(s => s.Equals(TweetMessage))));
        }

        [Test]
        public void SendTweetToServerShouldSendAStringToServer()
        {
            var writer = new Mock<IWriter>();
            var tweetRepo = new Mock<ITweetRepository>();
            tweetRepo.Setup(tr => tr.SaveTweet(It.IsAny<string>()));
            var client = new Client(writer.Object, tweetRepo.Object);

            client.SendTweetToServer(TweetMessage);

            tweetRepo.Verify(tr => tr.SaveTweet(It.Is<string>(s => s.Equals(TweetMessage))), Times.Once);
        }
    }
}