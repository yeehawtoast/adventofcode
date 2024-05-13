package main

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
	"strings"
	"sort"
)

func main() {
	file, err := os.Open("input.txt")
	if err != nil {
		fmt.Println("Error opening file:", err)
		return
	}
	defer file.Close()

	scanner := bufio.NewScanner(file)
	var sums []int
	sum := 0
	dasums := 0
	for scanner.Scan() {
		line := scanner.Text()

		// Check for an empty line
		if line == "" {
			sums = append(sums, sum)
			sum = 0 // Reset sum for the next section
			continue
		}

		// Convert the line to an integer
		num, err := strconv.Atoi(strings.TrimSpace(line))
		if err != nil {
			fmt.Println("Error converting line to integer:", err)
			return
		}

		// Add the integer to the sum
		sum += num
	}

	if err := scanner.Err(); err != nil {
		fmt.Println("Error reading file:", err)
		return
	}

	// Append the last sum after the last blank line (if any)
	if sum != 0 {
		sums = append(sums, sum)
	}

	fmt.Println("Sums of numbers in each section until the end of the file:", sums)

	findLargestThreeSums(sums);
	fmt.Println("final sum 1:", sums[0]);
	fmt.Println("final sum 2:", sums[1]);
	fmt.Println("final sum 3:", sums[2]);

	dasums = sums[0] + sums[1] + sums[2]

	fmt.Println("final sum for real:", dasums)
}

// From there we can write a function that finds the 3 largest of the sums.
func findLargestThreeSums(sums []int) []int {
	// Sort the sums in descending order
	sort.Slice(sums, func(i, j int) bool {
		return sums[i] > sums[j]
	})
	// If there are less than 3 sums, return all the sums
	if len(sums) <= 3 {
		return sums
	}

	// Otherwise, return the largest 3 sums
	return sums[:3]
}