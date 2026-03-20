using System.Net;
using NUnit.Framework;
using Reqnroll;
using RestSharp;
using TAF.Business.ApiModels;
using TAF.Core.Api;
using TAF.Tests.Steps.Support;

namespace TAF.Tests.Steps;

[Binding]
public sealed class UsersApiSteps
{
    private readonly ScenarioContext scenarioContext;

    public UsersApiSteps(ScenarioContext scenarioContext)
    {
        this.scenarioContext = scenarioContext;
    }

    [When("I request the users list")]
    public async Task WhenIRequestTheUsersList()
    {
        var request = new RestRequestBuilder("users", Method.Get)
            .Build();

        var response = await GetClient().ExecuteAsync<List<User>>(request);

        StoreResponse(response);
        scenarioContext[ScenarioKeys.UsersList] = response.Data;
    }

    [When("I request the \"(.*)\" endpoint")]
    public async Task WhenIRequestTheEndpoint(string endpoint)
    {
        var request = new RestRequestBuilder(endpoint, Method.Get)
            .Build();

        var response = await GetClient().ExecuteAsync(request);

        StoreResponse(response);
    }

    [When("I create a user with name \"(.*)\" and username \"(.*)\"")]
    public async Task WhenICreateAUserWithNameAndUsername(string name, string username)
    {
        var requestBody = new { name, username };
        var request = new RestRequestBuilder("users", Method.Post)
            .AddJsonBody(requestBody)
            .Build();

        var response = await GetClient().ExecuteAsync<User>(request);

        StoreResponse(response);
        scenarioContext[ScenarioKeys.CreatedUser] = response.Data;
    }

    [Then("the response status code should be (.*)")]
    public void ThenTheResponseStatusCodeShouldBe(int statusCode)
    {
        var response = GetLastResponse();
        
        Assert.That(response.ErrorMessage, Is.Null.Or.Empty);
        Assert.That(response.StatusCode, Is.EqualTo((HttpStatusCode)statusCode));
    }

    [Then("the response should have content-type \"(.*)\"")]
    public void ThenTheResponseShouldHaveContentType(string contentType)
    {
        var response = GetLastResponse();

        var contentTypeHeader = response.ContentHeaders?
            .FirstOrDefault(h => string.Equals(h.Name, "Content-Type", StringComparison.OrdinalIgnoreCase));
        var contentTypeValue = Convert.ToString(contentTypeHeader?.Value);

        Assert.That(contentTypeValue, Is.Not.Null.And.Not.Empty);
        Assert.That(contentTypeValue, Is.EqualTo(contentType));
    }

    [Then("the users list should contain required fields")]
    public void ThenTheUsersListShouldContainRequiredFields()
    {
        var users = GetUsersList();

        Assert.That(users, Is.Not.Null.And.Not.Empty);
        foreach (var user in users!)
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
    }

    [Then("the users list should contain (.*) users with unique ids and company names")]
    public void ThenTheUsersListShouldContainUsersWithUniqueIdsAndCompanyNames(int expectedCount)
    {
        var users = GetUsersList();

        Assert.That(users, Is.Not.Null);
        Assert.That(users!.Count, Is.EqualTo(expectedCount), $"Expected {expectedCount} users in the response.");

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
    }

    [Then("the created user should contain an id")]
    public void ThenTheCreatedUserShouldContainAnId()
    {
        var user = GetCreatedUser();

        Assert.That(user, Is.Not.Null, "Created user payload is missing.");
        Assert.That(user!.Id, Is.Not.Null, "Created user id is missing.");
    }

    private ApiClient GetClient()
    {
        if (scenarioContext.TryGetValue(ScenarioKeys.ApiClient, out ApiClient? client) && client != null)
        {
            return client;
        }

        throw new InvalidOperationException("API client is missing. Ensure the scenario has the @api tag.");
    }

    private void StoreResponse(RestResponse response)
    {
        scenarioContext[ScenarioKeys.LastResponse] = response;
    }

    private RestResponse GetLastResponse()
    {
        if (scenarioContext.TryGetValue(ScenarioKeys.LastResponse, out RestResponse? response) && response != null)
        {
            return response;
        }

        throw new InvalidOperationException("API response is missing. Make sure a request step ran first.");
    }

    private List<User>? GetUsersList()
    {
        if (scenarioContext.TryGetValue(ScenarioKeys.UsersList, out List<User>? users))
        {
            return users;
        }

        return null;
    }

    private User? GetCreatedUser()
    {
        if (scenarioContext.TryGetValue(ScenarioKeys.CreatedUser, out User? user))
        {
            return user;
        }

        return null;
    }
}
