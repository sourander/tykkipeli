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

    // Update is called once per frame
    void Update()
    {
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
            var bullet = Instantiate(BulletPrefab, ShotSpawnTransform.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody2D>().velocity = transform.right * projectileSpeed;
        }
    }
}
