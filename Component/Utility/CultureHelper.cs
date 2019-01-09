using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Component.Utility
{
    public static class CultureHelper
    {
        public static CultureType Culture { get; set; }
        public static bool IsArabic
        {
            get
            {
                return Culture == CultureType.Arabic;
            }
        }

        public static CultureType GetCulture(HttpContext context)
        {
            StringValues language;
            context.Request.Headers.TryGetValue("Accept-Language", out language);
            string cultureShort = language.ToString().Split('-')[0].ToLower();
            switch (cultureShort)
            {
                case "ar":
                    return CultureType.Arabic;
                case "en":
                    return CultureType.English;
                default:
                    return CultureType.English;
            }
        }

    }
}
