namespace Application.Helpers
{
    public static class GenerateVerificationCodeHelper
    {
        public static string GenerateVerificationCode()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}
