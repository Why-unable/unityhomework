using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager, IActionCallback 
{
    public RoundController sceneController;//RoundController相当于FirstController
    public CCAction action;
    public DiskFactory factory;
    
    // Start is called before the first frame update
    new void Start()
    {
        sceneController = Singleton<RoundController>.Instance;
        sceneController.actionManager = this;
        factory = Singleton<DiskFactory>.Instance;
    }

    //回调函数
    public void SSActionEvent(SSAction source,
        SSActionEventType events = SSActionEventType.Completed,
        int intParam = 0,
        string strParam = null,
        Object objectParam = null) {
            factory.FreeDisk(source.transform.gameObject);
    }

    public void MoveDisk(GameObject disk) {
        action = CCAction.GetSSAction(disk.GetComponent<DiskAttributes>().speedX, disk.GetComponent<DiskAttributes>().speedY);
        RunAction(disk, action, this);
    }
}
