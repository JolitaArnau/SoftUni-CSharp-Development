public class Song
{
    private const int MinArtistNameLenght = 3;
    private const int MaxArtistNameLenght = 20;
    private const int MinSongNameLenght = 3;
    private const int MaxSongNameLenght = 30;

    private const int MinSongMinutes = 0;
    private const int MaxSongMinutes = 14;
    private const int MinSongSeconds = 0;
    private const int MaxSongSeconds = 59;


    private string artist;
    private string name;
    private int minutes;
    private int seconds;

    public Song(string artist, string name, int minutes, int seconds)
    {
        Artist = artist;
        Name = name;
        Minutes = minutes;
        Seconds = seconds;
    }


    public string Artist
    {
        get => artist;

        private set
        {
            if (value.Length < MinArtistNameLenght || value.Length > MaxArtistNameLenght)
            {
                throw new InvalidArtistNameException();
            }

            artist = value;
        }
    }

    public string Name
    {
        get => name;

        private set
        {
            if (value.Length < MinSongNameLenght || value.Length > MaxSongNameLenght)
            {
                throw new InvalidSongNameException();
            }

            name = value;
        }
    }

    public int Minutes
    {
        get => minutes;

        private set
        {
            if (value < MinSongMinutes || value > MaxSongMinutes)
            {
                throw new InvalidSongMinutesException();
            }

            minutes = value;
        }
    }

    public int Seconds
    {
        get => seconds;

        private set
        {
            if (value < MinSongSeconds || value > MaxSongSeconds)
            {
                throw new InvalidSongSecondsException();
            }

            seconds = value;
        }
    }
}