using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role
{
    public GameObject role;

   public Role(int roleType, Vector3 initPos){
        string path = "Prefabs/" + ((roleType == FirstController.PRIEST) ? "myPriest" : "myDevil");
        role = GameObject.Instantiate(Resources.Load(path, typeof(GameObject))) as GameObject;
        role.transform.position = initPos;
        role.AddComponent<Click>();
   }
}
