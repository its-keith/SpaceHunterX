using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Laser : PowerUp
{
    public Sprite active;
    public Sprite inactive;

    public AudioClip audio;

    public GameObject laser;

    Laser()
    {
        this.lasts = 3f;
    }

    protected override void activatePowerUp()
    {

        string panel;
        if (gameObject.tag == "Player1")
        {
            panel = "LeftPanel";
        }
        else
        {
            panel = "RightPanel";
        }

        GameObject.Find("Canvas/" + panel + "/" + gameObject.tag + "_PUs/Laser").GetComponent<Image>().sprite = inactive; //Update the UI
        GameObjectUtil.Instantiate(laser, transform.position, gameObject); //Spawn the LaserObject and set the player as its parent
        hasPowerUp = false;
        cooldown = lasts;
        gameObject.GetComponent<PlayerShooting>().cooldown = lasts; //Stop the player from shooting
    }

    private void Update()
    {

        if ((Input.GetKeyDown(button) || Input.GetButton(arcadeButton)) && hasPowerUp && cooldown >= 0)
        {
            activatePowerUp();
        }

        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }

        if (cooldown <= 0)
        {
            cooldown = 0f;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Laser")
        {
            this.hasPowerUp = this.PickUp(gameObject.tag, collision.gameObject.tag, active, collision.gameObject); //Sets hasPowerUp to true
            this.PlayAudio(audio);
            this.ShowText("Laser");
        }
    }
}
