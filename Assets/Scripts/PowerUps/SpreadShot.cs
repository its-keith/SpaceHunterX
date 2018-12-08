using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpreadShot : PowerUp
{
    public Sprite active;
    public Sprite inactive;

    public AudioClip audio;

    [SerializeField]
    private GameObject bulletL;
    [SerializeField]
    private GameObject bulletR;

    GameObject player;
    Vector3 pos;
    float cooldownROF;

    public SpreadShot()
    {
        this.lasts = 5f;
    }

    protected override void activatePowerUp()
    {
        cooldown = lasts;
        hasPowerUp = false;

        string panel;
        if (gameObject.tag == "Player1")
        {
            panel = "LeftPanel";
        }
        else
        {
            panel = "RightPanel";
        }

        //Replace the UI image with the inactive sprite
        GameObject.Find("Canvas/" + panel + "/" + gameObject.tag + "_PUs/SpreadShot").GetComponent<Image>().sprite = inactive;
    }

    private void Start()
    {
    }

    private void Update()
    {
        if ((Input.GetKeyDown(button) || Input.GetButton(arcadeButton)) && hasPowerUp && cooldown >= 0)
        {
            activatePowerUp();
        }

        //if Active
        if (cooldown > 0)
        {
            //Lower PowerUp cooldown
            cooldown -= Time.deltaTime;
            //Lower RateOfFire cooldown
            cooldownROF -= Time.deltaTime;
            if (cooldownROF <= 0)
            {
                //Set RateOfFire cooldown equal to current RateOfFire of normal shots
                cooldownROF = gameObject.GetComponent<PlayerShooting>().rateOfFire;
                SpreadShooting();
            }

        }

        if (cooldown <= 0)
        {
            cooldown = 0f;
        }
    }

    /// <summary>
    /// Create SpreadShot bullets
    /// </summary>
    private void SpreadShooting()
    {
        //Position the first (left) bullet slighty to the left
        pos = new Vector3(transform.position.x - 30, transform.position.y + (gameObject.GetComponent<Collider2D>().bounds.size.y), transform.position.z + 1);
        Instantiate(bulletL, pos, bulletL.transform.rotation);
        //Change the position to the right for the right bullet
        pos.x += 60;
        Instantiate(bulletR, pos, bulletR.transform.rotation);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SpreadShot")
        {
            this.hasPowerUp = this.PickUp(gameObject.tag, collision.gameObject.tag, active, collision.gameObject);
            this.PlayAudio(audio);
            this.ShowText("Spread Shot");
        }
    }
}
