using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

        // Bullet is killed in 2 seconds
        Destroy(gameObject, 2);

    }

    /* private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    } */

    // Update is called once per frame
    void Update()
    {

    }
}
