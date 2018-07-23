namespace PhotoShare.Client.Contracts
{
    public interface ICommand
    {
        string Execute(string command, params string[] arguments);
    }
}