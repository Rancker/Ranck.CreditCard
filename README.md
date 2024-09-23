# Ranck Problem Statement
This task is aimed at understanding how you go about analyzing and solving a problem and therefore there are no right or wrong answers.

The most important thing is that you show us how you would tackle a problem and in the actual interview, we will be going through what you have done/would do differently if given more time.

Commit your changes in this repository at least 24 hours before the interview so we have some time to prepare.

Try not to spend more than 2.5 to 3 hours on this as we can discuss during the interview what you would have done differently/more when you had more time.

# The task
## C#
We as a bank have an external API that gives us a random set of currently active credit cards (https://random-data-api.com/api/v2/credit_cards).

Create an API in C# that:
- on request can show the user a list of 10 random credit cards with their expiration date and type.
- on request can show how many credit cards we have of each type (based on the 1st request)
- on request can show how many credit cards expire after the 1st of january of 2027 (based on the list in the 1st request).

## Web
- Create a React App to visualise the data from the API you created above.
- If you do not have React or web app skills, please provide another simple UI to visualise the API.

# Resources
API: https://random-data-api.com/api/v2/credit_cards

Documentation: https://random-data-api.com/documentation

## Prerequisites
- .NET 6 https://dotnet.microsoft.com/en-us/download/dotnet/6.0

## Nuget Packages
- AutoFixture  4.18.1
- AutoMapper 13.0.1
- FluentAssertions 6.12.0
- Moq 4.20.70
- Flurl.Http 4.0.2
- NUnit 3.13.3	
- Swashbuckle 6.5.0

## Third Parties

This Project has used these public resource:
- https://random-data-api.com/api/v2/credit_cards

## About APIs
This rest API consists of three Get APIs. This project support API versioning. (The current version is v1) All APIs return Json format. The response contains result, success, and errors:

Success is a boolean field and indicates the request is successful or not. (If the success is true, the result has value, otherwise errors has value)
Result consists of the return values of the API.
Errors which is a list of objects, consist of message and type. Message is the description of error and type is a unique number for the type of errors.
The first one returns a list of random CreditCard.
Example EndPoint:
/HTTP/GET api/v1/CreditCard

The result field has a collection of 3 fields: ex-

{
      "cardnumber": "1212-1221-1121-1234",
      "expirydate": "2027-09-02",
      "cardtype": "dankort"
}

The second one returns the crad types along with their count:
Example EndPoint: /HTTP/GET api/v1/CreditCard/card-types

The result field has collection of 2 fields: ex - 

 {
    "dankort": 2,
    "solo": 1,
    "maestro": 1,
    "visa": 1,
    "american_express": 2,
    "jcb": 1,
    "discover": 1,
    "diners_club": 1
  }

The third one returns the number of card expire after certain date, that date is configurable in the config file:
Example EndPoint: /HTTP/GET api/v1/CreditCard/expiring/after'

The result field hasone count: ex - 

"result": 7,

## Technical Details
- Used the onion architecture to create the solution frame
- Used Logging
- Used Exception handling
- Used dependency injection
- Flurl client to call the external API
- Used Swagger for UI
- AutoMapper to map
- Can go with react but as swagger is there used swagger for UI purpose.

## Running Credit Card API
Clone the project. Open the command prompt (in windows) and run the command "dotnet run" in this path: \src\Ranck.CreditCard.WebService

After running the app, the API will host on two different ports:
https://localhost:5001 ,
http://localhost:5000

You can run from Visual studio or from Visual studio code as well.

You can also browse this link to discover the API: https://localhost:5001/swagger/index.html
 
 
 ## Running Unit Tests
 You can go to the test explorer window in Visual Studio and push the run button, or run the command "dotnet test" in this path: \tests\Ranck.CreditCard.Application.Tests and tests\Ranck.CreditCard.Infrastructure.Tests and \tests\Ranck.CreditCard.WebService.Tests
 
##Future Suggestion
CI/CD
We can addean Action to the Github repository for building and running the unit tests every time the code is pushed to the repository. If all the checks (building and running tests) are passed, PR canbe merged to the main branch.

Docker
The project can also contains a simple docker file for dockerizing the API.

Add caching for calling the third party APIs.
  

