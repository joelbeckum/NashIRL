namespace NashIRL.Utils
{
    public static class StringExtensions
    {
        public static string Truncate(this string inputString, int maxLength)
        {
            if (string.IsNullOrEmpty(inputString))
            {
                return inputString;
            }

            return inputString.Length <= maxLength ? inputString : inputString.Substring(0, maxLength) + "...";
        }
    }
}
