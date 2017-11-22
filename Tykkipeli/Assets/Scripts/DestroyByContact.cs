using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	
	void Start () {
		
	}
	
	
	void Update () {
		
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
