using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMove : MonoBehaviour
{

    public float moveSpeed;
    bool touchground = false;
    void Start()
    {

    }





    void Update()
    {

        if (touchground == true)
        {
            transform.Translate(new Vector3(moveSpeed, 0, 0) * Time.deltaTime);
        }
    }


    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Boundary")
        {
            touchground = true;
        }
        if (col.gameObject.tag == "Palace")
        {
            moveSpeed *= -1;
        }
        //estää törmäämisen toiseen minioniin
        if (col.gameObject.tag == "Minion")
        {

            //ei toimi
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GetComponent<Collider2D>());

        }
    }
}
       




