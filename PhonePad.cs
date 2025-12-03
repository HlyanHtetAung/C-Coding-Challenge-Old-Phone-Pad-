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

        // Key mapping for old phone keypad
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
                // Finalize last key press if any
                if (previousKey != '\0')
                {
                    string letters = keypadMapping[previousKey];
                    int index = (keyPressCount - 1) % letters.Length;
                    decodedOutput += letters[index];
                }
                break;
            }

            if (currentKey == '*')
            {
                if (keyPressCount > 0)
                {
                    // Reduce current multi-tap count
                    keyPressCount--;
                    if (keyPressCount == 0)
                        previousKey = '\0'; // Reset if all presses removed
                }
                else if (decodedOutput.Length > 0)
                {
                    // Remove last finalized character
                    decodedOutput = decodedOutput.Substring(0, decodedOutput.Length - 1);
                }
                continue;
            }

            if (currentKey == ' ')
            {
                // Space: finalize current key
                if (previousKey != '\0')
                {
                    string letters = keypadMapping[previousKey];
                    int index = (keyPressCount - 1) % letters.Length;
                    decodedOutput += letters[index];
                    previousKey = '\0';
                    keyPressCount = 0;
                }
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
                    // Finalize previous key if any
                    if (previousKey != '\0')
                    {
                        string letters = keypadMapping[previousKey];
                        int index = (keyPressCount - 1) % letters.Length;
                        decodedOutput += letters[index];
                    }

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
}
