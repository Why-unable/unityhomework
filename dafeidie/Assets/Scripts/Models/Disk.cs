using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskAttributes : MonoBehaviour
{
    //public GameObject gameobj;
    public int score;
    public float speedX;
    public float speedY;
}

public class Disk
{
    public GameObject disk;


    public Disk(){
        disk = GameObject.Instantiate(Resources.Load("Prefabs/disk", typeof(GameObject))) as GameObject;
        disk.AddComponent<DiskAttributes>();
    }
}