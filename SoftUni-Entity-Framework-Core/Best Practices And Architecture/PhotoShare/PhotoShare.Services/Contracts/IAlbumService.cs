namespace PhotoShare.Services.Contracts
{
    using Models;
    
    public interface IAlbumService
    {
        Album AlbumByName(string name);
        
        Tag CreateTag(string tag);

        Album CreateAlbum(string username, string albumTitle, string bgColor, string[] tags);
        
        void AddTagToAlbum(string albumName, string tag);

        string ShareAlbum(int albumId, string username, string permission);

        void AddPicture(string albumName, string pictureTitle, string pictureFilePath);
    }
}