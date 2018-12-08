using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level2EnemyManager : MonoBehaviour {

    public Text notification;
    public AudioClip[] warning;
    //0 - Warning
    //1 - Alarm
    //2 - Large Enemy Incoming

    public bool startSpawning;
    public bool frozen;
    float freezeTime = 5f;

    public GameObject[] enemies;

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
        if (GameObject.Find("Level2Manager").GetComponent<LevelManager>().startSpawning = true && !frozen)
        {
            spawnTimer += Time.deltaTime;
        }
        //spawn yellow shooter enemies, movement: straight down
        if (spawnTimer > 2 && spawnTimer < 10)
        {
            if (!delay)
            {
                delay = true;
                StartCoroutine(SpawnShooters(enemies[0]));
            }
        }
        
        //spawn blue enemies, movement: diagonal 
        if (spawnTimer > 10 && spawnTimer < 20)
        {
            if (!delay)
            {
                delay = true;
                StartCoroutine(SpawnBlueUFOS(enemies[2], 2f));
            }
        }

        //spawn shotgun enemies, movement: straight down
        if (spawnTimer > 25 && spawnTimer < 32)
        {
            if (!delay)
            {
                delay = true;
                StartCoroutine(SpawnShotgun(enemies[3]));
            }
        }

        //spawn basic UFOS, moves diagonally
        if (spawnTimer > 35 && spawnTimer < 45)
        {
            if (!delay)
            {
                delay = true;
                StartCoroutine(SpawnUFOS(enemies[0], 0.25f));
            }
        }

        //spawn missle enemies, moves diagonally
        if (spawnTimer > 50 && spawnTimer < 57)
        {
            if (!delay)
            {
                delay = true;
                StartCoroutine(SpawnMissile(enemies[5]));
            }
        }

        //spawn more basic UFOs
        if (spawnTimer > 60 && spawnTimer < 65)
        {
            if (!delay)
            {
                delay = true;
                StartCoroutine(SpawnUFOS(enemies[0], 0.25f));
            }
        }

        //spawn red shooters, more health but slower. move straight down
        if (spawnTimer > 70 && spawnTimer < 77)
        {
            if (!delay)
            {
                delay = true;
                StartCoroutine(SpawnRedShooters(enemies[6], 3.0f));
            }
        }

        //spawn more red shooters (in different spawn locations), more health but slower. move straight down
        if (spawnTimer > 77 && spawnTimer < 82)
        {
            if (!delay)
            {
                delay = true;
                StartCoroutine(SpawnMoreRedShooters(enemies[6], 3.0f));
            }
        }
        
        //next two methods just spawn red shooters more frequently
        if (spawnTimer > 82 && spawnTimer < 86)
        {
            if (!delay)
            {
                delay = true;
                StartCoroutine(SpawnRedShooters(enemies[6], 3.0f));
            }
        }

        if (spawnTimer > 86 && spawnTimer < 90)
        {
            if (!delay)
            {
                delay = true;
                StartCoroutine(SpawnMoreRedShooters(enemies[6], 3.0f));
            }
        }

        //warning message
        if (spawnTimer > 98 && spawnTimer < 99)
        {
            if (!delay)
            {
                delay = true;
                StartCoroutine(Warning());
            }
        }
        
        //spawns boss
        if (spawnTimer > 106 && spawnTimer < 107)
        {
            if (!delay)
            {
                delay = true;
                StartCoroutine(SpawnBoss(enemies[4]));
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

    IEnumerator SpawnBlueUFOS(GameObject UFOBlue, float interval)
    {
        GameObjectUtil.Instantiate(UFOBlue, spawners[3].transform.position, null);
        //GameObjectUtil.Instantiate(UFOBlue, spawners[].transform.position, null);
        yield return new WaitForSeconds(interval);
        delay = false;
    }

    IEnumerator SpawnUFOS(GameObject UFO, float interval)
    {
        GameObjectUtil.Instantiate(UFO, spawners[0].transform.position, null);
        GameObjectUtil.Instantiate(UFO, spawners[6].transform.position, null);
        yield return new WaitForSeconds(interval);
        delay = false;
    }

    IEnumerator SpawnRedShooters(GameObject shooterRed, float interval)
    {
        GameObjectUtil.Instantiate(shooterRed, spawners[1].transform.position, null);
        GameObjectUtil.Instantiate(shooterRed, spawners[3].transform.position, null);
        GameObjectUtil.Instantiate(shooterRed, spawners[5].transform.position, null);
        yield return new WaitForSeconds(interval);
        delay = false;
    }

    IEnumerator SpawnMoreRedShooters(GameObject shooterRed, float interval)
    {
        GameObjectUtil.Instantiate(shooterRed, spawners[2].transform.position, null);
        GameObjectUtil.Instantiate(shooterRed, spawners[4].transform.position, null);
        yield return new WaitForSeconds(interval);
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

    IEnumerator SpawnShotgun(GameObject ship)
    {
        for (int i = 0; i < 2; i++)
        {
            GameObjectUtil.Instantiate(enemies[3], spawners[i + 1].transform.position, null);
            yield return new WaitForSeconds(1f);
        }
        
        for (int i = 5; i > 3; i--)
        {
            GameObjectUtil.Instantiate(enemies[3], spawners[i - 1].transform.position, null);
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(1f);
        delay = false;
    }

    IEnumerator SpawnBoss(GameObject ship)
    {
        GameObjectUtil.Instantiate(ship, spawners[3].transform.position, null);
        yield return new WaitForSeconds(1f);
        delay = false;
    }

    IEnumerator SpawnMissile(GameObject ship)
    {
        GameObjectUtil.Instantiate(ship, spawners[0].transform.position, null);
        yield return new WaitForSeconds(1.0f);
        GameObjectUtil.Instantiate(ship, spawners[3].transform.position, null);
        yield return new WaitForSeconds(1.0f);
        GameObjectUtil.Instantiate(ship, spawners[6].transform.position, null);
        yield return new WaitForSeconds(1.0f);
        delay = false;
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerFreezeTime")
        {
            frozen = true;
        }
    }
}
