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
    do! shouldSucceed (fun () -> task { return true } |> ShouldAsync.be true)
    do! shouldSucceed (fun () -> task { return 123 } |> ShouldAsync.be 123)
    do! shouldSucceed (fun () -> task { return "foo" } |> ShouldAsync.be "foo")
    do! shouldSucceed (fun () -> task { return Some "foo" } |> ShouldAsync.be (Some "foo"))

    do! shouldFail (fun () -> task { return false } |> ShouldAsync.be true)
    do! shouldFail (fun () -> task { return 456 } |> ShouldAsync.be 123)
    do! shouldFail (fun () -> task { return "bar" } |> ShouldAsync.be "foo")
    do! shouldFail (fun () -> task { return None } |> ShouldAsync.be (Some "foo"))
  }

  [<TestMethod>]
  member _.BeEmpty(): Task = task {
    do! shouldSucceed (fun () -> task { return "" :> char seq } |> ShouldAsync.beEmpty)
    do! shouldSucceed (fun () -> task { return [] :> string seq } |> ShouldAsync.beEmpty)

    do! shouldFail (fun () -> task { return "foo" :> char seq } |> ShouldAsync.beEmpty)
    do! shouldFail (fun () -> task { return [""] :> string seq } |> ShouldAsync.beEmpty)
  }

  [<TestMethod>]
  member _.BeOfType(): Task = task {
    do! shouldSucceed (fun () -> task { return false :> objnull } |> ShouldAsync.beOfType<bool>)
    do! shouldSucceed (fun () -> task { return Some false :> objnull } |> ShouldAsync.beOfType<Option<bool>>)
    do! shouldSucceed (fun () -> task { return "123" :> objnull } |> ShouldAsync.beOfType<string>)
    do! shouldSucceed (fun () -> task { return 123 :> objnull } |> ShouldAsync.beOfType<int>)

    do! shouldFail (fun () -> task { return 0 :> objnull } |> ShouldAsync.beOfType<bool>)
    do! shouldFail (fun () -> task { return Some 0 :> objnull } |> ShouldAsync.beOfType<Option<bool>>)
    do! shouldFail (fun () -> task { return "123" :> objnull } |> ShouldAsync.beOfType<int>)
    do! shouldFail (fun () -> task { return 123 :> objnull } |> ShouldAsync.beOfType<string>)
  }
