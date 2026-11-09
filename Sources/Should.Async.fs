namespace Belin.Should

open System
open System.Text.RegularExpressions

/// Contains asynchronous assertions for working with unit tests.
[<RequireQualifiedAccess>]
module AsyncShould =

  /// Tests whether the specified values are equal.
  let be<'T> (expected: 'T) (actual: Async<'T>) = async {
    let! actual = actual
    return actual |> Should.be<'T> expected
  }

  /// Tests whether the specified object is an instance of the expected type.
  let beAssignableTo<'T> (value: Async<'T>) = async {
    let! value = value
    return value |> Should.beAssignableTo<'T>
  }

  /// Tests whether the specified sequence is empty.
  let beEmpty<'T> (sequence: Async<'T seq>) = async {
    let! sequence = sequence
    return Should.beEmpty<'T> sequence
  }

  /// Tests whether the specified condition is false.
  let beFalse (condition: Async<bool>) = async {
    let! condition = condition
    return Should.beFalse condition
  }

  /// Tests whether the given value is greater than the specified lower bound.
  let beGreaterThan<'T when 'T :> IComparable<'T>> (lowerBound: 'T) (value: Async<'T>) = async {
    let! value = value
    return value |> Should.beGreaterThan<'T> lowerBound
  }

  /// Tests whether the given value is greater than or equal to the specified lower bound.
  let beGreaterThanOrEqualTo<'T when 'T :> IComparable<'T>> (lowerBound: 'T) (value: Async<'T>) = async {
    let! value = value
    return value |> Should.beGreaterThanOrEqualTo<'T> lowerBound
  }

  /// Tests whether the given value is less than the specified upper bound.
  let beLessThan<'T when 'T :> IComparable<'T>> (upperBound: 'T) (value: Async<'T>) = async {
    let! value = value
    return value |> Should.beLessThan<'T> upperBound
  }

  /// Tests whether the given value is less than or equal to the specified upper bound.
  let beLessThanOrEqualTo<'T when 'T :> IComparable<'T>> (upperBound: 'T) (value: Async<'T>) = async {
    let! value = value
    return value |> Should.beLessThanOrEqualTo<'T> upperBound
  }

  /// Tests whether the specified object is `null`.
  let beNull (value: Async<objnull>) = async {
    let! value = value
    return Should.beNull value
  }

  /// Tests whether the specified object is exactly an instance of the expected type.
  let beOfType<'T> (value: Async<'T>) = async {
    let! value = value
    return value |> Should.beOfType<'T>
  }

  /// Tests whether the specified value is contained in the given sequence.
  let beOneOf<'T> (sequence: 'T seq) (value: Async<'T>) = async {
    let! value = value
    return value |> Should.beOneOf<'T> sequence
  }

  /// Tests whether two sequences contain equal elements in the same order.
  let beSequence<'T> (expected: 'T seq) (actual: Async<'T seq>) = async {
    let! actual = actual
    return actual |> Should.beSequence<'T> expected
  }

  /// Tests whether the specified condition is true.
  let beTrue (condition: Async<bool>) = async {
    let! condition = condition
    return Should.beTrue condition
  }

  /// Tests whether the specified collection contains the given element.
  let contain<'T> (expected: 'T) (collection: Async<'T seq>) = async {
    let! collection = collection
    return collection |> Should.contain<'T> expected
  }

  /// Tests whether the specified string contains the given substring.
  let containString (substring: string) (value: Async<string>) = async {
    let! value = value
    return value |> Should.containString substring
  }

  /// Tests whether the specified string ends with the given suffix.
  let endWith (expectedSuffix: string) (value: Async<string>) = async {
    let! value = value
    return value |> Should.endWith expectedSuffix
  }

  /// Tests whether the specified string matches the given regular expression.
  let matchRegex (pattern: Regex) (value: Async<string>) = async {
    let! value = value
    return value |> Should.matchRegex pattern
  }

  /// Tests whether the specified values are unequal.
  let notBe<'T> (notExpected: 'T) (actual: Async<'T>) = async {
    let! actual = actual
    return actual |> Should.notBe<'T> notExpected
  }

  /// Tests whether the specified object is not `null`.
  let notBeNull (value: Async<objnull>) = async {
    let! value = value
    return Should.notBeNull value
  }

  /// Tests whether the specified collection does not contain the given element.
  let notContain<'T> (expected: 'T) (collection: Async<'T seq>) = async {
    let! collection = collection
    return collection |> Should.notContain<'T> expected
  }

  /// Tests whether the specified string does not contain the given substring.
  let notContainString (substring: string) (value: Async<string>) = async {
    let! value = value
    return value |> Should.notContainString substring
  }

  /// Tests whether the specified string does not match the given regular expression.
  let notMatchRegex (pattern: Regex) (value: Async<string>) = async {
    let! value = value
    return value |> Should.notMatchRegex pattern
  }

  /// Tests whether the specified string begins with the given prefix.
  let startWith (expectedPrefix: string) (value: Async<string>) = async {
    let! value = value
    return value |> Should.startWith expectedPrefix
  }
