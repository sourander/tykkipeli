using UnityEngine;
using System.Collections;

public class minionRun : MonoBehaviour
{

    public Transform[] target;
    int currentPoint;
    public float speed = 0.05f;
    public float sight = 3f;
    Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine("Run");
        anim.SetBool("running", true);
        Physics2D.queriesStartInColliders = false;
    }

    // Update is called once per frame
    void Update()
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.right, sight);
    }


    IEnumerator Run()
    {
        while (true)
        {

            if (transform.position.x == target[currentPoint].position.x)
            {
                currentPoint++;
                anim.SetBool("running", false);
            }


            if (currentPoint >= target.Length)
            {
                currentPoint = 1;
            }

            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target[currentPoint].position.x, transform.position.y), speed);

            yield return null;


        }
    }


    /*void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Projectile")
            Destroy(this.gameObject, 0.1f);
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.right * sight);

    }*/

}
