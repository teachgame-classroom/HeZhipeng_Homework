using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    public GameObject followTarget;
    public bool useTargetSpeed = true;
    public float speed;
    public float followDistance = 3;


    private GameObject barrel;
    private GameObject bullet;

    private Vector3[] track;
    private int trackLength;

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }

    public void init()
    {
        FindBarrel();
        // 设定移动速度
        if(followTarget != null && useTargetSpeed)
        {
            Spaceship spaceshipScript = followTarget.GetComponent<Spaceship>();
            Option optionScript = followTarget.GetComponent<Option>();
            if(spaceshipScript != null)
            {
                speed = spaceshipScript.speed;
            }
            else if(optionScript != null)
            {
                speed = optionScript.speed;
            }
        }
        // 计算跟踪数组长度
        trackLength = (int)(followDistance / speed / 0.015);
        // 初始化耿宗数组
        InitTrack();
        // 
        transform.position = followTarget.transform.position;
    }

    private void FindBarrel()
    {
        Transform barrelTransform = transform.Find("Barrel");
        if (barrelTransform != null)
        {
            barrel = barrelTransform.gameObject;
        }
    }

    private void InitTrack()
    {
        track = new Vector3[trackLength];
        for (int i = 0; i < track.Length; i++)
        {
            track[i] = followTarget.transform.position;
        }
    }

    private void Follow()
    {
        if (followTarget == null || track == null)
        {
            return;
        }
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (h != 0 || v != 0)
        {
            RememberTrack();
        }
        Move();
    }

    private void RememberTrack()
    {
        if(followTarget.transform.position != track[0])
        {
            track = Tool.Prepend(followTarget.transform.position, track, false);
        }
    }

    private void Move()
    {
        Vector3 limitPoint = track[track.Length - 1];
        if (limitPoint != Vector3.zero)
        {
            transform.position = limitPoint;
        }
    }

    public void SetBullet(GameObject bullet)
    {
        this.bullet = bullet;
    }

    public void Shoot()
    {
        Instantiate(bullet, barrel.transform.position, barrel.transform.rotation);
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; track != null && i < track.Length; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(track[i], 0.1f);
        }
    }
}
