using MovieTimeApp.Helpers;
using MovieTimeApp.Models.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace MovieTimeApp.Services
{
	public class ConfigService : BaseService, IConfigServive
    {
        private readonly IApiClient<IConfigApi> _configApi;
        private readonly IConfig _config;

        public ConfigService(IApiClient<IConfigApi> configApi, IConfig config)
        {
            _configApi = configApi;
            _config = config;
        }

        public async Task<Response<ConfigResponse>> GetConfigs()
        {
            var response = await RemoteRequestAsync<ConfigResponse>(_configApi.Client.GetConfigs(_config.ApiKey, new CancellationTokenSource().Token));
            return response;
        }
    }
}