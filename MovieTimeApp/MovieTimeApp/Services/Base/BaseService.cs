using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MovieTimeApp.Models.Responses;
using Newtonsoft.Json;
using Polly;
using Refit;
using Xamarin.Essentials;

namespace MovieTimeApp.Services
{
    public abstract class BaseService
    {
        protected Dictionary<int, CancellationTokenSource> runningTasks = new Dictionary<int, CancellationTokenSource>();

        public BaseService()
        {
            Connectivity.ConnectivityChanged += OnConnectivityChanged;
        }

        void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess != NetworkAccess.Internet)
            {
                var items = runningTasks.ToList();
                foreach (var item in items)
                {
                    item.Value.Cancel();
                    runningTasks.Remove(item.Key);
                }
            }
        }

        protected async Task<Response<TData>> RemoteRequestAsync<TData>(Task<HttpResponseMessage> task)
        {
            HttpResponseMessage responseMessage = await Policy
            .Handle<WebException>()
            .Or<ApiException>()
            .Or<TaskCanceledException>()
            .WaitAndRetryAsync
            (
                retryCount: 0, //3
                sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
            )
            .ExecuteAsync(async () =>
            {

                if (!runningTasks.ContainsKey(task.Id))
                {
                    var cts = new CancellationTokenSource();
                    runningTasks.Add(task.Id, cts);
                }

                var result = await task;

                if (runningTasks.ContainsKey(task.Id))
                {
                    runningTasks.Remove(task.Id);
                }

                return result;
            });

            if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                return Response<TData>.Error(AppResources.ApiKeyErrorMessage);
            }

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                var result = await Task.Run(() => JsonConvert.DeserializeObject<TData>(jsonResult));
                return Response<TData>.Ok(result);
            }
            else
            {
                var jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                var result = await Task.Run(() => JsonConvert.DeserializeObject<ErrorResponse>(jsonResult));
                return Response<TData>.Error(result.Message);
            }
        }
    }
}