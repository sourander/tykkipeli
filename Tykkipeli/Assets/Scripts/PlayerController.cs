using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerController : NetworkBehaviour
{

    void Start()
    {

    }

    public override void OnStartLocalPlayer()
    {
    // Tähän pitäisi keksiä miten vain toisen pelaajan sprite pyöräytetään ympäri

    }



    void Update()
    {
        // This is needed for Multiplayer
        if (!isLocalPlayer)
        {
            return;
        }

        // Template controls START
        // for network testing. REMOVE ->->
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 5.0f;
        var y = Input.GetAxis("Vertical") * Time.deltaTime * 5.0f;
        transform.Translate(x, 0, 0);
        transform.Translate(0, y, 0);
        // Template controls END
        // 


    }
}
