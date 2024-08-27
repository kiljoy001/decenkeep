namespace CustomGenerators.Tests

open System
open System.Net
open FsCheck
open FsCheck.FSharp
open CustomGenerators

module HttpStatusCodeGeneratorTest =
    let testHttpStatusCodeGenerator () =
        Prop.forAll HttpStatusCodeGenerator.httpStatusCodeArbitrary (fun statusCode ->
            Enum.IsDefined(typeof<HttpStatusCode>, statusCode)
            )
    let runTests () =
        printfn "Running HttpStatusCodeGeneratorTest..."
        Check.Quick testHttpStatusCodeGenerator

