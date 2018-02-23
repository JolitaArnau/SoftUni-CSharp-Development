public class InvalidSongLengthException : InvalidSongException
{
    private const string Message = "Invalid song length.";

    public InvalidSongLengthException(string message) : base(message)
    {
    }

    public InvalidSongLengthException() : base(Message)
    {
    }
}