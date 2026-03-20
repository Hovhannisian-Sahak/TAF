@ui
Feature: Global search
  As a visitor
  I want to search the site
  So that I can find relevant content

  Scenario Outline: Use global search
    Given I am on the Home page
    When I search globally for "<Term>"
    Then search results should contain "<Term>"

    Examples:
      | Term       |
      | BLOCKCHAIN |
      | Cloud      |
