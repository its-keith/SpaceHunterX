using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFireEnemy : Enemy
{

    RapidFireEnemy()
    {
        this.health = 1;
        this.points = 50;
        this.speed = 500;
        this.rateOfFire = 2f;
        this.powerUpChance = 10;
    }

    public GameObject bullet;
    Vector3 moveEnemy;
    float cooldown = 0;
    public int bulletAmount = 10;

    private bool setSide;
    private float xSpeed;

    protected override void move()
    {
        //Move forward
        if (transform.position.x > CameraScale.rightEdge && !setSide)
        {
            setSide = true;
            transform.Rotate(0, 0, -50);
            xSpeed = -speed;
        }
        else if (transform.position.x < CameraScale.leftEdge && !setSide)
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

        if (cooldown <= 0)
        {
            cooldown = rateOfFire;
            
            GameObject target;

            if (GameObject.Find("Player1(Clone)") && GameObject.Find("Player2(Clone)"))
            {
                target = UnityEngine.Random.Range(1, 3) == 1 ? GameObject.Find("Player1(Clone)") : GameObject.Find("Player2(Clone)");
            }
            else
            {
               target = GameObject.Find("Player1(Clone)");
            }

            StartCoroutine(Fire(target));
        }
    }

    IEnumerator Fire(GameObject target)
    {
        for (int i = 0; i < bulletAmount; i++)
        {
            //Stop shooting if blown up
            if (GetComponent<BoxCollider2D>().enabled)
            {
                bullet.transform.eulerAngles = new Vector3(0, 0, 180);
                GameObjectUtil.Instantiate(bullet, transform.position, null);
                yield return new WaitForSeconds(.1f);
            }
        }
    }
}
