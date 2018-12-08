using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    public GameObject bulletPrefab;

    public float baseROF = .25f;

    public float rateOfFire = .25f;
    public float cooldown = 0;

    public AudioClip audio;

	// Update is called once per frame
	void Update () {
        cooldown -= Time.deltaTime;

        if (cooldown <= 0)
        {
            cooldown = rateOfFire;

            gameObject.GetComponent<AudioSource>().PlayOneShot(audio);
            Instantiate(bulletPrefab, transform.position, transform.rotation);
        }
        

	}
}
