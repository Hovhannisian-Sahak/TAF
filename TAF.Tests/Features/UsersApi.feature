@api
Feature: Users API
  As an API consumer
  I want to work with users
  So that I can validate the users endpoint behavior

  Scenario: Get users returns list with required fields
    When I request the users list
    Then the response status code should be 200
    And the users list should contain required fields

  Scenario: Get users returns content-type header
    When I request the users list
    Then the response status code should be 200
    And the response should have content-type "application/json; charset=utf-8"

  Scenario: Get users returns ten unique users with company names
    When I request the users list
    Then the response status code should be 200
    And the users list should contain 10 users with unique ids and company names

  Scenario: Create user returns id
    When I create a user with name "API User" and username "api.user"
    Then the response status code should be 201
    And the created user should contain an id

  Scenario: Invalid endpoint returns 404
    When I request the "invalidendpoint" endpoint
    Then the response status code should be 404
