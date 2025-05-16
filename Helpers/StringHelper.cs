using System.Globalization;

public static class StringHelper
    {
    public static string CapitalizeEachWord(string input)
        {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
        return textInfo.ToTitleCase(input.ToLower());
        }
    }

