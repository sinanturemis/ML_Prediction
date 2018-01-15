using System;
using System.Linq;

namespace PredictionApp.Common.Helpers
{
    public class RandomHelper
    {
        private static Random randomGenerator = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[randomGenerator.Next(s.Length)]).ToArray());
        }

        public static bool RandomBoolean()
        {
            var value = randomGenerator.Next();
            return (value % 2 == 0);
        }

        public static int RandomInteger(int maxValue)
        {
            return randomGenerator.Next(maxValue);
        }

        public static int RandomInteger(int minValue, int maxValue)
        {
            return randomGenerator.Next(minValue, maxValue);
        }
        public static byte RandomEnum(Type enumType)
        {
            Array values = Enum.GetValues(enumType);
            var selectedEnumValue = values.GetValue(randomGenerator.Next(values.Length));
            var selectedEnum = (int)Convert.ChangeType(selectedEnumValue, enumType);
            return 5;
        }


        public static string RandomProduct(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[randomGenerator.Next(s.Length)]).ToArray());
        }

        public static double RandomDouble(double minimum, double maximum)
        {
            return randomGenerator.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}
