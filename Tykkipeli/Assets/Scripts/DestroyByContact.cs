using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DestroyByContact : NetworkBehaviour
{
    public Sprite[] sprites;
    public string resourceName;

    [SyncVar]
    public int bufftype = 1;

    void Start()
    {
        //lataa random spriten resourcekansiosta
        if (resourceName != "")
        {
            sprites = Resources.LoadAll<Sprite>(resourceName);
            GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
        }
    }


    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        //jos laatikko törmää toiseen laatikkoon, ei tapahdu mitään, mutta jos törmää muuhun, niin laatikko tuhoutuu
        if (other.gameObject.tag == "Box")
        {
            return;
        }

        if (other.gameObject.tag != "Box")
        {

            Destroy(gameObject);
        }

    }
}
