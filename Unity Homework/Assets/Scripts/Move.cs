using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    private GameObject controller;
    private Rigidbody rb;
    private float hor;
    private float ver;

    public float speed = 3;

	// Use this for initialization
	void Start () {
        controller = GameObject.Find("GameController");
        rb = gameObject.GetComponent<Rigidbody>();
        
	}
	
	// Update is called once per frame
	void Update () {
        BallMove();
    }

    private void BallMove()
    {
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(hor * speed, 0, ver * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ("Score".Equals(other.tag))
        {
            controller.GetComponent<Controller>().AddScore(10);
            Destroy(other.gameObject);
        }
    }
}
