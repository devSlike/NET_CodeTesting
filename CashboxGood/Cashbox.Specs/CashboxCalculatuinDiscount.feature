Feature: CashboxCalculatuinDiscount
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: Check that account can get 15% discount (10% + 5%, for previous orders and for selected products)
	Given I have entered 1 as a account id
	And I have entered 200m as a total price
	When I call method service method GetDiscount
	Then the result should be 0.15
