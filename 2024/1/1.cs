namespace adventOfCodeCSharp._2024;
using System.Text.RegularExpressions;


public class Advent20241
{
    public void Run()
    {
        string filePath = "input.txt";
    
string pattern = @"(\d+)";
List<int> leftSide = new List<int>();
List<int> rightSide = new List<int>();
int sum = 0;
int similarityScore = 0;
    
    try
    {
        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            int counter = 1;
            while ((line = reader.ReadLine()) != null)
            {
                MatchCollection matches = Regex.Matches(line, pattern);
                
                //add matches to the lists
                foreach (Match match in matches)
                {
                    if (counter % 2 == 0 && int.TryParse(match.Value, out int number))
                    {
                        rightSide.Add(number);
                    }
                    else if (counter % 2 != 0 && int.TryParse(match.Value, out int number2))
                    {
                        leftSide.Add(number2);
                    }
                    else
                    {
                        Console.WriteLine("Invalid number");
                        continue;
                    }
                    counter++;
                }
                
            }
            rightSide.Sort();
            leftSide.Sort();

            for (int i = 0; i < rightSide.Count(); i++)
            {
                if (rightSide[i] > leftSide[i])
                {
                    sum += rightSide[i] - leftSide[i];
                }
                else
                {
                    sum += leftSide[i] - rightSide[i];
                }
            }
            Console.WriteLine(sum);

        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }

    for (int i = 0; i < leftSide.Count(); i++)
    {
        int rightCount = rightSide.Count(n => n == leftSide[i]);
        similarityScore += leftSide[i] * rightCount;
        
    }
    
    Console.WriteLine($"Similarity Score: {similarityScore}");
    }
}