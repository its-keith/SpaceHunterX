using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : Enemy {
    
    UFO()
    {
        this.health = 1;
        this.points = 10;
        this.speed = 750;
        this.powerUpChance = 10;
    }

    private float vertSpeed = -100f;
    private bool leftSide = true;
    Vector3 moveEnemy;

    protected override void move()
    {
        screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        
        //Change direction if it starts on the right side of the screen
        if (transform.position.x > CameraScale.rightEdge && leftSide)
        {
            speed = -speed;
            leftSide = false;
        }

        //Set onscreen to true once the object is atleast 50 pixels onto the screen
        if(!onscreen && transform.position.x < CameraScale.rightEdge - 50 && transform.position.x > CameraScale.leftEdge + 50)
        {
            onscreen = true;
        }

        //Change direction when the enemy reaches the edge of the screen and only after it has moved into the screen far enough
        if (onscreen && (transform.position.x > CameraScale.rightEdge - 30 || transform.position.x < CameraScale.leftEdge + 30))
        {
            speed = -speed;
            
            //Automatically move it over 20 pixels.
            if (transform.position.x > CameraScale.rightEdge - 30)
            {
                transform.Translate(-20f, 0, 0);
            } else
            {
                transform.Translate(20f, 0, 0);
            }
        }

        moveEnemy = new Vector3(speed, vertSpeed, 0) * Time.deltaTime;
        GetComponent<Rigidbody2D>().MovePosition(transform.position + moveEnemy);

    }

    protected override void shoot()
    {
        //This enemy does not shoot
    }
    
}
