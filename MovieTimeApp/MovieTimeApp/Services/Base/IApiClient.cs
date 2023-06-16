namespace MovieTimeApp.Services
{
    public interface IApiClient<T>
    {
        T Client { get; }
    }
}