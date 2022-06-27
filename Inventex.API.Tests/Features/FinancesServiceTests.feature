Feature: FinanceServiceTests
    As a Developer
    I want to add new Finance through API
    So that It can be available for applications.
	
    Background: 
        Given The Endpoint https://inventex.azurewebsites.net/api/v1/finances is available
        And A User is already stored in user's  data
          | FirstName | LastName   | Email                  | Password    |
          | Ricardo   | De la Cruz | ric.cruz1212@gmail.com | Ss924@d#p_s | 

    @finance-adding
    Scenario: Add Finance with Unique Name
        When a Post Request is sent
          | Name   | Day        | Quantity | Type   | UserId |
          | Sample | 01/02/2020 | 20       | true   | 1      |
        Then A Response with Status 200 is received
        And a Finance Resource is included in Response Body
          | Id | Name   | Day        | Quantity | Type | UserId |
          | 1  | Sample | 01/02/2020 | 20       | true | 1      |
    
    @finance-adding
    Scenario: Add Finance with existing name
        Given A Finance is already stored
          | Id | Name   | Day        | Quantity | Type | UserId |
          | 1  | Sample | 01/02/2020 | 20       | true | 1      |
        When A Post Request is sent
          | Name   | Day        | Quantity | Type | UserId |
          | Sample | 01/02/2020 | 20       | true | 1      |
        Then A Response with Status 400 is received
        And An Error Message with value "Finance Name already exists." is returned
        And the second number is 70