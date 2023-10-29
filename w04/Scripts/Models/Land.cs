using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land
{
    public GameObject land;

    public Land(Vector3 initPos){
        land = GameObject.Instantiate(Resources.Load("Prefabs/myLand", typeof(GameObject))) as GameObject;
        land.transform.position = initPos;
    }
}
