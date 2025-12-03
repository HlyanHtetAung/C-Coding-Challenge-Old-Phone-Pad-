using System;

class Program
{
    public static void Main()
    {
        Console.WriteLine("*** Old Phone Pad Decoder ***");
        Console.WriteLine("Type your input ending with '#' or 'exit' to quit.\n");

        while (true)
        {
            Console.Write("Input: ");
            string? userInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userInput))
                continue;

            if (userInput.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Exiting application...");
                break;
            }

            try
            {
                string decodedText = PhonePad.OldPhonePad(userInput);
                Console.WriteLine("Output: " + decodedText);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
