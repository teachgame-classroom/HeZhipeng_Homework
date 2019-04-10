using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoStraight : MonoBehaviour
{
    public Vector3 unitVector = new Vector3(1, 0, 0);
    public float speed = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        //unitVector = Tool.GetUnitVector(unitVector);
        unitVector = unitVector.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += unitVector * speed * Time.deltaTime;
    }
}
