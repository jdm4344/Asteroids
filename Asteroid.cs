using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Jordan Machalek
 * Handles movement and other behaviors for an asteroid (ship)
 */
public class Asteroid : MonoBehaviour {

    //Attributes
    //Movement
    public Vector3 position;
    public Vector3 direction;
    public Vector3 velocity;
    public float speed;
    //Rotation
    public float totalRotation;
    public float anglePerFrame;
    //Type
    public int level;
    //Off-screen Detection
    private Camera cam;
    private float height;
    private float width;

    // Use this for initialization
    void Start ()
    {
        gameObject.GetComponent<SpriteRenderer>().bounds.Expand(10000);

        //Calculate velocity
        velocity = direction * speed;
        //Get its initial posiiton from its transform
        position = transform.position;
        transform.rotation = Quaternion.Euler(0,0, totalRotation);

        //Setup camera variables
        cam = Camera.main;
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;

        //Add to list of asteroids for collision detection
        GameObject.Find("SceneManager").GetComponent<AsteroidManager>().asteroidList.Add(gameObject);
        GameObject.Find("pShip").transform.Find("hullLarge (1)").GetComponent<pCollisions>().obstacles.Add(gameObject);
    }
	
	// Update is called once per frame
	void Update ()
    {
        Move();

        //Rotate();

        Wrap();
	}

    // Moves asteroid over time
    void Move()
    {
        position += velocity;
        //Update transform
        transform.position = position;
    }

    /*Randomly adjust the rotation of the asteroid
    void Rotate()
    {
        //anglePerFrame = Gaussian(direction.z, 0.1f);
        float rdmNegative = (int)Random.Range(0, 1) * 2 - 1; //Randomly gives either 1 or -1

        //Rotate left (positive)
        if (rdmNegative > 0)
        {
            totalRotation += anglePerFrame;
            direction = Quaternion.Euler(0, 0, anglePerFrame) * direction;
        }
        else if (rdmNegative < 0) //Rotate right (negative)
        {
            totalRotation -= anglePerFrame;
            direction = Quaternion.Euler(0, 0, -anglePerFrame) * direction;
        }

        transform.position = position;
        transform.rotation = Quaternion.Euler(0, 0, totalRotation);
    }*/

    //Wraps asteroid position from one edge of screen to opposite
    private void Wrap()
    {
        //Check X position
        if (transform.position.x > width / 2)
        {
            position.x -= width;
        }
        else if (transform.position.x < 0 - (width / 2))
        {
            position.x += width;
        }

        //Check Y position
        if (transform.position.y > height / 2)
        {
            position.y -= height;
        }
        else if (transform.position.y < 0 - (height / 2))
        {
            position.y += height;
        }
    }

    //Helper method - returns a Gaussian value
    float Gaussian(float mean, float stdDev)
    {
        float num1 = Random.Range(0f, 1f);
        float num2 = Random.Range(0f, 1f);
        float gaussNum = Mathf.Sqrt(-2.0f * Mathf.Log(num1)) * Mathf.Sin(2.0f * Mathf.PI * num2);
        return mean + stdDev * gaussNum;
    }
}
