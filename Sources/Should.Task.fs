namespace Belin.Should

open System
open System.Text.RegularExpressions
open System.Threading.Tasks

/// Contains asynchronous assertions for working with unit tests.
[<RequireQualifiedAccess>]
module ShouldAsync =

  /// Tests whether the specified values are equal.
  let be<'T> (expected: 'T) (actual: Task<'T>) = task {
    let! actual = actual
    return actual |> Should.be<'T> expected
  }

  /// Tests whether the specified object is an instance of the expected type.
  let beAssignableTo<'T> (value: Task<objnull>) = task {
    let! value = value
    return Should.beAssignableTo<'T> value
  }

  /// Tests whether the specified sequence is empty.
  let beEmpty<'T> (sequence: Task<'T seq>) = task {
    let! sequence = sequence
    return Should.beEmpty<'T> sequence
  }

  /// Tests whether the specified condition is false.
  let beFalse (condition: Task<bool>) = task {
    let! condition = condition
    return Should.beFalse condition
  }

  /// Tests whether the given value is greater than the specified lower bound.
  let beGreaterThan<'T when 'T :> IComparable<'T>> (lowerBound: 'T) (value: Task<'T>) = task {
    let! value = value
    return value |> Should.beGreaterThan<'T> lowerBound
  }

  /// Tests whether the given value is greater than or equal to the specified lower bound.
  let beGreaterThanOrEqualTo<'T when 'T :> IComparable<'T>> (lowerBound: 'T) (value: Task<'T>) = task {
    let! value = value
    return value |> Should.beGreaterThanOrEqualTo<'T> lowerBound
  }

  /// Tests whether the given value is less than the specified upper bound.
  let beLessThan<'T when 'T :> IComparable<'T>> (upperBound: 'T) (value: Task<'T>) = task {
    let! value = value
    return value |> Should.beLessThan<'T> upperBound
  }

  /// Tests whether the given value is less than or equal to the specified upper bound.
  let beLessThanOrEqualTo<'T when 'T :> IComparable<'T>> (upperBound: 'T) (value: Task<'T>) = task {
    let! value = value
    return value |> Should.beLessThanOrEqualTo<'T> upperBound
  }

  /// Tests whether the specified object is `null`.
  let beNull (value: Task<objnull>) = task {
    let! value = value
    return Should.beNull value
  }

  /// Tests whether the specified object is exactly an instance of the expected type.
  let beOfType<'T> (value: Task<objnull>) = task {
    let! value = value
    return Should.beOfType<'T> value
  }

  /// Tests whether the specified value is contained in the given sequence.
  let beOneOf<'T> (sequence: 'T seq) (value: Task<'T>) = task {
    let! value = value
    return value |> Should.beOneOf<'T> sequence
  }

  /// Tests whether two sequences contain equal elements in the same order.
  let beSequence<'T> (expected: 'T seq) (actual: Task<'T seq>) = task {
    let! actual = actual
    return actual |> Should.beSequence<'T> expected
  }

  /// Tests whether the specified condition is true.
  let beTrue (condition: Task<bool>) = task {
    let! condition = condition
    return Should.beTrue condition
  }

  /// Tests whether the specified collection contains the given element.
  let contain<'T> (expected: 'T) (collection: Task<'T seq>) = task {
    let! collection = collection
    return collection |> Should.contain<'T> expected
  }

  /// Tests whether the specified string contains the given substring.
  let containString (substring: string) (value: Task<string>) = task {
    let! value = value
    return value |> Should.containString substring
  }

  /// Tests whether the specified string ends with the given suffix.
  let endWith (expectedSuffix: string) (value: Task<string>) = task {
    let! value = value
    return value |> Should.endWith expectedSuffix
  }

  /// Tests whether the specified string matches the given regular expression.
  let matchRegex (pattern: Regex) (value: Task<string>) = task {
    let! value = value
    return value |> Should.matchRegex pattern
  }

  /// Tests whether the specified values are unequal.
  let notBe<'T> (notExpected: 'T) (actual: Task<'T>) = task {
    let! actual = actual
    return actual |> Should.notBe<'T> notExpected
  }

  /// Tests whether the specified object is not `null`.
  let notBeNull (value: Task<objnull>) = task {
    let! value = value
    return Should.notBeNull value
  }

  /// Tests whether the specified collection does not contain the given element.
  let notContain<'T> (expected: 'T) (collection: Task<'T seq>) = task {
    let! collection = collection
    return collection |> Should.notContain<'T> expected
  }

  /// Tests whether the specified string does not contain the given substring.
  let notContainString (substring: string) (value: Task<string>) = task {
    let! value = value
    return value |> Should.notContainString substring
  }

  /// Tests whether the specified string does not match the given regular expression.
  let notMatchRegex (pattern: Regex) (value: Task<string>) = task {
    let! value = value
    return value |> Should.notMatchRegex pattern
  }

  /// Tests whether the specified string begins with the given prefix.
  let startWith (expectedPrefix: string) (value: Task<string>) = task {
    let! value = value
    return value |> Should.startWith expectedPrefix
  }
