﻿public class InvalidSongMinutesException : InvalidSongLengthException
{
    private const string Message = "Song minutes should be between 0 and 14.";

    public InvalidSongMinutesException() : base(Message)
    {
    }
}