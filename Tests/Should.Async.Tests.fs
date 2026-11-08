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
    do! shouldSucceed (fun () -> async { return true } |> AsyncShould.be true)
    do! shouldSucceed (fun () -> async { return 123 } |> AsyncShould.be 123)
    do! shouldSucceed (fun () -> async { return "foo" } |> AsyncShould.be "foo")

    do! shouldFail (fun () -> async { return false } |> AsyncShould.be true)
    do! shouldFail (fun () -> async { return 456 } |> AsyncShould.be 123)
    do! shouldFail (fun () -> async { return "bar" } |> AsyncShould.be "foo")
  }

  [<TestMethod>]
  member _.BeEmpty(): Task = task {
    do! shouldSucceed (fun () -> async { return "" :> char seq } |> AsyncShould.beEmpty)
    do! shouldSucceed (fun () -> async { return [] :> string seq } |> AsyncShould.beEmpty)

    do! shouldFail (fun () -> async { return "foo" :> char seq } |> AsyncShould.beEmpty)
    do! shouldFail (fun () -> async { return [""] :> string seq } |> AsyncShould.beEmpty)
  }
