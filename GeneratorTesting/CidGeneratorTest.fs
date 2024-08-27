namespace CustomGenerators.Tests

open FsCheck
open FsCheck.FSharp
open CustomGenerators

module CidGeneratorTest =

    let base58Chars = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz".ToCharArray() |> Set.ofArray

    // Example property test to ensure CIDs are of valid length, start with "Qm", and contain only Base58 characters
    let prop_testCid (cid: string) =
        let cidBody = cid.Substring(2)
        let isValidLength = cidBody.Length >= 46 && cidBody.Length <= 48
        let hasValidPrefix = cid.StartsWith("Qm")
        let containsOnlyBase58 = cidBody.ToCharArray() |> Array.forall (fun c -> Set.contains c base58Chars)

        let isValidCid = isValidLength && hasValidPrefix && containsOnlyBase58
        
        if not isValidLength then
            failwithf "Invalid CID length: %s (body length: %d)" cid cidBody.Length
        
        if not hasValidPrefix then
            failwithf "Invalid CID prefix: %s" cid
        
        if not containsOnlyBase58 then
            let invalidChars = cidBody.ToCharArray() |> Array.filter (fun c -> not (Set.contains c base58Chars))
            failwithf "CID contains invalid characters: %s (invalid chars: %A)" cid invalidChars
        
        isValidCid

    // Run the property test
    let runTests () =
        printfn "Running CidGeneratorTest..."
        Check.QuickThrowOnFailure (Prop.forAll CidGenerator.ArbIpfsCid prop_testCid)