using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreezeTime : PowerUp
{
    public Sprite active;
    public Sprite inactive;

    public AudioClip audio;

    public GameObject circle;

    void Update()
    {
        if ((Input.GetKeyDown(button) || Input.GetButton(arcadeButton)) && hasPowerUp && cooldown >= 0)
        {
            activatePowerUp();
        }
    }

    protected override void activatePowerUp()
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

        GameObject.Find("Canvas/" + panel + "/" + gameObject.tag + "_PUs/FreezeTime").GetComponent<Image>().sprite = inactive; //Update the UI
        GameObjectUtil.Instantiate(circle, gameObject.transform.position, null); //Spawn the FreezeTimeCircle
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FreezeTime")
        {
            this.hasPowerUp = this.PickUp(gameObject.tag, collision.gameObject.tag, active, collision.gameObject); //Sets hasPowerUp to true
            this.PlayAudio(audio);
            this.ShowText("Time Freeze");
        }
    }
}
