# The Techs Lab

Welcome to The Techs Lab!

Basically, the instructions are in the tests. If you get the tests to pass, you win!

## What I've Already Done

- Created a basic ASP.NET Core Web API project.
- Added some NuGet Packages and Configuration
    - I added Marten.AspNetCore and configured it. It will use a new database called "techs".
- Created a `Techs` folder with:
- `TechsController.cs` - A basic controller that has ZERO endpoints. Most of your work will be here.
- `Models.cs` - The request and response models.
- `Entities.cs` - The thing we want to store in the database.

Your mission is to implement the controller methods needed to make the tests in the `Techs.Tests/Techs/AddingTechs.cs` pass.

## System Tests

Start with the first test (`CanAddTechs`) and work your way down from there.

The test checks -
- Returning a 201 Created response when a tech is added
- Returning a Location header.
- Verifies *some* of the POST response body.
- Runs a scenario to retrieve the tech and makes sure it matches what we got back from the POST.

The second test (`ModelIsValidated`) simply checks that there is *some* validation of the model by sending an empty JSON object. It should return a 400 Bad Request.

## Unit Tests

In the `Techs.Tests/TechModelValidation.cs` there are two `[Theory]` tests that check the model validation. You can run these to verify that the model validation is set up correctly.

I have supplied some examples of models that should pass, and some that should not.

Basically -
- `FirstName` cannot be empty.
- `LastName` cannot be empty.
- `Email` must be a valid email format. (word@word.word)
- `Sub` cannot be empty, and must start with either an `x` or an `a`.
