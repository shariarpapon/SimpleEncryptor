using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class Encryptor : MonoBehaviour
{
    sealed class EncryptionArchive
    {
        public static char[] characters = new char[]
        {
        '~' ,  '`' ,  '!' ,  '@' ,  '#' ,  '£' ,  '€' ,  '$' ,  '¢' ,  '¥' ,  '§' ,  '%' ,  '°' ,  '^' ,  '&' ,  '*' ,  '(' ,  ')' ,  '-' ,  '_' ,
        '+' ,  '=' ,  '{' ,  '}' ,  '[' ,  ']' ,  '|' ,  '\\' ,  '/' ,  ':' ,  ';' ,  '"' ,  '<' ,  '>' ,  ',' ,  '.' ,  '?',
        'A' ,  'a' ,  'B' ,  'b' ,  'C' ,  'c' ,  'D' ,  'd' ,  'E' ,  'e' ,  'F' ,  'f' ,
        'G' ,  'g' ,  'H' ,  'h' ,  'I' ,  'i' ,  'J' ,  'j' ,  'K' ,  'k' ,  'L' ,  'l' ,
        'M' ,  'm' ,  'N' ,  'n' ,  'O' ,  'o' ,  'P' ,  'p' ,  'Q' ,  'q' ,  'R' ,  'r' ,
        'S' ,  's' ,  'T' ,  't' ,  'U' ,  'u' ,  'V' ,  'v' ,  'W' ,  'w' ,  'X' ,  'x' ,
        'Y' ,  'y' ,  'Z' , 'z', ' '
        };

        private static char[] encryptionCharacters = new char[]
        {
            'o' , '¢' , 'Q' , ',' , '}' , 'O' , ' ', 's' , 'A' , 'L' , '&' , 'K' , 'j' , '§' , '?' , 'S' , '+' , 'W' , '-' , 'M' ,
            'u' , 'n' , 'D' , 'e' , 'P' , 'a' , 'G' , '£' , 'Z' , '.' , '"' , '/' , 'p' , 'U' , '€' , ':' , '#' , 'X' , ')' ,
            '@' , '%' , 'x' , 'F' , ']' , 'V' , 'R' , '`' , 'z' , 'w' , 'E' , 'i' , 'T' , 'v' , '(' , ';' , '°' , 'm' , 'J' , '¥' ,
            '|' , '{' , '!' , '>' , 'd' , 'b' , 'H' , '\\' , 'c' , '$' , '=' , 'f' , '<' , 'h' , 't' , '^' , '~' , 'I' , 'r' , '_' ,
            'q' , 'y' , 'Y' , 'g' , '*' , 'B' , 'l' , 'N' , '[' , 'C' , 'k' , '4' , '8' , '2' , '0' , '6' , '5' , '1' , '9' , '3' , '7'
        };

        public static string EncryptContent (string input)
        {
            string output = string.Empty;

            char[] chars = input.ToCharArray();

            foreach (char c in chars)
            {
                string enc = EncryptCharacter(c);
                if (enc == null)
                {
                    return null;
                }

                output += enc + "'";
            }

            return output;
        }

        public static string DecryptContent(string input)
        {
            string output = string.Empty;

            List<string> codes = new List<string>();
            char[] enChars = input.ToCharArray();

            string codeAdder = string.Empty;

            for (int i = 0; i < enChars.Length; i++)
            {
                if (enChars[i] == '\'')
                {
                    codes.Add(codeAdder);
                    codeAdder = string.Empty;
                }
                else
                {
                    codeAdder += enChars[i];
                }
            }

            for (int i = 0; i < codes.Count; i++)
            {
                output += DecryptCharacter(codes[i]);
            }

            return output;
        }

        private static char DecryptCharacter(string input)
        {
            char output = ' ';

            int code2 = int.Parse(input);
            int code1 = code2 - encryptionCharacters.Length;
            output = encryptionCharacters[code1];

            return output;
        }

        private static string EncryptCharacter(char input)
        {
            string output = string.Empty;
            int index = -1;

            for (int i = 0; i < encryptionCharacters.Length; i++)
            {
                if (encryptionCharacters[i] == input)
                {
                    index = i;
                }
            }

            if (index == -1)
            {
                Debug.LogError("Unable to find encryption for this character : " + input);
                return null;
            }
            else
            {
                int code1 = index;
                int code2 = encryptionCharacters.Length + code1;
                output = code2.ToString();
            }

            return output;
        }
    }

    public InputField eTmp;
    public InputField dTmp;

    public void Encrypt()
    {
        string input = eTmp.text;
        string encryption = EncryptionArchive.EncryptContent(input);
        if (encryption == null)
        {
            print("Unable to encrypt content, make sure you are not using this character- ' ");
            return;
        }

        print("Succesfully Encrypted");
        eTmp.text = encryption;

     
    }

    public void Decrypt()
    {
        string input = dTmp.text;
        string decryption = EncryptionArchive.DecryptContent(input);

        print("Succesfully Decrypted");
        dTmp.text = decryption;
    }

    public void Exit()
    {
        Application.Quit();
    }

}
