using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ConsoleApplication7
{
    class Program
    {

        const string originalhash = "40c2085ffd1bff372e4d5d199ad61a4793c957d7ac57ac5c47cee413c3d1b1df25e9e1191c7051a4758693062622ef15abf46591e8ea955a0a08946bf1a61753344b51da71045784330cbb2e9d599322eea82829bb36149ebe04188a8e463bb926df792fec1a156ac2cceef3ba4064e80ad4c940774dc7042c9c012ac4c106c9c23e797ac6f59567189492e1a71b6c7e919c64dd21ba9ba417a1d2e9e1150eb3471230c686a1b518bb722b3fbfc13805f89fb82be562170e9f818172a61b61305909f52b2bb30dfed1bb57a9b6678edecfeed5f847f3a5d3d0f4735f3dfd81220c54c9c5c82f6d0a0667891d6c04a5b7dff13ae6d36fc6aad348f15aa25fcd87970a07fd388658e78ed9f4c4ff257c4dd825dd423c9162fb898b8f1011d97febc76bd57aa8e976eb9933421b132ec0e97a55c3929aab77e08ad47d6de14799ee1db57e8799bb99563a24f76d3fb849562485abebed945a5062a01f561d49603b4ef716dade785e31eefb8b0e192ff795b3460e8142966c6bb23614ee0b976b91375f130679c58552ecbe8bdbeacb4bbe9559d781811bdcac979cdaa223f1a6030c94decc591863d7049cee6340fded381564105c324c773e035d9d4e3e5e3c4f";
        const string e = "a45b0e817b72b3b082574fe555112d2b"; //MD5 of my email address

        static void Main(string[] args)
        {

            int evIndex = 0;
            String[] encryptedValues = SplitInParts(originalhash, 32); //split the original hash to MD5 length. 28 results -> email length is 56
            string email = "";

            Loop(email, evIndex, encryptedValues); //Recursively loop through all permutations of two alphanumeric


            Console.ReadLine();
        }

        //Recursively loop through to test each permutation of 2 alphanumeric ( including _.@+ ) char
        public static string Loop(string email, int evIndex, String[] encryptedValues)
        {
            string myhash;  //myhash will be J from prompt
            char[] alphanumeric = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_.@+".ToArray();

            foreach (char ch1 in alphanumeric)
            {
                foreach (char ch2 in alphanumeric)
                {
                    string s = ch1.ToString() + ch2.ToString();
                    myhash = MD5(e + email + s + MD5(email + s));
                    

                    if (myhash == encryptedValues[evIndex])
                    {
                        email = email + ch1 + ch2;
                        evIndex++;
                        if (email.Length < 56)
                        {
                            Loop(email, evIndex, encryptedValues);
                        }
                        if (email.Length == 56) { Console.WriteLine(email); } //Print our answer when it's complete
                        return "found a couple characters";
                    }
                }
            }
            return "back out";
        }

        //Calculate one way MD5
        public static string MD5(string Value)
        {
            if (Value == "") return "";
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(Value);
            bs = x.ComputeHash(bs);
            x.Clear();  // dispose
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            return s.ToString();
        }


        //Split long string into pieces each of partLength
        public static String[] SplitInParts(string value, Int32 partLength)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if (partLength <= 0)
            {
                throw new ArgumentException("Part length has to be positive.", "partLength");
            }
            List<string> returnString = new List<string>();

            for (var i = 0; i < value.Length; i += partLength)
            {
                returnString.Add(value.Substring(i, Math.Min(partLength, value.Length - i)));
            }

            return returnString.ToArray();
        }
    }
}