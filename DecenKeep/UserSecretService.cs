namespace DecenKeep
{
    using BCrypt.Net;

    public class UserSecretService: ISecretHash
    {
        public async Task<string> HashUserSecret(string input)
        {
            return await Task.Run(() => BCrypt.EnhancedHashPassword(input));
        }

        public async Task<bool> VerifyUserSecret(string input, string hashedSecret)
        {
            return await Task.Run(() => BCrypt.EnhancedVerify(input, hashedSecret));
        }
    }
}