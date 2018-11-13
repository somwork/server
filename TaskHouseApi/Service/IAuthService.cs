using TaskHouseApi.Model;
using TaskHouseApi.Model.ServiceModel;

namespace TaskHouseApi.Service
{
    public interface IAuthService
    {
        User Authenticate(LoginModel loginModel);
    }
}
