using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour
{
    IUserAction userAction;
    GUIStyle msgStyle, titleStyle;
    void Start()
    {
        userAction = SSDirector.GetInstance().CurrentSceneController as IUserAction;

        msgStyle = new GUIStyle();
        msgStyle.normal.textColor = Color.white;
        msgStyle.alignment = TextAnchor.MiddleCenter;
        msgStyle.fontSize = 30;

        titleStyle = new GUIStyle();
        titleStyle.normal.textColor = Color.white;
        titleStyle.alignment = TextAnchor.MiddleCenter;
        titleStyle.fontSize = 60;
    }

    // Update is called once per frame
    void OnGUI() {
        // 重新开始的按钮
        if(GUI.Button(new Rect(Screen.width*0.4f, Screen.height*0.65f, Screen.width*0.2f, Screen.height*0.1f), "Restart")){
            userAction.Restart();
        }
        // 检查是否正确
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height*0.2f), "Preists and Devils", titleStyle);
        if(userAction.GetGameState() == FirstController.WIN){

            GUI.Label(new Rect(0, Screen.height*0.8f, Screen.width, Screen.height*0.2f), "You Win.", msgStyle);
        }     
        else if(userAction.GetGameState() == FirstController.FAILED){
            UnityEngine.Debug.Log("shulou");
            GUI.Label(new Rect(0, Screen.height*0.8f, Screen.width, Screen.height*0.2f), "You failed.", msgStyle);
        }
    }
}