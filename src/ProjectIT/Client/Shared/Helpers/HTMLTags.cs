using System.Text.RegularExpressions;

namespace ProjectIT.Client.Shared.Helpers
{
    public class HTMLTags
    {
        private readonly Dictionary<string, string> _htmlEntitiesTable = new()
        {
            { "&nbsp;", " " },
            { "&amp;", "&" },
            { "&quot;", "\"" },
            { "&apos;", "'" },
            { "&lt;", "<" },
            { "&gt;", ">" },
            { "&cent;", "¢" },
            { "&pound;", "£" },
            { "&yen;", "¥" },
            { "&euro;", "€" },
            { "&copy;", "©" },
            { "&reg;", "®" },
            { "&trade;", "™" },
            { "&times;", "×" },
            { "&divide;", "÷" }
        };

        public string RemoveFromText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException(nameof(text), "Input text cannot be null or empty.");
            }

            try
            {
                // Remove HTML tags
                var strippedString = Regex.Replace(text, "<[^>]*>", " ");

                // Replace HTML entities
                foreach (var (key, val) in _htmlEntitiesTable)
                {
                    strippedString = strippedString.Replace(key, val);
                }

                return strippedString;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw new Exception("Error occurred while processing HTML entities and tags.", ex);
            }
        }
    }
}
