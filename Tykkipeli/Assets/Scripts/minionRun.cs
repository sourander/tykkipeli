using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class minionRun : NetworkBehaviour
{

    Animator anim;

    [SyncVar]
    public bool spawnedOnLeft;

    [SyncVar]
    public string ownerName = "";

    public float moveSpeed;
    bool touchground = false;

    public float damagerate = 40;

    private bool isAttacking = false;

    // Use this for initialization
    void Start()
    {

        if (!spawnedOnLeft)
        {
            moveSpeed *= -1;
        }

        anim = GetComponent<Animator>();
        anim.SetBool("running", true);
        
    }

    // Update is called once per frame
    void Update()
    {

        if (touchground == true)
        {
            transform.Translate(new Vector3(moveSpeed, 0, 0) * Time.deltaTime);
        }

        anim.SetBool("attacking", isAttacking);

    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Boundary")
        {
            touchground = true;
        }


        //estää törmäämisen toiseen minioniin
        if (col.gameObject.tag == "Minion")
        {
            Debug.Log("A minion collided with another minion");
            Collider2D thisCollider = GetComponent<Collider2D>();

            // Nimetään collider target uusiksi
            GameObject theOtherObject = col.gameObject;

            // Ladataan skriptit muuttujiin
            Collider2D theOtherCollider = theOtherObject.GetComponent<Collider2D>();
            minionRun runnerScript = theOtherObject.GetComponent<minionRun>();


            // Ignore collision while in air
            if (touchground == false)
            {
                Physics2D.IgnoreCollision(thisCollider, theOtherCollider);
            }

            // Ignore collision on friendly minions
            if (runnerScript.ownerName == ownerName)
            {
                Debug.Log("...which is owned by " + ownerName);
                Physics2D.IgnoreCollision(thisCollider, theOtherCollider);
            }


        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Minion")
        {
            // Nimetään collider target uusiksi
            GameObject theOtherObject = collision.gameObject;

            // Ladataan skriptit muuttujiin
            minionRun runnerScript = theOtherObject.GetComponent<minionRun>();
            MinionHealth theOtherMinionHealth = theOtherObject.GetComponent<MinionHealth>();

            if (runnerScript.ownerName != ownerName)
            {
                isAttacking = true;
                theOtherMinionHealth.currentHealth = Mathf.Min(theOtherMinionHealth.currentHealth - damagerate * Time.deltaTime, 50.0F);
                Debug.Log("The other health is:" + theOtherMinionHealth.currentHealth);
            }
        }

        if (collision.gameObject.tag == "Palace")
        {
            isAttacking = true;

            PalaceHealth palaceHealth = collision.gameObject.GetComponent<PalaceHealth>();

            if (palaceHealth != null)
            {
                palaceHealth.currentHealth = Mathf.Min(palaceHealth.currentHealth - damagerate * Time.deltaTime, 150.0F);
            }
        }
    }


    void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Minion")
        {
            // Nimetään collider target uusiksi
            GameObject theOtherObject = collision.gameObject;

            // Ladataan skriptit muuttujiin
            minionRun runnerScript = theOtherObject.GetComponent<minionRun>();

            if (runnerScript.ownerName != ownerName)
            {
                isAttacking = false;
            }
        }

    } 




}
