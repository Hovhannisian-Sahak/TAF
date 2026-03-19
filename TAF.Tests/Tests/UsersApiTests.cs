using System.Net;
using NUnit.Framework;
using TAF.Business.ApiModels;
using TAF.Core.Api;
using TAF.Core.Configuration;
using TAF.Core.Logging;

namespace TAF.Tests.Tests;

[TestFixture]
[Category("API")]
[Parallelizable(ParallelScope.All)]
public class UsersApiTests
{
    private static readonly log4net.ILog Log = AppLogger.For<UsersApiTests>();
    private ApiClient _client = null!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _ = Configuration.Api; // force configuration + logging setup
        Log.Info($"Starting API test fixture: {TestContext.CurrentContext.Test.Name}");
        _client = new ApiClient(Configuration.Api.BaseUrl);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        Log.Info($"Finished API test fixture: {TestContext.CurrentContext.Test.Name}");
    }

    [Test]
    public async Task GetUsers_Should_Return_UserList_With_Expected_Fields()
    {
        Log.Info("Task #1: Requesting list of users.");
        var request = new RestRequestBuilder("users", RestSharp.Method.Get)
            .Build();

        var response = await _client.ExecuteAsync<List<User>>(request);

        Assert.That(response.ErrorException, Is.Null);
        Assert.That(response.ErrorMessage, Is.Null.Or.Empty);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        Assert.That(response.Data, Is.Not.Null.And.Not.Empty);
        foreach (var user in response.Data!)
        {
            Assert.That(user.Id, Is.Not.Null, "User id is missing.");
            Assert.That(user.Name, Is.Not.Null.And.Not.Empty, "User name is missing.");
            Assert.That(user.Username, Is.Not.Null.And.Not.Empty, "User username is missing.");
            Assert.That(user.Email, Is.Not.Null.And.Not.Empty, "User email is missing.");
            Assert.That(user.Address, Is.Not.Null, "User address is missing.");
            Assert.That(user.Phone, Is.Not.Null.And.Not.Empty, "User phone is missing.");
            Assert.That(user.Website, Is.Not.Null.And.Not.Empty, "User website is missing.");
            Assert.That(user.Company, Is.Not.Null, "User company is missing.");
        }

        Log.Info("Task #1 completed: users validated with required fields.");
    }

    [Test]
    public async Task GetUsers_Should_Have_ContentType_Header()
    {
        Log.Info("Task #2: Requesting list of users to validate headers.");
        var request = new RestRequestBuilder("users", RestSharp.Method.Get)
            .Build();

        var response = await _client.ExecuteAsync(request);

        Assert.That(response.ErrorException, Is.Null);
        Assert.That(response.ErrorMessage, Is.Null.Or.Empty);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var contentTypeHeader = response.ContentHeaders?
            .FirstOrDefault(h => string.Equals(h.Name, "Content-Type", StringComparison.OrdinalIgnoreCase));
        var contentTypeValue = Convert.ToString(contentTypeHeader?.Value);

        Assert.That(contentTypeValue, Is.Not.Null.And.Not.Empty);
        Assert.That(contentTypeValue, Is.EqualTo("application/json; charset=utf-8"));

        Log.Info("Task #2 completed: content-type header validated.");
    }

    [Test]
    public async Task GetUsers_Should_Return_Ten_Unique_Users_With_CompanyName()
    {
        Log.Info("Task #3: Requesting list of users for content validation.");
        var request = new RestRequestBuilder("users", RestSharp.Method.Get)
            .Build();

        var response = await _client.ExecuteAsync<List<User>>(request);

        Assert.That(response.ErrorException, Is.Null);
        Assert.That(response.ErrorMessage, Is.Null.Or.Empty);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        Assert.That(response.Data, Is.Not.Null);
        var users = response.Data!;
        Assert.That(users.Count, Is.EqualTo(10), "Expected 10 users in the response.");

        var ids = new HashSet<int>();
        foreach (var user in users)
        {
            Assert.That(user.Id, Is.Not.Null, "User id is missing.");
            Assert.That(ids.Add(user.Id!.Value), Is.True, "Duplicate user id detected.");
            Assert.That(user.Name, Is.Not.Null.And.Not.Empty, "User name is missing.");
            Assert.That(user.Username, Is.Not.Null.And.Not.Empty, "User username is missing.");
            Assert.That(user.Company, Is.Not.Null, "User company is missing.");
            Assert.That(user.Company!.Name, Is.Not.Null.And.Not.Empty, "User company name is missing.");
        }

        Log.Info("Task #3 completed: list content validated.");
    }

    [Test]
    public async Task CreateUser_Should_Return_Id()
    {
        Log.Info("Task #4: Creating user.");
        var requestBody = new { name = "API User", username = "api.user" };
        var request = new RestRequestBuilder("users", RestSharp.Method.Post)
            .AddJsonBody(requestBody)
            .Build();

        var response = await _client.ExecuteAsync<User>(request);

        Assert.That(response.ErrorException, Is.Null);
        Assert.That(response.ErrorMessage, Is.Null.Or.Empty);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        Assert.That(response.Content, Is.Not.Null.And.Not.Empty);
        Assert.That(response.Data, Is.Not.Null);
        Assert.That(response.Data!.Id, Is.Not.Null, "Created user id is missing.");

        Log.Info("Task #4 completed: user creation validated.");
    }

    [Test]
    public async Task InvalidEndpoint_Should_Return_404()
    {
        Log.Info("Task #5: Requesting invalid endpoint.");
        var request = new RestRequestBuilder("invalidendpoint", RestSharp.Method.Get)
            .Build();

        var response = await _client.ExecuteAsync(request);

        Assert.That(response.ErrorMessage, Is.Null.Or.Empty);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));

        Log.Info("Task #5 completed: 404 validated.");
    }
}
