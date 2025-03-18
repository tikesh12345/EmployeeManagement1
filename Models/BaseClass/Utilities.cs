using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using static ReturnClass;

/// <summary>
/// Summary description for Utilities
/// </summary>
public class Utilities
{
    private static string _numbers = "123456789";
    ReturnBool rb = new ReturnBool();
    public Utilities()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string AES_CBC_PKCS5Padding_Encryption(string data, string key)
    {
        RijndaelManaged rijndaelCipher = new RijndaelManaged();
        rijndaelCipher.Mode = CipherMode.CBC; //remember this parameter
        rijndaelCipher.Padding = PaddingMode.PKCS7; //remember this parameter

        rijndaelCipher.KeySize = 0x80;
        rijndaelCipher.BlockSize = 0x80;
        byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
        byte[] keyBytes = new byte[0x10];
        int len = pwdBytes.Length;

        if (len > keyBytes.Length)
        {
            len = keyBytes.Length;
        }

        Array.Copy(pwdBytes, keyBytes, len);
        rijndaelCipher.Key = keyBytes;
        rijndaelCipher.IV = keyBytes;
        ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
        byte[] plainText = Encoding.UTF8.GetBytes(data);

        return Convert.ToBase64String
        (transform.TransformFinalBlock(plainText, 0, plainText.Length));
    }

