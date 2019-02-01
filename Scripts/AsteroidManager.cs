using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Jordan Machalek
 * Handles creation of 
 */
public class AsteroidManager : MonoBehaviour {

    //Attributes
    public List<GameObject> asteroidList;
    public int round;
    public List<GameObject> asteroidPrefabs;
    public GameObject childPrefabs; //second level asteroid to be created

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        RoundCheck();
	}

    //Creates a new wave of asteroids and places them in the game
    void MakeWave()
    {
        int rNum = Random.Range(8, 20);

        for(int i = 0; i < rNum; i++)
        {
            GameObject newAsteroid = Instantiate(asteroidPrefabs[0], GenerateVector(-6, 6, -4.5f, 4.5f), Quaternion.identity);


            newAsteroid.GetComponent<Asteroid>().direction = GenerateVector(-1, 1, -1, 1);
            Vector3 dir = newAsteroid.GetComponent<Asteroid>().direction;
            newAsteroid.GetComponent<Asteroid>().totalRotation = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            newAsteroid.GetComponent<Asteroid>().speed = 0.005f;
            newAsteroid.GetComponent<Asteroid>().level = 1;
        }
    }

    //Check to see if all asteroids have been destroyed; if so, start new round
    void RoundCheck()
    {
        if(asteroidList.Count == 0)
        {
            //Increment
            round++;
            //Generate new asteroids
            MakeWave();
        }
    }

    //Creates 2-3 new asteroids from parent if
    public void MakeNew(GameObject ob)
    {
        if (ob.GetComponent<Asteroid>().level == 1)
        {
            for (int i = 0; i < Random.Range(2, 3); i++)
            {
                GameObject newAsteroid = Instantiate(childPrefabs, GenerateVector(-6, 6, -4.5f, 4.5f), Quaternion.identity);

                newAsteroid.transform.position = ob.transform.position;
                newAsteroid.GetComponent<Asteroid>().direction = GenerateVector(-1, 1, -1, 1);
                newAsteroid.GetComponent<Asteroid>().speed = Random.Range(0.01f, 0.02f);
                newAsteroid.GetComponent<Asteroid>().level = 2;
            }
        }
    }

    //Generates a random vector3 with x and y components based on given ranges
    Vector3 GenerateVector(float x1, float x2, float y1, float y2)
    {
        //get X and Z positions based on desired range
        float xPos = Random.Range(x1, x2);
        float yPos = Random.Range(y1, y2);

        Vector3 pos = new Vector3(xPos, yPos, 0);

        return pos;
    }
}
