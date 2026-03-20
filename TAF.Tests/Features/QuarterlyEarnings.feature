@ui
Feature: Quarterly earnings download
  As a visitor
  I want to open the quarterly earnings download
  So that I can access the report

  Scenario: Open Quarterly Earnings download
    Given I am on the Home page
    When I open the Quarterly Earnings page from Home
    Then the Quarterly Earnings page should be opened
    When I open the Quarterly Earnings download link
    Then the Quarterly Earnings download page should be opened
