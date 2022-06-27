Feature: MachinesServiceTests
	As a Developer
	I want to add new Machine through API
	In order to make it available for applications.
	
	Background: 
		Given The Endpoint https://inventex.azurewebsites.net/machines is available

@machine-adding
Scenario: Add Machine with Unique Name
	When a Post Request is sent
		| Name   | Description      | Lifetime         | Active | UserId |
		| Sample | A Sample Machine | Day 			   | true   | 1      |
  	Then A Response with Status 200 is received
  	And a Machine Resource is included in Response Body
  		| Id | Name   | Description      | Lifetime | Active | UserId |
        | 1  | Sample | A Sample Machine | Day      | true   | 1      |
    
@machine-adding
Scenario: Add Machine with existing Name
	Given A Machine is already stored
		| Id | Name   | Description      | Lifetime | Active | UserdId |
		| 1  | Sample | A Sample Machine | Day      | true   | 1       |
  	When A Post Request is sent
  		| Name   | Description          | Lifetime | Active | UserdId |
        | Sample | The Ultimate Machine | Day      | true   | 1       |
    Then A Response with Status 400 is received
    And An Error Message with value "Machine Name already exists." is returned
	And the second number is 70