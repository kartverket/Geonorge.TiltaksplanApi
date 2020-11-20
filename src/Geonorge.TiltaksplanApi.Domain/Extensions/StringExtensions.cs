namespace Geonorge.TiltaksplanApi.Domain.Extensions
{
    public static class StringExtensions
    {
        public static string ToModel(this string value)
        {
            return value != string.Empty ? value : null;
        }

        public static string ToViewModel(this string value)
        {
            return value != null ? value : "";
        }
    }
}