    public string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public string MD5Hash(string input)
    {
        StringBuilder hash = new StringBuilder();
        MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
        byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

        for (int i = 0; i < bytes.Length; i++)
        {
            hash.Append(bytes[i].ToString("x2"));
        }
        return hash.ToString();
    }
    /// <summary>
    /// Generates MD5 Hash for given string
    /// </summary>
    public string GenerateMd5Hash(string input)
    {
        MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
        byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
        StringBuilder sBuilder = new StringBuilder();
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }
        return sBuilder.ToString();
    }
    /// <summary>
    /// Compares and verifies MD5 hash of two strings
    /// </summary>
    public bool VerifyMd5Hash(string input, string hash)
    {
        string hashOfInput = GenerateMd5Hash(input);
        StringComparer comparer = StringComparer.Ordinal;

        if (0 == comparer.Compare(hashOfInput, hash))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected Random rGen = new Random();
    protected string[] strCharacters = { "A","B","C","D","E","F","G",
    "H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y",
    "Z","1","2","3","4","5","6","7","8","9","0","a","b","c","d","e","f","g","h",
    "i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z"};

    /// <summary>
    /// This method is used to generate random lowercase password with given length
    /// </summary>
    public string GenPassLowercase(int i)
    {
        int p = 0;
        string strPass = "";
        for (int x = 0; x <= i; x++)
        {
            p = rGen.Next(0, 35);
            strPass += strCharacters[p];
        }
        return strPass.ToLower();
    }

    /// <summary>
    /// This method is used to generate random uppercase password with given length
    /// </summary>
    public string GenPassWithCap(int i)
    {
        int p = 0;
        string strPass = "";
        for (int x = 0; x <= i; x++)
        {
            p = rGen.Next(0, 60);
            strPass += strCharacters[p];
        }
        return strPass.ToUpper();
    }
    /// <summary>
    /// This method is used to generate random string with given length
    /// </summary>
    public string GenRendomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public Int64 GenRendomNumber(int length)
    {
        Random rn = new Random();
        StringBuilder builder = new StringBuilder(6);
        string numberAsString = "";
        int numberAsNumber = 0;

        for (var i = 0; i < length; i++)
        {
            builder.Append(_numbers[rn.Next(0, _numbers.Length)]);
        }

        numberAsString = builder.ToString();
        numberAsNumber = int.Parse(numberAsString);
        return numberAsNumber;
    }
    public string Encrypt_AES(string clearText, string EncryptionKey)
    {
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }

    public string Decrypt_AES(string cipherText, string EncryptionKey)
    {
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }

    /// <summary>
    /// AES 265 Bit Encryption
    /// </summary>
    /// <param name="plain_text"></param>
    /// <param name="encryption_key"></param>
    /// <param name="invect"></param>
    /// <returns>cipher text</returns>
    public string Aes_Encrypt_256(string plain_text, string encryption_key, string invect)
    {
        var sToEncrypt = plain_text;
        var myRijndael = new RijndaelManaged()
        {
            Padding = PaddingMode.Zeros,
            Mode = CipherMode.CBC,
            KeySize = 256,
            BlockSize = 128
        };

        var key = Encoding.ASCII.GetBytes(encryption_key);
        var IV = Encoding.ASCII.GetBytes(invect);
        var encryptor = myRijndael.CreateEncryptor(key, IV);
        var msEncrypt = new MemoryStream();
        var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
        var toEncrypt = Encoding.ASCII.GetBytes(sToEncrypt);

        csEncrypt.Write(toEncrypt, 0, toEncrypt.Length);
        csEncrypt.FlushFinalBlock();

        var encrypted = msEncrypt.ToArray();
        return (Convert.ToBase64String(encrypted));
    }
    /// <summary>
    /// Decrypt AES Cipher Text
    /// </summary>
    /// <param name="cipher_text"></param>
    /// <param name="encryption_key"></param>
    /// <param name="invect"></param>
    /// <returns>Plain Text</returns>
    public static string Aes_Decrypt_256(string cipher_text, string encryption_key, string invect)
    {
        var sEncryptedString = cipher_text;
        var myRijndael = new RijndaelManaged()
        {
            Padding = PaddingMode.Zeros,
            Mode = CipherMode.CBC,
            KeySize = 256,
            BlockSize = 128
        };

        var key = Encoding.ASCII.GetBytes(encryption_key);
        var IV = Encoding.ASCII.GetBytes(invect);

        var decryptor = myRijndael.CreateDecryptor(key, IV);
        var sEncrypted = Convert.FromBase64String(sEncryptedString);
        var fromEncrypt = new byte[sEncrypted.Length];
        var msDecrypt = new MemoryStream(sEncrypted);
        var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);

        var strmReader = new StreamReader(csDecrypt);
        var str = strmReader.ReadToEnd();
        return str; // (Encoding.ASCII.GetString(str));
    }
    public string Encrypt_AES_WK(string clearText, string EncryptionKey)
    {
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            //encryptor.BlockSize = 64;
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }
    public string Decrypt_AES_WK(string cipherText, string EncryptionKey)
    {
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }
    public string ConvertNumbertoWords(long number)
    {
        if (number == 0) return "शून्य";
        if (number < 0) return "minus " + ConvertNumbertoWords(Math.Abs(number));
        string words = "";
        if ((number / 1000000000) > 0)
        {
            words += ConvertNumbertoWords(number / 1000000000) + " अरब ";
            number %= 1000000000;
        }
        if ((number / 10000000) > 0)
        {
            words += ConvertNumbertoWords(number / 10000000) + " करोड़ ";
            number %= 10000000;
        }
        if ((number / 100000) > 0)
        {
            words += ConvertNumbertoWords(number / 100000) + " लाख ";
            number %= 100000;
        }
        if ((number / 1000) > 0)
        {
            words += ConvertNumbertoWords(number / 1000) + " हजार ";
            number %= 1000;
        }
        if ((number / 100) > 0)
        {
            words += ConvertNumbertoWords(number / 100) + " सौ ";
            number %= 100;
        }
        //if ((number / 10) > 0)  
        //{  
        // words += ConvertNumbertoWords(number / 10) + " RUPEES ";  
        // number %= 10;  
        //}  
        if (number > 0 && number < 100)
        {
            if (words != "") words += "";
            var unitsMap = new[]
            {
               "", "एक", "दो", "तीन", "चार", "पाँच", "छह", "सात", "आठ", "नौ", "दस",
            "ग्यारह", "बारह", "तेरह", "चौदह", "पन्द्रह", "सोलह", "सत्रह", "अठारह", "उन्नीस", "बीस",
            "इक्कीस", "बाईस", "तेईस", "चौबीस", "पच्चीस", "छब्बीस", "सत्ताईस", "अट्ठाईस", "उनतीस", "तीस",
            "इकतीस", "बत्तीस", "तैंतीस", "चौंतीस", "पैंतीस", "छत्तीस", "सैंतीस", "अड़तीस", "उनतालीस", "चालीस",
            "इकतालीस", "बयालीस", "तैंतालीस", "चौवालीस", "पैंतालीस", "छियालीस", "सैंतालीस", "अड़तालीस", "उनचास", "पचास",
            "इक्यावन", "बावन", "तिरेपन", "चौवन", "पचपन", "छप्पन", "सत्तावन", "अट्ठावन", "उनसठ", "साठ",
            "इकसठ", "बासठ", "तिरेसठ", "चौंसठ", "पैंसठ", "छियासठ", "सड़सठ", "अड़सठ", "उनहत्तर", "सत्तर",
            "इकहत्तर", "बहत्तर", "तिहत्तर", "चौहत्तर", "पचहत्तर", "छिहत्तर", "सतहत्तर", "अठहत्तर", "उनासी", "अस्सी",
            "इक्यासी", "बयासी", "तिरासी", "चौरासी", "पचासी", "छियासी", "सत्तासी", "अट्ठासी", "नवासी", "नब्बे",
            "इक्यानबे", "बानबे", "तिरानबे", "चौरानबे", "पंचानबे", "छियानबे", "सत्तानबे", "अट्ठानबे", "निन्यानबे"
            };
            //var tensMap = new[]
            //{
            //    "शून्य", "दस", "बीस", "तीस", "चालीस", "पचास", "साठ", "सत्तर", "अस्सी", "नब्बे"
            //};
            if (number < 100) words += unitsMap[number];
            else
            {

                if ((number % 10) > 0) words += " " + unitsMap[number % 10];
            }
        }
        return words;
    }
    /// <summary>
    /// Generates SHA256 Hash
    /// </summary>
    /// <param name="strData"></param>
    /// <returns>string</returns>
    public string GenerateSHA256FromString(string strData)
    {
        var message = Encoding.ASCII.GetBytes(strData);
        SHA256Managed hashString = new SHA256Managed();
        string hex = "";

        var hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
        {
            hex += String.Format("{0:x2}", x);
        }
        return hex;
    }
    /// <summary>
    /// Get Client IP
    /// </summary>
    /// <param name="context"></param>
    /// <param name="allowForwarded"></param>
    /// <returns></returns>
    //public static string GetRemoteIPAddress(HttpContext context, bool allowForwarded = true)
    //{
    //    if (allowForwarded)
    //    {
    //        string header = (context.Request.Headers["CF-Connecting-IP"].FirstOrDefault() ?? context.Request.Headers["X-Forwarded-For"].FirstOrDefault());
    //        if (IPAddress.TryParse(header, out IPAddress ip))
    //        {
    //            return ip.ToString(); ;
    //        }
    //    }
    //    return context.Connection.RemoteIpAddress.ToString();
    //}

    /// <summary>
    /// Get Client IP
    /// </summary>
    /// <param name="context"></param>
    /// <param name="allowForwarded"></param>
    /// <returns></returns>
    public static string GetRemoteIPAddress(HttpContext context, bool allowForwarded = true)
    {
        string root_ip = context.Request.Headers["CF-Connecting-IP"].FirstOrDefault();
        string ip_behindproxy = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        string framework_ip = null;
        if (ip_behindproxy == "" || ip_behindproxy == null)
            framework_ip = context.Connection.RemoteIpAddress.ToString();
        string client_ip = null;

        client_ip = root_ip + "," + framework_ip + ", " + ip_behindproxy;
        client_ip = client_ip.Trim().Trim(',');
        return client_ip;
    }


    /// <summary>
    /// convert date string DD-MM-YYYY to YYYY-MM-DD
    /// </summary>
    /// <param name="date"></param>
    /// <param name="separator"></param>
    /// <returns></returns>
    public static string ConvertDateDMYtoYMD(string date, string separator)
    {
        string d = date.Substring(0, 2);
        string m = date.Substring(3, 2);
        string y = date.Substring(6, 4);

        return y + separator + m + separator + d;
    }


    /// <summary>
    /// convert date string YYYY-MM-DD to DD-MM-YYYY
    /// </summary>
    /// <param name="date"></param>
    /// <param name="separator"></param>
    /// <returns></returns>
    public static string ConvertDateYMDtoDMY(string date, string separator)
    {
        string d = date.Substring(0, 2);
        string m = date.Substring(3, 2);
        string y = date.Substring(6, 4);

        return d + separator + m + separator + y;
    }

    public ReturnClass.ReturnBool ValidateMailId(string emailId)
    {
        Match match = Regex.Match(emailId, @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" + @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                      RegexOptions.IgnoreCase);

        if (!match.Success)
        {
            rb.status = false;
            rb.message = "Email Id is not valid .Format must be as - abc@gmail.com";

        }
        else
        {
            rb.status = true;
            rb.message = " ";
        }
        return rb;
    }

    public ReturnClass.ReturnBool ValidateMobile(string mobileNumber)
    {
        Regex Number = new Regex(@"^[0-9]{10}$");
        if (!Number.IsMatch(mobileNumber))
        {
            rb.status = false;
            rb.message = "Mobile Number Not Valid.Number must be of 10 digits";

        }
        else
        {
            rb.status = true;
            rb.message = " ";

        }
        return rb;
    }

    public ReturnClass.ReturnBool ValidateGst(string gstNo)
    {
        Regex gstIn = new Regex("^([0-9]{2}[a-zA-Z]{4}([a-zA-Z]{1}|[0-9]{1}).[0-9]{3}[A-Za-z]([A-Za-z]|[0-9]){3})$");
        if (!gstIn.IsMatch(gstNo))
        {
            rb.status = false;
            rb.message = "Invalid Gst Number.The GSTIN consists of 15 digits";

        }
        else
        {
            rb.status = true;
            rb.message = " ";
        }
        return rb;
    }


    //To validate password
    public ReturnClass.ReturnBool ValidatePassword(string password)
    {
        var input = password;


        if (string.IsNullOrWhiteSpace(input))
        {
            throw new Exception("Password should not be empty");
        }

        var hasNumber = new Regex(@"[0-9]+");
        var hasUpperChar = new Regex(@"[A-Z]+");
        var hasMiniMaxChars = new Regex(@".{8,15}");
        var hasLowerChar = new Regex(@"[a-z]+");
        // var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

        if (!hasLowerChar.IsMatch(input))
        {
            rb.message = "Password should contain At least one lower case letter";
            rb.status = false;
            return rb;
        }
        else if (!hasUpperChar.IsMatch(input))
        {
            rb.message = "Password should contain At least one upper case letter";
            rb.status = false;
            return rb;
        }
        else if (!hasMiniMaxChars.IsMatch(input))
        {
            rb.message = "Password should not be less than 8 or greater than 15 characters";
            rb.status = false;
            return rb;
        }
        else if (!hasNumber.IsMatch(input))
        {
            rb.message = "Password should contain At least one numeric value";
            rb.status = false;
            return rb;
        }

        //else if (!hasSymbols.IsMatch(input))
        //{
        //    rb.message = "Password should contain At least one special case characters";
        //    rb.status = false;
        //    return rb;
        //}
        else
        {
            rb.status = true;
            return rb;
        }
    }
    /// <summary>
    /// Get Current Build from appsettings.json
    /// </summary>
    /// <returns></returns>
    public ReturnClass.ReturnBool GetAppSettings(string rootNode, string firstChildrenNode)
    {
        var configurationBuilder = new ConfigurationBuilder();
        var projectDir = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);    // Added by NKN
        var path = Path.Combine(projectDir, "appsettings.json");                            // Added by NKN
        //var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"); // Commented by NKN
        configurationBuilder.AddJsonFile(path, false);

        var root = configurationBuilder.Build();

        ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
        try
        {
            rb.message = root.GetSection(rootNode).GetSection(firstChildrenNode).Value;
            rb.status = true;
        }
        catch (Exception ex)
        {
            rb.error = ex.ToString();
        }
        return rb;
    }
}

public static class StreamExtensions
{
    public static byte[] ReadAllBytes(this Stream instream)
    {
        if (instream is MemoryStream)
            return ((MemoryStream)instream).ToArray();

        using (var memoryStream = new MemoryStream())
        {
            instream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}