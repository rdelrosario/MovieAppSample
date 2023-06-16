namespace MovieTimeApp.Helpers
{
	public interface IConfig
	{
        string AppName { get; }
        string ApiUrl { get; }
        string ApiKey { get; }
        string DefaultImageFormat { get; }
        string ImageBaseUrl { get; set; }
    }
}

