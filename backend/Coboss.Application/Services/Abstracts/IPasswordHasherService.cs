namespace Coboss.Application.Services.Abstracts
{
    public interface IPasswordHasherService
    {
        bool Compare(string password, string hash, byte[] salt);
        string Hash(string password, byte[] salt);
        byte[] RandomSalt(int bytes);
    }
}
