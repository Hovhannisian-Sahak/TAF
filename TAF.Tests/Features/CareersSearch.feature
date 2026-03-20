@ui
Feature: Careers search
  As a visitor
  I want to search for open positions
  So that I can review vacancy details

  Scenario Outline: Search careers by criteria
    Given I am on the Home page
    When I search careers for "<Keyword>" in "<Location>"
    And I open the latest result and view apply
    Then the vacancy details should contain keyword "<Keyword>"
    And the vacancy details should contain location "<Location>"

    Examples:
      | Keyword | Location |
      | Java    | Belarus  |
      | C#      | Armenia  |
