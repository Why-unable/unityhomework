using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBeh : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print("hello!");
        GameObject cube=GameObject.CreatePrimitive(PrimitiveType.Cube);        
        cube.name="a.cube";
        cube.transform.position=new Vector3(0,Random.Range(0,5),0);
        cube.transform.parent=this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("update!!");
    }

    void OnEnable(){
        Debug.Log("Enabled!!");
    }

    void OnDisable(){
        Debug.Log("Disabled!!");
    }
}
