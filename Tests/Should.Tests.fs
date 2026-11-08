namespace Belin.Should

open Microsoft.VisualStudio.TestTools.UnitTesting

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
  member _.BeEmpty() =
    shouldFail (fun () -> "foo" |> Should.beEmpty)
    shouldFail (fun () -> [""] |> Should.beEmpty)
    shouldSucceed (fun () -> "" |> Should.beEmpty)
    shouldSucceed (fun () -> [] |> Should.beEmpty)
