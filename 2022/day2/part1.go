package main

func main(){

}

// POINTS AND ASSIGNMENTS

// A and X == ROCK == 1
// B and Y == PAPER == 2
// C and Z == SCISSORS == 3

// 0 for Lose 
// 3 for draw
// 6 for win

//THE GAME
// A|| x > z||c
// z||c > b||y
// b||y > a||x

//POINTS OUTCOME
// Shape Points + OUTCOME


//Psedocode

//Take input, then line by line. 
// for each line, read each character, with a lookahead of 1 to make sure you don't eat the character
// read the character, find the value for the character 
// at the end of the line (ie, if the lookahead sees a newline) take the two previous characters and compare their values. 
// run points outcome and clear the values for the temporary character value holders.
//add points outcome to totalsum
//move to the next line
//using the signed integers, you can add negatives to positive sums
// do this until the end of file and print the final total
