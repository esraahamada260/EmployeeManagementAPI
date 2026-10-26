# Performance Measurement Report

## Objective

To compare the execution time of two independent asynchronous operations using sequential and parallel execution.

## Methodology

The performance was measured using the `Stopwatch` class in C#. Each operation simulated a one-second asynchronous wait using `Task.Delay()`.

Two execution strategies were tested:

* **Sequential Execution:** The second operation starts after the first operation completes.
* **Parallel Asynchronous Execution:** Both operations are initiated and awaited together using `Task.WhenAll()`.

## Results

| Execution Type          | Execution Time |
| ----------------------- | -------------: |
| Sequential Execution    |        2027 ms |
| Parallel Execution      |        1015 ms |
| Time Saved              |        1012 ms |
| Performance Improvement |         49.93% |

## Analysis

Sequential execution took 2027 milliseconds because the two operations were performed one after another.

Parallel asynchronous execution took 1015 milliseconds because both operations were initiated before awaiting their completion.

The measured execution time decreased by 1012 milliseconds, representing a 49.93% improvement compared with sequential execution.

## Conclusion

In this experiment, parallel asynchronous execution was approximately twice as fast as sequential execution.

The results demonstrate that `Task.WhenAll()` can reduce total waiting time when independent asynchronous operations are executed together.

However, these measurements apply to the simulated asynchronous operations and do not directly establish the performance improvement of database queries or EF Core operations.
