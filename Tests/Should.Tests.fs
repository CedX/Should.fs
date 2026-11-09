namespace Belin.Should

open Microsoft.VisualStudio.TestTools.UnitTesting
open System

/// Tests the features of the `Should` module.
[<TestClass>]
type ShouldTests() =

  /// Tests whether the specified action throws an `AssertFailedException`.
  static let shouldFail (action: unit -> unit) =
    try
      action()
      Assert.Fail "The specified action did not throw any exception."
    with ex ->
      Assert.IsInstanceOfType<AssertFailedException> ex |> ignore<AssertFailedException>

  /// Tests whether the specified action does not throw an `AssertFailedException`.
  static let shouldSucceed (action: unit -> unit) =
    try
      action()
      Assert.IsTrue true
    with ex ->
      Assert.IsNotInstanceOfType<AssertFailedException> ex

  [<TestMethod>]
  member _.Be() =
    shouldSucceed (fun () -> true |> Should.be true)
    shouldSucceed (fun () -> 123 |> Should.be 123)
    shouldSucceed (fun () -> "foo" |> Should.be "foo")
    shouldSucceed (fun () -> Some "foo" |> Should.be (Some "foo"))

    shouldFail (fun () -> false |> Should.be true)
    shouldFail (fun () -> 456 |> Should.be 123)
    shouldFail (fun () -> "bar" |> Should.be "foo")
    shouldFail (fun () -> None |> Should.be (Some "foo"))

  [<TestMethod>]
  member _.BeEmpty() =
    shouldSucceed (fun () -> Should.beEmpty "")
    shouldSucceed (fun () -> Should.beEmpty [])

    shouldFail (fun () -> Should.beEmpty "foo")
    shouldFail (fun () -> Should.beEmpty [""])

  [<TestMethod>]
  member _.BeOfType() =
    shouldSucceed (fun () -> Should.beOfType<bool> false)
    shouldSucceed (fun () -> Should.beOfType<Option<bool>> (Some false))
    shouldSucceed (fun () -> Should.beOfType<string> "123")
    shouldSucceed (fun () -> Should.beOfType<int> 123)
    shouldSucceed (fun () -> Should.beOfType<DateTime> (DateTime()))

    shouldFail (fun () -> Should.beOfType<bool> 0)
    shouldFail (fun () -> Should.beOfType<Option<bool>> (Some 0))
    shouldFail (fun () -> Should.beOfType<int> "123")
    shouldFail (fun () -> Should.beOfType<string> 123)
    shouldFail (fun () -> Should.beOfType<Uri> (DateTime()))
