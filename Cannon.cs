using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Jordan Machalek
 * Handles firing of a cannon and describes position
 */
public class Cannon : MonoBehaviour {

    //Attributes
    public Vector3 position;
    public Vector3 direction;
    public string side; //"left" or "right"
    public GameObject projectile;
    public float iniDirection; //Initial direciton of the cannon
    public float anglePerFrame;
    private Quaternion rotation;

	// Use this for initialization
	void Start ()
    {
        //Save initial direction to use for determining whether to fire cannon
        iniDirection = transform.eulerAngles.z;
        //Set rotation to a default of 90 deg
        rotation = Quaternion.Euler(0, 0, 90);
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Update position and angle to use for firing of projectiles
        position = transform.position;

        //Determine direction in relation to ship based on ship's rotation
        direction = GameObject.Find("pShip").GetComponent<ShipPhysics>().direction;

        //Check for user input and fire if recieved
        Fire();
    }

    //Handles creation of bullet and input for firing
    void Fire()
    {
        GameObject newBullet;

        //Check for firing input determine direction the cannon faces
        //If left, fire left; if right, fire right
        if(Input.GetKeyDown(KeyCode.A) && iniDirection == 90 && GameObject.Find("SceneManager").GetComponent<BulletManager>().canFire == true)
        {
            if(side == "left")
            {
                //Debug.Log("Left cannon dir = " + direction);

                //Create bullet object
                newBullet = Instantiate(projectile, position, Quaternion.identity);

                //Set direction of created bullet to direction cannon is facing - rotate +90 deg
                newBullet.GetComponent<Bullet>().direction = rotation * direction;

                //Debug.Log("New bullet dir = " + newBullet.GetComponent<Bullet>().direction);

                //Account for new bullet
                GameObject.Find("SceneManager").GetComponent<BulletManager>().CheckBullets();
            }
        }
        else if(Input.GetKeyDown(KeyCode.D) && iniDirection == 270 && GameObject.Find("SceneManager").GetComponent<BulletManager>().canFire == true)
        {
            if(side == "right")
            {
                //Debug.Log("Right cannon dir = " + direction);

                //Create bullet object
                newBullet = Instantiate(projectile, position, Quaternion.identity);

                //Set direction of created bullet to direction cannon is facing - rotate -90 deg
                newBullet.GetComponent<Bullet>().direction = -1 * (rotation * direction);

                //Debug.Log("New bullet dir = " + newBullet.GetComponent<Bullet>().direction);

                //Account for new bullet
                GameObject.Find("SceneManager").GetComponent<BulletManager>().CheckBullets();
            }
        }
        /*
         * Meant to fire both cannons at once, does not always occur. Not implemented due to time.
        else if(Input.GetKeyDown(KeyCode.S) &&  GameObject.Find("SceneManager").GetComponent<BulletManager>().canFire == true)
        {
            //Create bullet object
            newBullet = Instantiate(projectile, position, Quaternion.identity);

            if (side == "left")
            {
                //Set direction of created bullet to direction cannon is facing - rotate +90 deg
                newBullet.GetComponent<Bullet>().direction = rotation * direction;
            }
            
            if (side == "right")
            {
                //Set direction of created bullet to direction cannon is facing - rotate -90 deg
                newBullet.GetComponent<Bullet>().direction = -1 * (rotation * direction);
            }

            //Account for new bullet
            GameObject.Find("SceneManager").GetComponent<BulletManager>().CheckBullets();
        }*/
    }
}
