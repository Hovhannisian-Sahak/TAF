@ui
Feature: Insights carousel
  As a visitor
  I want to open an Insights article from the carousel
  So that I can read the article details

  Scenario: Open Insights article from carousel
    Given I am on the Home page
    When I open the Insights page from Home
    And I swipe the Insights carousel
    And I open the current Insights article
    Then the opened article title should match the carousel title
