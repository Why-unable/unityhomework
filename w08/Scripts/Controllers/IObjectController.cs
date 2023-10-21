using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectController
{
    void DealClick();
    void CreateModel();
    void Reset();
    GameObject GetModelGameObject();
}
