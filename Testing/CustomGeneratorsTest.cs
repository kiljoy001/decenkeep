using CustomGenerators;

namespace Testing;
using FsCheck.Xunit;

public class CustomGeneratorsTest
{
    [Property(Arbitrary = new[] { typeof(ByteArrayGenerator) })]
    public bool GeneratedByteArraysShouldHaveCorrectLength(byte[] bytes)
    {
        // Test that the length is within the expected range
        return bytes.Length is >= 1 and <= 100;
    }

    [Property(Arbitrary = new[] { typeof(ByteArrayGenerator) })]
    public bool GeneratedByteArraysShouldContainValidByteValues(byte[] bytes)
    {
        // Test that all values in the byte array are valid byte values
        return bytes.All(b => b is >= 0 and <= 255);
    }

    [Property(Arbitrary = new[] { typeof(CidGenerator) })]
    public bool GeneratedCidLenthIsCorrect(string cid)
    {
        return cid.Length is >= 48 and <= 50;
    }
}