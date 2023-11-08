using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
public class LifeGame : MonoBehaviour
{
    // Entities and their states / Model
    private int gridSize = 80;
    private float buttonSize =8f;
    private float buttonSpacing = 0f;
    private float buttonSpacing2 = 20f;
    private Rect firstGridRect;
    private Rect actionRect;
    private Rect cleanRect;
    private int[,] cellboard;
    private int[,] new_cellboard;
    private int action=0;
    public UnityEngine.Color buttonColor = UnityEngine.Color.red; // 要更改的按钮颜色
    public int[,] dir = new int[,]
    {
        {-1, -1}, // 左上
        {-1, 0},  // 上
        {-1, 1},  // 右上
        {0, 1},   // 右
        {1, 1},   // 右下
        {1, 0},   // 下
        {1, -1},  // 左下
        {0, -1}   // 左
    };
    private int count=0;

    // System Handlers
    void Start () {
        Initboard();
    }
    void OnGUI()
    {
        // 计算网格的位置和大小
        float gridWidth = (buttonSize + buttonSpacing) * gridSize;
        float gridHeight = (buttonSize + buttonSpacing) * gridSize;
        float gridX = (Screen.width - gridWidth) / 2f; // 在视图中央水平居中
        float gridY = (Screen.height - gridHeight) / 2f; // 在视图中央垂直居中
        firstGridRect = new Rect(gridX, gridY, gridWidth, gridHeight);

        // 绘制网格
        DrawGrid();

        // 计算开始按钮的位置和大小
        float actionButtonX = firstGridRect.x + firstGridRect.width + buttonSpacing2;
        float actionButtonY = firstGridRect.y;
        actionRect = new Rect(actionButtonX, actionButtonY, buttonSize * 5, buttonSize * 3);
        // 绘制按钮
        DrawActionButton();

        // 计算清除按钮的位置和大小
        float cleanButtonX = firstGridRect.x + firstGridRect.width + buttonSpacing2;
        float cleanButtonY = firstGridRect.y + buttonSize * 5;
        cleanRect = new Rect(cleanButtonX, cleanButtonY, buttonSize * 5, buttonSize * 3);
        // 绘制按钮
        DrawCleanButton();

        //更新(count用来控制更新速度)
        if(action==1&&count%10==1){
            updateCellBoard();
        }
        count++;
        if(count>10000000){
            count=0;
        }

    }

    // Components /controls
    void Initboard(){
        UnityEngine.Debug.Log("init");
        cellboard = new int[gridSize, gridSize];
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                cellboard[i,j]=0;
            }
        }
        new_cellboard=new int[gridSize,gridSize];

    }
    void DrawGrid()
    {   
        GUIStyle customButtonStyle = new GUIStyle(GUI.skin.button);
        // 创建纯色背景贴图
        Texture2D backgroundTexture = new Texture2D(1, 1);
        backgroundTexture.SetPixel(0, 0, buttonColor);
        backgroundTexture.Apply();
        // 设置按钮背景贴图
        customButtonStyle.normal.background = backgroundTexture;
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                float x = firstGridRect.x + j * (buttonSize + buttonSpacing);
                float y = firstGridRect.y + i * (buttonSize + buttonSpacing);
                Rect buttonRect = new Rect(x, y, buttonSize, buttonSize);

                if(action==0){
                    if(cellboard[i,j]==0&&GUI.Button(buttonRect, "")){
                        cellboard[i,j]=1-cellboard[i,j];
                    }
                    else if(cellboard[i,j]==1&&GUI.Button(buttonRect, "",customButtonStyle)) {
                        cellboard[i,j]=1-cellboard[i,j];
                    }
                }
                else if(action==1){
                    if(cellboard[i,j]==0){
                        GUI.Button(buttonRect, "");
                    }
                    else if(cellboard[i,j]==1) {
                        GUI.Button(buttonRect, "",customButtonStyle);      
                    }
                }
            }
        }
        customButtonStyle.normal.background = null;
    }

    void DrawActionButton()
    {
        if (action==0 && GUI.Button(actionRect, "start")){
            action=1-action; 
        }
        else if(action==1 && GUI.Button(actionRect, "stop"))
        {
            action=1-action; 
        }
    }

    void DrawCleanButton()
    {
        if(action==0 && GUI.Button(cleanRect, "clean"))
        {
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    cellboard[i,j]=0;
                }
            }
        }
    }

    void updateCellBoard(){
        
        //红为1，黑为0
        //格周，多于3红则为黑，3红则为红，2红则不变，少于2红则为黑
        //在生命游戏中，红代表存活的细胞，黑代表死亡的细胞
        for(int i=0;i<gridSize;i++){
            for(int j=0;j<gridSize;j++){

                int life_num=0;
                for(int k=0;k<8;k++){
                    int new_i=dir[k,0]+i;
                    int new_j=dir[k,1]+j;
                    if(new_i>=0&&new_i<gridSize && new_j>=0&&new_j<gridSize){
                        if(cellboard[new_i,new_j]==1){
                            life_num++;
                        }
                    }
                }
                if(life_num>3)  new_cellboard[i,j]=0;
                else if(life_num==3)  new_cellboard[i,j]=1;
                else if(life_num==2) new_cellboard[i,j]=cellboard[i,j];
                else if(life_num<2)  new_cellboard[i,j]=0;
            }
        }
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                cellboard[i,j]=new_cellboard[i,j];
            }
        }
    }


}