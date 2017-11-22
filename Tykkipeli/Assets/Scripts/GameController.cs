using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //spawnaa tässä tapauksessa laatikoita taivaalta, muuttujia voidaan muokata unity editorin puolella
    public GameObject boxes;
    public Vector3 spawnValues;
    public int boxCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    void Start()
    {
        //aloittaa heti scenen alussa laatikoiden tiputtamisen
        StartCoroutine (SpawnWaves());
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
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);

                //joku kiinteä quaternion arvo, joka ilmeisesti oltava
                Quaternion spawnRotation = Quaternion.identity;

                // luo laatikon instanssin
                Instantiate(boxes, spawnPosition, spawnRotation);

                //tämä alustaa arvon jota voidaan muokata unity editorissa, kuinka tiheään laatikot ilmestyvät taivaalle
                yield return new WaitForSeconds(spawnWait);
            }
            //aika turha, "laatikkojen tippumiswavejen" väli, joka voidaan pistää nollaksikin
            yield return new WaitForSeconds(waveWait);
        }
    }
}
