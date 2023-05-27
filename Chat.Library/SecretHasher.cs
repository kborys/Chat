using System.Security.Cryptography;

namespace Chat.Library;

public static class SecretHasher
{
    private const int _saltSize = 16; // 128 bits
    private const int _keySize = 32; // 256 bits
    private const int _iterations = 10000;
    private static readonly HashAlgorithmName _algorithm = HashAlgorithmName.SHA256;

    private const char segmentDelimiter = ':';

    /// <summary>
    /// Creates a hash from given string in the following format: [key]:[salt]:[iterations]:[algorithm]
    /// </summary>
    /// <param name="secret">String value to be hashed</param>
    public static string Hash(string secret)
    {
        var salt = RandomNumberGenerator.GetBytes(_saltSize);
        var key = Rfc2898DeriveBytes.Pbkdf2(
                secret,
                salt,
                _iterations,
                _algorithm,
                _keySize
            );

        return string.Join(
            segmentDelimiter,
            Convert.ToBase64String(key),
            Convert.ToBase64String(salt),
            _iterations,
            _algorithm);
    }

    public static bool Verify(string secret, string hash)
    {
        var segments = hash.Trim().Split(segmentDelimiter);
        var key = Convert.FromBase64String(segments[0]);
        var salt = Convert.FromBase64String(segments[1]);
        var iterations = int.Parse(segments[2]);
        var algorithm = new HashAlgorithmName(segments[3]);

        var inputSecretKey = Rfc2898DeriveBytes.Pbkdf2(
                secret,
                salt,
                iterations,
                algorithm,
                key.Length
            );

        return key.SequenceEqual(inputSecretKey);
    }
}
