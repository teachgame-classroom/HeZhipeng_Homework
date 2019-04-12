using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5;
    public float limitLiftTime = 20;
    private float bornTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        bornTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        AutoDestroy();
    }

    private void Move()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
    }

    private void AutoDestroy()
    {
        float liftTime = Time.time - bornTime;
        if (liftTime > limitLiftTime)
        {
            Destroy(gameObject);
        }
    }
}
