using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskCreator : MonoBehaviour
{
    public GameObject[] workers = null;
    public GameObject foodsSet;
    private TaskCondition condition = null;


    // Start is called before the first frame update
    void Start()
    {
        //foodsSet = GameObject.Find("FoodSet");
    }

    // Update is called once per frame
    void Update()
    {
        RefreshTask();
    }

    private void RefreshTask()
    {
        if (condition == null || IsTaskComplete())
        {
            GiveBackFoods(foodsSet);
            CollectFoods(foodsSet);
        }
    }

    private void CollectFoods(GameObject foodSet = null)
    {
        GameObject[] foods = null;
        if (foodSet == null)
        {
            foods = GameObject.FindGameObjectsWithTag("Goods");
        }
        else
        {
            foods = new GameObject[foodSet.transform.childCount];
            for (int i = 0; i < foods.Length; i++)
            {
                foods[i] = foodSet.transform.GetChild(i).gameObject;
            }
        }
        condition = new TaskCondition();
        condition.collectType = typeof(Food);
        condition.collectNum = Random.Range(1, 10);
        for(int i = 0; i < condition.collectNum; i++)
        {
            foods[i].transform.position = Random.insideUnitCircle * 3;
            foods[i].transform.position += foodSet == null ? Vector3.right * 3 : foodSet.transform.position;
            foods[i].SetActive(true);
        }
        OtherTool.SetText("ContentText", string.Format("任务内容:收集{0}个食物", condition.collectNum));
        workers = new GameObject[] { GameObject.Find("Sirika") };
    }

    private void GiveBackFoods(GameObject foodsSet = null)
    {
        if (foodsSet == null)
        {
            for (int i = 0; i < workers.Length; i++)
            {
                workers[i].transform.DetachChildren();
            }
        }
        else
        {
            for (int i = 0; i < workers.Length; i++)
            {
                int childCount = workers[i].transform.childCount;
                for (int j = 0; j < childCount; j++)
                {
                    workers[i].transform.GetChild(0).parent = foodsSet.transform;
                }
            }
        }
    }

    public bool IsTaskComplete()
    {
        bool ret = false;
        if (workers != null)
        {
            for (int i = 0; i < workers.Length; i++)
            {
                ret = ret || IsWorkerComplete(workers[i]);
                if (ret)
                {
                    break;
                }
            }
        }
        return ret;
    }

    public bool IsWorkerComplete(GameObject worker)
    {
        bool ret = false;
        if (worker != null)
        {
            int workerCollectNum = 0;
            for (int i = 0; i < worker.transform.childCount; i++)
            {
                GameObject collection = worker.transform.GetChild(i).gameObject;
                if (collection.GetComponent(condition.collectType) != null)
                {
                    workerCollectNum++;
                    if (workerCollectNum >= condition.collectNum)
                    {
                        ret = true;
                        break;
                    }
                }
            }
        }
        return ret;
    }
}

//[System.Serializable]
public class TaskCondition
{
    // 限时
    public float limitTime;
    public int collectNum;
    public string collectName;
    public System.Type collectType;
    //public T[] collectObj;
    //public int killEnemy;
}
