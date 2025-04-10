using Microsoft.AspNetCore.Mvc;

namespace DemoApi.OnCallDevelopers;

public class OnCallController(ILogger<OnCallController> _logger) : ControllerBase
{

    // GET /who-is-on-call
    private int _hitCount = 0;
 
    [HttpGet("who-is-on-call")] // "Attributes" - adding data to a thing in .net that other code can read.
    public async Task<ActionResult> GetOnCallDeveloper(CancellationToken token)
    {
        _logger.LogInformation("Starting a long running thing.");
        // fake, go look it up in the database, whatever (we'll get there TODAY)
        await Task.Delay(2000, token); // simulating a delay from a database.
        _logger.LogInformation("Finished a Log Running Thing");
        var response = new WhoIsOnCall("Dave", "555-1212", "dave@aol.com", $"This has been called {++_hitCount} times");
        // 200 Ok 
        return Ok(response);
       
    }
}

public record WhoIsOnCall(string Name, string Phone, string Email, string Called);

//public class WhoIsOnCall(string Name, )
//{
   
//    private string Name { get;  } = string.Empty;
//    private string Phone { get; } = string.Empty;
//    private string Email { get; } = string.Empty;
//    private string Called { get; } = string.Empty;
//}
