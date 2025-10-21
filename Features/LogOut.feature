Feature: LogOut

Testing the Logout Feature of Sauce Demo App

@Logout
Scenario Outline: Test Logout Functionality for different users
    Given I am logged in as "<username>" with "<password>"
    When I click on the menu button
    And I select the logout option
    Then I should be redirected to the login page
    And I should see the login form

Examples:
    | username      | password     |
    | standard_user | secret_sauce |
    | problem_user  | secret_sauce |
    |               |              |  # Will fallback to AppConfig.json default
