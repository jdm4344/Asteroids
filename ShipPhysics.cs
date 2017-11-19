﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Jordan Machalek
 * Interprets keyboard input to affect translation and rotation of game object
 * using physics defined within the script.
 */
public class ShipPhysics : MonoBehaviour {

    //Attributes
    //Basic Positioning
    public Vector3 position;
    public Vector3 direction;
    public Vector3 velocity;
    //Rotation
    public float anglePerFrame;
    public float totalRotation;
    //Acceleration
    public Vector3 acceleration;
    public float accelRate;
    public float maxSpeed;
    //Screen Wrapping
    private Camera cam;
    private float height;
    private float width;

	// Use this for initialization
	void Start ()
    {
        //Determine initial position based on Transform
        position = transform.position;
        //Setup camera variables
        cam = Camera.main;
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Rotate();

        Acceleration();

        Wrap();
	}

    //Rotates vehicle each frame depending on keyboard input
    private void Rotate()
    {
        //Rotate left (positive)
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            totalRotation += anglePerFrame;
            direction = Quaternion.Euler(0, 0, anglePerFrame) * direction;
        }
        else if(Input.GetKey(KeyCode.RightArrow)) //Rotate right (negative)
        {
            totalRotation -= anglePerFrame;
            direction = Quaternion.Euler(0, 0, -anglePerFrame) * direction;
        }

        //Update transform component
        transform.position = position;
        transform.rotation = Quaternion.Euler(0, 0, totalRotation);
    }

    //Handles acceleration and decelleration of vehicle
    private void Acceleration()
    {
        //Check input for acceleration, else decellerate
        if (Input.GetKey(KeyCode.UpArrow))
        {
            acceleration = accelRate * direction;
            velocity += acceleration;
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
            position += velocity;
        }
        else
        {
            acceleration = accelRate * direction;
            velocity.x = velocity.x * 0.99f;
            velocity.y = velocity.y * 0.99f;
            position += velocity;
        }
    }

    //Wraps vehicle position from one edge of screen to opposite
    private void Wrap()
    {
        //Check X position
        if(transform.position.x > width / 2)
        {
            position.x -= width;
        }
        else if(transform.position.x < 0 - (width / 2))
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
}
