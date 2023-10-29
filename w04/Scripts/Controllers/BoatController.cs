using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : IObjectController
{
    public bool onLeftside; // 船是否在左侧岸边
    IUserAction userAction; // 用户操作接口
    public int[] seat; // 座位数组，存储船上的乘客座位号
    public Boat boatModel; // 船的模型对象
    public int seatNum=2; //船的座位   //添加这个是为了方便控制船的位置数
                                      //在下面的初始化、循环中统一用seatNum
    public BoatController()
    {
        userAction = SSDirector.GetInstance().CurrentSceneController as IUserAction; // 获取当前场景的用户操作接口
        seat = new int[seatNum]; // 初始化座位数组
        Reset(); // 重置船的状态
    }

    public void Reset()
    {
        onLeftside = true; // 船在左侧岸边
        for (int i = 0; i < seatNum; i++)
        {
            seat[i] = -1; // 初始化座位数组，表示座位为空
        }
    }

    public int embark(int roleID)
    {
        for (int i = 0; i < seatNum; i++)
        {
            if (seat[i] == -1) // 找到一个空座位
            {
                seat[i] = roleID; // 将角色ID设置为座位号
                return i; // 返回座位索引
            }
        }
        return -1; // 没有空座位，返回-1
    }

    public int getEmptySeat()
    {
        for (int i = 0; i < seatNum; i++)
        {
            if (seat[i] == -1) // 找到一个空座位
            {
                return i; // 返回座位索引
            }
        }
        return -1; // 没有空座位，返回-1
    }
    public Boolean isEmpty()    //与getEmptySeat几乎一样；
    {                           //添加这个是为了判断船是否为空，若为空就不让船移动
        for (int i = 0; i < seatNum; i++)
        {
            if(seat[i] != -1){
                return false;
            }
        }
        return true;
    }

    public void disembark(int roleID)
    {
        for (int i = 0; i < seatNum; i++)
        {
            if (seat[i] == roleID) // 找到角色对应的座位
            {
                seat[i] = -1; // 将座位设置为空
                return;
            }
        }
    }

    public void CreateModel()
    {
        boatModel = new Boat(Position.boatLeftPos); // 创建船的模型对象，并设置初始位置为左侧岸边
        boatModel.boat.GetComponent<Click>().setClickAction(this); // 获取船模型上的 Click 组件，并设置点击事件的处理程序为当前 BoatController 对象
    }

    public void DealClick()
    {
        userAction.MoveBoat(); // 处理船的点击事件，调用用户操作接口的 MoveBoat() 方法
    }

    public GameObject GetModelGameObject()
    {
        return boatModel.boat; // 返回船模型的游戏对象
    }
}