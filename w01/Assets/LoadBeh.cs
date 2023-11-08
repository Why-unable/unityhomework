using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBeh : MonoBehaviour
{
    public Transform res;
    public int row = 5;
    public int col = 10;
    public float x_len = 1f;
    public float z_len = 1f;

    void Start()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                GameObject newobj = Instantiate(res, this.transform).gameObject;
                newobj.transform.position = new Vector3(i * x_len, 1f, j * z_len);
            }
        }
    }
}
