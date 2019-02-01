using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Jordan Machalek
 * Handles player health and represents it by drawing a GameObject to the screen
 */
public class HealthManager : MonoBehaviour {

    //Attributes
    public GameObject healthImage;
    private GameObject[] images;
    private GameObject newObj;
    public int lives;
    public Vector3 imgPosition;
    public GameObject textObj;

	// Use this for initialization
	void Start ()
    {
        images = new GameObject[3];

        DrawHealth();
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckHealth();
	}

    //Draws game objects and keeps track by placing in an array
    void DrawHealth()
    {
        for(int i = 0; i < 3; i++)
        {
            lives++;
            //Create object
            newObj = Instantiate(healthImage, imgPosition, Quaternion.identity);
            //Decrease size
            newObj.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            //Add object to array
            images[i] = newObj;
            //Move next object's position to the right
            imgPosition.x += 1;
        }
    }

    //Take away a life and destroy a health object
    public void TakeDamage()
    {
        lives--;

        Destroy(images[lives]);
    }

    //Check if the player is out of lives and end the game if so.
    void CheckHealth()
    {
        if (lives == 0 || lives < -1)
        {

            //Display text to declare game over
            GameObject end = Instantiate(textObj);
            //Set position to center
            end.transform.position = Vector3.zero;
            //Set text
            end.GetComponentInChildren<TextMesh>().text = "Game Over\n Your score: " + GameObject.Find("SceneManager").GetComponent<ScoreManager>().score.ToString();

            //Hide the score from the upper-left corner
            GameObject.Find("SceneManager").GetComponent<ScoreManager>().scoreObj.SetActive(false);

            //Destroy player
            Destroy(GameObject.Find("pShip"));

            //Destroy all asteroids
            for (int i = 0; i < GameObject.Find("SceneManager").GetComponent<AsteroidManager>().asteroidList.Count; i++)
            {
                Destroy(GameObject.Find("SceneManager").GetComponent<AsteroidManager>().asteroidList[i]);
                //GameObject.Find("SceneManager").GetComponent<AsteroidManager>().asteroidList.RemoveAt(i);
            }

            //Destroy the AsteroidManager so no new ones are spawned
            Destroy(GameObject.Find("SceneManager").GetComponent<AsteroidManager>());

            //Set lives to -1 to avoid NullReferenceExceptions with continued checks
            lives = -1;
        }
    }
}
