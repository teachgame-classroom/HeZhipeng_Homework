using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PrimaryWeaponType
{
    Normal = 0,
    Double = 1,
    Laser = 2
}
public class VicViper : Spaceship
{
    public string VICVIPER_PATH = "Prefabs/Vicviper/Vicviper";
    // 复活机会
    public int revival = 1;
    public int power = 0;
    public KeyCode shootKey = KeyCode.J;
    public KeyCode usePower = KeyCode.K;
    
    public const int missileIdx = 1;
    public int maxMissileNum = 1;

    public string OPTION_PATH = "Prefabs/Vicviper/Option";
    public int optionMaxNum = 3;
    public Vector3 optionOffset = Vector3.down;

    private GameObject vicviperPre;
    private Vector3 halfLocalScale;

    private PrimaryWeaponType primaryWeapon = PrimaryWeaponType.Normal;
    private int missileNum = 0;

    private GameObject[] options;
    private int optionNum = 0;

    // Start is called before the first frame update
    //void Start()
    //{
    //    LoadBullets();
    //    FindBarrel();
    //}

    // Update is called once per frame
    void Update()
    {
        Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (Input.GetKeyDown(shootKey))
        {
            Shoot();
        }
        if (Input.GetKeyDown(usePower))
        {
            GroupUp();
        }
    }

    protected override void Init()
    {
        base.Init();
        primaryWeapon = PrimaryWeaponType.Normal;
        vicviperPre = Resources.Load<GameObject>(VICVIPER_PATH);
        halfLocalScale = gameObject.transform.localScale * .5f;
        LoadOptions();
    }

    private void LoadOptions()
    {
        GameObject optionPre = Resources.Load<GameObject>(OPTION_PATH);
        options = new GameObject[optionMaxNum];
        Option optionScript = optionPre.GetComponent<Option>();
        for (int i = 0; i < optionMaxNum; i++)
        {
            if(i <= 0)
            {
                optionScript.followTarget = gameObject;
            }
            else
            {
                optionScript.followTarget = options[i - 1];
            }
            options[i] = Instantiate(optionPre, transform.position, Quaternion.identity, transform);
            options[i].SetActive(false);
        }
    }

    protected override void Move(float horizontal, float vertical)
    {
        Vector3 recordPos = gameObject.transform.position;
        base.Move(horizontal, vertical);

        float x = gameObject.transform.position.x;
        float y = gameObject.transform.position.y;
        if (Tool.IsOutOfCameraX(x, halfLocalScale.x))
        {
            x = recordPos.x;
        }
        if (Tool.IsOutOfCameraY(y, halfLocalScale.y))
        {
            y = recordPos.y;
        }
        gameObject.transform.position = new Vector3(x, y, 0);
    }

    protected void Shoot()
    {
        if(primaryWeapon == PrimaryWeaponType.Normal || primaryWeapon == PrimaryWeaponType.Laser)
        {
            NormalShoot();
        }

        if(primaryWeapon == PrimaryWeaponType.Double)
        {
            DoubleShoot();
        }

        if (missileNum > 0)
        {
            MissileShoot();
        }
    }

    private void NormalShoot()
    {
        Shoot(Vector3.right, ShootPosition.Barrel, Quaternion.identity);
        for (int i = 0; i < optionNum; i++)
        {
            Instantiate(bullets[bulletIdx], options[i].transform.position, Quaternion.identity);
        }
    }

    private void DoubleShoot()
    {
        NormalShoot();

        Shoot(Vector3.right, ShootPosition.MySelf, Quaternion.Euler(0, 0, 45));
        for (int i = 0; i < optionNum; i++)
        {
            Instantiate(bullets[bulletIdx], options[i].transform.position, Quaternion.Euler(0, 0, 45));
        }
    }

    private void MissileShoot()
    {
        int memoIdx = bulletIdx;
        bulletIdx = missileIdx;
        Shoot(Vector3.right, ShootPosition.MySelf, Quaternion.Euler(0, 0, 315));
        for (int i = 0; i < optionNum; i++)
        {
            Instantiate(bullets[missileIdx], options[i].transform.position, Quaternion.Euler(0, 0, 315));
        }
        bulletIdx = memoIdx;
    }

    private void GroupUp()
    {
        if (power <= 0)
        {
            return;
        }
        bool groupUpSuccess = false;
        switch (power)
        {
            case 1:
                groupUpSuccess = SpeedUp();
                break;
            case 2:
                groupUpSuccess = AddMissile();
                break;
            case 3:
                groupUpSuccess = UseDouble();
                break;
            case 4:
                groupUpSuccess = UseLaser();
                break;
            case 5:
                groupUpSuccess = AddOption();
                break;
            //case 6:
            //    groupUpSuccess = GetForce();
            //    break;
            default:
                groupUpSuccess = AddOption();
                break;

        }
        if (groupUpSuccess)
        {
            power = 0;
        }
    }

    private bool SpeedUp()
    {
        bool ret = false;

        if(speed <= 10)
        {
            speed++;
            ret = true;
        }
        return ret;
    }

    private bool AddMissile()
    {
        if (missileNum >= maxMissileNum)
        {
            return false;
        }
        missileNum++;
        return true;
    }

    private bool UseDouble()
    {
        if(primaryWeapon == PrimaryWeaponType.Double)
        {
            return false;
        }
        primaryWeapon = PrimaryWeaponType.Double;
        return true;
    }

    private bool UseLaser()
    {
        if(primaryWeapon == PrimaryWeaponType.Laser)
        {
            return false;
        }
        primaryWeapon = PrimaryWeaponType.Laser;
        bulletIdx = (int)primaryWeapon;
        return true;
    }

    private bool AddOption()
    {
        if(optionNum >= optionMaxNum)
        {
            return false;
        }
        int activeIdx = optionNum;
        Option optionScript = options[activeIdx].GetComponent<Option>();
        optionScript.SetBullet(bullets[Mathf.Min(bulletIdx, 1)]);
        optionScript.init();
        options[activeIdx].SetActive(true);
        optionNum++;
        return true;
    }

    private bool GetForce()
    {
        return false;
    }

    private void Relive()
    {
        Init();
        float cameraHalfWidth = Tool.GetCameraScale().x * .5f;
        Vector3 initPosition = new Vector3(Tool.GetCamera().transform.position.x - cameraHalfWidth + halfLocalScale.x, 0, 0);
        //Instantiate(vicviperPre, initPosition, Quaternion.identity, transform.parent);
        transform.position = initPosition;
        gameObject.SetActive(true);
    }

    public override void Hurt(int damage)
    {
        if (damage <= 0)
        {
            return;
        }
        hp -= damage;
        if (hp <= 0)
        {
            Die(false);
        }
        revival--;
        if (revival > 0)
        {
            Relive();
        }
    }

    public void GetPowerBox()
    {
        power++;
        power = power % 6 + 1;
    }

    public void GetRevivalBox()
    {
        revival++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if("Ground".Equals(collision.tag))
        {
            Hurt(hp);
        }
    }
    
}
