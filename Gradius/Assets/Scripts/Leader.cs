﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : MonoBehaviour
{
    public int memberNum = 5;
    public Vector3 offset = Vector3.right;
    public string memberPrefabPath = "Prefabs/Enemies/Enemy_1";
    public bool useRouteMoveMode = false;

    private GameObject memberPrefab;
    private GameObject[] members;
    private Transform[] routePoints;

    // Start is called before the first frame update
    void Start()
    {
        memberPrefab = Resources.Load<GameObject>(memberPrefabPath);
        if(useRouteMoveMode)
        {
            routePoints = FindRoutePoints();
            memberPrefab.GetComponent<AutoMove>().moveMode = MoveMode.Route;
            members = BuildMembers(memberPrefab, memberNum, routePoints[0].position, offset);
        }
        else
        {
            members = BuildMembers(memberPrefab, memberNum, transform.position, offset);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private Transform[] FindRoutePoints()
    {
        Transform route = transform.Find("Route");
        Transform[] ret = new Transform[route.childCount];
        for (int i = 0; i < route.childCount; i++)
        {
            Transform tempChild = route.GetChild(i);
            if (tempChild.gameObject.activeInHierarchy)
            {
                ret[i] = tempChild;
            }
        }
        return ret;
    }

    private GameObject[] BuildMembers(GameObject prefab, int num, Vector3 firstPos, Vector3 offset)
    {
        Reward rewardScript = prefab.GetComponent<Reward>();
        if(rewardScript != null)
        {
            rewardScript.inLine = true;
            rewardScript.awards[0].odds = 10;
        }
        GameObject[] ret = new GameObject[num];
        for (int i = 0; i < num; i++)
        {
            ret[i] = Instantiate(prefab, firstPos + offset * i, Quaternion.identity, transform);
        }
        return ret;
    }

    public Transform[] GetRoutePoints()
    {
        return routePoints;
    }

    public bool WillAce()
    {
        bool ret = true;
        int liftMemberCount = 0;
        for(int i = 0; i < memberNum; i++)
        {
            if(members[i] != null)
            {
                liftMemberCount++;
            }
        }
        ret = liftMemberCount <= 1;
        return ret;
    }

    public bool IsAce()
    {
        bool ret = true;
        for (int i = 0; i < memberNum; i++)
        {
            if (members[i] != null)
            {
                ret = false;
                break;
            }
        }
        return ret;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(transform.position, .2f);
    }
}
