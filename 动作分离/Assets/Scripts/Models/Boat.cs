using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat
{
    public GameObject boat;//船对象

    public Boat(Vector3 initPos){
        boat = GameObject.Instantiate(Resources.Load("Prefabs/myBoat", typeof(GameObject))) as GameObject;
        boat.transform.position = initPos;
        boat.AddComponent<Click>();
    }

}
