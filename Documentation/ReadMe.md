# BDD assertions for MSTest
A thin [BDD](https://en.wikipedia.org/wiki/Behavior-driven_development) wrapper
around [MSTest](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-mstest-intro) assertions,
for [F#](https://learn.microsoft.com/en-us/dotnet/fsharp).
	
## Quick start
Install the latest version of **BDD assertions for MSTest** with [NuGet](https://www.nuget.org) package manager:

```powershell
dotnet package add Belin.Should
```

For detailed instructions, see the [installation guide](Installation.md).

## Usage
Are you tired of writing your [MSTest](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-mstest-intro) assertions this way?

```fsharp
open Microsoft.VisualStudio.TestTools.UnitTesting

Assert.IsTrue true
Assert.AreEqual(expected, actual)
Assert.IsGreaterThan(lowerBound, value)
Assert.Contains(value, collection)
Assert.HasCount(expected, collection)
Assert.EndsWith(suffix, value)
Assert.IsInstanceOfType<IDisposable> value
Assert.Throws<Exception> action
```

Why not try this instead?

```fsharp
open Belin.Should
open Microsoft.VisualStudio.TestTools.UnitTesting

true |> Should.beTrue
actual |> Should.be expected
value |> Should.beGreaterThan lowerBound
value |> Should.beOneOf collection
collection |> Seq.length |> Should.be expected
value |> Should.endWith
value |> Should.beAssignableTo<IDisposable>
action |> Should.throw<Exception>
```

Much better! (Personal opinion)
