package main

import (
	"fmt"
)

func main() {
	inputtime := 
	inputdistance := 
	var count int = 0

	// use holdbutton func to calculate total speed for that round
	for x := 0; x <= len(inputtime)-1; x++ {
		for j := 0; j <= inputtime[x]; j++ {
			speed := 1 * j
			freetime := inputtime[x] - j
			distance := speed * freetime
			fmt.Println("your distance =", distance)
			if distance > inputdistance[x] {
				count++
			}
		}
		fmt.Println(count)
		count = 0
	}
}
