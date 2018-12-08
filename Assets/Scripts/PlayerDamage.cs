using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour {

    public Sprite[] shields;
    public GameObject explosion;

    public AudioClip[] audio;
    public Text notification;

    public int shieldLevel = 2;

    bool delay;

	// Update is called once per frame
	void Update () {

        if (shieldLevel == 3)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = shields[2];
        } else if (shieldLevel == 2)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = shields[1];
        } else if (shieldLevel == 1)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = shields[0];
        } else if (shieldLevel == 0) 
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
        }

        UpdateUI();
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Enemy" || collision.tag == "EnemyWeapon") && shieldLevel >= 0)
        { 
            if (!delay)
            {
                if (shieldLevel == 0)
                {
                    Explode();
                } else
                {
                    shieldLevel--;
                    delay = true;
                    gameObject.GetComponent<AudioSource>().PlayOneShot(audio[2]);
                    StartCoroutine(Hit());
                }
            }
        } else if (collision.gameObject.tag == "Shield" && shieldLevel < 3)
        {
            Destroy(collision.gameObject);
            shieldLevel++;
            gameObject.GetComponent<AudioSource>().PlayOneShot(audio[0]);
            gameObject.GetComponent<AudioSource>().PlayOneShot(audio[1]);
            Text txt = Instantiate(notification);
            txt.transform.SetParent(GameObject.Find("Canvas").transform, true);
            txt.text = "Shield";
        }
    }

    void UpdateUI()
    {
        string panel;
        if (gameObject.tag == "Player1")
        {
            panel = "LeftPanel";
        } else 
        {
            panel = "RightPanel";
        }

        if (shieldLevel >= 3)
        {
            Panel(panel, true, true, true);
        } else if (shieldLevel == 2)
        {
            Panel(panel, false, true, true);
        } else if (shieldLevel == 1)
        {
            Panel(panel, false, false, true);
        } else if (shieldLevel == 0)
        {
            Panel(panel, false, false, false);
        }
    }

    void Panel(string panel, bool green, bool yellow, bool orange)
    {
        GameObject.Find("Canvas/" + panel + "/" + "ShieldLevel/Green").SetActive(green);
        GameObject.Find("Canvas/" + panel + "/" + "ShieldLevel/Yellow").SetActive(yellow);
        GameObject.Find("Canvas/" + panel + "/" + "ShieldLevel/Orange").SetActive(orange);
    }

    IEnumerator Hit()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(.1f);
            gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
            yield return new WaitForSeconds(.1f);
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
        delay = false;
    }

    void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
