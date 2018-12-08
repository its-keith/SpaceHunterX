using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFORed :Enemy {


    UFORed()
    {
        this.health = 5;
        this.points = 50;
        this.speed = 500;
        this.rateOfFire = 1f;
        this.powerUpChance = 50;
    }

    public GameObject bullet;

    private float vertSpeed = -100f;
    private bool leftSide = true;
    Vector3 moveEnemy;

    float cooldown = 0;

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
        if (!onscreen && transform.position.x < CameraScale.rightEdge - 50 && transform.position.x > CameraScale.leftEdge + 50)
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
            }
            else
            {
                transform.Translate(20f, 0, 0);
            }
        }

        moveEnemy = new Vector3(speed, vertSpeed, 0) * Time.deltaTime;
        GetComponent<Rigidbody2D>().MovePosition(transform.position + moveEnemy);

    }

    protected override void shoot()
    {
        cooldown -= Time.deltaTime;

        if (cooldown <= 0)
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(audio[1]);
            cooldown = rateOfFire;
            
            GameObjectUtil.Instantiate(bullet, transform.position, null);
            bullet.transform.Rotate(0, 0, 45);
            GameObjectUtil.Instantiate(bullet, transform.position, null);
            bullet.transform.Rotate(0, 0, 90);
            GameObjectUtil.Instantiate(bullet, transform.position, null);
            bullet.transform.Rotate(0, 0, 135);
            GameObjectUtil.Instantiate(bullet, transform.position, null);
            bullet.transform.Rotate(0, 0, 180);
            GameObjectUtil.Instantiate(bullet, transform.position, null);
            bullet.transform.Rotate(0, 0, 225);
            GameObjectUtil.Instantiate(bullet, transform.position, null);
            bullet.transform.Rotate(0, 0, 270);
            GameObjectUtil.Instantiate(bullet, transform.position, null);
            bullet.transform.Rotate(0, 0, 315);
            GameObjectUtil.Instantiate(bullet, transform.position, null);
        }
    }
}
