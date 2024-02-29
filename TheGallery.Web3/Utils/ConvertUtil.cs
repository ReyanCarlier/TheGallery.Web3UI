using System.Numerics;

namespace TheGallery.Web3UI.Utils
{
    public static class ConvertUtil
    {
        public static BigInteger ConvertToEpochUtcSeconds(DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return new BigInteger(0);
            }
            long epoch = (dateTime.Value.Ticks - 621355968000000000) / 10000000;
            return new BigInteger(epoch);
        }

        public static DateTime ConvertToDateTime(BigInteger? epoch)
        {
            if (epoch == null)
            {
                return DateTime.MinValue;
            }
            long ticks = (long)epoch.Value * 10000000 + 621355968000000000;
            return new DateTime(ticks, DateTimeKind.Utc);
        }

        public static BigInteger ConvertToEpochUtcMilliseconds(DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return new BigInteger(0);
            }
            DateTime utcDateTime = dateTime.Value.ToUniversalTime();
            DateTime epoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            long unixTimeStampInMilliseconds = (long)(utcDateTime - epoch).TotalMilliseconds;
            return new BigInteger(unixTimeStampInMilliseconds);
        }
    }
}
