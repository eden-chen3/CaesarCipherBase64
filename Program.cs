using System;
using System.Text;

public class CaesarCipherBase64
{
    // 将字符串转换为Base64
    public static string ToBase64(string plainText)
    {
        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(plainTextBytes);
    }

    // 凯撒密码加密
    public static string CaesarEncrypt(string base64Text, int shift)
    {
        var chars = base64Text.ToCharArray();
        for (int i = 0; i < chars.Length; i++)
        {
            char c = chars[i];
            if (char.IsLetter(c))
            {
                char offset = char.IsLower(c) ? 'a' : 'A';
                chars[i] = (char)(((c - offset + shift) % 26) + offset);
            }
        }
        return new string(chars);
    }

    // 凯撒密码解密
    public static string CaesarDecrypt(string caesarText, int shift)
    {
        return CaesarEncrypt(caesarText, 26 - shift); // 逆向偏移可以通过正向偏移26-shift实现
    }

    // 从Base64解码回字符串
    public static string FromBase64(string base64Text)
    {
        var base64Bytes = Convert.FromBase64String(base64Text);
        return Encoding.UTF8.GetString(base64Bytes);
    }

    public static void Main()
    {
        Console.WriteLine("您想加密还是解密？" + Environment.NewLine + "1.加密" + Environment.NewLine + "2.解密");
        string Judge = Console.ReadLine();
        if (Judge == "1")
        {
            Console.WriteLine("请输入明文：");
            string originalText = Console.ReadLine();
            Console.WriteLine("请输入密钥：");
            int shift = Convert.ToInt32(Console.ReadLine()); // 偏移量;

            // 加密过程
            string base64Text = ToBase64(originalText);
            string encryptedText = CaesarEncrypt(base64Text, shift);
            Console.WriteLine($"Encrypted (Base64 + Caesar {shift}): {encryptedText}");
        }
        else if (Judge == "2")
        {
            Console.WriteLine("请输入密文：");
            string encryptedText = Console.ReadLine();
            Console.WriteLine("请输入密钥：");
            int shift = Convert.ToInt32(Console.ReadLine()); // 偏移量

            // 解密过程
            string decryptedBase64Text = CaesarDecrypt(encryptedText, shift);
            string decryptedText = FromBase64(decryptedBase64Text);
            Console.WriteLine($"Decrypted Text: {decryptedText}");
        }
        else
        {
            Console.WriteLine("无效选项");
        }

        Console.Write("按任意键退出...");
        Console.ReadKey(true);

    }
}