using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloTest : MonoBehaviour
{
    public int myInt = 0;
    private string myStr;
    public Vector3 myVec = Vector3.zero;
    private int[] myArray = new int[100];

    // Start is called before the first frame update
    void Start()
    {
        myStr = "Hello Unity";
        for(int i = 0; i < myArray.Length; i++)
        {
            myArray[i] = i + 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Sum1to100 = " + Sum1to100());
        }

    }

    private int Sum1to100()
    {
        int sum = 0;

        for(int i = 0; i < myArray.Length; i++)
        {
            sum += myArray[i];
        }

        return sum;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(myVec, 0.5f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, myVec);
    }

}
