using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ParticleSpawned : NetworkBehaviour {

    Animator anim;

    public string nameOfTheAnimator;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();

 
        Debug.Log("A particle spawner wants to play an animation called" + nameOfTheAnimator);
        anim.Play(nameOfTheAnimator);
        Destroy(gameObject, 3);
    }
	
	// Update is called once per frame
	void Update () {

    }

}
