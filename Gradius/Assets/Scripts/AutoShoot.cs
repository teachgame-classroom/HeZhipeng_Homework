using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShoot : MonoBehaviour
{
    // 射击频率
    public float shootFrequency = 3f;
    // 可瞄准的最大角度
    public float aimAngle = 180;
    // 原定瞄准敌人各个角度的Sprite
    public Sprite[] aimSprites;

    // 玩家GameObject 用于MoveMode.Situ
    private GameObject vicViper;
    private SpriteRenderer sr;

    private float deltaTime = 0;
    private Spaceship spaceshipScript;

    // Start is called before the first frame update
    void Start()
    {
            vicViper = GameObject.FindGameObjectWithTag("VicViper");
        sr = GetComponent<SpriteRenderer>();
        spaceshipScript = GetComponent<Spaceship>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Tool.IsOutOfCameraX(gameObject.transform.position.x, -gameObject.transform.localScale.x * .5f))
        {
            return;
        }
        Aim();
        Shoot();
    }

    private void Aim()
    {
        if (vicViper == null)
        {
            return;
        }
        float eachAngle = aimAngle / aimSprites.Length;
        // 当前对象到vicViper的向量
        Vector3 mv = vicViper.transform.position - transform.position;
        // mv与x轴正方向的夹角
        float angleMVX = 0;
        // 非倒置 在世界坐标系第四象限时固定为0
        if (!IsInvert() && mv.y < 0 && mv.x>=0)
        {
            angleMVX = 0;
        }// 非倒置 在世界坐标系第三象限时固定为180
        else if(!IsInvert() && mv.y <0 && mv.x < 0)
        {
            angleMVX = 180;
        }// 倒置 在世界坐标系第二象限时固定为0
        else if(IsInvert() && mv.y >= 0 && mv.x <= 0)
        {
            angleMVX = 180;
        }// 倒置 在世界坐标系第一象限时固定为180
        else if (IsInvert() && mv.y >= 0 && mv.x > 0)
        {
            angleMVX = 0;
        }
        else
        {
            // 求出与x轴正方向的夹角
            angleMVX = Vector3.Angle(Vector3.right, mv);
        }
        // 设定该使用的Sprite
        sr.sprite = aimSprites[(int)(angleMVX / eachAngle)];
    }

    private void Shoot()
    {
        deltaTime += Time.deltaTime;
        if (spaceshipScript == null || !spaceshipScript.CanShoot() || deltaTime < shootFrequency || vicViper == null)
        {
            return;
        }
        // 当前对象发射点到vicViper的向量
        Vector3 bv = vicViper.transform.position - spaceshipScript.GetBarrel().transform.position;
        Vector3 direction = Vector3.zero;
        // 非倒置 在世界坐标系第四象限时固定为向右
        if (!IsInvert() && bv.y < 0 && bv.x >= 0)
        {
            direction = Vector3.right;
        }//非倒置 在世界坐标系第三象限时固定为向左
        else if (!IsInvert() && bv.y < 0 && bv.x < 0)
        {
            direction = Vector3.left;
        }// 倒置 在世界坐标系第二象限时固定为向右
        else if (IsInvert() && bv.y >= 0 && bv.x <= 0)
        {
            direction = Vector3.right;
        }// 倒置 在世界坐标系第一象限时固定为向右
        else if (IsInvert() && bv.y >= 0 && bv.x > 0)
        {
            direction = Vector3.left;
        }
        else
        {
            direction = bv;
        }
        spaceshipScript.Shoot(direction, ShootPosition.Barrel, Quaternion.identity);
        // 重置计时
        deltaTime = 0;
    }

    private bool IsInvert()
    {
        return transform.localScale.y < 0;
    }
}
