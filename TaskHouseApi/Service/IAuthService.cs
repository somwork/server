using TaskHouseApi.Model;
using TaskHouseApi.Model.ServiceModel;

namespace TaskHouseApi.Service
{
    public interface IAuthService
    {
        User Authenticate(LoginModel loginModel);
        User Retrieve(int Id);
        User Update(User user);
        bool DeleteRefrechToken(RefreshToken refreshToken);
    }
}
