using MovieTimeApp.Models.Responses;
using System.Threading.Tasks;

namespace MovieTimeApp.Services
{
    public interface IConfigServive
    {
        Task<Response<ConfigResponse>> GetConfigs();
    }
}
