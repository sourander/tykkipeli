using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CannonRotation : NetworkBehaviour
{

    public GameObject BulletPrefab;
    public GameObject MinionPrefabTEMP; // OBS! HUOM! VÄÄRÄ!
    public GameObject MinionSpawnPointLeft;
    public GameObject MinionSpawnPointRight;

    public Transform ShotSpawnTransform;

    public float projectileSpeed;
    public float reloadRate = 0.5f;
    private float nextShotTime;

    public bool isOnTheLeftSide;

    [SyncVar]
    public int bullettype; // 0 = normal bullet, 1 = buff, 2 = second buff, 3 = third type of buff..

    // public float minRotation;
    // public float maxRotation;

    public override void OnStartLocalPlayer()
    {
        MinionSpawnPointLeft = GameObject.Find("MinionSpawnPointLeft");
        MinionSpawnPointRight = GameObject.Find("MinionSpawnPointRight");

        if (transform.position.x < -0.1)
        {
            isOnTheLeftSide = true;
        }
        else isOnTheLeftSide = false;
    }
    
    void Update()
    {
        // This is needed for Multiplayer
        if (!isLocalPlayer)
        {
            return;
        }



        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize(); //normalizing the vector. Meaning that all the sum of the vector will be equal to 1.

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; // find the angle in degrees
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);


        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextShotTime)
        {
            nextShotTime = Time.time + reloadRate;
            CmdFire();
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

    public void SetBuff(int bufftype)
    {
        bullettype = bufftype;
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
}
