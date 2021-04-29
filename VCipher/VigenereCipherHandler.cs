using System.Collections.Generic;
using System.Text;

namespace VCipher
{
    public class VigenereCipherHandler
    {
        static string alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        string text;
        string key;
        string textEdited = "";
        public VigenereCipherHandler(string text, string key)
        {
            this.text = text;
            this.key = key;
        }

        public string DecodeText()
        {
            Queue<int> notNeededLettersPosition = new Queue<int>();
            Queue<char> notNeededLetters = new Queue<char>();
            for (int i = 0; i < text.Length; i++)
            {
                if (alphabet.Contains(text[i].ToString()))
                {
                    textEdited += text[i];
                }
                else
                {
                    notNeededLetters.Enqueue(text[i]);
                    notNeededLettersPosition.Enqueue(i);
                }
            }
            StringBuilder textDecoded = new StringBuilder();
            textDecoded.Append(Decode(textEdited, key));
            while (notNeededLetters.Count > 0)
            {
                textDecoded.Insert(notNeededLettersPosition.Dequeue(), notNeededLetters.Dequeue().ToString());
            }
            return textDecoded.ToString();
        }
        public string EncodeText()
        {
            Queue<int> notNeededLettersPosition = new Queue<int>();
            Queue<char> notNeededLetters = new Queue<char>();
            for (int i = 0; i < text.Length; i++)
            {
                if (alphabet.Contains(text[i].ToString()))
                {
                    textEdited += text[i];
                }
                else
                {
                    notNeededLetters.Enqueue(text[i]);
                    notNeededLettersPosition.Enqueue(i);
                }
            }
            StringBuilder textEncoded = new StringBuilder();
            textEncoded.Append(Encode(textEdited, key));
            while (notNeededLetters.Count > 0)
            {
                textEncoded.Insert(notNeededLettersPosition.Dequeue(), notNeededLetters.Dequeue().ToString());
            }
            return textEncoded.ToString();
        }
        private static string Decode(string text, string key)
        {
            StringBuilder decodedText = new StringBuilder();
            int[] keyLettersIndexes = new int[key.Length];
            for (int i = 0; i < key.Length; i++)
            {
                keyLettersIndexes[i] = alphabet.IndexOf(key[i]);
            }
            for (int i = 0; i < text.Length; i++)
            {
                decodedText.Append(alphabet[(alphabet.IndexOf(text[i]) + alphabet.Length - keyLettersIndexes[i % key.Length]) % alphabet.Length]);
            }
            return decodedText.ToString();
        }
        private static string Encode(string text, string key)
        {
            StringBuilder encodedText = new StringBuilder();
            int[] keyLettersIndexes = new int[key.Length];
            for (int i = 0; i < key.Length; i++)
            {
                keyLettersIndexes[i] = alphabet.IndexOf(key[i]);
            }
            for (int i = 0; i < text.Length; i++)
            {
                encodedText.Append(alphabet[(alphabet.IndexOf(text[i]) + keyLettersIndexes[i % key.Length]) % alphabet.Length]);
            }
            return encodedText.ToString();
        }
    }
}
