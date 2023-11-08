using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
ECS or MVC conception is applied on the small game
Firstly, entitis and their states are defined as the game model.
And then, give some components/controls which can modify the game model.
The end, system handler OnGUI called by game circle provides a game UI view.
*/
public class ChessGame : MonoBehaviour {

    // Entities and their states / Model
    private static int player;
    private static int count;
    private int winner;
    private int[,] chessBoard = new int[3, 3];
    public UnityEngine.Color buttonColor = UnityEngine.Color.red; // 要更改的按钮颜色


    // System Handlers
    void Start () {
        Init();


    }

    // View to render entities / models
    // Here! you cannot modify model directly, use components/controls to do it
    void OnGUI() {

        GUIStyle customButtonStyle = new GUIStyle(GUI.skin.button);
        // 创建纯色背景贴图
        Texture2D backgroundTexture = new Texture2D(1, 1);
        backgroundTexture.SetPixel(0, 0, buttonColor);
        backgroundTexture.Apply();
        // 设置按钮背景贴图
        customButtonStyle.normal.background = backgroundTexture;

        GUI.Box(new Rect(210, 25, 300, 300), "");
        if (GUI.Button(new Rect(310, 270, 100, 30), "Restart")) Init();
        if (!GameOver()) {
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++) {
                            GUI.skin.button.normal.textColor = buttonColor;
                    Rect buttonRect = new Rect(255 + j * 70, 50 + i * 70, 70, 70);
                    if (chessBoard[i, j] == 0 && GUI.Button(buttonRect, "-", customButtonStyle)) {
                        PutChess(i,j);
                        UnityEngine.Debug.Log("change");
                    }
                    else if (chessBoard[i, j] == 1) GUI.Button(new Rect(255 + j * 70, 50 + i * 70, 70, 70), "O", customButtonStyle);
                    else if (chessBoard[i, j] == 2) GUI.Button(new Rect(255 + j * 70, 50 + i * 70, 70, 70), "X");
                    GUI.skin.button.normal.textColor = UnityEngine.Color.white;
                }
            }
        }
        else {
            if (winner != 0)
                GUI.Box(new Rect(260, 50, 200, 200), "\n\n\n\n\nCongratulations!\n Player "+winner+" has won.");
            else
                GUI.Box(new Rect(260, 50, 200, 200), "\n\n\n\n\n\nThis is a draw!");
        }
        customButtonStyle.normal.background = null;
    }

    // Components /controls
    void Init() {
        player = 1;
        winner = 0;
        count = 0;
        for(int i = 0; i < 3; i++)
            for(int j = 0; j < 3; j++)
                chessBoard[i, j] = 0;
    }

    void PutChess(int i,int j) {
        chessBoard[i, j] = player;
        player = 3 - player;
        count++;       
    }

    bool GameOver() {
        for(int i = 0; i < 3; i++) {       
            if (chessBoard[i, 0] != 0 
                && chessBoard[i, 0] == chessBoard[i, 1] && chessBoard[i, 0] == chessBoard[i, 2]) winner = chessBoard[i, 0];
           
            if (chessBoard[0, i] != 0 
                && chessBoard[0, i] == chessBoard[1, i] && chessBoard[0, i] == chessBoard[2, i]) winner = chessBoard[0, i];
        }
        
        if (chessBoard[0, 0] != 0 && chessBoard[0, 0] == chessBoard[1, 1] && chessBoard[0, 0] == chessBoard[2, 2]) winner = chessBoard[0, 0];
        if (chessBoard[0, 2] != 0 && chessBoard[0, 2] == chessBoard[1, 1] && chessBoard[0, 2] == chessBoard[2, 0]) winner = chessBoard[0, 2];

        if (count < 9 && winner == 0) return false;

        return true;
    }

}
