using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController
{
    GameObject moveObject;

    public bool IsMoving(){
        return(this.moveObject != null && this.moveObject.GetComponent<Move>().isMoving == true);
    }


    public void SetMove(GameObject moveObject, Vector3 destination) {
        // 设置一个新的移动
        Move test;
        this.moveObject = moveObject;
        if (!moveObject.TryGetComponent<Move>(out test)) {
            moveObject.AddComponent<Move>();
        }
        this.moveObject.GetComponent<Move>().destination = destination;

        if (this.moveObject.transform.localPosition.y > destination.y) {
            //UnityEngine.Debug.Log("mid_d");
            this.moveObject.GetComponent<Move>().mid_destination = new Vector3(destination.x, this.moveObject.transform.localPosition.y, destination.z);
        }
        else {
            this.moveObject.GetComponent<Move>().mid_destination = new Vector3(this.moveObject.transform.localPosition.x, destination.y, destination.z);
        }
    }
}
