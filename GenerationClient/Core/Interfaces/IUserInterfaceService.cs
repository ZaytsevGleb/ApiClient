namespace GenerationClient.Core.Interfaces
{
    public interface IUserInterfaceService
    {
        void PrintLine(string text);
        string ReadLine();
        int ReadNumber();
        void Clear();
    }
}
