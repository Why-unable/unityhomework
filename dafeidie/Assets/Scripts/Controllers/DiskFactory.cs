using System.IO;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MyException : System.Exception
{
    public MyException() { }
    public MyException(string message) : base(message) { }
}

public class DiskFactory : MonoBehaviour
{
    
    List<GameObject> disk_used;
    List<GameObject> disk_free;
    System.Random rand;
    public int miss=0;
    // Start is called before the first frame update
    void Start()
    {
        
        disk_used = new List<GameObject>();
        disk_free = new List<GameObject>();
        rand = new System.Random();
        //Disk disk = GetDisk(1); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setAttributes(GameObject disk,int round){
        //根据不同round设置diskAttributes的值
        //随意的旋转角度
        disk.transform.localEulerAngles = new Vector3(-rand.Next(20,40),0,0);

        DiskAttributes attri = disk.GetComponent<DiskAttributes>();
        attri.score = rand.Next(1,4);
        //由分数来决定速度、颜色、大小
        //速度（大小）
        attri.speedX = (rand.Next(1,5) + attri.score + round) * 0.2f;
        attri.speedY = (rand.Next(1,5) + attri.score + round) * 0.2f;
        //颜色
        Color[] color={Color.blue, Color.green, Color.red};
        disk.GetComponent<Renderer>().material.color = color[attri.score-1];
        //大小
        disk.transform.localScale += new Vector3(-0.2f * (attri.score-1), 0, -0.2f * (attri.score-1));
        
        //飞碟可从四个方向飞入（左上、左下、右上、右下）
        int[,] dir={{1,1},{1,-1},{-1,1},{-1,-1}};
        float[,] screenPos={{Screen.width*0.1f,0},{Screen.width*0.1f,Screen.height*1.1f},{Screen.width*1f,0},{Screen.width*0.8f,Screen.height*0.8f}};
        int direction = rand.Next(0,4);

        //速度方向
        attri.speedX *= dir[direction,0];
        attri.speedY *= dir[direction,1];
        //初始位置
        disk.transform.Translate(Camera.main.ScreenToWorldPoint(new Vector3(screenPos[direction,0], screenPos[direction,1], 8)));
        UnityEngine.Debug.Log("trans",disk.transform);
    }

    public GameObject GetDisk(int round) {
        GameObject disk;
        if (disk_free.Count != 0) {
            disk = disk_free[0];
            disk_free.Remove(disk);
        }
        else {
            disk=new Disk().disk;
        }
        setAttributes(disk,round);
        //setAttributes(disk,round);
        disk_used.Add(disk);
        disk.SetActive(true);
        UnityEngine.Debug.Log("generate disk");
        return disk;
    }

    public void FreeDisk(GameObject disk) {
        disk.SetActive(false);
        //将位置和大小恢复到预制，这点很重要！
        disk.transform.position = new Vector3(0, 0,0);
        disk.transform.localScale = new Vector3(2f,0.1f,2f);
        if (!disk_used.Contains(disk)) {
            throw new MyException("Try to remove a item from a list which doesn't contain it.");
        }
        UnityEngine.Debug.Log("free disk");
        disk_used.Remove(disk);
        disk_free.Add(disk);
    }
}