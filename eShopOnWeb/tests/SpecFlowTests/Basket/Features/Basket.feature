Feature: Add a product to a basket

Scenario: Add an item to the basket

	Given the basket of 'John Smith' contains 'Prism White T-Shirt'
	
	When 'John Smith' adds a 'Roslyn Red Sheet' to his basket

	Then the basket of 'John Smith' contains 'Roslyn Red Sheet, Prism White T-Shirt'

Scenario: Add an item to my basket

	Given I am logged in as 'Adam Smith'

	And my basket contains 'Prism White T-Shirt'
	
	When I add a 'Roslyn Red Sheet' to my basket

	Then my basket contains 'Roslyn Red Sheet, Prism White T-Shirt'

Scenario: Item is added only to my basket

	Given I am logged in as 'Adam Smith'
	
	When I add a 'Roslyn Red Sheet' to my basket

	Then the basket of 'John Doe' is empty

Scenario: Add an item to my basket

	#I am logged in as 'Adam Smith' implicitely
	Given my basket contains 'Prism White T-Shirt'
	
	When I add a 'Roslyn Red Sheet' to my basket

	Then my basket contains 'Roslyn Red Sheet, Prism White T-Shirt'

Scenario: Add an item to my basket (complex)

	Given my basket contains following products:
		| Name                | Amount |
		| Prism White T-Shirt | 1      |
	
	When I add the following product to my basket:
		| Name             | Amount |
		| Roslyn Red Sheet | 2      |

	Then my basket contains following products:
		| Name                | Amount |
		| Roslyn Red Sheet    | 2      |
		| Prism White T-Shirt | 1      |