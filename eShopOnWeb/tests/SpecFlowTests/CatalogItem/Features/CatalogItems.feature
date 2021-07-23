Feature: CatalogItems
	Simple calculator for adding two numbers
	

Scenario: Add an item to the catalog

	When I add the following item to the catalog:
		| Name          | Price | Description                                | Brand | Type |
		| Big Blue Ball | 2     | A ball that is big and definitely not red. | -     | -    |

	Then the catalog contains the following items:
		| Name          | Price | Description                                | Brand | Type |
		| Big Blue Ball | 2     | A ball that is big and definitely not red. | -     | -    |
		
Scenario: Cannot add an item without a description to the catalog

	When I add the following item to the catalog:
		| Name          | Price | Description | Brand | Type |
		| Big Blue Ball | 2     | -           | -     | -    |

	#for simplification only
	#generally "not contain" assertions are not recommended
	Then the catalog does not contain the following items:
		| Name          | Price | Description | Brand | Type |
		| Big Blue Ball | 2     | -           | -     | -    |