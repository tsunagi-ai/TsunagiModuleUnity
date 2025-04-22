using System;

namespace TsunagiModule.Goap
{
    /// <summary>
    /// Convert values for GOAP inner calculation
    /// </summary>
    public static class ValueConverter
    {
        public static float ToFloat(int value)
        {
            return value;
        }

        public static float ToFloat(bool value)
        {
            return value ? 1 : 0;
        }

        public static int ToInt(float value)
        {
            return (int)Math.Round(value);
        }

        public static bool ToBool(float value)
        {
            return value >= 0.5f;
        }
    }
}
