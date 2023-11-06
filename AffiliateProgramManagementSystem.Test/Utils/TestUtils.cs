namespace AffiliateProgramManagementSystem.Test.Utils;

/// <summary>
/// Helper methods class
/// </summary>
public class TestUtils
{
    private static Random random = new Random();

    /// <summary>
    /// Generates a random string of a given length.
    /// </summary>
    public static string RandomString(int length = 50)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
