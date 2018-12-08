using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGreen : Enemy
{

    EnemyGreen()
    {
        this.health = 2;
        this.points = 1000;
        this.speed = 300;
        this.rateOfFire = 2f;
        this.powerUpChance = 0;
    }

    private float vertSpeed;
    private Vector3 moveEnemy;
    private bool moving;

    public GameObject bullet;

    float cooldown;

    protected override void move()
    {
        //Move down until x is 600
        if (transform.position.y > 600)
        {
            speed = 0;
            vertSpeed = -200;
        }
        else if (!moving)
        {
            moving = true;
            vertSpeed = 0;
            speed = 300;
        }

        //Change direction when the enemy reaches the edge of the screen and only after it has moved into the screen far enough
        if (transform.position.y <= 600 && (transform.position.x > CameraScale.rightEdge - 30 || transform.position.x < CameraScale.leftEdge + 30))
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
        if (transform.position.y <= 600 && cooldown <= 0)
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(audio[1]);
            cooldown = rateOfFire;

            for (int i = 0; i < 3; i++)
            {
                bullet.transform.eulerAngles = new Vector3(bullet.transform.eulerAngles.x, bullet.transform.eulerAngles.y, Random.Range(140, 220));
                GameObjectUtil.Instantiate(bullet, transform.position, null);
            }

        }
    }
}
