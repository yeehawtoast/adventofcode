using System.Text.RegularExpressions;

string filePath = "input.txt";
string fileContent = File.ReadAllText(filePath);


string mulPattern = @"mul\((\d+),(\d+)\)";
string dontPattern = @"don't\(\)";
string doPattern = @"do\(\)";

Regex mulRegex = new Regex(mulPattern);
Regex dontRegex = new Regex(dontPattern);
Regex doRegex = new Regex(doPattern);


bool canProcess = true;
int totalSum = 0;

int index = 0;

while (index < fileContent.Length)
{
    Match dontMatch = dontRegex.Match(fileContent, index);
    Match doMatch = doRegex.Match(fileContent, index);

    if (dontMatch.Success && dontMatch.Index == index)
    {
        canProcess = false;
        Console.WriteLine("Encountered 'don't()'. Pausing processing.");
        index += dontMatch.Length;
        continue;
    }
    else if (doMatch.Success && doMatch.Index == index)
    {
        canProcess = true;
        Console.WriteLine("Encountered 'do()'. Resuming processing.");
        index += doMatch.Length; 
        continue;
    }


    if (canProcess)
    {
        Match mulMatch = mulRegex.Match(fileContent, index);
        if (mulMatch.Success && mulMatch.Index == index)
        {
            int number1 = int.Parse(mulMatch.Groups[1].Value);
            int number2 = int.Parse(mulMatch.Groups[2].Value);
            int result = number1 * number2;
            totalSum += result;

            Console.WriteLine($"Processed: {mulMatch.Value}, Result: {result}");
            index += mulMatch.Length;
            continue;
        }
    }
    index++;
}

Console.WriteLine($"Total Sum: {totalSum}");
