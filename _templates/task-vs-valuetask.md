---
title: "Task vs Task<T> vs ValueTask<T>"
section: "1.3"
tags: [async, runtime, interview]
status: mastered
related: [async-state-machine, gc-generations]
---

# `Task` vs `Task<T>` vs `ValueTask<T>`

> **One-line summary:** `Task` represents a future; `Task<T>` a future that produces a value; `ValueTask<T>` is the same idea as `Task<T>` but tries to avoid allocating when the value is already there.

## The mental model

Think of `Task` as an **envelope you always have to mail**, even when the letter is already written.

- `Task` / `Task<T>` are **reference types** — allocated on the heap. Every call produces a new object, even if the work was synchronous.
- `ValueTask<T>` is a **struct wrapper**. On the synchronous happy path, it stays on the stack — zero heap allocation. If the work is truly async, it falls back to wrapping a `Task<T>` internally (same cost as `Task<T>`).

```
Caller invokes an async method:
      │
      ▼
  ┌─────────────────────────────────────┐
  │  Is the value already known NOW?    │
  └─────────────────────────────────────┘
         │                     │
       YES                     NO (real async work)
         │                     │
         ▼                     ▼
  ValueTask<T> on stack   ValueTask<T> wraps a real Task<T>
  0 heap allocations      (same allocation as plain Task<T>)
```

So `ValueTask<T>` = *"bet on the hot path being synchronous."* If your method is rarely synchronous, `ValueTask<T>` adds complexity without savings.

## Core rules

1. **Default to `Task` / `Task<T>`.** Simpler, safer, allocation cost is usually negligible.
2. **Reach for `ValueTask<T>` only when** the method is in a hot loop **AND** usually synchronous (e.g., `Stream.ReadAsync` returning from a buffer).
3. **Never await a `ValueTask<T>` twice.** Its internal state may be reused. #1 footgun.
4. **Never access `.Result` or `.GetAwaiter().GetResult()` on a `ValueTask<T>` without awaiting first.**
5. **`Task.FromResult(x)` > `Task.Run(() => x)`** for already-known values — no thread pool hop.

## Minimal example

```csharp
using System.Threading.Tasks;

public class CountService
{
    private int? _cache;

    // Plain Task<T> — always allocates a Task object, even when cached
    public Task<int> GetCountAsync()
        => Task.FromResult(_cache ?? LoadFromDb());

    // ValueTask<T> — zero heap allocations on the cached path
    public ValueTask<int> GetCountValueAsync()
    {
        if (_cache is int cached)
            return new ValueTask<int>(cached);          // stack only

        return new ValueTask<int>(LoadFromDbAsync());   // falls back to Task<int>
    }

    private int LoadFromDb() => 42;
    private Task<int> LoadFromDbAsync() => Task.FromResult(42);
}
```

## Gotchas

- **Double-await is undefined behavior.** `var vt = FooAsync(); await vt; await vt; // 🧨`
- **Don't return `ValueTask` from interface members unless you've measured.** It forces complexity on every implementer.
- **`async` keyword always adds a state machine.** Even `async ValueTask<T>` costs something — the savings come from *not awaiting* internally when the value is ready.
- **Faulted `Task`s don't throw on creation.** An exception is only observed when the task is awaited. This is why `async void` silently eats exceptions — there's no awaiter.
- **`IValueTaskSource<T>` (pooled value tasks) is advanced territory.** Rarely needed. Most devs should never touch it.

## Interview questions this answers

- *"What's the difference between `Task` and `ValueTask`?"* → allocation profile + hot-path optimization + the double-await footgun.
- *"When would you use `ValueTask`?"* → high-frequency methods that are usually synchronous. Measure first.
- *"Why does `async` cost something even when the method returns synchronously?"* → state machine generation by the compiler.
- *"What does `Task.FromResult` do?"* → creates a `Task` already in `RanToCompletion` state — no scheduling, no thread-pool hop.
- *"What happens if I await the same `ValueTask<T>` twice?"* → possibly garbage or an `InvalidOperationException`. The contract permits reuse.

## See also

- `async-state-machine.md` — what the compiler actually generates for `async`/`await`
- `gc-generations.md` — why allocation count matters at scale (Gen 0 pressure)
