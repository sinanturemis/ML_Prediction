using System;

namespace PredictionApp.Common.Extension
{
    public static class DoubleExtensions
    {
        public static float ToGPSFormat(this double value)
        {
            return (float)Math.Round(value, 6);
        }
        public static double ToSatisfactionIndexFormat(this double value)
        {
            return Math.Round(value, 2);
        }

        public static double ToPriceFormat(this double value)
        {
            return Math.Round(value, 4);
        }



    }
}
