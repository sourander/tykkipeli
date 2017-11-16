using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletController : NetworkBehaviour
{

    public string ownerName;

    // Use this for initialization
    void Start()
    {

        // Bullet is killed in 2 seconds
        Destroy(gameObject, 2);

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);

        if (other.gameObject.tag == "Box")
        {
            Destroy(other.gameObject);
        }

                 

        if (other.gameObject.tag == "Player")
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                GameObject player = playerHealth.gameObject;
                if (player.GetComponent<PlayerID>().playerUniqueName != this.ownerName)
                {
                CmdTellServerWhoGotShot(player.GetComponent<PlayerID>().playerUniqueName, 20);
                }
            }

        }

        if (other.gameObject.tag == "Minion")
        {
            MinionHealth minionHealth = other.gameObject.GetComponent<MinionHealth>();
            if(minionHealth != null)
            {
                minionHealth.TakeDamage(20);
            }
            
        }
       
       
    }

    [Command]
    void CmdTellServerWhoGotShot(string uniqueID, int damage)
    {
        GameObject obj = GameObject.Find(uniqueID);
        obj.GetComponent<PlayerHealth>().TakeDamage(damage);
        Debug.Log("Player health: " + obj.GetComponent<PlayerHealth>().currentHealth.ToString() );
    }

    // Update is called once per frame
    void Update()
    {

    }
}
