﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool
{

    private static GameObject camera;
    private static Vector3 cameraScale = Vector3.zero;

    public static bool IsOutOfCameraX(float positionX, float scaleX = 0)
    {
        bool ret = true;
        //float deltaX = positionX - GetCamera().transform.position.x;
        float deltaX = positionX - Camera.main.transform.position.x;
        float limitDistanceX = GetCameraScale().x * .5f - scaleX;
        ret = deltaX * deltaX > limitDistanceX * limitDistanceX;
        return ret;
    }

    public static bool IsOutOfCameraY(float positionY, float scaleY = 0)
    {
        bool ret = true;
        //float deltaY = positionY - GetCamera().transform.position.y;
        float deltaY = positionY - Camera.main.transform.position.y;
        float limitDistanceY = GetCameraScale().y * .5f - scaleY;
        ret = deltaY * deltaY > limitDistanceY * limitDistanceY;
        return ret;
    }

    public static Vector3 GetCameraScale()
    {
        if (cameraScale != Vector3.zero)
        {
            return cameraScale;
        }
        float width = 0;
        float height = 0;
        //height = GetCamera().GetComponent<Camera>().orthographicSize * 2;
        height = Camera.main.orthographicSize * 2;
        // 摄像机纵横比例
        //float aspectRatio = Screen.width * 1.0f / Screen.height;
        float aspectRatio = Camera.main.aspect;
        width = height * aspectRatio;
        //Vector3 ret = new Vector2(width, height);
        Vector3 ret = Vector3.right * width + Vector3.up * height;
        return ret;
    }

    public static GameObject GetCamera()
    {
        if (camera == null)
        {
            //camera = GameObject.FindGameObjectWithTag("MainCamera");
            camera = Camera.main.gameObject;
        }
        return camera;
    }

    public static T[] Prepend<T>(T item, T[] array, bool increaseLength)
    {
        T[] retArray = new T[1] { item };

        if (array != null && array.Length > 0)
        {
            int retArrayLength = increaseLength ? array.Length + 1 : array.Length;
            retArray = new T[retArrayLength];
            retArray[0] = item;
            for (int i = 1; i < retArray.Length; i++)
            {
                retArray[i] = array[i - 1];
            }
        }
        return retArray;
    }

    public static T[] Append<T>(T item, T[] array, bool increaseLength)
    {
        T[] retArray = new T[1] { item };

        if (array != null && array.Length > 0)
        {
            int retArrayLength = increaseLength ? array.Length + 1 : array.Length;
            if (increaseLength)
            {
                retArray = new T[array.Length + 1];
                for (int i = 0; i < array.Length; i++)
                {
                    retArray[i] = array[i];
                }
            }
            else
            {
                retArray = new T[array.Length];
                for (int i = 1; i < array.Length; i++)
                {
                    retArray[i - 1] = array[i];
                }
            }
            retArray[retArray.Length - 1] = item;
        }

        return retArray;
    }
}
