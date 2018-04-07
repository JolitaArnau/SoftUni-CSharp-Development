namespace Twitter.Repositories
{
    using System;
    using System.IO;
    using Contracts;
    
    public class TweetRepository : ITweetRepository
    {
        private const string ServerFileName = "server.txt";
        private const string ContentSeparator = "========================";
        private readonly string filePath = Path.Combine(Environment.CurrentDirectory, ServerFileName);
        
        
        public void SaveTweet(string content)
        {
            File.AppendAllText(this.filePath, $"{content}{Environment.NewLine}{ContentSeparator}{Environment.NewLine}");
        }
    }
}