static bool ProcessLine(string line, out bool isValidReport)
{
    isValidReport = true;
    bool problemDampener = true; // Allows ignoring the first failure
    string[] tokens = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

    if (tokens.Length < 2)
    {
        isValidReport = false; // Not enough data to process
        return false;
    }

    bool ascending = int.Parse(tokens[1]) > int.Parse(tokens[0]); // Determine sequence trend

    for (int i = 1; i < tokens.Length; i++)
    {
        // Validate and parse current and previous tokens
        if (!int.TryParse(tokens[i], out int current) || !int.TryParse(tokens[i - 1], out int previous))
        {
            isValidReport = false; // Invalid input
            return false;
        }

        int diff = current - previous;

        // Check conditions for invalid report
        if ((ascending && (diff == 0 || diff > 3)))
        {
            if (problemDampener)
            {
                Console.WriteLine("Problem Dampener Used");
                problemDampener = false; // Use dampener for the first issue
            }
            else
            {
                isValidReport = false; // Subsequent issues invalidate the report
                break;
            }
        }
        else if ((!ascending && (diff == 0 || diff < -3)))
        {
            if (problemDampener)
            {
                Console.WriteLine("Problem Dampener Used");
                problemDampener = false; // Use dampener for the first issue
            }
            else
            {
                isValidReport = false; // Subsequent issues invalidate the report
                break;
            }
        }
    }

    return true; // Process completed successfully
}