using System.Text.RegularExpressions;

string filePath = "input.txt";

string horzInputNorm = "(XMAS)";
string horzInputReverse = "(SAMX)";
int totalXmas = 0;
int totalCrossMAS = 0;
List<char[]> fileinput = new List<char[]>();

//error debugging, remove when done with puzzle
int OnLine = 1;
int CharNum = 1;

Regex horzInputRegex = new Regex(horzInputNorm);
Regex horzInputReverseRegex = new Regex(horzInputReverse);

RunPart1();
RunPart2();

void RunPart1()
{
    try
    {
        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                {
                    totalXmas += horzInputRegex.Count(line);
                    totalXmas += horzInputReverseRegex.Count(line);
               
                    char[] charLine = line.ToCharArray();
                    fileinput.Add(charLine);
                }

            }

            for (int i = 0; i <= fileinput.Count - 1; i++)
            {
                CheckLinesPart1(fileinput[i], i);
                OnLine++;
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}, on line {OnLine} in character {CharNum}");
    }

    Console.WriteLine($"Total XMAS: {totalXmas}");
}

void CheckLinesPart1(char[] line, int fiInputLine )
{
    int correctCount = 0;
    for (int i = 0; i < line.Length; i++)
    {
        switch (line[i])
        {
            case 'X':
                //check verts
                // we are in line[i] so we need the line's index number in fileinput plus one and minus one with its line[i]
                
                //Vert down
                if (
                    fileinput.Count > fiInputLine + 3 &&
                    fileinput[fiInputLine + 1][i] == 'M' &&
                    fileinput[fiInputLine + 2][i] == 'A' &&
                    fileinput[fiInputLine + 3][i] == 'S'
                )
                {
                    totalXmas++;
                }
                //vert up
                if (
                    fiInputLine - 3 >= 0 &&
                    fileinput[fiInputLine].Length > i &&
                    fileinput[fiInputLine - 1][i] == 'M' && 
                    fileinput[fiInputLine - 2][i] == 'A' &&
                    fileinput[fiInputLine - 3][i] == 'S'
                )
                {
                    totalXmas++;
                }
                //nw
                if (
                    fiInputLine - 3 >= 0 &&
                    fileinput[fiInputLine].Length > i &&
                    i >= 3 &&
                    fileinput[fiInputLine - 1][i-1] == 'M' &&
                    fileinput[fiInputLine - 2][i - 2] == 'A' &&
                    fileinput[fiInputLine - 3][i - 3] == 'S'
                )
                {
                    totalXmas++;

                }
                //NE
                if (
                    fiInputLine - 3 >= 0 &&
                    fileinput[fiInputLine].Length > i &&
                    i <= line.Length - 4 &&
                    fileinput[fiInputLine - 1][i + 1] == 'M' &&
                    fileinput[fiInputLine - 2][i + 2] == 'A' &&
                    fileinput[fiInputLine - 3][i + 3] == 'S'
                )
                {
                    totalXmas++;

                }
                //SW
                if (
                    fileinput.Count > fiInputLine + 3 &&
                    i >= 3 &&
                    fileinput[fiInputLine + 1][i - 1] == 'M' &&
                    fileinput[fiInputLine + 2][i - 2] == 'A' &&
                    fileinput[fiInputLine + 3][i - 3] == 'S'
                )
                {
                    totalXmas++;

                }
                //SE
                if (
                    fileinput.Count > fiInputLine + 3 &&
                    i <= line.Length - 4 &&
                    fileinput[fiInputLine + 1][i + 1] == 'M' &&
                    fileinput[fiInputLine + 2][i + 2] == 'A' &&
                    fileinput[fiInputLine + 3][i + 3] == 'S'
                )
                {
                    totalXmas++;

                }

                break;


        } 
        CharNum++;
    }

    CharNum = 0;

}

void RunPart2()
{
    for (int i = 0; i <= fileinput.Count - 1; i++)
    {
        CheckLinesPart2(fileinput[i], i);
        OnLine++;
    }
    Console.WriteLine($"Total Crossed MAS: {totalCrossMAS}");
}

void CheckLinesPart2(char[] line, int fiInputLine )
{
    int correctCount = 0;
    for (int i = 0; i < line.Length; i++)
    {
        switch (line[i])
        {
            case 'A':

                if (fileinput.Count > fiInputLine + 1 &&
                    fiInputLine > 0 &&
                    i > 0 &&
                    i < fileinput[fiInputLine].Length - 1 &&
                    (((fileinput[fiInputLine-1][i-1] == 'M' && fileinput[fiInputLine + 1][i+1] == 'S') && (fileinput[fiInputLine-1][i+1] == 'M' && fileinput[fiInputLine + 1][i-1] == 'S')) ||
                    ((fileinput[fiInputLine-1][i-1] == 'S' && fileinput[fiInputLine + 1][i+1] == 'M') && (fileinput[fiInputLine-1][i+1] == 'M' && fileinput[fiInputLine + 1][i-1] == 'S')) ||
                    ((fileinput[fiInputLine-1][i-1] == 'M' && fileinput[fiInputLine + 1][i+1] == 'S') && (fileinput[fiInputLine-1][i+1] == 'S' && fileinput[fiInputLine + 1][i-1] == 'M')) ||
                    ((fileinput[fiInputLine-1][i-1] == 'S' && fileinput[fiInputLine + 1][i+1] == 'M') && (fileinput[fiInputLine-1][i+1] == 'S' && fileinput[fiInputLine + 1][i-1] == 'M')))
                    )
                {
                    totalCrossMAS++;
                }

                break;


        } 
        CharNum++;
    }

    CharNum = 0;

}