namespace Steganographer.Helpers
{
    internal static class BinaryHelper
    {
        public static List<int> GetBits(string str)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
            List<int> bits = [];

            for (int i = 0; i < bytes.Length; i++)
            {
                for (int x = 7; x >= 0; x--)
                {
                    bits.Add((bytes[i] >> x) & 1);
                }
            }

            return bits;
        }
    }
}
