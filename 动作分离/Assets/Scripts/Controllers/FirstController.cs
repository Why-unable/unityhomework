using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : MonoBehaviour, ISceneController, IUserAction
{
    public static int LEFTLAND = 0;
    public static int RIGHTLAND = 1;
    public static int BOAT = 2;

    public static int PRIEST = 0;
    public static int DEVIL = 1;

    public static int PLAYING = 0;
    public static int WIN = 1;
    public static int FAILED = 2;

    public CCActionManager actionManager;
    public JudgeController JudgeCtrl;
    public BoatController BoatCtrl;
    public RoleController[] RoleCtrl = new RoleController[6];
    public LandController[] LandCtrl = new LandController[2];
    public MoveController MoveCtrl;

    int[] rolesID = new int[6]{0,1,2,3,4,5};
    public int gameState;

    void Awake(){
        SSDirector director = SSDirector.GetInstance();
        director.CurrentSceneController = this;
        director.CurrentSceneController.Initialize();

        this.gameObject.AddComponent<UserGUI>();
        this.gameObject.AddComponent<CCActionManager>();
        this.gameObject.AddComponent<JudgeController>();
    }
    public void JudgeCallback(int gameState){
        this.gameState=gameState;

    } 
    public void Initialize(){
        //如果有，则释放原有的GameObject
        for(int i = 0; i < 6; i++){
            if(RoleCtrl[i] != null){
                Destroy(RoleCtrl[i].GetModelGameObject());
            }
        }
        for(int i = 0; i < 2; i++){
            if(LandCtrl[i] != null){
                Destroy(LandCtrl[i].GetModelGameObject());
            }
        }
        if(BoatCtrl != null){
            Destroy(BoatCtrl.GetModelGameObject());
        }
        // 加载控制器和模型
        BoatCtrl = new BoatController();
        BoatCtrl.CreateModel();
        JudgeCtrl = new JudgeController();

        for(int i = 0; i < 6; i++){
            int roleType = (i < 3) ? PRIEST : DEVIL;
            RoleCtrl[i] = new RoleController(roleType, rolesID[i]);
            RoleCtrl[i].CreateModel();
        }
        LandCtrl[0] = new LandController(LEFTLAND, rolesID);
        LandCtrl[1] = new LandController(RIGHTLAND, rolesID);
        LandCtrl[0].CreateModel();
        LandCtrl[1].CreateModel();
        //MoveCtrl = new MoveController();
        //开始游戏
        gameState = PLAYING;
    }

    //将角色的ID转换成数组的下标
    int IDToNumber(int ID){
        for(int i = 0; i < 6; i++){
            if(rolesID[i] == ID){
                return i;
            }
        }
        return -1;
    }

    //点击船时执行
    public void MoveBoat(){
        if(gameState != PLAYING || actionManager.IsMoving() || BoatCtrl.isEmpty()) return;//MoveCtrl.IsMoving() 
        //CheckAndSetGameState();
        if(BoatCtrl.onLeftside){
            //MoveCtrl.SetMove(BoatCtrl.GetModelGameObject(), Position.boatRightPos);
            for(int i = 0; i < BoatCtrl.seatNum; i++){
                if(BoatCtrl.seat[i] != -1){
                    RoleController r = RoleCtrl[IDToNumber(BoatCtrl.seat[i])];
                    UnityEngine.Debug.Log(r.GetModelGameObject().transform.parent==null);

                    r.GetModelGameObject().transform.parent = BoatCtrl.GetModelGameObject().transform;
                    //MoveCtrl.SetMove(r.GetModelGameObject(), Position.seatRightPos[i]);
                    //actionManager.MoveRole(r.GetModelGameObject(),Position.seatRightPos[i],5);
                }
            }
            actionManager.MoveBoat(BoatCtrl.GetModelGameObject(),Position.boatRightPos,5);
        }
        else{
            actionManager.MoveBoat(BoatCtrl.GetModelGameObject(),Position.boatLeftPos,5);
            //MoveCtrl.SetMove(BoatCtrl.GetModelGameObject(), Position.boatLeftPos);
            for(int i = 0; i < BoatCtrl.seatNum; i++){
                if(BoatCtrl.seat[i] != -1){
                    RoleController r = RoleCtrl[IDToNumber(BoatCtrl.seat[i])];
                    r.GetModelGameObject().transform.parent = BoatCtrl.GetModelGameObject().transform;
                    //actionManager.MoveRole(r.GetModelGameObject(),Position.seatLeftPos[i],5);
                    //MoveCtrl.SetMove(r.GetModelGameObject(), Position.seatLeftPos[i]);
                }
            } 
        }
        BoatCtrl.onLeftside = !BoatCtrl.onLeftside;
    }

    //点击角色时执行
    public void MoveRole(int id){
        int num = IDToNumber(id);
        if(gameState != PLAYING || actionManager.IsMoving()) return;
        
        int seat;
        switch(RoleCtrl[num].roleState){
            case 0: // LEFTLAND
                if(!BoatCtrl.onLeftside || (BoatCtrl.getEmptySeat() == -1)) return;//增加判断getEmptySeat :
                LandCtrl[0].LeaveLand(id);                                         //如果上不了船，就直接返回，而
                seat = BoatCtrl.embark(id);                                        //不用改动role、land、boat的位置参数
                RoleCtrl[num].GoTo(BOAT);
                if(seat == -1) return;   //增加判断getEmptySeat后，这句应该可以略去
                actionManager.MoveRole(RoleCtrl[num].GetModelGameObject(),Position.seatLeftPos[seat],5);
                //MoveCtrl.SetMove(RoleCtrl[num].GetModelGameObject(), Position.seatLeftPos[seat]);
                break;
            case 1: // RIGHTLAND
                if(BoatCtrl.onLeftside || (BoatCtrl.getEmptySeat() == -1)) return;//同上
                LandCtrl[1].LeaveLand(id);
                seat = BoatCtrl.embark(id);
                RoleCtrl[num].GoTo(BOAT);
                if(seat == -1) return;
                actionManager.MoveRole(RoleCtrl[num].GetModelGameObject(),Position.seatRightPos[seat],5);
                //MoveCtrl.SetMove(RoleCtrl[num].GetModelGameObject(), Position.seatRightPos[seat]);
                break;
            case 2: //BOAT
                if(BoatCtrl.onLeftside){
                    RoleCtrl[num].GetModelGameObject().transform.parent=null;
                    seat = LandCtrl[0].getEmptySeat();
                    BoatCtrl.disembark(id);
                    LandCtrl[0].GoOnLand(id);
                    RoleCtrl[num].GoTo(LEFTLAND);
                    actionManager.MoveRole(RoleCtrl[num].GetModelGameObject(),Position.roleLeftPos[seat],5);
                    //MoveCtrl.SetMove(RoleCtrl[num].GetModelGameObject(), Position.roleLeftPos[seat]);
                }
                else{
                    
                    //UnityEngine.Debug.Log("mid_d");
                    RoleCtrl[num].GetModelGameObject().transform.parent=null;//LandCtrl[1].GetModelGameObject().transform;
                    seat = LandCtrl[1].getEmptySeat();
                    BoatCtrl.disembark(id);
                    LandCtrl[1].GoOnLand(id);
                    RoleCtrl[num].GoTo(RIGHTLAND);
                    //CheckAndSetGameState();//新添加一个在这里，在最后一个角色上右岸之后判定成功
                    actionManager.MoveRole(RoleCtrl[num].GetModelGameObject(),Position.roleRightPos[seat],5);
                    //MoveCtrl.SetMove(RoleCtrl[num].GetModelGameObject(), Position.roleRightPos[seat]);
                }
                break;
            default: break;
        }
    }

    //判断游戏状态
    // public void CheckAndSetGameState(){
    //     if(gameState != PLAYING) return;
    //     //判断是否失败
    //     int[,] rolePos = new int[2, 3]{{0, 0, 0}, {0, 0, 0}};
    //     foreach(RoleController r in RoleCtrl){
    //         rolePos[r.roleType, r.roleState]++;  //roletype: 0天使、1魔鬼
    //     }                                        //rolestate:0左岸、1右岸、2船上
    //     if((rolePos[0,0]>0 && rolePos[0,0]<rolePos[1,0]) ||   //左岸存在天使 && 左岸的天使数量少于左岸魔鬼的数量
    //        (rolePos[0,1]>0 && rolePos[0,1]<rolePos[1,1]) ||   //右岸存在天使 && 右岸的天使数量少于右岸魔鬼的数量
    //        (rolePos[0,2]>0 && rolePos[0,2] < rolePos[1,2])){  //船上最多只能承载两个角色时，这行可以删去
    //         gameState = FAILED;
    //         return;
    //     }  
    //     //判断是否成功
    //     foreach(RoleController r in RoleCtrl){
    //         if(r.roleState != RIGHTLAND){//存在角色未到达右岸，则直接返回 //r.roleType == 0 && 
    //             UnityEngine.Debug.Log("这里");
    //             return;
    //         }
    //     }
    //     UnityEngine.Debug.Log("其实是这里");
    //     gameState = WIN;  //上面的情况都不符合，则说明所有角色都安全到达右岸，win
    //     return;
    // }

    //Reset按钮执行的功能
    public void Restart(){
        Initialize();
        gameState = PLAYING;
    }

    //获取游戏当前状态
    public int GetGameState(){
        return gameState;
    }
}
