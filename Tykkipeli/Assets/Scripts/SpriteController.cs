using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpriteController : NetworkBehaviour {

    public override void OnStartLocalPlayer()
    {
        GetComponent<SpriteRenderer>().flipX = true;

    }
}
