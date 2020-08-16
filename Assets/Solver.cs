using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Solver : MonoBehaviour
{
    InputField[,] board = new InputField[9,9];
    public InputField[] temp = new InputField[81];

    void Start()
    {
        //Initialize board and fill in empty spaces with 0's
        for(int i = 0;i < 9;i ++){
            for(int j = 0;j < 9;j ++){
                board[j,i] = temp[i*9+j];
                if(board[j,i].text == ""){
                    board[j,i].text = "0";
                }
            }
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("Done. Solved: " + Solve());
        }
    }
    private bool Valid(int row, int col, string num){
        //Check column
        for(int i = 0;i < 9;i ++){
            if(board[i,col].text == num){
                return false;
            }
        }
        //Check row
        for(int j = 0;j < 9;j ++){
            if(board[row, j].text == num){
                return false;
            }
        }
        //Check boxes
        int rowStart = row-(row%3);
        int colStart = col-(col%3);

        for(int i = rowStart;i < rowStart + 3 && i < 9;i ++){
            for(int j = colStart;j < colStart + 3 && j < 9;j ++){
                if(board[i,j].text == num){
                    return false;
                }
            }
        }
        //Valid, passed checks
        return true;
    }

    private bool Solve(){
        bool isFull = true;
        int row = -1;
        int col = -1;

        //Check for 0's/empty spaces, if there are, record the row and col
        for(int i = 0;i < 9 && isFull;i ++){
            for(int j = 0;j < 9 && isFull;j ++){
                if(board[i,j].text == "0"){
                    row = i;
                    col = j;

                    isFull = false;
                }
            }
        }
        //If no empty spaces, puzzle solved
        if(isFull){
            return true;
        }else{
            //Try values that could be valid in the spot
            //If value fails later, then backtrack and replace value with 0
            for(int i = 1;i <=9;i ++){
                if (Valid(row,col, "" + i)){
                    board[row,col].text = "" + i;
                    if(Solve()){
                        return true;
                    }else{
                        board[row,col].text = "" + 0;
                    }
                }
            }
            //Backtracking
            return false;
        }
    }
}
