package main

import (
	"bufio"
	"fmt"
	"os"
	"regexp"
	"strconv"
	"strings"
)

func checkThresholds(line string) bool {
	parts := strings.Split(line, ":")
	if len(parts) != 2 {
		return false
	}

	colorsData := strings.Split(parts[1], ";")
	for _, colorData := range colorsData {
		colorValues := strings.Split(strings.TrimSpace(colorData), ",")
		for _, val := range colorValues {
			val = strings.TrimSpace(val)
			if strings.Contains(val, " ") {
				countColor := strings.Split(val, " ")
				count, err := strconv.Atoi(strings.TrimSpace(countColor[0]))
				if err != nil {
					continue
				}
				color := getColorID(strings.TrimSpace(countColor[1]))
				if !belowThreshold(color, count) {
					return false
				}
			}
		}
	}

	return true
}

func belowThreshold(color int, count int) bool {
	thresholds := map[int]int{
		1: 13, // Green
		2: 14, // Blue
		3: 12, // Red
	}

	threshold, found := thresholds[color]
	if !found {
		return false
	}

	return count <= threshold
}

func getColorID(color string) int {
	switch color {
	case "green":
		return 1
	case "blue":
		return 2
	case "red":
		return 3
	default:
		return 0 // Default color ID, you can handle other cases accordingly
	}
}

func main() {
	file, err := os.Open("input.txt")
	if err != nil {
		fmt.Println("Error opening input file:", err)
		return
	}
	defer file.Close()

	var lines []string
	scanner := bufio.NewScanner(file)
	for scanner.Scan() {
		lines = append(lines, scanner.Text())
	}

	if err := scanner.Err(); err != nil {
		fmt.Println("Error reading input file:", err)
		return
	}

	gameSum := 0

	for _, line := range lines {
		if checkThresholds(line) {
			gameID := extractGameID(line)
			fmt.Println("correct game ID:", gameID)
			gameSum = gameSum + gameID
			fmt.Println("new sum:", gameSum)
		}
	}

	fmt.Println("Final Sum of Game IDs:", gameSum)
}

func extractGameID(line string) int {
	re := regexp.MustCompile(`Game (\d+):`)
	match := re.FindStringSubmatch(line)

	if len(match) >= 2 {
		gameID, _ := strconv.Atoi(match[1])
		return gameID
	}

	return 0
}
