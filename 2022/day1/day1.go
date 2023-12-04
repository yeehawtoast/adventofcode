package main
import (
    "bufio"
    "fmt"
    "os"
    "strconv"
    "strings"
)
var um int = 0
func main() {
    // Open the file
    file, err := os.Open("input.txt")
    if err != nil {
        fmt.Println("Error opening file:", err)
        return
    }
    defer file.Close()

    scanner := bufio.NewScanner(file)
    sum := 0

    
    for scanner.Scan() {
        line := scanner.Text()

        // Check for an empty line
        if line == "" {
            if um < sum  {
              um = sum
              sum = 0
            }else{
              sum = 0
            }
        }

        // Convert the line to an integer
        if line != ""{
        num, err := strconv.Atoi(strings.TrimSpace(line))
        if err != nil {
            fmt.Println("Error converting line to integer:", err)
            return
        }

        // Add the integer to the sum
        sum += num
      }
    }

    if err := scanner.Err(); err != nil {
        fmt.Println("Error reading file:", err)
        return
    }

    fmt.Println("Sum of numbers until an empty line is reached:", um)
}

