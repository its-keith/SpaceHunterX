using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RapidFire : PowerUp {
    
    public Sprite active;
    public Sprite inactive;

    public AudioClip audio;

    public RapidFire()
    {
        this.lasts = 5f;
    }

    protected override void activatePowerUp()
    {
        gameObject.GetComponent<PlayerShooting>().rateOfFire = .1f;
        hasPowerUp = false;
        cooldown = lasts;

        string panel;
        if (gameObject.tag == "Player1")
        {
            panel = "LeftPanel";
        }
        else
        {
            panel = "RightPanel";
        }

        GameObject.Find("Canvas/" + panel + "/" + gameObject.tag + "_PUs/RapidFire").GetComponent<Image>().sprite = inactive;
    }

    private void Update()
    {

        if ((Input.GetKeyDown(button) ||Input.GetButton(arcadeButton)) && hasPowerUp && cooldown >= 0)
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
            gameObject.GetComponent<PlayerShooting>().rateOfFire = gameObject.GetComponent<PlayerShooting>().baseROF;
        }   
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RapidFire")
        {
            this.hasPowerUp = this.PickUp(gameObject.tag, collision.gameObject.tag, active, collision.gameObject);
            this.PlayAudio(audio);
            this.ShowText("Rapid Fire");
        }
    }
}
