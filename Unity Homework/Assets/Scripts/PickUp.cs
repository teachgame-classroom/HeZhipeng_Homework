using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    // 可拾取的类型
    [Header("可拾取的类型")]
    public string[] tags = new string[] { "Goods" };

    [Header("可拾取的半径")]
    public float radius = 1f;

    public KeyCode pickUpKey = KeyCode.Space;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pickUpKey))
        {
            GameObject[] canPickUpGameObjs = OtherTool.FindGameObjectsWithTags(tags);
            float sqrRadius = radius*radius;
            int pickUpGameObjIdx = -1;
            for(int i = 0; i < canPickUpGameObjs.Length; i++)
            {
                float sqrDistance = (canPickUpGameObjs[i].transform.position - transform.position).sqrMagnitude;
                if (sqrDistance <= sqrRadius)
                {
                    sqrRadius = sqrDistance;
                    pickUpGameObjIdx = i;
                }
            }

            if (pickUpGameObjIdx >= 0)
            {
                canPickUpGameObjs[pickUpGameObjIdx].transform.parent = transform;
                canPickUpGameObjs[pickUpGameObjIdx].SetActive(false);
            }
        }
    }
}
