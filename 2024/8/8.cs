string filePath = "input.txt";

Dictionary<int, (int, int)> fileValues = new Dictionary<int, (int, int)>();
List<char> diskCompressed = new List<char>();
List<char> diskUncompressed = new List<char>();
List<char> diskUncompressedNoDots = new List<char>();
List<int> finalCompressed = new List<int>();
List<int> sums = new List<int>();

part1();



void processData()
{
    try
    {
        using (StreamReader reader = new StreamReader(filePath))
        {
            int currentChar;
            int idCount = 0;
            List<int> values = new List<int>();

            while ((currentChar = reader.Read()) != -1)
            {
                if (!char.IsDigit((char)currentChar)) continue; // Skip non-digit characters

                int digitValue = currentChar - '0';
                values.Add(digitValue);

                // If two values have been collected, create a pair and reset for the next
                if (values.Count == 2)
                {
                    fileValues.Add(idCount, (values[0], values[1]));
                    idCount++;
                    values.Clear();
                }
            }

            // Handle the case where there is an odd number of digits
            if (values.Count == 1)
            {
                fileValues.Add(idCount, (values[0], 0)); // Pad with 0
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }

    Console.WriteLine("Part 1");

    foreach (var kvp in fileValues)
    {
        int keyvalue = kvp.Key;
        
        // add key '0' however many times item 1 is
        for (int i = 0; i < kvp.Value.Item1; i++)
        {
            diskUncompressed.Add((char)(keyvalue + '0'));
            diskUncompressedNoDots.Add((char)(keyvalue + '0'));
        }

        for (int i = 0; i < kvp.Value.Item2; i++)
        {
            diskUncompressed.Add('.');
        }
    }
}

void compressFile()
{
    int j = diskUncompressedNoDots.Count - 1;
    int numbersCount = diskUncompressedNoDots.Count;
    int dotCount = diskUncompressed.Count - numbersCount;

    for (int i = 0; i < diskUncompressed.Count-dotCount; i++)
    {
        if (diskUncompressed[i] == '.')
        {
            diskCompressed.Add(diskUncompressedNoDots[j]);
            j--;
            numbersCount--;
        }

        if (diskUncompressed[i] != '.')
        {
            diskCompressed.Add(diskUncompressed[i]);
        }

        if (numbersCount == 0)
        {
            break;
        }
    }
    Console.WriteLine("It worked!");
}

void adjustFile()
{
    for (int i = 0; i < diskCompressed.Count; i++)
    {
        int numericValue = diskCompressed[i] - '0';
        finalCompressed.Add(numericValue);
    }
}

void runCheckSum()
{
    long finalSum = 0;
    for (int i = 0; i < finalCompressed.Count; i++)
    {
        int sum = finalCompressed[i] * i;
        sums.Add(sum);
    }

    foreach (int sum in sums)
    {
        finalSum += sum;
    }
    Console.WriteLine(finalSum);
}

void part1()
{
    processData();
    compressFile();
    adjustFile();
    runCheckSum(); 
}


//Part 2

