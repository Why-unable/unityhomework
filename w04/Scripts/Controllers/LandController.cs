using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandController: IObjectController
{
    public int side;
    public int[] seat;
    IUserAction userAction;
    Land landModel;

    public LandController(int side, int[] rolesID){
        userAction = SSDirector.GetInstance().CurrentSceneController as IUserAction;
        this.side = side;
        this.seat = new int[6]{-1,-1,-1,-1,-1,-1};
        if(this.side == FirstController.LEFTLAND){
            for(int i = 0; i < 6; i++){
                this.seat[i] = rolesID[i];
            }
        }
    }

    public bool LeaveLand(int roleID){
        for(int i = 0; i < 6; i++){
            if(seat[i] == roleID){
                seat[i] = -1;
                return true;
            }
        }
        return false;
    }

    public bool GoOnLand(int roleID){
        for(int i = 0; i < 6; i++){
            if(seat[i] == -1){
                seat[i] = roleID;
                return true;
            }
        }
        return false;
    }

    public int getEmptySeat(){
        for(int i = 0; i < 6; i++){
            if(seat[i] == -1){
                return i;
            }
        }
        return -1;
    }


    public void CreateModel(){
        landModel = new Land(Position.landInitPos[side]);
    }

    public void Reset(){}

    public void DealClick(){}

    public GameObject GetModelGameObject(){
        return landModel.land;
    }
}
