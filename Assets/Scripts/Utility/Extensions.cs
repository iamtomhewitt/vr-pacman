public static class Extensions
{
    public static bool IsBetween(this int item, int min, int max)
    {
        return item <= max && item >= min;
    }
}