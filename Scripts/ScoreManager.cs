using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Jordan Machalek
 * Records score gained by player for destroying asteroids
 * Score is increased externally when asteroids are destroyed
 */
public class ScoreManager : MonoBehaviour {

    //Attributes
    public GameObject numObj;
    public GameObject scoreObj;
    public Vector3 position;
    public int score;

	// Use this for initialization
	void Start ()
    {
        //Start with 0 points
        score = 0;

        //Create the score object on the screen
        scoreObj = Instantiate(numObj, position, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Change the text to match the player's score
        scoreObj.GetComponentInChildren<TextMesh>().text = score.ToString();
	}
}
