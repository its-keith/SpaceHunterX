using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level5EnemyManager : MonoBehaviour {

    public Text notification;
    public AudioClip[] warning;
    //0 - Warning
    //1 - Alarm
    //2 - Large Enemy Incoming

    public bool startSpawning;
    public bool frozen;
    float freezeTime = 5f;

    public GameObject[] enemies;
    //0 - Rapid Fire Enemy
    //1 - ShotgunEnemy
    //2 - Red UFO
    //3 - Yellow UFO
    //4 - Level Five Boss

    public GameObject[] spawners;
    //0 - Left
    //1 - Top Left
    //2 - Top Middle Left
    //3 - Top Middle
    //4 - Top Middle Right
    //5 - Top Right
    //6 - Right

    private float spawnTimer;
    private bool delay;

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Level5Manager").GetComponent<LevelManager>().startSpawning = true && !frozen)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer > 3 && spawnTimer < 15)
            {
                if (!delay)
                {
                    delay = true;
                    StartCoroutine(SpawnRapidFire(enemies[0]));
                }
            }

            if (spawnTimer > 15 && spawnTimer < 16)
            {
                if (!delay)
                {
                    delay = true;
                    StartCoroutine(SpawnShooters(enemies[1]));
                }
            }

            if (spawnTimer > 25 && spawnTimer < 45)
            {
                if (!delay)
                {
                    delay = true;
                    StartCoroutine(SpawnUFOS(enemies[2], 8f));
                }
            }

            if (spawnTimer > 45 && spawnTimer < 55)
            {
                if (!delay)
                {
                    delay = true;
                    StartCoroutine(SpawnUFOS(enemies[3], .25f));
                }
            }

            if (spawnTimer > 55 && spawnTimer < 75)
            {
                if (!delay)
                {
                    delay = true;
                    StartCoroutine(SpawnUFOS(enemies[2], 8f));
                }
            }

            //Player warning
            if (spawnTimer > 75 && spawnTimer < 76)
            {
                if (!delay)
                {
                    delay = true;
                    StartCoroutine(Warning());
                }
            }
            //Spawn Boss
            if (spawnTimer > 82 && spawnTimer < 83)
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

    IEnumerator SpawnRapidFire(GameObject enemy)
    {
        GameObjectUtil.Instantiate(enemy, spawners[0].transform.position, null);
        yield return new WaitForSeconds(2f);
        GameObjectUtil.Instantiate(enemy, spawners[6].transform.position, null);
        yield return new WaitForSeconds(2f);
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
