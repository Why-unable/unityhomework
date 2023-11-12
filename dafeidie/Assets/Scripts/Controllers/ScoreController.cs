using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    int score;
    public RoundController roundController;
    public UserGUI userGUI;
    // Start is called before the first frame update
    void Start()
    {
        roundController = (RoundController)SSDirector.getInstance().currentSceneController;

        //这行代码使得RoundController中的gameObject.AddComponent<ScoreController>();的同时，
        //将自身赋给了roundController.scoreController 
        roundController.scoreController = this;

        userGUI = this.gameObject.GetComponent<UserGUI>();
    }

    public void Record(GameObject disk) {
        score += disk.GetComponent<DiskAttributes>().score;
        userGUI.score = score;
    }
    public void Publish(){
        UnityEngine.Debug.Log("publish");
        if(score>=1)
            score-=1;
        userGUI.score = score;
    }

    
}
