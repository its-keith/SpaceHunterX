﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1EnemyManager : MonoBehaviour
{
    public Text notification;
    public AudioClip[] warning;
    //0 - Warning
    //1 - Alarm
    //2 - Large Enemy Incoming

    public bool startSpawning;
    public bool frozen;
    float freezeTime = 5f;

    public GameObject[] enemies;
    //0 - Yellow UFO
    //1 - Yellow Shooter
    //2 - Orange UFO
    //3 - Orange Missile
    //4 - Boss

    public GameObject[] spawners;
    //0 - Left
    //1 - Top Left
    //2 - Top Middle Left
    //3 - Top Middle
    //4 - Top Middle Right
    //5 - Top Right
    //6 - Right

    private float spawnTimer = 0;
    private bool delay;
    
    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Level1Manager").GetComponent<LevelManager>().startSpawning = true && !frozen)
        {
            spawnTimer += Time.deltaTime;

            //Spawn Yellow UFOs
            if (spawnTimer > 3 && spawnTimer < 20)
            {
                if (!delay)
                {
                    delay = true;
                    StartCoroutine(SpawnUFOS(enemies[0], 1f));
                }
            }
            //Spawn Yellow UFOs faster
            if (spawnTimer > 20 && spawnTimer < 30)
            {
                if (!delay)
                {
                    delay = true;
                    StartCoroutine(SpawnUFOS(enemies[0], .25f));
                }
            }
            //Spawn a group of Yellow Shooters
            if (spawnTimer > 35 && spawnTimer < 36)
            {
                if (!delay)
                {
                    delay = true;
                    StartCoroutine(SpawnShooters(enemies[1]));
                }
            }
            //Spawn missile enemy
            if (spawnTimer > 40 && spawnTimer < 48)
            {
                if (!delay)
                {
                    delay = true;
                    StartCoroutine(SpawnMissileEnemies(enemies[3]));
                }
            }
            //Spawn a group of Yellow Shooters
            if (spawnTimer > 49 && spawnTimer < 50)
            {
                if (!delay)
                {
                    delay = true;
                    StartCoroutine(SpawnShooters(enemies[1]));
                }
            }
            //Spawn orange UFOS
            if (spawnTimer > 55 && spawnTimer < 65)
            {
                if(!delay)
                {
                    delay = true;
                    StartCoroutine(SpawnUFOS(enemies[2], 5f));
                }
            }

            //Spawn yellow UFOS
            if (spawnTimer > 65 && spawnTimer < 80)
            {
                if (!delay)
                {
                    delay = true;
                    StartCoroutine(SpawnUFOS(enemies[0], 1f));
                }
            }

            //Spawn yellow UFOS faster
            if (spawnTimer > 80 && spawnTimer < 85)
            {
                if (!delay)
                {
                    delay = true;
                    StartCoroutine(SpawnUFOS(enemies[0], .25f));
                }
            }

            //Player warning
            if (spawnTimer > 95 && spawnTimer < 96)
            {
                if (!delay)
                {
                    delay = true;
                    StartCoroutine(Warning());
                }
            }
            //Spawn Boss
            if (spawnTimer > 102 && spawnTimer < 103)
            {
                if (!delay)
                {
                    delay = true;
                    StartCoroutine(SpawnBoss(enemies[4]));
                    
                }
            }
        }
        if (frozen)
        {
            freezeTime -= Time.deltaTime;
        }
        if (freezeTime <= 0)
        {
            frozen = false;
            freezeTime = 5f;
        }
    }

    IEnumerator Warning()
    {
        for (int i = 0; i < 3; i++)
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(warning[1]);
            yield return new WaitForSeconds(1.5f);
        }
        for (int i = 0; i < 2; i++)
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(warning[0]);
            Text txt = Instantiate(notification);
            txt.transform.SetParent(GameObject.Find("Canvas").transform, true);
            txt.text = "Warning!";
            yield return new WaitForSeconds(1f);
        }
        gameObject.GetComponent<AudioSource>().PlayOneShot(warning[2]);
        Text txt2 = Instantiate(notification);
        txt2.transform.SetParent(GameObject.Find("Canvas").transform, true);
        txt2.text = "Large Enemy Incoming!";
        delay = false;
    }

    IEnumerator SpawnBoss(GameObject ship)
    {
        GameObjectUtil.Instantiate(ship, spawners[3].transform.position, null);
        yield return new WaitForSeconds(1f);
        delay = false;
    }

        IEnumerator SpawnMissileEnemies(GameObject ship)
    {
        GameObjectUtil.Instantiate(ship, spawners[0].transform.position, null);
        yield return new WaitForSeconds(1f);
        GameObjectUtil.Instantiate(ship, spawners[6].transform.position, null);
        yield return new WaitForSeconds(1f);
        delay = false;
    }

    IEnumerator SpawnShooters(GameObject ship)
    {
        for (int i = 0; i < 5; i++)
        {
            GameObjectUtil.Instantiate(enemies[1], spawners[i + 1].transform.position, null);
            yield return new WaitForSeconds(.5f);
        }
        for (int i = 4; i > 0; i--)
        {
            GameObjectUtil.Instantiate(enemies[1], spawners[i].transform.position, null);
            yield return new WaitForSeconds(.5f);
        }
        yield return new WaitForSeconds(1f);
        delay = false;
    }

    IEnumerator SpawnUFOS(GameObject UFO, float interval)
    {
        GameObjectUtil.Instantiate(UFO, spawners[0].transform.position, null);
        GameObjectUtil.Instantiate(UFO, spawners[6].transform.position, null);
        yield return new WaitForSeconds(interval);
        delay = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerFreezeTime")
        {
            frozen = true;
        }
    }
}
