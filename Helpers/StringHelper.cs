using System.Globalization;
using System.Text.RegularExpressions;

public static class StringHelper
    {
    public static string CapitalizeEachWord(string input)
        {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
        return textInfo.ToTitleCase(input.ToLower());
        }

    public static string FilterQuery(string query)
        {

        HashSet<string> Stopwords = new HashSet<string>
    {
        "a", "an", "and", "are", "as", "at", "be", "but", "by", "for", "if", "in", "into",
        "is", "it", "no", "not", "of", "on", "or", "such", "that", "the", "their", "then",
        "there", "these", "they", "this", "to", "was", "will", "with", "you", "your", "from",
        "up", "down", "can", "could", "should", "would", "have", "has", "had", "do", "does",
        "did", "just", "so", "than", "too", "very", "also", "about", "after", "again", "before",
        "because", "been", "being", "both", "during", "each", "few", "he", "her", "here", "him",
        "his", "how", "i", "me", "my", "our", "ours", "she", "some", "we", "what", "when", "where",
        "which", "who", "whom", "why", "all", "any", "more", "most", "other", "over", "under",
        "once","new", "trendy", "latest", "stylish", "fashionable", "classic", "elegant", "cute", "cool",
        "perfect", "pretty", "beautiful", "casual", "formal", "designer", "comfy", "comfortable", "simple",
        "modern", "hot", "musthave", "premium", "basic", "chic",
        "men", "women", "mens", "womens", "boys", "girls", "unisex", "kids", "ladies", "gentleman",
        "clothes", "clothing", "wear", "apparel", "outfit", "dress", "top", "bottoms", "accessory",
        "item", "stuff", "piece", "thing",
        "brand", "branded", "fashion", "collection", "wear", "edition", "line", "original", "pack", "set", "size", "packof",
        "best", "offer", "sale", "discount", "top", "hot", "trending", "deal", "limited", "exclusive", "genuine","color"
    };

        if (string.IsNullOrWhiteSpace(query))
            return string.Empty;

        // 1. To lowercase
        query = query.ToLowerInvariant();

        // 2. Remove numbers
        query = Regex.Replace(query, @"\d+", "");

        // 3. Remove special characters (keep only letters and spaces)
        query = Regex.Replace(query, @"[^a-z\s]", "");

        // 4. Reduce repeated characters (e.g., cooool → cool)
        query = Regex.Replace(query, @"(\w)\1{2,}", "$1");

        // 5. Remove stopwords
        var words = query.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var filteredWords = words.Where(w => !Stopwords.Contains(w));

        // 6. Remove duplicates
        filteredWords = filteredWords.Distinct().ToList();

        return string.Join(" ", filteredWords);
        }
    }



