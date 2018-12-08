using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunEnemy : Enemy {
    
    ShotgunEnemy()
    {
        this.health = 1;
        this.points = 50;
        this.speed = 500;
        this.rateOfFire = 0f;
        this.powerUpChance = 10;
    }

    public GameObject bullet;
    Vector3 moveEnemy;
    float cooldown = 0;
    public int shotAmount = 5;

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

            StartCoroutine(Shotgun());
        }
    }

    IEnumerator Shotgun()
    {
        for (int i = 0; i < shotAmount; i++)
        {
            bullet.transform.eulerAngles = new Vector3(0, 0, Random.Range(160, 220));
            GameObjectUtil.Instantiate(bullet, transform.position, null);
            yield return new WaitForSeconds(.05f);
        }
        yield return new WaitForSeconds(0.35f);
    }

}
