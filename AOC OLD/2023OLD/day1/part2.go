package main

import (
	"bufio"
	"fmt"
	"log"
	"os"
	"regexp"
	"strconv"
	"strings"
	"unicode"
)

const maxInt = int(^uint(0) >> 1)

func findDigit(line string) (int, string) {
	wordToDigit := map[string]string{
		"one":   "1",
		"two":   "2",
		"three": "3",
		"four":  "4",
		"five":  "5",
		"six":   "6",
		"seven": "7",
		"eight": "8",
		"nine":  "9",
	}

	re := regexp.MustCompile("one|two|three|four|five|six|seven|eight|nine")
	digit := re.FindString(line)
	index := strings.Index(line, digit)

	return index, wordToDigit[digit]
}

func tigidDinf(line string) (int, string) {
	wordToDigit := map[string]string{
		"eno":   "1",
		"owt":   "2",
		"eerht": "3",
		"ruof":  "4",
		"evif":  "5",
		"xis":   "6",
		"neves": "7",
		"thgie": "8",
		"enin":  "9",
	}

	re := regexp.MustCompile("eno|owt|eerht|ruof|evif|xis|neves|thgie|enin")
	digit := re.FindString(line)
	index := len(line) - strings.Index(line, digit) - len(digit)

	return index, wordToDigit[digit]
}

func reverseString(s string) string {
	rns := []rune(s)
	for i, j := 0, len(rns)-1; i < j; i, j = i+1, j-1 {
		rns[i], rns[j] = rns[j], rns[i]
	}

	return string(rns)
}

func getFirstDigit(calibrationMap map[int]string) string {
	c := ""
	i := maxInt
	for k, v := range calibrationMap {
		if k < i {
			c = v
			i = k
		}
	}

	return c
}

func getLastDigit(calibrationMap map[int]string) string {
	c := ""
	i := -1
	for k, v := range calibrationMap {
		if k > i {
			c = v
			i = k
		}
	}

	return c
}

func main() {


	
	file, err := os.Open("input.txt")
	if err != nil {
		log.Fatal(err)
	}

	defer file.Close()

	fileScanner := bufio.NewScanner(file)
	fileScanner.Split(bufio.ScanLines)

	calibrationValue := 0

	for fileScanner.Scan() {
		calibrationMap := map[int]string{}
		line := fileScanner.Text()
		length := len(line)

		for i := 0; i < length; i++ {
			c := rune(line[i])
			if unicode.IsDigit(c) {
				calibrationMap[i] = string(c)

				break
			}
		}

		if i, digit := findDigit(line); digit != "" {
			calibrationMap[i] = digit
		}

		line = reverseString(line)

		for i := 0; i < length; i++ {
			c := rune(line[i])
			if unicode.IsDigit(c) {
				calibrationMap[length-i-1] = string(c)

				break
			}
		}

		if i, digit := tigidDinf(line); digit != "" {
			calibrationMap[i] = digit
		}

		if x, err := strconv.Atoi(
			getFirstDigit(calibrationMap) + getLastDigit(calibrationMap),
		); err == nil {
			calibrationValue += x
		}
	}

	fmt.Println("Calibration value: ", calibrationValue)
}
