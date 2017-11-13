namespace InquirerCS
{
    internal static class MathHelper
    {
        internal static int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }
    }
}
