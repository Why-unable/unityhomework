using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeController : MonoBehaviour{

    public FirstController firstCtrl;

    void Start(){
        UnityEngine.Debug.Log("judgeControl here start");
        firstCtrl = (FirstController)SSDirector.GetInstance().CurrentSceneController;
    }

    //判断游戏状态
    void Update(){
        if(firstCtrl.gameState != FirstController.PLAYING) return;
        //判断是否失败
        int[,] rolePos = new int[2, 3]{{0, 0, 0}, {0, 0, 0}};
        foreach(RoleController r in firstCtrl.RoleCtrl){
            rolePos[r.roleType, r.roleState]++;  //roletype: 0天使、1魔鬼
        }                                        //rolestate:0左岸、1右岸、2船上
        if((rolePos[0,0]>0 && rolePos[0,0]<rolePos[1,0]) ||   //左岸存在天使 && 左岸的天使数量少于左岸魔鬼的数量
           (rolePos[0,1]>0 && rolePos[0,1]<rolePos[1,1]) ||   //右岸存在天使 && 右岸的天使数量少于右岸魔鬼的数量
           (rolePos[0,2]>0 && rolePos[0,2] < rolePos[1,2])){  //船上最多只能承载两个角色时，这行可以删去
            
            firstCtrl.JudgeCallback(FirstController.FAILED);
            //firstCtrl.gameState = FAILED;
            return;
        }  
        //判断是否成功
        foreach(RoleController r in firstCtrl.RoleCtrl){
            if(r.roleState != FirstController.RIGHTLAND){//存在角色未到达右岸，则直接返回 //r.roleType == 0 && 
                UnityEngine.Debug.Log("这里");
                return;
            }
        }
        UnityEngine.Debug.Log("其实是这里");
        firstCtrl.JudgeCallback(FirstController.WIN);
        //firstCtrl.gameState = WIN;  //上面的情况都不符合，则说明所有角色都安全到达右岸，win
        return;
    }

}
