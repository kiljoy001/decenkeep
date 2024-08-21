namespace CustomGenerators

open FsCheck.FSharp

module ByteArrayGenerator =
   let byteGenerator =
       Gen.choose(1, 100)
       |> Gen.bind (fun size ->
                Gen.arrayOfLength size (Gen.choose (0, 255) |> Gen.map byte ))
   let byteArrayGen = Arb.fromGen byteGenerator