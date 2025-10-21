Feature: LogIn

Testing the Login Feature of Sause Demo App

@Login
Scenario Outline: Test Login with different credentials
	Given I am on the Sauce Demo login page
	When I enter username "<username>"
	And I enter password "<password>"
	And I click on the login button
	Then I should be redirected to the inventory page
	And I should see the products list

Examples: 
	| username      | password     |
	| standard_user | secret_sauce |
	| problem_user  | secret_sauce |
    |               |              | # Will fallback to AppConfig.json default