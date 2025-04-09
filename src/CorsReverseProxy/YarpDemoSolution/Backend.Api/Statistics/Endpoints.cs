using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Statistics;

public static class Endpoints
{
    public static IEndpointRouteBuilder MapStatisticsEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/statistics", ([FromBody]CalculateStatisticsRequest request) =>
        {
            var mean = request.Values.Average();
            var sortedValues = request.Values.OrderBy(v => v).ToList();
            var count = sortedValues.Count;
            var median = count % 2 == 0 
                ? (sortedValues[count / 2 - 1] + sortedValues[count / 2]) / 2 
                : sortedValues[count / 2];
            
            // Corrected standard deviation calculation
            var variance = request.Values.Average(v => Math.Pow(v - mean, 2));
            var standardDeviation = Math.Sqrt(variance);

            var response = new CalculateStatisticsResponse()
            {
                Mean = mean,
                Median = median,
                StandardDeviation = standardDeviation
            };
            return TypedResults.Ok(response);
        });
        return endpoints;
    }
}

public record CalculateStatisticsRequest
{
    public required List<float> Values { get; init; }
}

public record CalculateStatisticsResponse
{
    public float Mean { get; init; }
    public float Median { get; init; }
    public double StandardDeviation { get; init; }
}