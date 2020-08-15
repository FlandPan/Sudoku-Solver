using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Solver : MonoBehaviour
{
    static InputField[,] board = new InputField[9,9];
    public InputField[] temp = new InputField[81];

    void Start()
    {
        //Initialize board
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
            Solve();
        }
    }
    private static bool Valid(int row, int col, string num){
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

        for(int i = rowStart;i < row + 3 && i < 9;i ++){
            for(int j = colStart;j < col + 3 && j < 9;j ++){
                if(board[i,j].text == num){
                    return false;
                }
            }
        }
        //Valid, passed checks
        return true;
    }

    private static bool Solve(){
        bool isFull = true;
        int row = -1;
        int col = -1;

        for(int i = 0;i < 9 && isFull;i ++){
            for(int j = 0;j < 9 && isFull;j ++){
                if(board[i,j].text == "0"){
                    row = i;
                    col = j;

                    isFull = false;
                }
            }
        }
        if(isFull){
            return true;
        }
        for(int i = 1;i <=9;i ++){
            if (Valid(row,col, "" + i)){
                board[row,col].text = "" + i;
                Debug.Log(row + " " + col + " " + i);
                Debug.Log(UnityEngine.StackTraceUtility.ExtractStackTrace());
                if(Solve()){
                    return true;
                }else{
                    board[row,col].text = "" + 0;
                }
            }
        }
        return false;
    }
}
