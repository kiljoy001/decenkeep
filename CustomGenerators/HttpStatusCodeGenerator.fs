namespace CustomGenerators

open FsCheck.FSharp
open System.Net
open System

module HttpStatusCodeGenerator =
    let httpStatusCodeGenerator =
        let allStatusCodes = Enum.GetValues(typeof<HttpStatusCode>) |> Seq.cast<HttpStatusCode> |> Seq.toList
        Gen.elements allStatusCodes
        
    let httpStatusCodeArbitrary =
        Arb.fromGen httpStatusCodeGenerator

    