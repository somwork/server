using TaskHouseApi.Model;

namespace TaskHouseApi.Service
{
    public interface IPasswordService
    {
        (string saltText, string saltechashedPassword) GenerateNewPassword(User user);

        string GenerateSaltedHashedPassword(string password, string saltText);
    }
}