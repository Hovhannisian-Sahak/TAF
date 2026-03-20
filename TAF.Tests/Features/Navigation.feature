@ui
Feature: Site navigation
  As a visitor
  I want to open main sections from Home
  So that I can access key pages

  Scenario Outline: Open section from Home
    Given I am on the Home page
    When I open the "<Page>" page from Home
    Then the "<Page>" page should be opened

    Examples:
      | Page               |
      | Careers            |
      | Insights           |
      | Quarterly Earnings |
