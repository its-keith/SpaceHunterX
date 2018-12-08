using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOBlue : Enemy
{

    UFOBlue()
    {
        this.health = 2;
        this.points = 20;
        this.speed = 600;
        this.rateOfFire = 1;
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

        if (transform.position.x > CameraScale.rightEdge && leftSide)
        {
            speed = -speed;
            leftSide = false;
        }

        if (!onscreen && transform.position.x < CameraScale.rightEdge - 50 && transform.position.x > CameraScale.leftEdge + 50)
        {
            onscreen = true;
        }

        if (onscreen && (transform.position.x > CameraScale.rightEdge - 30 || transform.position.x < CameraScale.leftEdge + 30))
        {
            speed = -speed;
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
            for (int i = 0; i < 1; i++)
            {
                bullet.transform.eulerAngles = new Vector3(0, 0, Random.Range(140, 220));
                GameObjectUtil.Instantiate(bullet, transform.position, null);
            }
            //GameObjectUtil.Instantiate(bullet, transform.position, null);
            //bullet.transform.Rotate(0, 0, 90);
            //GameObjectUtil.Instantiate(bullet, transform.position, null);
            //bullet.transform.Rotate(0, 0, 110);
            //GameObjectUtil.Instantiate(bullet, transform.position, null);
            //bullet.transform.Rotate(0, 0, 130);
            //GameObjectUtil.Instantiate(bullet, transform.position, null);
            //bullet.transform.Rotate(0, 0, 150);
            //GameObjectUtil.Instantiate(bullet, transform.position, null);
        }
    }
}
