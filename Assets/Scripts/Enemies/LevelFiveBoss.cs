using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFiveBoss : Enemy
{

    LevelFiveBoss()
    {
        this.health = 100;
        this.points = 5000;
        this.speed = 300;
        this.rateOfFire = 5f;
        this.powerUpChance = 0;
    }

    private float vertSpeed;
    private Vector3 moveEnemy;
    private bool moving;

    public GameObject bullet;
    public GameObject missile;

    float cooldown;
    
    public int shotAmount = 8;
    public int burstAmount = 3;
    public int missileAmount = 5;

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

            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                StartCoroutine(Shotgun());
            } else
            {
                StartCoroutine(Missiles());
            }

            
        }
    }

    public override void explode()
    {
        AddScore(this.points);
        gameObject.GetComponent<AudioSource>().PlayOneShot(audio[0]);
        Animator animator = gameObject.GetComponent<Animator>();
        animator.SetInteger("Explosion", UnityEngine.Random.Range(1, 5)); //Play the explosion animation
        gameObject.GetComponent<BoxCollider2D>().enabled = false; //Turn off the collider so it no longer affects player bullets
        Destroy(gameObject, .4f); //Delete after .9 seconds (The length of the explosion animation)
        GameObject.Find("Level5Manager").GetComponent<LevelManager>().LoadNextLevel("Level 5 Complete!", "Win");
    }

    IEnumerator Missiles()
    {
        for (int i = 0; i < missileAmount; i++)
        {
            if (GameObject.Find("Player1(Clone)") && GameObject.Find("Player2(Clone)"))
            {
                missile.GetComponent<MissileObject>().target = UnityEngine.Random.Range(1, 3) == 1 ? GameObject.Find("Player1(Clone)") : GameObject.Find("Player2(Clone)");
            }
            else
            {
                missile.GetComponent<MissileObject>().target = GameObject.Find("Player1(Clone)");
            }

            Instantiate(missile, transform);
            yield return new WaitForSeconds(.5f);
        }
    }

    IEnumerator Shotgun()
    {
        for (int j = 0; j < burstAmount; j++)
        {
            for (int i = 0; i < shotAmount; i++)
            {
                bullet.transform.eulerAngles = new Vector3(0, 0, UnityEngine.Random.Range(160, 220));
                GameObjectUtil.Instantiate(bullet, transform.position, null);
                yield return new WaitForSeconds(.05f);
            }
            yield return new WaitForSeconds(0.35f);
        }
    }
}
