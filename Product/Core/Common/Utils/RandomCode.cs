namespace Product.Core.Utils
{
    public class RandomCode
    {
        private static readonly Random random = new();
        private static readonly string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public static string Generate()
        {
            var code = new char[5];

            for (int i = 0; i < code.Length; i++)
            {
                code[i] = characters[random.Next(characters.Length)];
            }

            return new string(code);
        }
    }

}