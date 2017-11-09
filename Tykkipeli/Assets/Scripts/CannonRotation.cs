using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CannonRotation : NetworkBehaviour
{

    public GameObject BulletPrefab;
    public Transform ShotSpawnTransform;
    public float projectileSpeed;
    public float reloadRate = 0.5f;
    private float nextShotTime;

    // public float minRotation;
    // public float maxRotation;

    
    void Update()
    {
        // This is needed for Multiplayer
        if (isLocalPlayer)
        {
        
        


            // Template controls START
            // for network testing. REMOVE ->->
            var x = Input.GetAxis("Horizontal") * Time.deltaTime * 5.0f;
            var y = Input.GetAxis("Vertical") * Time.deltaTime * 5.0f;
            transform.Translate(x, 0, 0);
            transform.Translate(0, y, 0);
            // Template controls END
            // 


            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            difference.Normalize(); //normalizing the vector. Meaning that all the sum of the vector will be equal to 1.

            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; // find the angle in degrees
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

            /* if (rotZ > maxRotation)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, maxRotation);
            }

            if (rotZ < minRotation)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, minRotation);
            } */

            if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextShotTime)
            {
                nextShotTime = Time.time + reloadRate;
                CmdFire();
            }

        }
    }

    [Command]
    void CmdFire()
    {
        var bullet = Instantiate(BulletPrefab, ShotSpawnTransform.position, Quaternion.identity) as GameObject;
        bullet.GetComponent<Rigidbody2D>().velocity = transform.right * projectileSpeed;

        NetworkServer.Spawn(bullet);
        bullet.GetComponent<BulletController>().ownerName = transform.name;
    }
}
