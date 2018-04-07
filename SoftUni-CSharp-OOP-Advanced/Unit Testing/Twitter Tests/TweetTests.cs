using Moq;
using NUnit.Framework;
using Twitter.Contracts;
using Twitter.Models;

namespace TwitterTests
{
    [TestFixture]
    public class TweetTests
    {
        private const string TweetMessage = "This is just a tweet message test.";
        
        [Test]
        public void ReceiveMessageShouldCallClientToWriteMessage()
        {
            var client = new Mock<IClient>();
            client.Setup(c => c.WriteTweet(It.IsAny<string>()));
            var tweet = new Tweet(client.Object);
            
            tweet.ReceiveMessage(TweetMessage);
            
            client.Verify(c => c.WriteTweet(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void ReceiveMessageShouldCallClientToSendMessageToServer()
        {
            var client = new Mock<IClient>();
            client.Setup(c => c.SendTweetToServer(It.IsAny<string>()));
            var tweet = new Tweet(client.Object);
            
            tweet.ReceiveMessage(TweetMessage);
            
            client.Verify(c => c.SendTweetToServer(It.IsAny<string>()), Times.Once);
        }
    }
}