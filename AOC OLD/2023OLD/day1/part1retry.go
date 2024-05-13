package main

import (
	"fmt"
	"os"
	"strconv"
)

func main() {
	file, err := os.Open("input.txt") // Replace "file.txt" with your file name
	if err != nil {
		fmt.Println("Error opening file:", err)
		return
	}
	defer file.Close()

	var currentNumber string
	var numbers []int
  sum :=0

	// Read file character by character
	for {
		var char [1]byte
		_, err := file.Read(char[:])

		if err != nil {
			break // Exit loop at end of file
		}

		if char[0] >= '0' && char[0] <= '9' {
			if len(currentNumber) == 0 {
				currentNumber += string(char[0])
			} else if len(currentNumber) == 1 {
				if currentNumber[0] != '0' { // If the first digit is not '0'
					currentNumber += string(char[0])
				} else { // If the first digit is '0', replace it
					currentNumber = string(char[0]) + string(currentNumber[1])
				}
			} else { // Replace the second digit
				currentNumber = string(currentNumber[0]) + string(char[0])
			}
		} else if char[0] == '\n' {
			if len(currentNumber) == 1 {
				currentNumber += string(currentNumber[0]) // Use the first digit as the second digit
			}
			num, _ := strconv.Atoi(currentNumber)
			numbers = append(numbers, num)
			currentNumber = "" // Reset currentNumber for the next line
		}
	}

	// Print the array
	for _, num := range numbers {
		sum += num
	}
  fmt.Println("the sum is:", sum)
}
