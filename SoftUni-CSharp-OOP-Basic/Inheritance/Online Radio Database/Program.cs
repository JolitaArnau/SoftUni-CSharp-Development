using System;
using System.Collections.Generic;
using System.Linq;


class Program
{
    static void Main()
    {
        var songsCount = int.Parse(Console.ReadLine());
        var playlist = new List<Song>();

        for (int i = 0; i < songsCount; i++)
        {
            var songs = Console.ReadLine().Split(';');
            var artistName = songs[0];
            var songName = songs[1];
            var songLength = songs[2].Split(':');
            var minutes = int.Parse(songLength[0]);
            var seconds = int.Parse(songLength[1]);

            try
            {
                Song song = new Song(artistName, songName, minutes, seconds);
                playlist.Add(song);
                Console.WriteLine("Song added.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        int totalMinutes = playlist.Sum(x => x.Minutes);
        int totalSeconds = playlist.Sum(x => x.Seconds);

        totalSeconds += totalMinutes * 60;

        int finalMinutes = totalSeconds / 60;
        int finalSeconds = totalSeconds % 60;
        int finalHours = finalMinutes / 60;
        finalMinutes %= 60;
        
        Console.WriteLine($"Songs added: {playlist.Count}");
        Console.WriteLine($"Playlist length: {finalHours}h {finalMinutes}m {finalSeconds}s");
    }
}