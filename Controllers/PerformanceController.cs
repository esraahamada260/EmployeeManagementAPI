using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PerformanceController : ControllerBase
{
private static async Task SimulateOperationAsync(
CancellationToken cancellationToken)
{
await Task.Delay(1000, cancellationToken);
}


[HttpGet("compare")]
public async Task<IActionResult> ComparePerformance(
    CancellationToken cancellationToken)
{
    // Sequential execution
    var sequentialStopwatch = Stopwatch.StartNew();

    await SimulateOperationAsync(cancellationToken);
    await SimulateOperationAsync(cancellationToken);

    sequentialStopwatch.Stop();

    // Parallel execution
    var parallelStopwatch = Stopwatch.StartNew();

    await Task.WhenAll(
        SimulateOperationAsync(cancellationToken),
        SimulateOperationAsync(cancellationToken)
    );

    parallelStopwatch.Stop();

    var sequentialTime = sequentialStopwatch.ElapsedMilliseconds;
    var parallelTime = parallelStopwatch.ElapsedMilliseconds;

    var improvementPercentage = sequentialTime > 0
        ? (sequentialTime - parallelTime) * 100.0 / sequentialTime
        : 0;

    return Ok(new
    {
        SequentialExecution = new
        {
            ExecutionTimeMilliseconds = sequentialTime,
            Description = "Operations executed one after another."
        },
        ParallelExecution = new
        {
            ExecutionTimeMilliseconds = parallelTime,
            Description = "Operations awaited together using Task.WhenAll."
        },
        PerformanceComparison = new
        {
            ImprovementPercentage = Math.Round(
                improvementPercentage, 2),
            FasterExecution = parallelTime < sequentialTime
                ? "Parallel"
                : parallelTime > sequentialTime
                    ? "Sequential"
                    : "Both took approximately the same time."
        }
    });
}


[HttpGet("cancellation-test")]
public async Task<IActionResult> CancellationTest(CancellationToken cancellationToken)
{
    try
    {
        await Task.Delay(TimeSpan.FromSeconds(15), cancellationToken);

        return Ok(new
        {
            Message = "Request completed successfully."
        });
    }
    catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
    {
        Console.WriteLine("The request was cancelled.");

        throw;
    }
}

}
