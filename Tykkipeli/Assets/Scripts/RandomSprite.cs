using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour {

    public Sprite[] sprites;
    public string resourceName;
    
    
	void Start ()
    {
        //lataa random spriten resourcekansiosta
        if (resourceName != "")
        {
            sprites = Resources.LoadAll<Sprite>(resourceName);
            GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
        }
		
	}
	
	
	void Update ()
    {
		
	}
}
