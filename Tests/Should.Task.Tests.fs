namespace Belin.Should

open Microsoft.VisualStudio.TestTools.UnitTesting
open System.Threading.Tasks

/// Tests the features of the `ShouldAsync` module.
[<TestClass>]
type ShouldAsyncTests() =

  /// Tests whether the specified action throws an `AssertFailedException`.
  static let shouldFail (action: unit -> Task<unit>) = task {
    try
      do! action()
      Assert.Fail "The specified action did not throw any exception."
    with ex ->
      Assert.IsInstanceOfType<AssertFailedException> ex |> ignore<AssertFailedException>
  }

  /// Tests whether the specified action does not throw an `AssertFailedException`.
  static let shouldSucceed (action: unit -> Task<unit>) = task {
    try
      do! action()
      Assert.IsTrue true
    with ex ->
      Assert.IsNotInstanceOfType<AssertFailedException> ex
  }

  [<TestMethod>]
  member _.Be(): Task = task {
    do! shouldSucceed (fun () -> Task.result true |> ShouldAsync.be true)
    do! shouldSucceed (fun () -> Task.result 123 |> ShouldAsync.be 123)
    do! shouldSucceed (fun () -> Task.result "foo" |> ShouldAsync.be "foo")
    do! shouldSucceed (fun () -> Task.result (Some "foo") |> ShouldAsync.be (Some "foo"))

    do! shouldFail (fun () -> Task.result false |> ShouldAsync.be true)
    do! shouldFail (fun () -> Task.result 456 |> ShouldAsync.be 123)
    do! shouldFail (fun () -> Task.result "bar" |> ShouldAsync.be "foo")
    do! shouldFail (fun () -> Task.result None |> ShouldAsync.be (Some "foo"))
  }

  [<TestMethod>]
  member _.BeEmpty(): Task = task {
    do! shouldSucceed (fun () -> Task.result ("" :> char seq) |> ShouldAsync.beEmpty)
    do! shouldSucceed (fun () -> Task.result ([] :> string seq) |> ShouldAsync.beEmpty)

    do! shouldFail (fun () -> Task.result ("foo" :> char seq) |> ShouldAsync.beEmpty)
    do! shouldFail (fun () -> Task.result ([""] :> string seq) |> ShouldAsync.beEmpty)
  }

  [<TestMethod>]
  member _.BeOfType(): Task = task {
    do! shouldSucceed (fun () -> Task.result (false :> objnull) |> ShouldAsync.beOfType<bool>)
    do! shouldSucceed (fun () -> Task.result (Some false :> objnull) |> ShouldAsync.beOfType<Option<bool>>)
    do! shouldSucceed (fun () -> Task.result ("123" :> objnull) |> ShouldAsync.beOfType<string>)
    do! shouldSucceed (fun () -> Task.result (123 :> objnull) |> ShouldAsync.beOfType<int>)

    do! shouldFail (fun () -> Task.result (0 :> objnull) |> ShouldAsync.beOfType<bool>)
    do! shouldFail (fun () -> Task.result (Some 0 :> objnull) |> ShouldAsync.beOfType<Option<bool>>)
    do! shouldFail (fun () -> Task.result ("123" :> objnull) |> ShouldAsync.beOfType<int>)
    do! shouldFail (fun () -> Task.result (123 :> objnull) |> ShouldAsync.beOfType<string>)
  }
