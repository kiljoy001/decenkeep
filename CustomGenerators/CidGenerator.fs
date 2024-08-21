namespace CustomGenerators

open FsCheck.FSharp

module CidGenerator =
    let base58Chars = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz".ToCharArray()

    // Step 1: Generate a length between 46 and 48
    let lengthGen = Gen.choose (46, 48)
    
    // Step 2: Generate an array of the specified length with elements from base58Chars
    let arrayGen length = Gen.arrayOfLength length (Gen.elements base58Chars)
    
    // Step 3: Bind the length generator to the array generator
    let cidGen = lengthGen |> Gen.bind arrayGen
    
    // Step 4: Map the character array to a string prefixed with "Qm"
    let ipfsCidGen = cidGen |> Gen.map (fun cid -> "Qm" + new System.String(cid))

    let ArbIpfsCid = Arb.fromGen ipfsCidGen
