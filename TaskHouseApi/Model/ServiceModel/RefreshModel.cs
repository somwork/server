namespace TaskHouseApi.Model.ServiceModel
{
    // Model used to send accesstoken and
    // refreshtoken from client
    public class RefreshModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}