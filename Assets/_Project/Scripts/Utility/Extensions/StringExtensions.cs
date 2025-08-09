namespace Project
{
    public static class StringExtensions
    {
        public static string ToGeneratedFieldName(this string source) =>
            $"<{source}>k__BackingField";
    }
}
