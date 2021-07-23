namespace SpecFlowTests.Utils
{
    public static class StringExtensions
    {
        public static string TrimToNull(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return null;
            return text.Trim();
        }
    }
}