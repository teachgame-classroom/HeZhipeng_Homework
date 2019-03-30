using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherTool {
    public static T[] Add<T>(T[] array, T item)
    {
        T[] ret = null;
        if(array == null)
        {
            ret = new T[] { item };
        }
        else
        {
            ret = new T[array.Length + 1];
            for (int i = 0; i < array.Length; i++)
            {
                ret[i] = array[i];
            }
            ret[array.Length] = item;
        }
        return ret;
    }

    public static T[] AddRange<T>(T[] array, T[] range)
    {
        T[] ret = null;
        if (array == null)
        {
            ret = range;
        }
        else
        {
            ret = new T[array.Length + range.Length];
            for (int i = 0; i < array.Length; i++)
            {
                ret[i] = array[i];
            }
            for(int i = array.Length; i < ret.Length; i++)
            {
                ret[i] = range[i - array.Length];
            }
        }
        return ret;
    }

    public static GameObject[] FindGameObjectsWithTags(string[] tags)
    {
        GameObject[] ret = null;
        for (int i = 0; i < tags.Length; i++)
        {
            ret = AddRange(ret, GameObject.FindGameObjectsWithTag(tags[i]));
        }
        return ret;
    }

    public static void SetText(string textName, string content)
    {
        UnityEngine.UI.Text text = GameObject.Find(textName).GetComponent<UnityEngine.UI.Text>();
        text.text = content;
    }
}
