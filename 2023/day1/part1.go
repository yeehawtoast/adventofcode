package main

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
	
)

func main() {
	// Open a text file for scanning
	file, err := os.Open("input.txt")
	if err != nil {
		fmt.Println("Error opening file:", err)
		return
	}
	defer file.Close()

	// Create a scanner to read character by character
	scanner := bufio.NewScanner(file)
	scanner.Split(bufio.ScanRunes) // Set scanner to read runes (characters) individually

	var integers []int
	var currentNumber string

	// Scan character by character
	for scanner.Scan() {
		currentChar := scanner.Text()

		if currentChar == "\n" {
			// Check if the current number is an integer and store it if so
			if num, err := strconv.Atoi(currentNumber); err == nil {
				integers = append(integers, num)
			}
			currentNumber = "" // Reset the current number for the next line
		} else if _, err := strconv.Atoi(currentChar); err == nil {
			// If the current character is a digit, add it to the current number string
			currentNumber += currentChar
		}
	}

	if err := scanner.Err(); err != nil {
		fmt.Println("Error during scanning:", err)
	}

	// Display the integers found
	fmt.Println("Integers found in each line:")
	for _, nums := range integers {
		fmt.Println(nums)
	}
}

