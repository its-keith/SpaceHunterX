using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PowerUp : MonoBehaviour {

    public Text notification;

    [SerializeField]
    protected KeyCode button;
    public string arcadeButton;

    protected bool hasPowerUp;

    protected float lasts;
    public float cooldown = 0;

    private void Start()
    {
    }

    public bool PickUp(String player, String PowerUp, Sprite sprite, GameObject go)
    {
        String panel;
        if (player == "Player1")
        {
            panel = "LeftPanel";
        } else
        {
            panel = "RightPanel";
        }

        GameObject.Find("Canvas/" + panel + "/" + player + "_PUs/" + PowerUp).GetComponent<Image>().sprite = sprite;
        GameObjectUtil.Destroy(go);
        
        return true;
    }

    public void PlayAudio(AudioClip audio)
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(audio);
    }

    public void ShowText(string text)
    {
        Text txt = Instantiate(notification);
        txt.transform.SetParent(GameObject.Find("Canvas").transform, true);
        txt.text = text;
    }

    protected abstract void OnTriggerEnter2D(Collider2D collision);
    protected abstract void activatePowerUp();
    
}
