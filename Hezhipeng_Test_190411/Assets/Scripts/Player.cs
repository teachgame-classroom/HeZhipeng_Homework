using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject barrelObj;
    public GameObject bulletObj;
    public GameObject explosionObj;

    public float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (h != 0 || v != 0)
        {
            Vector3 moveDirection = Vector3.right * h + Vector3.up * v;
            Move(moveDirection, speed);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            KillMySelf();
        }
    }

    private void Move(Vector3 direction, float speed)
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    private void Shoot()
    {
        Instantiate(bulletObj, barrelObj.transform.position, Quaternion.identity);
    }

    private void KillMySelf()
    {
        Instantiate(explosionObj, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
