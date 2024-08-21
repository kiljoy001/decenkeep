namespace Testing;

using System;
using System.Threading.Tasks;
using BCrypt.Net;
using DecenKeep;
using FsCheck;
using FsCheck.Xunit;
using Xunit;

public class UserSecretServiceTests
{
    private readonly UserSecretService _userSecretService;

    public UserSecretServiceTests()
    {
        _userSecretService = new UserSecretService();
    }

    [Property]
    public void HashUserSecret_ShouldReturnHashOfCorrectLength(string input)
    {
        var hash = _userSecretService.HashUserSecret(input).Result;
        // Ensuring the hash is 60 characters long as per BCrypt standard
        Assert.Equal(60, hash.Length);
    }

    [Property]
    public void HashAndVerifyUserSecret_ShouldReturnTrueForValidSecret(string input)
    {
        var hash = _userSecretService.HashUserSecret(input).Result;
        var isVerified = _userSecretService.VerifyUserSecret(input, hash).Result;
        Assert.True(isVerified);
    }

    [Property]
    public void VerifyUserSecret_ShouldReturnFalseForInvalidSecret(string input, string invalidInput)
    {
        if (input == invalidInput) return; // Ensure invalidInput is different from input
        
        var hash = _userSecretService.HashUserSecret(input).Result;
        var isVerified = _userSecretService.VerifyUserSecret(invalidInput, hash).Result;
        Assert.False(isVerified);
    }
}
