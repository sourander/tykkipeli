using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletController : NetworkBehaviour
{

    public string ownerName;
    public int thisBulletHasBuffNro;
    int buffTypeFromBox;

    public GameObject ParticleFXGenerator;

    public AudioClip[] soundClips;
    // 0 = Cannon leaving barrel
    // TODO: 1 = Bullet hitting an object and exploding
    // TODO: 2 = 'Ouch', bullet hits a minion

    // Use this for initialization
    void Start()
    {
        // Play Sound
        AudioSource.PlayClipAtPoint(soundClips[0], new Vector3(0, 0, 0));

        // Bullet is killed in 5 seconds. For safety. Bombs are dangerous.
        Destroy(gameObject, 5);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        Destroy(gameObject);

        if (other.gameObject.tag == "Box")
        {
            // Get the 'buff type' the box is carrying
            DestroyByContact box = other.gameObject.GetComponent<DestroyByContact>();
            GameObject boxYouCollidedWith = box.gameObject;
            buffTypeFromBox = boxYouCollidedWith.GetComponent<DestroyByContact>().bufftype;

            CmdTellServerWhoGetsSpecialBulllet(ownerName, buffTypeFromBox);
            CmdSpawnParticleFX(boxYouCollidedWith.transform.position, "BoxExplotion", other.gameObject.tag, thisBulletHasBuffNro);
            Destroy(other.gameObject);
        }


        if (other.gameObject.tag == "Player")
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();

            GameObject theThingYouCollidedWith = other.gameObject;
            CmdSpawnParticleFX(theThingYouCollidedWith.transform.position, "Explotion", other.gameObject.tag, thisBulletHasBuffNro);

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
            switch (thisBulletHasBuffNro)
            {
                case 1:
                    Debug.Log("A Minion was shot with a " + thisBulletHasBuffNro + "buff bullet");
                    break;
                case 2:
                    Debug.Log("A Minion was shot with a " + thisBulletHasBuffNro + "buff bullet");
                    break;
                case 3:
                    Debug.Log("A Minion was shot with a " + thisBulletHasBuffNro + "buff bullet");
                    break;
                case 4:
                    Debug.Log("A Minion was shot with a " + thisBulletHasBuffNro + "buff bullet");
                    break;
                default:
                    Debug.Log("A Minion was shot with a " + thisBulletHasBuffNro + "buff bullet. This is the default bullet.");
                    break;
            }
            MinionHealth minionHealth = other.gameObject.GetComponent<MinionHealth>();
            if(minionHealth != null)
            {
                minionHealth.TakeDamage(20);
            }

            GameObject min = other.gameObject;
            Vector3 position = new Vector3(min.transform.position.x, min.transform.position.y);
            CmdSpawnParticleFX(position, "AirExplotion", other.gameObject.tag, thisBulletHasBuffNro);

        }

        // Activate Explotion if the bullet hits the ground
        if (other.gameObject.tag == "Boundary")
        {
            CmdSpawnParticleFX(transform.position, "Explotion", other.gameObject.tag, thisBulletHasBuffNro);
        }

        // Activate Explotion if the bullet hits the ground
        if (other.gameObject.tag == "Palace")
        {
            CmdSpawnParticleFX(transform.position, "AirExplotion", other.gameObject.tag, thisBulletHasBuffNro);
        }


    }



    [Command]
    void CmdTellServerWhoGotShot(string uniqueID, int damage)
    {
        GameObject obj = GameObject.Find(uniqueID);
        obj.GetComponent<PlayerHealth>().TakeDamage(damage);
        Debug.Log("Player health: " + obj.GetComponent<PlayerHealth>().currentHealth.ToString() );
    }

    [Command]
    void CmdTellServerWhoGetsSpecialBulllet(string ownerName, int bufftype)
    {
        GameObject obj = GameObject.Find(ownerName);
        obj.GetComponent<CannonRotation>().SetBuff(bufftype);
        Debug.Log("Player callled '" + ownerName + "' activated a buff #" + bufftype);
    }


    [Command]
    void CmdSpawnParticleFX(Vector3 position, string nameOfTheAnimator, string theTagOfTheOtherCollider, int theBulletHasBuff)
    {
        if (theBulletHasBuff != 3)
        {
        var particle = Instantiate(ParticleFXGenerator, position, Quaternion.identity) as GameObject;

        NetworkServer.Spawn(particle);
        particle.GetComponent<ParticleSpawned>().nameOfTheAnimator = nameOfTheAnimator;
        }
        if (nameOfTheAnimator == "BoxExplotion")
        {
            var particle2 = Instantiate(ParticleFXGenerator, position, Quaternion.identity) as GameObject;
            particle2.GetComponent<ParticleSpawned>().nameOfTheAnimator = "AirExplotion";
            NetworkServer.Spawn(particle2);
        }

        if (theTagOfTheOtherCollider == "Minion" || theTagOfTheOtherCollider == "Boundary")
        {
            if (theBulletHasBuff == 2)
            {
                var particle2 = Instantiate(ParticleFXGenerator, position, Quaternion.identity) as GameObject;
                particle2.GetComponent<ParticleSpawned>().nameOfTheAnimator = "ThunderCloud";
                NetworkServer.Spawn(particle2);
            }
            else if (theBulletHasBuff == 3)
            {
                if(theTagOfTheOtherCollider == "Minion")
                {
                    position = new Vector3(position.x, position.y - 0.3f);
                }
                var particle2 = Instantiate(ParticleFXGenerator, position, Quaternion.identity) as GameObject;
                particle2.GetComponent<ParticleSpawned>().nameOfTheAnimator = "OilDrop";
                NetworkServer.Spawn(particle2);
            }
        }

    }


}
