using System;
using System.Collections.Generic;

namespace CodingChallenge
{
    public static class PhonePad
    {
        public static string OldPhonePad(string input)
        {
            if (string.IsNullOrEmpty(input))
                return "";

            if (!input.EndsWith("#"))
                return ""; // tests want safe return, not exception

            var keypadMapping = new Dictionary<char, string>
            {
                { '2', "ABC" }, { '3', "DEF" }, { '4', "GHI" }, { '5', "JKL" },
                { '6', "MNO" }, { '7', "PQRS" }, { '8', "TUV" }, { '9', "WXYZ" }
            };

            string decodedOutput = "";
            char previousKey = '\0';
            int keyPressCount = 0;

            foreach (char currentKey in input)
            {
                if (currentKey == '#')
                {
                    FinalizePreviousKey(ref previousKey, ref keyPressCount, ref decodedOutput, keypadMapping);
                    break;
                }

                if (currentKey == '*')
                {
                    // Delete last character group OR last output letter
                    if (previousKey != '\0')
                    {
                        previousKey = '\0';
                        keyPressCount = 0;
                    }
                    else if (decodedOutput.Length > 0)
                    {
                        decodedOutput = decodedOutput[..^1];
                    }

                    continue;
                }

                if (currentKey == ' ')
                {
                    FinalizePreviousKey(ref previousKey, ref keyPressCount, ref decodedOutput, keypadMapping);
                    continue;
                }

                if (char.IsDigit(currentKey) && keypadMapping.ContainsKey(currentKey))
                {
                    if (currentKey == previousKey)
                    {
                        keyPressCount++;
                    }
                    else
                    {
                        FinalizePreviousKey(ref previousKey, ref keyPressCount, ref decodedOutput, keypadMapping);
                        previousKey = currentKey;
                        keyPressCount = 1;
                    }
                }
            }

            return decodedOutput;
        }

        private static void FinalizePreviousKey(ref char previousKey, ref int keyPressCount, ref string decodedOutput, Dictionary<char, string> keypadMapping)
        {
            if (previousKey != '\0')
            {
                decodedOutput += GetLetter(previousKey, keyPressCount, keypadMapping);
                previousKey = '\0';
                keyPressCount = 0;
            }
        }

        private static char GetLetter(char key, int pressCount, Dictionary<char, string> keypadMapping)
        {
            string letters = keypadMapping[key];
            int index = (pressCount - 1) % letters.Length;
            return letters[index];
        }
    }
}
