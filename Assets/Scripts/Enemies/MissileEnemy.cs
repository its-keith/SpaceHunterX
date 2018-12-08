using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileEnemy : Enemy {

    MissileEnemy()
    {
        this.health = 3;
        this.points = 30;
        this.speed = 300;
        this.rateOfFire = 3f;
        this.powerUpChance = 30;
    }

    public GameObject missile;
    Vector3 moveEnemy;

    private bool setSide;
    private float cooldown;
    private float xSpeed;


    protected override void move()
    {
        //Move forward
        if (transform.position.x > CameraScale.rightEdge && !setSide)
        {
            setSide = true;
            transform.Rotate(0, 0, -50);
            xSpeed = -speed;
        } else if (transform.position.x < CameraScale.leftEdge && !setSide)
        {
            setSide = true;
            transform.Rotate(0, 0, 50);
            xSpeed = speed;
        }
        moveEnemy = new Vector3(xSpeed, -speed, 0) * Time.deltaTime;
        GetComponent<Rigidbody2D>().MovePosition(transform.position + moveEnemy);
    }

    protected override void shoot()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0 && health > 0)
        {
            cooldown = rateOfFire;
            if (GameObject.Find("Player1(Clone)") && GameObject.Find("Player2(Clone)"))
            {
                missile.GetComponent<MissileObject>().target = Random.Range(1, 3) == 1 ? GameObject.Find("Player1(Clone)") : GameObject.Find("Player2(Clone)");
            } else if (GameObject.Find("Player1(Clone)"))
            {
                missile.GetComponent<MissileObject>().target = GameObject.Find("Player1(Clone)");
            } else
            {
                missile.GetComponent<MissileObject>().target = GameObject.Find("Player2(Clone)");
            }
            
            Instantiate(missile, transform);
        }
    }
}
