using System;

while (true)
{
    Console.Clear();
    Console.WriteLine("Choose an option:");
    Console.WriteLine("1) Is anagram?");
    Console.WriteLine("0) Exit");
    Console.Write("Select option (0-3): ");

    var choice = Console.ReadLine()?.Trim();

    switch (choice)
    {
        case "1":
            var i = 0;
            var str1 = string.Empty;
            var str2 = string.Empty;

            while (i < 2)
            {

                Console.Write($"Enter string {i + 1}: ");
                var input = Console.ReadLine() ?? string.Empty;
                if (i == 0)
                {
                    str1 = input;
                    i++;
                    continue;
                }
                else
                {
                    str2 = input;
                    bool isAnagram = IsAnagram(str1, str2);
                    Console.WriteLine(isAnagram ? "The strings are anagrams." : "The strings are not anagrams.");
                    break;
                }
            }
            break;

        case "0":
        case "q":
        case "exit":
            Console.WriteLine("Exiting...");
            return;

        default:
            Console.WriteLine("Unknown option. Please choose 0-3.");
            break;
    }

    Console.WriteLine();
    Console.Write("Press Enter to continue...");
    Console.ReadLine();
}

static bool IsAnagram(string s1, string s2)
{
    if (s1.Length != s2.Length) return false;

    var charCount = new int[256]; // Assuming ASCII characters

    foreach (var c in s1)
    {
        charCount[c]++;
    }

    foreach (var c in s2)
    {
        charCount[c]--;
        if (charCount[c] < 0) return false;
    }

    foreach (var count in charCount)
    {
        if (count != 0) return false;
    }

    return true;
}