# Lab 2

Back in the Techs.Api!

## Part 1

We promised those jokers over in the SoftwareCenter team that we would be the provider for something they need.

They want to be able to do the following on our API:

```http
GET http://localhost:1338/software/techs/{sub}
Accept: application/json
```

We will return to them something like this:

```http
200 Ok
Content-Type: application/json

{
    "name": "Brad Morley"
}
```

Which is the first and last name of the tech we have with that `sub` property.

If we don't have a tech with that sub property, we will return a 404.

### Instructions
I want you to do this however you like. The important part is you *get it done*. 

- You *could* create a different controller for just this, or you could add it to my `TechsController`
- You *could* add a method to my `ITechRepository` and implement it on the service class.
- You *could*/*should* have a test for this (A System Test).
- Maybe in the test have two tests - one that returns some data, and on that shows a 404 is returned if there is no tech with that `sub`.

#### Extra Credit

Only the SoftwareCenter folks should be able to call this endpoint. How would you "secure" it?

Hints:
- Authorize the call.
- Setup JWTS 
- Create a security policy that only folks in the `SoftwareCenter` role (in their token) can access this endpoint.

## Part 2

### Getting All Techs

We need a way to get a list of all the Techs. So we want a:

```http
GET http://localhost:1338/techs
Accept: application/json
```

That will return a response that looks like this:

```http
200 Ok
Content-Type: application/json

[
    { id: "3983989383", "name": "Smith, Bob"},
    { id:: "37u378836", "name": "Jones, Sue"}
]
```

Note: In the event there are no techs, PANIC, but just return an empty array.

### Retiring a Tech

Sometimes techs get fired, leave the company, retire, or just move on to other jobs.

We need a way to remove the tech from both the `GET /techs` endpoint, and also `GET /techs/{id}`. 

Note: We are a business here. We never really *delete* any data. Just make it so that it doesn't show up anymore (this is a "soft delete").

Hints:

Consider:

```http
DELETE http://localhost:1338/techs/06183c0b-ef8f-4a11-945b-e50d1d4f9d76
```

As long as they call this with a Guid, this should *always* return a 204 status code (No content).

Do *not* return a 404 if that tech does not exist, or is already retired. That is a major HTTP party foul.

Suggestions:

You could add a property to the `TechEntity` called something like `Retired`, that is a boolean.

When they `DELETE` a tech, you could look them up, change `Retired` to `true`, and store them again.

**Make sure they aren't still returned on the GET requests after this**.

### List of Retired Techs

So, we need another endpoint where *some people* can get a list of all all the techs that are retired.

Suggestions:

- This should probably be another resource. For example, you might try something like:
- `GET /techs?retired=true` to include those retired folks, but this is a query string party foul.
- Maybe something like `GET /retired-techs` might be better? But you do you.

