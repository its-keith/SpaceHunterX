using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    //Speed of bullet
    public float speed = 2000f;
    
	// Update is called once per frame
	void Update () {
        //Get its position
        Vector3 position = transform.position;

        Vector3 velocity = new Vector3(0, speed * Time.deltaTime, 0);

        //Update position with new y value
        position += transform.rotation * velocity;

        //Apply to the game object
        transform.position = position;
	}


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && gameObject.tag == "PlayerBullet")
        {
            GameObjectUtil.Destroy(gameObject);
        } else if ((collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2") && gameObject.tag == "Enemy")
        {
            GameObjectUtil.Destroy(gameObject);
        }
    }
    }
