using System.Text.RegularExpressions;

namespace ProjectIT.Client.Shared.Helpers;

public static class HTMLTags
{
    private static readonly Dictionary<string, string> _htmlEntitiesTable = new()
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

    public static string RemoveFromText(string text)
    {
        // Check for null or empty input
        if (string.IsNullOrEmpty(text))
        {
            // Return an empty string or handle it based on your requirements
            return string.Empty;
        }

        // Remove HTML tags
        var strippedString = Regex.Replace(text, "<[^>]*>", " ");

        // Replace HTML entities
        foreach (var (key, val) in _htmlEntitiesTable)
        {
            strippedString = strippedString.Replace(key, val);
        }

        return strippedString;
    }
}
