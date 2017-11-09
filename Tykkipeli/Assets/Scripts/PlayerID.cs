using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerID : NetworkBehaviour {

    [SyncVar] public string playerUniqueName;
    private NetworkInstanceId playerNetID;

    public override void OnStartLocalPlayer()
    {
        GetNetIdentity();
        SetIdentity();
    }

    void Update()
    {
       if(transform.name == "" || transform.name == "Player(Clone)")
        {
            SetIdentity();
        }
    }

    [Client]
    void GetNetIdentity()
    {
        playerNetID = GetComponent<NetworkIdentity>().netId;
        CmdTellServerMyIdentity(MakeUniqueIdentity() );
    }


    void SetIdentity()
    {
        if (!isLocalPlayer)
        {
            this.transform.name = playerUniqueName;
        }
        else
        {
            transform.name = MakeUniqueIdentity();
        }
    }

    string MakeUniqueIdentity()
    {
        return "Player" + playerNetID.ToString();
    }

    [Command]
    void CmdTellServerMyIdentity(string name)
    {
        playerUniqueName = name;
    }

}
