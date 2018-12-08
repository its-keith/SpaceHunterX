using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Missiles : PowerUp
{
    public Sprite active;
    public Sprite inactive;

    public AudioClip audio;

    public GameObject missile;
    GameObject[] enemies;

    protected override void activatePowerUp()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 0)
        {
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

            GameObject.Find("Canvas/" + panel + "/" + gameObject.tag + "_PUs/Missiles").GetComponent<Image>().sprite = inactive;
            for (int i = 0; i < enemies.Length || i < 3; i++) // Shoot at six enemies at most
            {
                missile.GetComponent<MissileObject>().target = enemies[i];
                Instantiate(missile, transform.position, transform.rotation);
            }
        }
    }

    private void Update()
    {
        if ((Input.GetKeyDown(button) || Input.GetButton(arcadeButton)) && hasPowerUp)
        {
            activatePowerUp();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Missiles")
        {
            this.hasPowerUp = this.PickUp(gameObject.tag, collision.gameObject.tag, active, collision.gameObject);
            this.PlayAudio(audio);
            this.ShowText("Missiles");
        }
    }
}
