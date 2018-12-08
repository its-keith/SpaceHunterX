using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : Enemy
{

    ShooterEnemy()
    {
        this.health = 1;
        this.points = 10;
        this.speed = 500;
        this.rateOfFire = 2f;
        this.powerUpChance = 10;
    }

    public GameObject bullet;

    Vector3 moveEnemy;

    float cooldown = 0;

    protected override void move()
    {
        //Move down in a straight line
        moveEnemy = new Vector3(0, -speed, 0) * Time.deltaTime;
        GetComponent<Rigidbody2D>().MovePosition(transform.position + moveEnemy);
    }

    protected override void shoot()
    {
        cooldown -= Time.deltaTime;

        if (cooldown <= 0)
        {
            cooldown = rateOfFire;
            gameObject.GetComponent<AudioSource>().PlayOneShot(audio[1]);
            Instantiate(bullet, transform.position, bullet.transform.rotation);
        }
    }
}
