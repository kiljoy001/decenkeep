namespace CustomGenerators.Tests

open FsCheck
open FsCheck.FSharp
open CustomGenerators

module ByteArrayGeneratorTest =

    // Example property test to ensure byte arrays are of valid length
    let prop_testByteArray (bytes: byte[]) =
        bytes.Length >= 1 && bytes.Length <= 100 // Check the length of the array

    // Run the property test
    let runTests () =
        Check.QuickThrowOnFailure (Prop.forAll ByteArrayGenerator.byteArrayGen prop_testByteArray)