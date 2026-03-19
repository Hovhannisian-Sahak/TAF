using RestSharp;

namespace TAF.Core.Api;

public sealed class RestRequestBuilder
{
    private readonly RestRequest _request;

    public RestRequestBuilder(string resource, Method method)
    {
        _request = new RestRequest(resource, method);
    }

    public RestRequestBuilder AddHeader(string name, string value)
    {
        _request.AddHeader(name, value);
        return this;
    }

    public RestRequestBuilder AddQueryParameter(string name, string value)
    {
        _request.AddQueryParameter(name, value);
        return this;
    }

    public RestRequestBuilder AddUrlSegment(string name, string value)
    {
        _request.AddUrlSegment(name, value);
        return this;
    }

    public RestRequestBuilder AddJsonBody<T>(T body) where T : class
    {
        _request.AddJsonBody(body);
        return this;
    }

    public RestRequest Build() => _request;
}
