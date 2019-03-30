using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charactar : MonoBehaviour
{
    private void Update()
    {
        //RemoveChildrens(GameObject.Find("FoodSet"));
    }

    private void RemoveChildrens(GameObject goodsSet = null)
    {
        bool isTaskComplete = GameObject.Find("GameController").GetComponent<TaskCreator>().IsTaskComplete();
        if (!isTaskComplete)
        {
            return;
        }
        if(goodsSet == null)
        {
            transform.DetachChildren();
        }
        else
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).parent = goodsSet.transform;
            }
        }
    }
}
