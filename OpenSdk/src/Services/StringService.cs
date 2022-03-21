namespace OpenSdk.Services
{
    public static class StringService
    {
        public static string UppercaseFirst(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return string.Empty;
            }

            var a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);

            return new string(a);
        }

        public static string LowercaseFirst(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return string.Empty;
            }

            var a = s.ToCharArray();
            a[0] = char.ToLower(a[0]);

            return new string(a);
        }
    }
}
