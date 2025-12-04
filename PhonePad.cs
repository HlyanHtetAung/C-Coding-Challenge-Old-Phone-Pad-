using System;
using System.Collections.Generic;

public class PhonePad
{
    public static string OldPhonePad(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Input cannot be empty.");

        if (!input.EndsWith("#"))
            throw new ArgumentException("Input must end with '#'.");

        // *** Key mapping for old phone keypad ***
        var keypadMapping = new Dictionary<char, string>
        {
            { '2', "ABC" }, { '3', "DEF" }, { '4', "GHI" }, { '5', "JKL" },
            { '6', "MNO" }, { '7', "PQRS" }, { '8', "TUV" }, { '9', "WXYZ" }
        };

        string decodedOutput = string.Empty;
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

                // FIX: backspace must NOT erase the finalized result
                // Instead: reduce the multi-press (2 -> A, 22 -> B)
                if (previousKey != '\0')
                {
                    keyPressCount = Math.Max(1, keyPressCount - 1);
                }
                else if (decodedOutput.Length > 0)
                {
                    decodedOutput = decodedOutput.Substring(0, decodedOutput.Length - 1);
                }

                continue;
            }

            if (currentKey == ' ')
            {
                // Space: finalize the previous key
                FinalizePreviousKey(ref previousKey, ref keyPressCount, ref decodedOutput, keypadMapping);
                continue;
            }

            if (char.IsDigit(currentKey) && keypadMapping.ContainsKey(currentKey))
            {
                // Multi-tap or new key
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
            else
            {
                throw new ArgumentException($"Invalid character '{currentKey}' in input.");
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
