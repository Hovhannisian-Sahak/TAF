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

  Scenario Outline: Navigate to Services category from Home
    Given I am on the Home page
    When I open the "<Category>" service from Home
    Then the "<Category>" service page title should be displayed
    And the "Our Related Expertise" section should be displayed

    Examples:
      | Category       |
      | Generative AI  |
      | Responsible AI |
