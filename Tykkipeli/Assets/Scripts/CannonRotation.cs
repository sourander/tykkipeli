using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CannonRotation : NetworkBehaviour
{
    Animator anim;

    public GameObject BulletPrefab;
    public GameObject MinionPrefabTEMP; // OBS! HUOM! VÄÄRÄ!
    public GameObject MinionSpawnPointLeft;
    public GameObject MinionSpawnPointRight;
    private GameObject startScreen;
    public GameObject theGameController;
    public GameController gameControllerScript;

    public Transform ShotSpawnTransform;

    public float projectileSpeed;
    public float reloadRate = 0.5f;
    private float nextShotTime;

    private bool hasPressedReadyCheck = false;
    public bool isOnTheLeftSide;

    [SyncVar]
    public int bullettype; // 0 = normal bullet, 1 = buff, 2 = Thunder Cloud, 3 = Oil Drop

    [SyncVar]
    public bool hasJustShotABullet;


    void Start()
    {
        anim = GetComponent<Animator>();

        // Locate startround(.png)
        // Locate the GameController object and class
        startScreen = GameObject.Find("startround");
    }

    public override void OnStartLocalPlayer()
    {
        MinionSpawnPointLeft = GameObject.Find("MinionSpawnPointLeft");
        MinionSpawnPointRight = GameObject.Find("MinionSpawnPointRight");

        hasJustShotABullet = false;

        if (transform.position.x < -0.1)
        {
            isOnTheLeftSide = true;
        }
        else isOnTheLeftSide = false;

        startScreen.SetActive(true);


    }
    
    void Update()
    {
        // This is needed for Multiplayer
        if (hasJustShotABullet)
        {
            PlayAnimation();
            hasJustShotABullet = false;
        }


        if (!isLocalPlayer)
        {
            return;
        }

        // If a player presses 'A', hide the start and tell the game controller that you are ready
        if (Input.GetKeyDown(KeyCode.A) && !hasPressedReadyCheck)
        {
            hasPressedReadyCheck = true;
            startScreen.SetActive(false);
            CmdIncreaseReadyPlayerCount();
        }



        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize(); //normalizing the vector.

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; // find the angle in degrees
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);


        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextShotTime)
        {
            nextShotTime = Time.time + reloadRate;
            hasJustShotABullet = true;
            StartCoroutine(ShootWithDelay());
        }

        if (bullettype == 1 || Input.GetKeyDown(KeyCode.C))
        {
            
            bullettype = 0;
            if (isOnTheLeftSide)
            {
                CmdSpawnMinion(MinionSpawnPointLeft.transform.position);
            }
            else
            {
                CmdSpawnMinion(MinionSpawnPointRight.transform.position);
            }

        }


    }

    void PlayAnimation()
    {
        anim.Play("CannonShooting");
    }

    public void SetBuff(int bufftype)
    {
        bullettype = bufftype;
    }

    IEnumerator ShootWithDelay()
    {
        print(Time.time);
        yield return new WaitForSeconds(0.45f);
        CmdFire();
    }

    [Command]
    void CmdFire()
    {
        var bullet = Instantiate(BulletPrefab, ShotSpawnTransform.position, Quaternion.identity) as GameObject;

        

        bullet.GetComponent<Rigidbody2D>().velocity = transform.right * projectileSpeed;
        bullet.GetComponent<BulletController>().ownerName = transform.name;
        bullet.GetComponent<BulletController>().thisBulletHasBuffNro = bullettype;

        NetworkServer.Spawn(bullet);

        Debug.Log("You shot a bullet with a buff #" + bullettype);
        bullettype = 0;

    }

    [Command]
    void CmdSpawnMinion(Vector3 position)
    {

        var minion = Instantiate(MinionPrefabTEMP, position, Quaternion.identity) as GameObject;

        NetworkServer.Spawn(minion);
        minion.GetComponent<minionRun>().ownerName = transform.name;
        minion.GetComponent<minionRun>().spawnedOnLeft = isOnTheLeftSide;
    }

    [Command]
    void CmdIncreaseReadyPlayerCount()
    {
        theGameController = GameObject.Find("GameController");
        GameController gameControllerScript = theGameController.GetComponent<GameController>();
        gameControllerScript.IncreaseReadyCount(1);
    }

}
