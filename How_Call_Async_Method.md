###   How to call asynchronous method from synchronous method in C#?

## Create Invoke Function

```C#
private T InvokeAsyncMethod<T>(Func<Task<T>> func)
{
    return Task.Factory.StartNew(func)
        .Unwrap()
        .GetAwaiter()
        .GetResult();
}
```
## Call Invoke Function
```C#
                var party = InvokeAsyncMethod(() => CreateAsync(item));
```
