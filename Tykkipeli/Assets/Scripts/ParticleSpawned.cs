using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ParticleSpawned : NetworkBehaviour {

    Animator anim;

    [SyncVar]
    public string nameOfTheAnimator;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();

        anim.Play(nameOfTheAnimator);

        Destroy(gameObject, 2);
    }
	
	// Update is called once per frame
	void Update () {

    }

}
