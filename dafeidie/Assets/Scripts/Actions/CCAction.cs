using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//飞碟从界面左右两侧飞入，离开界面时运动结束
public class CCAction : SSAction
{
    public float speedX;
    public float speedY;
    public static CCAction GetSSAction(float x, float y) {
        CCAction action = ScriptableObject.CreateInstance<CCAction>();
        //CCFlyAction action = SSAction.Instance;
        action.speedX = x;
        action.speedY = y;
        return action;
    }
    // Start is called before the first frame update
    public override void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        //检查当前脚本所附加的游戏对象是否处于活动状态
        if (this.transform.gameObject.activeSelf == false) {//飞碟已经被"销毁"
            Debug.Log("1");
            this.destroy = true;
            this.callback.SSActionEvent(this);
            return;
        }
        Vector3 vec3 = Camera.main.WorldToScreenPoint (this.transform.position);
        if (vec3.x < -200 || vec3.x > Camera.main.pixelWidth + 200 || vec3.y < -200 || vec3.y > Camera.main.pixelHeight + 200) {
            Debug.Log("2");
            this.destroy = true;
            this.callback.SSActionEvent(this);
            return;
        }
        //更新位置
        //Time.deltaTime表示从上一帧到当前帧的时间间隔
        // += 速度*时间
        transform.position += new Vector3(speedX, speedY, 0) * Time.deltaTime * 2;
        
        
    }
}
