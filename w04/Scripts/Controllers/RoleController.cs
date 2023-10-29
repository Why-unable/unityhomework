using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleController : IObjectController
{

    public int roleType;
    public int roleState;
    public int id;
    Role roleModel;
    IUserAction userAction;

    public RoleController(int roleType, int id){
        userAction = SSDirector.GetInstance().CurrentSceneController as IUserAction;

        this.roleType = roleType;
        this.id = id;
        Reset();
    }

    public void Reset(){
        this.roleState = FirstController.LEFTLAND;
    }

    public void GoTo(int pos){
        this.roleState = pos;
    }

    public void CreateModel(){
        roleModel = new Role(roleType, Position.roleLeftPos[id]);
        roleModel.role.GetComponent<Click>().setClickAction(this);
    }

    public void DealClick(){
        userAction.MoveRole(this.id);
    }

    public GameObject GetModelGameObject(){
        return roleModel.role;
    }
}
