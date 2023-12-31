﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour
{
    public int mode;
    public int score;
    public int miss;
    public int round;
    public string gameMessage;
    private IUserAction action;
    public GUIStyle bigStyle, blackStyle, smallStyle;//自定义字体格式
    public Font pixelFont;
    private int menu_width = Screen.width / 5, menu_height = Screen.width / 10;//主菜单每一个按键的宽度和高度
    // Start is called before the first frame update
    void Start()
    {
        mode = 0;
        score = 0;
        miss = 0;
        round = 0;
        gameMessage = "";
        action = SSDirector.getInstance().currentSceneController as IUserAction;

        //pixelStyle
        //pixelFont = Font.Instantiate(Resources.Load("Fonts/ThaleahFat", typeof(Font))) as Font;
        //if (pixelFont == null) Debug.Log("null");
        //pixelFont.fontSize = 50;
        //pixelFont = Arial;
        
        //大字体初始化
        bigStyle = new GUIStyle();
        bigStyle.normal.textColor = Color.white;
        bigStyle.normal.background = null;
        bigStyle.fontSize = 50;
        bigStyle.alignment=TextAnchor.MiddleCenter;

        //black
        blackStyle = new GUIStyle();
        blackStyle.normal.textColor = Color.black;
        blackStyle.normal.background = null;
        blackStyle.fontSize = 50;
        blackStyle.alignment=TextAnchor.MiddleCenter;

        //小字体初始化
        smallStyle = new GUIStyle();
        smallStyle.normal.textColor = Color.white;
        smallStyle.normal.background = null;
        smallStyle.fontSize = 20;
        smallStyle.alignment=TextAnchor.MiddleCenter;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI() {
        //GUI.skin.button.font = pixelFont;
        GUI.skin.button.fontSize = 35;
        switch(mode) {
            case 0:
                mainMenu();
                break;
            case 1:
                GameStart();
                break;
            
        }       
    }

    void mainMenu() {
        GUI.Label(new Rect(Screen.width / 2 - menu_width * 0.5f, Screen.height * 0.1f, menu_width, menu_height), "HIT UFO", bigStyle);
        bool button = GUI.Button(new Rect(Screen.width / 2 - menu_width * 0.5f, Screen.height * 3 / 7, menu_width, menu_height), "START");
        if (button) {
            mode = 1;
        }
        
    }
    void GameStart() {
        UnityEngine.Debug.Log(Camera.main.pixelWidth);
        UnityEngine.Debug.Log(Screen.width*1f);
        GUI.Label(new Rect(Screen.width / 2 - menu_width * 0.5f, Screen.height * 0.1f, menu_width, menu_height), gameMessage,bigStyle);//, bigStyle300, 60, 50, 200
        GUI.Label(new Rect(Screen.width*0.8f, Screen.height * 0.8f-30, menu_width, menu_height), "Score: " + score, smallStyle);//0,0,100,50
        GUI.Label(new Rect(Screen.width*0.8f, Screen.height * 0.8f, menu_width, menu_height), "Round: " + round, smallStyle);
    }
}
