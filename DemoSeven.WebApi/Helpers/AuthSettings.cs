namespace DemoSeven.WebApi.Helpers
{
    //cannot be internal == JWT
    
    public sealed class AuthSettings
    {
        public string Secret { get; set; }
    }
}