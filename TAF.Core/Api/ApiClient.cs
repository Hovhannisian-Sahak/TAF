using log4net;
using RestSharp;
using TAF.Core.Logging;

namespace TAF.Core.Api;

public sealed class ApiClient
{
    private readonly RestClient _client;
    private readonly ILog _log;

    public ApiClient(string baseUrl)
    {
        _log = AppLogger.For<ApiClient>();
        var options = new RestClientOptions(baseUrl)
        {
            ThrowOnAnyError = false
        };
        _client = new RestClient(options);
    }

    public async Task<RestResponse> ExecuteAsync(RestRequest request, CancellationToken cancellationToken = default)
    {
        LogRequest(request);
        var response = await _client.ExecuteAsync(request, cancellationToken);
        LogResponse(response);
        return response;
    }

    public async Task<RestResponse<T>> ExecuteAsync<T>(RestRequest request, CancellationToken cancellationToken = default)
    {
        LogRequest(request);
        var response = await _client.ExecuteAsync<T>(request, cancellationToken);
        LogResponse(response);
        return response;
    }

    private void LogRequest(RestRequest request)
    {
        _log.Info($"API request: {request.Method} {request.Resource}");
    }

    private void LogResponse(RestResponse response)
    {
        _log.Info($"API response: {(int)response.StatusCode} {response.StatusCode}, Error: {response.ErrorMessage ?? "none"}");
    }
}
