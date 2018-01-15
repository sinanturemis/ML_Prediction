namespace PredictionApp.Common
{
    public class Constants
    {
        public static class LatitudeRange
        {
            public const int Min = -90;
            public const int Max = 90;
        }

        public static class AgeRange
        {
            public const int Min = 10;
            public const int Max = 100;
        }

        public static class ExpectedStockAmount
        {
            public const int Min = 5;
            public const int Max = 80;
        }

        public static class LongitudeRange
        {
            public const int Min = -180;
            public const int Max = 180;
        }
        public static class SatisfactionIndex
        {
            public const int Min = 0;
            public const int Max = 1;
        }

        public static class CustomerVisitorCountRangeInGroup
        {
            public const int Min = 1;
            public const int Max = 10;
        }
        public static class CustomerVisitDurationRange
        {
            public const int Min = 1;
            public const int Max = 120;
        }
        public static class IngredentsAmount
        {
            public const int Min = 1;
            public const int Max = 3;
        }

        public static class ProductExpirationDayRange
        {
            public const int Min = 15;
            public const int Max = 1000;
        }
        public static class ProductUnitPrice
        {
            public const int Min = 1;
            public const int Max = 5000;
        }
        public static class SameProductAmountInFood
        {
            public const int Min = 1;
            public const int Max = 5;
        }
        public static class TableCountInRestaurant
        {
            public const int Min = 1;
            public const int Max = 9;
        }
    }
}
