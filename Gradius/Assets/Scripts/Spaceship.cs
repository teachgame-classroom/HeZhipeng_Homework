using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ShootPosition
{
    MySelf = 0,
    Barrel = 1
}
public class Spaceship : MonoBehaviour
{
    public Team team = Team.leftTeam;
    public int maxHp = 1;
    public string bulletPath = "Prefabs/Bullets";
    public float speed = 5;
    public bool orientToDirection = false;
    public string dieEffectPath = "Prefabs/Effects/Die/Normal";

    protected int hp = 1;
    protected GameObject barrel;
    protected GameObject[] bullets;
    protected int bulletIdx = 0;
    protected bool canShoot = false;
    protected Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Spaceship spaceship = collision.GetComponent<Spaceship>();
        if(spaceship != null && spaceship.team != team)
        {
            spaceship.Hurt(1);
        }
    }

    protected virtual void Init()
    {
        hp = maxHp;
        LoadBullets();
        FindBarrel();
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    protected void LoadBullets()
    {
        if(bullets == null)
        {
            bullets = Resources.LoadAll<GameObject>(bulletPath);
        }
        canShoot = bullets != null;
        bulletIdx = 0;
    }

    protected void FindBarrel()
    {
        Transform barrelTransform = transform.Find("Barrel");
        if(barrelTransform == null)
        {
            barrel = null;
        }
        else
        {
            canShoot = true;
            barrel = barrelTransform.gameObject;
        }
    }

    protected virtual void Move(float horizontal, float vertical)
    {
        Vector3 deltaDis = (Vector3.up * vertical + Vector3.right * horizontal) * speed * Time.deltaTime;
        gameObject.transform.Translate(deltaDis);
    }

    public void Shoot(Vector3 direction, ShootPosition position, Quaternion rotation)
    {
        if (!canShoot)
        {
            return;
        }
        Bullet bulletScript = bullets[bulletIdx].GetComponent<Bullet>();
        bulletScript.shootTeam = team;
        bulletScript.direction = direction;
        bullets[bulletIdx].transform.localScale = transform.localScale;
        if (position == ShootPosition.Barrel)
        {
            Instantiate(bullets[bulletIdx], barrel.transform.position, rotation);
        }
        else if(position == ShootPosition.MySelf)
        {
            Instantiate(bullets[bulletIdx], transform.position, rotation);
        }

    }

    public virtual void Hurt(int damage)
    {
        if(damage <= 0)
        {
            return;
        }
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }

    protected void Die()
    {
        Die(true);
    }

    protected void Die(bool doDestroy)
    {
        GameObject dieEffect = Resources.Load<GameObject>(dieEffectPath);
        dieEffect.transform.localScale = transform.localScale;
        Instantiate(dieEffect, transform.position, Quaternion.identity);
        if (doDestroy)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
        Reward rewardScript = gameObject.GetComponent<Reward>();
        if (rewardScript != null)
        {
            rewardScript.BuildAward();
        }
    }

    public GameObject GetBarrel()
    {
        return barrel;
    }

    public GameObject[] GetBullets()
    {
        return bullets;
    }

    public int GetBulletIndex()
    {
        return bulletIdx;
    }

    public bool CanShoot()
    {
        return canShoot;
    }
}
