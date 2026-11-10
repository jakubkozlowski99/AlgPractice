using AlgPractice;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;


var logs = new List<LogEntry>
            {
                new(new DateTime(2026, 10, 26, 10, 00, 00), "user1", "LOGIN"),
                new(new DateTime(2026, 10, 26, 10, 01, 00), "user2", "LOGIN"),
                new(new DateTime(2026, 10, 26, 10, 02, 00), "user1", "LOGOUT"),
                new(new DateTime(2026, 10, 26, 10, 03, 00), "user1", "LOGIN"),
                new(new DateTime(2026, 10, 26, 10, 04, 00), "user3", "LOGIN"),
                new(new DateTime(2026, 10, 26, 10, 05, 00), "user2", "LOGOUT"),
                new(new DateTime(2026, 10, 26, 10, 06, 00), "user1", "LOGOUT"),
            };

while (true)
{
    Console.Clear();
    Console.WriteLine("Choose an option:");
    Console.WriteLine("1) Is anagram?");
    Console.WriteLine("2) Find first unique character");
    Console.WriteLine("3) Count logins");
    Console.WriteLine("4) Find suspicious user");
    Console.WriteLine("5) Two Sum");
    Console.WriteLine("6) Valid Parentheses");
    Console.WriteLine("7) Move Zeroes");
    Console.WriteLine("0) Exit");
    Console.Write("Select option (0-7): ");

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

        case "2":
            Console.Write("Enter a string to find the first unique character: ");
            var inputString = Console.ReadLine() ?? string.Empty;
            FindFirstUniqueCharacter(inputString);
            break;

        case "3":

            var loginCounts = GetLoginCount(logs);

            if(loginCounts.Count == 0)
            {
                Console.WriteLine("No login entries found.");
                break;
            }

            Console.WriteLine("Login counts:");
            foreach (var kvp in loginCounts)
            {
                Console.WriteLine($"User: {kvp.Key}, Logins: {kvp.Value}");
            }
            break;
        
        case "4":
            var suspiciousUsers = GetSuspiciousUser(logs);

            if (suspiciousUsers.Count == 0)
            {
                Console.WriteLine("No suspicious users found.");
            }
            else
            {
                Console.WriteLine("Suspicious users (more than 2 logins within 5 minutes):");
                foreach (var user in suspiciousUsers)
                {
                    Console.WriteLine($"User: {user}");
                }
            }
            break;

        case "5":

            Console.Write("Enter amount of numbers you want to enter:");
            var inputAmount = Console.ReadLine() ?? string.Empty;

            int convertedAmount;

            if (Int32.TryParse(inputAmount, out var amount))
            {
                convertedAmount = amount;
            }
            else
            {
                Console.WriteLine("Wrong input, you have to enter a number.");
                break;
            }

            var numbers = new int[convertedAmount];

            Console.Write("Enter the numbers separated by spaces: ");
            var numInput = Console.ReadLine() ?? string.Empty;
            var numStrings = numInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            for (int j = 0; j < numStrings.Length; j++)
            {
                if (Int32.TryParse(numStrings[j], out var number))
                {
                    numbers[j] = number;
                }
                else
                {
                    Console.WriteLine($"Invalid input '{numStrings[j]}'. Please enter only integers.");
                    break;
                }
            }

            Console.WriteLine("Your numbers:");
            for (int j = 0; j < numbers.Length; j++)
            {
                if (j == 0) Console.Write("[");
                Console.Write(numbers[j]);
                if (j == numbers.Length - 1) Console.Write("]");
                else Console.Write(", ");
            }
            Console.WriteLine("\nEnter a target sum:");

            var targetInput = Console.ReadLine() ?? string.Empty;

            int target;

            if (Int32.TryParse(targetInput, out var targetConverted))
            {
                target = targetConverted;
            }
            else
            {
                Console.WriteLine("Wrong target input, you have to enter a number.");
                break;
            }

            var result = TwoSum(numbers, target);

            Console.WriteLine($"Indexes giving that target: {result!.Item1}, {result.Item2}");

            break;

        case "6":
            Console.Write("Enter a string to check for valid parentheses: ");
            var parenthesesInput = Console.ReadLine() ?? string.Empty;

            foreach (var c in parenthesesInput)
            {
                if (!"(){}[]".Contains(c))
                {
                    Console.WriteLine("Invalid input. Please enter a string containing only parentheses: (), {}, []");
                    break;
                }
            }

            bool isValid = isValidParentheses(parenthesesInput);
            Console.WriteLine(isValid);
            break;

        case "7":
            Console.Write("Enter the numbers separated by spaces: ");
            var numbersInput = Console.ReadLine() ?? string.Empty;
            var numberStrings = numbersInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var numberArray = new int[numberStrings.Length];
            for (int j = 0; j < numberStrings.Length; j++)
            {
                if (Int32.TryParse(numberStrings[j], out var number))
                {
                    numberArray[j] = number;
                }
                else
                {
                    Console.WriteLine($"Invalid input '{numberStrings[j]}'. Please enter only integers.");
                    break;
                }
            }
            var resultArray = MoveZeroes(numberArray);
            Console.WriteLine("Result after moving zeroes:");
            Console.WriteLine(string.Join(", ", resultArray));
            break;

        case "0":
        case "q":
        case "exit":
            Console.WriteLine("Exiting...");
            return;

        default:
            Console.WriteLine("Unknown option. Please choose 0-7.");
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

static bool FindFirstUniqueCharacter(string s)
{
    var charCount = new Dictionary<char, int>();

    foreach (var c in s)
    {
        if (charCount.ContainsKey(c))
        {
            charCount[c]++;
        }
        else
        {
            charCount[c] = 1;
        }
    }

    foreach (var c in s)
    {
        if (charCount[c] == 1)
        {
            Console.WriteLine($"The first unique character is: '{c}'");
            return true;
        }
    }
    
    Console.WriteLine("No unique character found.");
    return false;
}

static Dictionary<string, int> GetLoginCount(List<LogEntry> logs)
{
    if (logs == null || logs.Count == 0)
    {
        return new Dictionary<string, int>();
    }

    var loginCounts = new Dictionary<string, int>();

    foreach (var log in logs)
    {
        if (log.Action == "LOGIN")
        {
            if (loginCounts.ContainsKey(log.UserId))
            {
                loginCounts[log.UserId]++;
            }
            else
            {
                loginCounts[log.UserId] = 1;
            }
        }
    }

    return loginCounts;
}

static List<string> GetSuspiciousUser(List<LogEntry> logs)
{
    var loginTimestamps = new Dictionary<string, List<DateTime>>();

    foreach (var log in logs)
    {
        if (log.Action == "LOGIN")
        {
            if (!loginTimestamps.ContainsKey(log.UserId))
            {
                loginTimestamps[log.UserId] = new List<DateTime>();
            }
            loginTimestamps[log.UserId].Add(log.Timestamp);
        }
    }

    var results = new List<string>();

    foreach (var kvp in loginTimestamps)
    {
        var timestamps = kvp.Value;
        timestamps.Sort();
        for (int i = 2; i < timestamps.Count; i++)
        {
            if ((timestamps[i] - timestamps[i - 2]).TotalMinutes <= 5)
            {
                results.Add(kvp.Key);
                break;
            }
        }
    }

    return results;
}

static Tuple<int, int>? TwoSum(int[] numbers, int target)
{
    var numToIndex = new Dictionary<int, int>();

    for (int i = 0; i < numbers.Length; i++)
    {
        int missing = target - numbers[i];

        if (numToIndex.ContainsKey(missing))
        {
            var result = new Tuple<int, int>(numToIndex[missing], i);

            return result;

        }
        else numToIndex.Add(numbers[i], i);
    }
    return null; // Return null if no solution is found
}

static bool isValidParentheses(string s)
{
    var stack = new Stack<char>();

    foreach (var c in s)
    {
        if (c == '}' || c == ']' || c == ')')
        {
            if (stack.Count == 0) return false;

            var top = stack.Pop();

            if ((c == '}' && top != '{') ||
                (c == ']' && top != '[') ||
                (c == ')' && top != '('))
            {
                return false;
            }
        }
        else stack.Push(c);
    }
    if (stack.Count == 0) return true;

    return false;
}

static int[] MoveZeroes(int[] numbers)
{
    int insertIndex = 0;
    int zeroCount = 0;

    for (int i = 0; i < numbers.Length; i++)
    {
        if (numbers[i] != 0)
        {
            numbers[insertIndex] = numbers[i];
            insertIndex++;
        }
        else zeroCount++;
    }

    for (int i = 0; i < zeroCount; i++)
    {
        numbers[insertIndex + i] = 0;
    }

    return numbers;
}

