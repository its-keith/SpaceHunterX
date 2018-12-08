using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Enemy
{

    Turret()
    {
        this.health = 25;
        this.points = 050;
        this.speed = 0;
        this.rateOfFire = 5f;
        this.powerUpChance = 100;
    }

    public GameObject bullet;
    public int bulletAmount = 8;

    float cooldown;
    public bool leftSide;

    protected override void move()
    {
        if (leftSide)
        {
            transform.position = new Vector3(transform.parent.position.x - 300, transform.parent.position.y - 100, transform.parent.position.z);
        } else
        {
            transform.position = new Vector3(transform.parent.position.x + 300, transform.parent.position.y - 100, transform.parent.position.z);
        }
        
    }

    protected override void shoot()
    {
        cooldown -= Time.deltaTime;

        if (cooldown <= 0 && transform.parent.position.y <= 600)
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
