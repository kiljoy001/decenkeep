namespace CustomGenerators.Tests

module TestRunner =

    [<EntryPoint>]
    let main _ =
        ByteArrayGeneratorTest.runTests ()
        CidGeneratorTest.runTests ()
        HttpStatusCodeGeneratorTest.runTests ()
        0