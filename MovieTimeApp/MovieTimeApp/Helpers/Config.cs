namespace MovieTimeApp.Helpers
{
    public class Config : IConfig
    {
        public string AppName => "Movie Time";
        public string ApiUrl => "https://api.themoviedb.org/3";
        public string ApiKey => "SPECIFY API KEY HERE";
        public string ImageBaseUrl { get; set; }
        public string DefaultImageFormat => "w300";
    }
}
