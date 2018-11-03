namespace TaskHouseApi.Model.ServiceModel
{
    // Model use to send username and password
    // from client
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
