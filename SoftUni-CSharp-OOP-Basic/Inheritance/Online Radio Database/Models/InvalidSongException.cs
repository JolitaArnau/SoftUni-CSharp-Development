using System;

public class InvalidSongException : Exception
{
    private const string Message = "Invalid song.";

    public InvalidSongException(string message) : base(message)
    {
    }

    public InvalidSongException() : base(Message)
    {
    }
}