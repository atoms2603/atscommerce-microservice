using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using TimeZoneConverter;

namespace AtsCommerce.Core.Common;

public class Utilities
{
    public class CryptoHelper
    {
        public static string AES128_Encrypt(string Value, string Password)
        {
            byte[] Buffer = Encoding.Unicode.GetBytes(Value);

            using (ICryptoTransform transform = GetAES128Transform(Password, false))
            {
                byte[] encyptedBlob = transform.TransformFinalBlock(Buffer, 0, Buffer.Length);
                return Convert.ToBase64String(encyptedBlob);
            }
        }

        public static string AES128_Decrypt(string Value, string Password)
        {
            byte[] Buffer = Convert.FromBase64String(Value);

            using (ICryptoTransform transform = GetAES128Transform(Password, true))
            {
                byte[] decyptedBlob = transform.TransformFinalBlock(Buffer, 0, Buffer.Length);
                return Encoding.Unicode.GetString(decyptedBlob);
            }
        }

        private static ICryptoTransform GetAES128Transform(string password, bool AsDecryptor)
        {
            const int KEY_SIZE = 16;
            var sha256CryptoServiceProvider = new SHA256CryptoServiceProvider();
            var hash = sha256CryptoServiceProvider.ComputeHash(Encoding.Unicode.GetBytes(password));
            var key = new byte[KEY_SIZE];
            var iv = new byte[KEY_SIZE];
            Buffer.BlockCopy(hash, 0, key, 0, KEY_SIZE);
            //Buffer.BlockCopy(hash, KEY_SIZE, iv, 0, KEY_SIZE); // On the Windows side, the IV is always 0 (zero)

            if (AsDecryptor)
                return new AesCryptoServiceProvider().CreateDecryptor(key, iv);
            else
                return new AesCryptoServiceProvider().CreateEncryptor(key, iv);
        }

        public static string GetHashPassword(string password)
        {

            // 1.-Create the salt value with a cryptographic PRNG
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[20]);

            // 2.-Create the RFC2898DeriveBytes and get the hash value
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            // 3.-Combine the salt and password bytes for later use
            byte[] hashBytes = new byte[40];
            Array.Copy(salt, 0, hashBytes, 0, 20);
            Array.Copy(hash, 0, hashBytes, 20, 20);

            // 4.-Turn the combined salt+hash into a string for storage
            string hashPass = Convert.ToBase64String(hashBytes);

            return hashPass;
        }

        public static bool VerifyHashedPassword(string password, string hashPass)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hashPass))
            {
                return false;
            }

            try
            {
                // Extract the bytes
                byte[] hashBytes = Convert.FromBase64String(hashPass);
                // Get the salt
                byte[] salt = new byte[20];
                Array.Copy(hashBytes, 0, salt, 0, 20);
                // Compute the hash on the password the user entered
                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
                byte[] hash = pbkdf2.GetBytes(20);
                // compare the results
                for (int i = 0; i < 20; i++)
                {
                    if (hashBytes[i + 20] != hash[i])
                    {
                        return false;
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public static class EnumHelper
    {
        public static string GetName<T>(int value)
        {
            return Enum.GetName(typeof(T), value);
        }

        public static T TryGetValue<T>(string name) where T : struct, IConvertible
        {
            Type enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                throw new Exception("T must be an Enumeration type.");
            }
            return Enum.TryParse(name, true, out T val) ? val : default;
        }

        public static bool IsValid<T>(string name) where T : struct, IConvertible
        {
            try
            {
                return Enum.TryParse(name, true, out T val);
            }
            catch
            {
                return false;
            }
        }
    }

    public static class StringHelper
    {
        public static string ReplaceVariable(string input, Dictionary<string, string> messageParams)
        {
            if (messageParams == null || !messageParams.Any() || string.IsNullOrEmpty(input))
                return input;

            foreach (var messageParam in messageParams)
            {
                input = input.Replace("{{" + messageParam.Key + "}}", messageParam.Value ?? "");
            }

            return input;
        }

        public static string RandomString(int length = 5)
        {
            string valid = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder sb = new StringBuilder();
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    _ = sb.Append(valid[(int)(num % (uint)valid.Length)]);
                }
            }
            return sb.ToString();
        }

        public static string RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max).ToString();
        }

        public static int CountWords(string s)
        {
            s = RemoveHtmlTag(s);
            MatchCollection collection = Regex.Matches(s, @"[\S]+");
            return collection.Count;
        }

        public static string RemoveHtmlTag(string input)
        {
            return Regex.Replace(input, "<.*?>", string.Empty);
        }

        public static string NormalizeKeyword(string keyword)
        {
            // Remove space around string
            keyword = keyword.Trim();

            // Remove repeat space to single space
            keyword = Regex.Replace(keyword, @" +", " ");
            return keyword;
        }

        public static string ReplaceNewLineCharacter(string input)
        {
            if (input == null) return null;
            return input.Replace(Environment.NewLine, "<br>")
                .Replace("\n", "<br>");
        }
    }

    public class FileHelper
    {
        public static bool IsImageFile(string contentType)
        {
            switch (contentType.ToLower())
            {
                case "image/apng":
                case "image/heic":
                case "image/gif":
                case "image/jpeg":
                case "image/png":
                    return true;
                default:
                    return false;
            }
        }
    }

    public static class DateTimeHelper
    {
        public static DateTime ConvertEpochToDatetime(long epochSeconds)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(epochSeconds);
            return dateTimeOffset.DateTime;
        }

        public static DateTime ConvertUtcToLocalTime(DateTime dateTime, string destinationTZ)
        {
            var timeZoneInfo = TZConvert.GetTimeZoneInfo("Asia/SaiGon");
            try
            {
                timeZoneInfo = TZConvert.GetTimeZoneInfo(destinationTZ);
            }
            catch { }

            DateTimeOffset dateTimeOffset = dateTime;
            var isDaylightSavingTime = timeZoneInfo.IsDaylightSavingTime(dateTimeOffset);

            var hourOffset = timeZoneInfo.BaseUtcOffset.Hours;

            if (isDaylightSavingTime)
                hourOffset += 1;

            return dateTime.AddHours(hourOffset);
        }

        public static DateTime ConvertToUTC(DateTime dateTime, string sourceTimeZone)
        {
            var timeZoneInfo = TZConvert.GetTimeZoneInfo("Asia/SaiGon");
            try
            {
                timeZoneInfo = TZConvert.GetTimeZoneInfo(sourceTimeZone);
            }
            catch { }

            DateTimeOffset dateTimeOffset = dateTime;
            var isDaylightSavingTime = timeZoneInfo.IsDaylightSavingTime(dateTimeOffset);

            var hourOffset = timeZoneInfo.BaseUtcOffset.Hours;

            if (isDaylightSavingTime)
                hourOffset += 1;

            return dateTime.AddHours(-hourOffset);
        }
    }

    public static class UrlHelper
    {
        public static string GenerateThumbnailImage(string imageUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(imageUrl))
                    return imageUrl;
                Uri uri = new Uri(imageUrl);
                string filename = Path.GetFileName(uri.LocalPath);
                return imageUrl.Replace(filename, $"thumbnail_{filename}");
            }
            catch (Exception)
            {
                return imageUrl;
            }
        }
    }

    public static class CalculatorHelper
    {
        public static double CalculatePercent(int amount, int total)
        {
            var percent = total != 0 ? amount * 1.0 / total * 100 : amount * 1.0 / 1 * 100;
            return percent;
        }

        public static int GetAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;

            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;

            return (a - b) / 10000;
        }
    }
}

