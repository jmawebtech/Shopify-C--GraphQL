namespace ShopifyGraphQL
{
    public class ShopifyDateFormatHelper
    {
        /// <summary>
        /// The integration will transform the time to the site local timezone before transferring it to Shopify.
        /// There is no need to send UTC time and a date time offset.
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        public string MakeISO8601Date(DateTime dt, string timeZone)
        {
            if (string.IsNullOrEmpty(timeZone))
                timeZone = "UTC";

            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
            DateTimeOffset dateTimeOffSet = new DateTimeOffset(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, tz.BaseUtcOffset);
            return $"{dateTimeOffSet:s}";
        }
    }
}
