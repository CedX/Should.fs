namespace Belin.Should

open Microsoft.VisualStudio.TestTools.UnitTesting
open System.Threading.Tasks

/// Tests the features of the `AsyncShould` module.
[<TestClass>]
type AsyncShouldTests() =

  /// Tests whether the specified action throws an `AssertFailedException`.
  static let shouldFail (action: unit -> Async<unit>) = async {
    try
      do! action()
      Assert.Fail "The specified action did not throw any exception."
    with ex ->
      Assert.IsInstanceOfType<AssertFailedException> ex |> ignore<AssertFailedException>
  }

  /// Tests whether the specified action does not throw an `AssertFailedException`.
  static let shouldSucceed (action: unit -> Async<unit>) = async {
    try
      do! action()
      Assert.IsTrue true
    with ex ->
      Assert.IsNotInstanceOfType<AssertFailedException> ex
  }

  [<TestMethod>]
  member _.Be(): Task = task {
    do! shouldSucceed (fun () -> Async.result true |> AsyncShould.be true)
    do! shouldSucceed (fun () -> Async.result 123 |> AsyncShould.be 123)
    do! shouldSucceed (fun () -> Async.result "foo" |> AsyncShould.be "foo")
    do! shouldSucceed (fun () -> Async.result (Some "foo") |> AsyncShould.be (Some "foo"))

    do! shouldFail (fun () -> Async.result false |> AsyncShould.be true)
    do! shouldFail (fun () -> Async.result 456 |> AsyncShould.be 123)
    do! shouldFail (fun () -> Async.result "bar" |> AsyncShould.be "foo")
    do! shouldFail (fun () -> Async.result None |> AsyncShould.be (Some "foo"))
  }

  [<TestMethod>]
  member _.BeEmpty(): Task = task {
    do! shouldSucceed (fun () -> Async.result ("" :> char seq) |> AsyncShould.beEmpty)
    do! shouldSucceed (fun () -> Async.result ([] :> string seq) |> AsyncShould.beEmpty)

    do! shouldFail (fun () -> Async.result ("foo" :> char seq) |> AsyncShould.beEmpty)
    do! shouldFail (fun () -> Async.result ([""] :> string seq) |> AsyncShould.beEmpty)
  }

  [<TestMethod>]
  member _.BeOfType(): Task = task {
    do! shouldSucceed (fun () -> Async.result (false :> objnull) |> AsyncShould.beOfType<bool>)
    do! shouldSucceed (fun () -> Async.result (Some false :> objnull) |> AsyncShould.beOfType<Option<bool>>)
    do! shouldSucceed (fun () -> Async.result ("123" :> objnull) |> AsyncShould.beOfType<string>)
    do! shouldSucceed (fun () -> Async.result (123 :> objnull) |> AsyncShould.beOfType<int>)

    do! shouldFail (fun () -> Async.result (0 :> objnull) |> AsyncShould.beOfType<bool>)
    do! shouldFail (fun () -> Async.result (Some 0 :> objnull) |> AsyncShould.beOfType<Option<bool>>)
    do! shouldFail (fun () -> Async.result ("123" :> objnull) |> AsyncShould.beOfType<int>)
    do! shouldFail (fun () -> Async.result (123 :> objnull) |> AsyncShould.beOfType<string>)
  }
