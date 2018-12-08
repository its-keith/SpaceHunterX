using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    //Attributes that will be defined in the child class
    public int health;
    public int points;
    public int speed;
    public float rateOfFire;
    
    protected Vector2 screenPosition;
    protected bool onscreen;
    protected bool hitDelay;

    protected float freezeTime = 5f;
    protected float freezeCooldown;
    protected bool isFrozen;

    public int powerUpChance;

    public GameObject cat;
    public int catChance = 90;

    public GameObject[] PowerUps;
    // 0 - Rapid Fire
    // 1 - Spread Shot
    // 2 - Missiles
    // 3 - Laser
    // 4 - Freeze Time
    // 5 - Bomb
    // 6 - Shield

    protected abstract void shoot();
    protected abstract void move();

    [SerializeField]
    public AudioClip[] audio;

    protected void Update()
    {
        //When an enemy is hit by the "FreezeTime" PowerUp, do not execute move() and shoot() and count down until they can move again
        if (isFrozen)
        {
            freezeCooldown -= Time.deltaTime;
        } else
        {
            move();
            shoot();
        }
        //If cooldown is done, let the enemy move again
        if (isFrozen && freezeCooldown <= 0)
        {
            isFrozen = false;
            freezeCooldown = 0;
        }
    }
    
    /// <summary>
    /// Checks for a collision with a BoxCollider2D marked as a "Trigger"
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Only execute if the collision is that PlayerLaser object
        if (collision.gameObject.tag == "PlayerLaser")
        {
            //hitDelay bool adds a delay between damage, as opposed to damaging every frame
            if (!hitDelay)
            {
                health--;
                this.hitDelay = true;
                if (health <= 0) { explode(); } else { StartCoroutine(Hit()); StartCoroutine(HitDelay()); }
            }
        }
    }

    /// <summary>
    /// Checks for a collision with a BoxCollider2D marked as a "Trigger"
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Checks for collisions based on what the object is
        if (collision.gameObject.tag == "PlayerBullet")
        {
            health--;
            if (health <= 0) { explode(); } else { StartCoroutine(Hit()); }
        } else if (collision.gameObject.tag == "PlayerBomb")
        {
            health -= 10;
            if (health <= 0) { explode(); }
        } else if (collision.gameObject.tag == "PlayerFreezeTime")
        {
            frozen();
        } else if (collision.gameObject.tag == "PlayerMissile")
        {
            health -= 5;
            if (health <= 0) { explode(); }
        }
    }

    protected void frozen()
    {
        this.isFrozen = true;
        this.freezeCooldown = this.freezeTime;
    }

    //Plays a hit sound effect and then flashes the enemy gray
    IEnumerator Hit()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(audio[1]);
        gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        yield return new WaitForSeconds(.1f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    //Wait .1 seconds before allowing the enemy to be hit again
    IEnumerator HitDelay()
    {
        yield return new WaitForSeconds(.1f);
        this.hitDelay = false;
    }

    //Play an explosion sound effect, play an explosion animation, then destroy the object
    public virtual void explode()
    {
        DropPowerUp();
        AddScore(this.points);
        gameObject.GetComponent<AudioSource>().PlayOneShot(audio[0]);
        Animator animator = gameObject.GetComponent<Animator>();
        animator.SetInteger("Explosion", Random.Range(1,5)); //Play the explosion animation
        gameObject.GetComponent<BoxCollider2D>().enabled = false; //Turn off the collider so it no longer affects player bullets
        Destroy(gameObject, .4f); //Delete after .9 seconds (The length of the explosion animation)
    }

    /// <summary>
    /// Each enemy has a defined chance to drop a powerup. If the roll succeeds, then we roll 1-100 to decide which powerup drops
    /// </summary>
    void DropPowerUp()
    {
        int num = Random.Range(1, 100);
        if (num < powerUpChance)
        {
            num = Random.Range(1, 100);
            if (num > 0 && num <= 15) //15% Chance
            {
                //Spawn Shield
                Instantiate(PowerUps[6], transform.position, PowerUps[6].transform.rotation);
            }  else if (num > 15 && num <= 40) //25% Chance
            {
                //Spawn Rapid Fire
                Instantiate(PowerUps[0], transform.position, PowerUps[0].transform.rotation);
            } else if (num > 40 && num <= 60) //20% Chance
            {
                //Spawn Spread Shot
                Instantiate(PowerUps[1], transform.position, PowerUps[1].transform.rotation);
            } else if ((num > 60 && num <= 70)) //10% Chance 
            {
                //Spawn Missiles
                Instantiate(PowerUps[2], transform.position, PowerUps[2].transform.rotation);
            } else if (num > 70 && num <= 80) //10% Chance
            {
                //Spawn Laser
                Instantiate(PowerUps[3], transform.position, PowerUps[3].transform.rotation);
            } else if (num > 80 && num <= 90) //10% Chance
            {
                //Spawn Freeze Time
                Instantiate(PowerUps[4], transform.position, PowerUps[4].transform.rotation);
            } else if (num > 90 && num <= 100) //10% Chance
            {
                //Spawn Bomb
                Instantiate(PowerUps[5], transform.position, PowerUps[5].transform.rotation);
            }
        } else if (num > powerUpChance && num < (catChance + powerUpChance))
        {
            Instantiate(cat, transform.position, transform.rotation);
        }
    }

    protected void AddScore(int points)
    {
        Scoring.UpdateScore(points);
    }
    
}