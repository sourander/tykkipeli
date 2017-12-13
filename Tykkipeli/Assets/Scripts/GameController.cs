using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameController : NetworkBehaviour
{
    //spawnaa tässä tapauksessa laatikoita taivaalta, muuttujia voidaan muokata unity editorin puolella
    public GameObject boxes;
    public Vector3 spawnValues;
    public int boxCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    private bool CoroutineIsPlaying = false;

    public int readyPlayerCount;


    void Start()
    {

    }

    void Update()
    {
        if (!isServer) return;

        if (readyPlayerCount >= 2 && !CoroutineIsPlaying)
        {
            CoroutineIsPlaying = true;
            StartCoroutine(SpawnWaves());
        }
        
    }

    public void IncreaseReadyCount(int v)
    {
        Debug.Log("Increasing ReadyCount with 1");
        readyPlayerCount += v;
    }


    IEnumerator SpawnWaves()
    {
        //aika ennenkuin laatikot alkavat tippua taivaalta
        yield return new WaitForSeconds(startWait);

        //infinite looppi, laatikot tippuu pelissä koko ajan, jota voidaan muokata myöhemminkin (pelin päättyminen?)
        while (true)
        {
            //(for loopilla määritelty) laatikoiden määrä tippumassa on vakio (unityn puolella määrätty boxCount -arvo)
            for (int i = 0; i < boxCount; i++)
            {
                //arpoo x:n arvon uudelle instanssille mistä laatikko tippuu vaakasuunnassa, muut arvot kiinteitä
                // Arpoo myös buffin 1-4
                Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                int rndBuff = UnityEngine.Random.Range(1, 4);

                //joku kiinteä quaternion arvo, joka ilmeisesti oltava
                Quaternion spawnRotation = Quaternion.identity;

                // luo laatikon instanssin
                // Instantiate(boxes, spawnPosition, spawnRotation);
                CmdSpawnBox(spawnPosition, rndBuff);

                //tämä alustaa arvon jota voidaan muokata unity editorissa, kuinka tiheään laatikot ilmestyvät taivaalle
                yield return new WaitForSeconds(spawnWait);
            }
            //aika turha, "laatikkojen tippumiswavejen" väli, joka voidaan pistää nollaksikin
            yield return new WaitForSeconds(waveWait);
        }
    }

    [Command]
    void CmdSpawnBox(Vector3 spawnPosition, int rndBuff)
    {
        var box = Instantiate(boxes, spawnPosition, Quaternion.identity) as GameObject;
        box.GetComponent<DestroyByContact>().bufftype = rndBuff;

        NetworkServer.Spawn(box);
    }
}
